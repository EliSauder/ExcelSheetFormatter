using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatter.Configuration {
    public interface IFormatterConfiguration {

        ConfigurationSectionBoms BomConfiguration { get; }
        ConfigurationSectionFiles FileConfiguration { get; }
        ConfigurationElementBom GetBomConfig(string bomName);

    }
}
