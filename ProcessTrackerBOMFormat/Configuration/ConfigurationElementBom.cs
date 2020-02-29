using System.Configuration;

namespace ProcessTrackerBOMFormat.Configuration {
    public class ConfigurationElementBom : ConfigurationElement {

        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get { return (string)this["name"]; } 
            set { this["name"] = value; } 
        }

        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
        public bool Enabled { 
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; } 
        }

        [ConfigurationProperty("uniqueKey")]
        public ConfigurationElementUniqueKey UniqueKey {
            get { return (ConfigurationElementUniqueKey)this["uniqueKey"]; }
        }

        [ConfigurationProperty("fields", IsDefaultCollection = false)]
        public ConfigurationCollectionColumns BomColumnFieldCollection {
            get { return (ConfigurationCollectionColumns)base["fields"]; }
        }

    }
}
