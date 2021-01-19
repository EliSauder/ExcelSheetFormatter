using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Formatter.Configuration {
    public class FormatterConfiguration : IFormatterConfiguration {

        public XmlDocument XmlDocument = null;
        public ConfigurationSectionBoms BomConfiguration { get; private set; } = null;
        public ConfigurationSectionFiles FileConfiguration { get; private set; } = null;
        public ConfigurationSectionParsing ParsingConfiguration { get; private set; } = null;
        public string InputFolderPath => Path.Combine(FileConfiguration.RootDirectory, FileConfiguration.InputFolder);
        public string OutputFolderPath => Path.Combine(FileConfiguration.RootDirectory, FileConfiguration.OutputFolder);
        public Exception Error { get; private set; } = null;
        public bool HasError => Error != null;

        public string ConfigurationFilePath {
            get {
                string configFile = ConfigurationManager.AppSettings["configFileLocation"];

                configFile = Environment.ExpandEnvironmentVariables(configFile);
                configFile = Regex.Replace(configFile, "%PRGMNAME%", Assembly.GetExecutingAssembly().GetName().Name, RegexOptions.IgnoreCase); //configFile.Replace("%ASSEMBLY%", Assembly.GetExecutingAssembly().GetName().Name);

                return Path.GetFullPath(configFile);
            }
        }
        public bool ConfigurationExists => File.Exists(ConfigurationFilePath);

        public FormatterConfiguration() {
            try {
                LoadConfiguration();
            } catch(XmlException e) {
                Error = e;
            }
        }

        public ConfigurationElementBom GetBomConfig(string bomName) => BomConfiguration.BomCollection[bomName];

        private void LoadConfiguration() {
            if (!ConfigurationExists) return;
            XmlDocument = new XmlDocument();
            try {
                XmlDocument.Load(ConfigurationFilePath);
            } catch (Exception e) {
                throw new XmlException("Error in configuration file: " + e.Message, e);
            }
            XmlNode node = XmlDocument.SelectSingleNode(Properties.Resources.APPLICATION_CONFIGURATION_SECTION);
            this.BomConfiguration = new ConfigurationSectionBoms(node);
            this.FileConfiguration = new ConfigurationSectionFiles(node);
            this.ParsingConfiguration = new ConfigurationSectionParsing(node);
        }

        public void ReloadConfiguration() => LoadConfiguration();

        public void CreateConfigurationFromAppConfig() {

            if (ConfigurationExists) throw new ConfigurationException("Configuration file already exists");

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            ConfigurationSectionGroup configGroups = config.SectionGroups[Properties.Resources.APPLICATION_CONFIGURATION_SECTION];

            string directoryPath = Path.GetDirectoryName(ConfigurationFilePath);

            Directory.CreateDirectory(directoryPath);

            XmlTextWriter xmlTextWriter = new XmlTextWriter(ConfigurationFilePath, Encoding.UTF8);
            xmlTextWriter.WriteStartDocument(false);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 4;
            xmlTextWriter.WriteStartElement(configGroups.SectionGroupName);
            foreach (ConfigurationSection section in configGroups.Sections) {
                xmlTextWriter.WriteNode(XmlReader.Create(new StringReader(section.SectionInformation.GetRawXml())), false);
            }
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Close();

            LoadConfiguration();
        }

        public void UpdateConfigurationFile() {
            string filePath = ConfigurationFilePath;
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string directory = Path.GetDirectoryName(fileName);
            string extention = Path.GetExtension(fileName);
            string tempFileName = Path.Combine(directory, fileName + "-temp" + extention);

            StreamWriter outStream = new StreamWriter(File.OpenWrite(tempFileName));

            XmlDocument.Save(outStream);

            outStream.Close();

            File.Delete(filePath);
            File.Move(tempFileName, filePath);
        }

        public void UpdateFileConfigurations(string rootDirectory, string inputFolder, string outputFolder) {

            if (XmlDocument == null) throw new Exception("Configuration has not been loaded");

            FileConfiguration.RootDirectory = rootDirectory;
            FileConfiguration.InputFolder = inputFolder;
            FileConfiguration.OutputFolder = outputFolder;

            UpdateConfigurationFile();

            ReloadConfiguration();
        }

        public void ValidateConfiguration() {

            if (this.HasError) throw new XmlException("Configuration not loaded properly. " + this.Error.Message);

            foreach (ConfigurationElementBom bom in this.BomConfiguration.BomCollection) {
                foreach (ConfigurationElementColumn column in bom.ColumnCollection) {
                    if (column.PopulationCollection != null) {
                        foreach (ConfigurationElementPopulation population in column.PopulationCollection) {
                            if (bom.ColumnCollection[population.ToColumn] == null) throw new InvalidExpressionException("boms." + bom.Name + ".fields." + column.Name + ".populations." + population.Name + ".toColumn references invalid column");
                        }
                    }
                }
            }
        }

        public void UpdateConfigurationFileFromAppConfig() {

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSectionGroup configGroups = config.SectionGroups[Properties.Resources.APPLICATION_CONFIGURATION_SECTION];
            XmlNode parentNode = null;
            foreach (XmlNode node in XmlDocument.ChildNodes) {
                parentNode = node.Name.Equals(Properties.Resources.APPLICATION_CONFIGURATION_SECTION) ? node : parentNode;
            }


            foreach (ConfigurationSection section in configGroups.Sections) {
                XmlNode xmlSection = null;
                foreach (XmlNode childNode in parentNode.ChildNodes) {
                    if (childNode.Name.Equals(section.SectionInformation.Name)) {
                        xmlSection = childNode;
                    }
                }
                if (xmlSection == null) parentNode.InnerXml += section.SectionInformation.GetRawXml();
            }

            UpdateConfigurationFile();
        }
    }
}