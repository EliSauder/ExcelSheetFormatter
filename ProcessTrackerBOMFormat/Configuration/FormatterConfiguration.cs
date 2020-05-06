using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatter.Configuration {
    public class FormatterConfiguration : IFormatterConfiguration {
        public FormatterConfiguration() {
            BomConfiguration = (ConfigurationSectionBoms)ConfigurationManager.GetSection(Properties.Resources.BOM_CONFIGURATION_SECTION);
            FileConfiguration = (ConfigurationSectionFiles)ConfigurationManager.GetSection(Properties.Resources.FILE_CONFIGURATION_SECTION);
        }

        public ConfigurationSectionBoms BomConfiguration { get; }

        public ConfigurationSectionFiles FileConfiguration { get; }

        public ConfigurationElementBom GetBomConfig(string bomName) {
            return BomConfiguration.BomCollection[bomName];
        }
    }
}
