using System;
using System.Configuration;
using System.Xml;

namespace Formatter.Configuration {
    public class ConfigurationSectionFiles : ConfigurationSection {

        private XmlNode _xmlNode;

        public ConfigurationSectionFiles() { }

        public ConfigurationSectionFiles(XmlNode node) {
            foreach (XmlAttribute attribute in node.Attributes) {
                if (Properties.Contains(attribute.Name))
                    this[this.Properties[attribute.Name]] = this.Properties[attribute.Name].Converter.ConvertFrom(attribute.Value);
            }

            foreach (XmlNode childNode in node.ChildNodes) {
                if (this.Properties.Contains(childNode.Name))
                    this[childNode.Name] = Activator.CreateInstance(this[childNode.Name].GetType(), childNode);
            }

            _xmlNode = node["directories"];
        }

        [ConfigurationProperty("rootDirectory", IsRequired = true)]
        public string RootDirectory {
            get { return _xmlNode.Attributes["rootDirectory"].Value; }//(string)base["rootDirectory"]; }
            set { 
                base["rootDirectory"] = value;
                _xmlNode.Attributes["rootDirectory"].Value = value;
            }
        }

        [ConfigurationProperty("inputFolder", IsRequired = true)]
        public string InputFolder {
            get { return _xmlNode.Attributes["inputFolder"].Value; }
            set { 
                base["inputFolder"] = value;
                _xmlNode.Attributes["inputFolder"].Value = value;
            }
        }

        [ConfigurationProperty("oldInputFolder", IsRequired = true)]
        public string OldInputFolder {
            get { return _xmlNode.Attributes["oldInputFolder"].Value; }
            set { 
                base["oldInputFolder"] = value;
                _xmlNode.Attributes["oldInputFolder"].Value = value;
            }
        }

        [ConfigurationProperty("outputFolder", IsRequired = true)]
        public string OutputFolder {
            get { return _xmlNode.Attributes["outputFolder"].Value; }
            set { 
                base["outputFolder"] = value;
                _xmlNode.Attributes["outputFolder"].Value = value;
            }
        }

        [ConfigurationProperty("logFolder", IsRequired = true)]
        public string LogFolder {
            get { return _xmlNode.Attributes["logFolder"].Value; }
            set { 
                base["logFolder"] = value;
                _xmlNode.Attributes["logFolder"].Value = value;
            }
        }
    }
}
