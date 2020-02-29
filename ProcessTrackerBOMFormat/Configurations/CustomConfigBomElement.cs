using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBomElement : ConfigurationElement {

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
        public CustomConfigBomUniqueKeyElement UniqueKey {
            get { return (CustomConfigBomUniqueKeyElement)this["uniqueKey"]; }
        }

        [ConfigurationProperty("fields", IsDefaultCollection = false)]
        public CustomConfigBomColumnCollection BomColumnFieldCollection {
            get { return (CustomConfigBomColumnCollection)base["fields"]; }
        }

    }
}
