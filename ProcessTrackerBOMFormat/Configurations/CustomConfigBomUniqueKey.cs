using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBomUniqueKey : ConfigurationElement {

        [ConfigurationProperty("whereLook", IsRequired = true)]
        [RegexStringValidator(@"^(\w+\d+(?:(?:,\w+\d+)+)?|)$")]
        public string WhereLook { 
            get { return (string)this["whereLook"]; } 
            set { this["whereLook"] = value; } 
        }

        [ConfigurationProperty("valueFind", IsRequired = true)]
        public string ValueFind {
            get { return (string)this["valueFind"]; }
            set { this["valueFind"] = value; }
        }

    }
}
