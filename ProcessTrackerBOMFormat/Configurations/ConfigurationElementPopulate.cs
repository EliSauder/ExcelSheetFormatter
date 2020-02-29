using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTrackerBOMFormat.Configurations {
    class ConfigurationElementPopulate : ConfigurationElement {

        [ConfigurationProperty("toColumn", IsRequired = true)]
        public string ToColumn {
            get { return (string)this["toColumn"]; }
            set { this["toColumn"] = value; }
        }

        [ConfigurationProperty("fromColumn", IsRequired = true)]
        public string FromColumn {
            get { return (string)this["fromColumn"]; }
            set { this["fromColumn"] = value; }
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

        [ConfigurationProperty("replaceValue", IsRequired = true)]
        public string ReplaceValue {
            get { return (string)this["replaceValue"]; }
            set { this["replaceValue"] = value; }
        }

        [ConfigurationProperty("active", IsRequired = false, DefaultValue = true)]
        public bool Active {
            get { return (bool)this["active"]; }
            set { this["active"] = value; }
        }
    }
}
