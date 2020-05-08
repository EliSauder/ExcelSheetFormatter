using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatter.Configuration {
    public interface IFormatterConfiguration {

        string InputFolderPath { get; }
        string OutputFolderPath { get; }
        ConfigurationSectionBoms BomConfiguration { get; }
        ConfigurationSectionFiles FileConfiguration { get; }
        ConfigurationElementBom GetBomConfig(string bomName);
        void CreateConfigurationFromAppConfig();
        string ConfigurationFilePath { get; }
        bool ConfigurationExists { get; }
        void ReloadConfiguration();
        void UpdateFileConfigurations(string rootDirectory, string inputFolder, string outputFolder);
        void ValidateConfiguration();
        Exception Error { get; }
        bool HasError { get; }
    }
}
