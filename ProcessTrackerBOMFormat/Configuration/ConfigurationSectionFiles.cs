using System.Configuration;

namespace Formatter.Configuration
{
    public class ConfigurationSectionFiles : ConfigurationSection
    {

        [ConfigurationProperty("rootDirectory", IsRequired = true)]
        public string RootDirectory {
            get { return (string)base["rootDirectory"]; }
            set { base["rootDirectory"] = value; }
        }

        [ConfigurationProperty("inputFolder", IsRequired = true)]
        public string InputFolder {
            get { return (string)base["inputFolder"]; }
            set { base["inputFolder"] = value; }
        }

        [ConfigurationProperty("oldInputFolder", IsRequired = true)]
        public string OldInputFolder {
            get { return (string)base["oldInputFolder"]; }
            set { base["oldInputFolder"] = value; }
        }

        [ConfigurationProperty("outputFolder", IsRequired = true)]
        public string OutputFolder {
            get { return (string)base["outputFolder"]; }
            set { base["outputFolder"] = value; }
        }

        [ConfigurationProperty("logFolder", IsRequired = true)]
        public string LogFolder {
            get { return (string)base["logFolder"]; }
            set { base["logFolder"] = value; }
        }
    }
}
