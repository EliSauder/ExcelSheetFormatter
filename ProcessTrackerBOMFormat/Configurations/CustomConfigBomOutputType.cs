using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBomOutputType : ConfigurationElement {

        public CustomConfigBomOutputType() { }
        public CustomConfigBomOutputType(string value) {
            this.Value = value;
        }

        [ConfigurationProperty("value", DefaultValue = "individual", IsRequired = true)]
        [RegexStringValidator("^individual|compact$")]
        public string Value {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}
