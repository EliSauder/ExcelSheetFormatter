using System.Configuration;
using ProcessTrackerBOMFormat.Utility;

namespace ProcessTrackerBOMFormat.Configuration {
    public class ConfigurationElementPopulation : ConfigurationElement {

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("checkColumn", IsRequired = true)]
        public string CheckColumn {
            get { return (string)this["checkColumn"]; }
            set { this["checkColumn"] = value; }
        }

        [ConfigurationProperty("condition", IsRequired = false, DefaultValue = "EQUALS")]
        public StringEvaluation.StringEvalCondition Condition {
            get { return (StringEvaluation.StringEvalCondition)this["condition"]; }
            set { this["condition"] = value; }
        }

        [ConfigurationProperty("findValue", IsRequired = true)]
        public string FindValue {
            get { return (string)this["findValue"]; }
            set { this["findValue"] = value; }
        }

        [ConfigurationProperty("setValue", IsRequired = true)]
        public string SetValue {
            get { return (string)this["setValue"]; }
            set { this["setValue"] = value; }
        }

        [ConfigurationProperty("toColumn", IsRequired = true)]
        public string ToColumn {
            get { return (string)this["toColumn"]; }
            set { this["toColumn"] = value; }
        }

        [ConfigurationProperty("active", IsRequired = false, DefaultValue = true)]
        public bool Active {
            get { return (bool)this["active"]; }
            set { this["active"] = value; }
        }
    }
}
