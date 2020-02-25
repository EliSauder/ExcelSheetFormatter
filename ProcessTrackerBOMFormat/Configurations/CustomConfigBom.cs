using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBom : ConfigurationElement {

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
        public CustomConfigBomUniqueKey UniqueKey {
            get { return (CustomConfigBomUniqueKey)this["uniqueKey"]; }
        }

        [ConfigurationProperty("fields", IsDefaultCollection = false)]
        public CustomConfigBomColumnFieldCollection BomColumnFieldCollection {
            get { return (CustomConfigBomColumnFieldCollection)base["fields"]; }
        }

    }
}
