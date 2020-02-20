﻿using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBomColumnField : ConfigurationElement {
        public CustomConfigBomColumnField() { }
        public CustomConfigBomColumnField(string name, bool enabled, string header, string output, int order, bool oRide) {
            this.Name = name;
            this.Header = header;
            this.Enabled = enabled;
            this.Output = output;
            this.Order = order;
            this.Override = oRide;
        }

        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get {return (string)this["name"];}
            set {this["name"] = value;}
        }

        [ConfigurationProperty("header", IsRequired = true)]
        public string Header {
            get {return (string)this["header"];}
            set {this["header"] = value;}
        }

        [ConfigurationProperty("output", IsRequired = false, DefaultValue = "")]
        public string Output {
            get { return (string)this["output"]; }
            set { this["output"] = value; }
        }

        [ConfigurationProperty("order", IsRequired = false)]
        [IntegerValidator(MinValue = 0, ExcludeRange = false)]
        public int Order {
            get {return (int)this["order"];}
            set {this["order"] = value;}
        }

        [ConfigurationProperty("disabled", IsRequired = false, DefaultValue = false)]
        public bool Enabled {
            get { return (bool)this["disabled"]; }
            set { this["disabled"] = value; }
        }

        [ConfigurationProperty("override", IsRequired = false, DefaultValue = false)]
        public bool Override {
            get { return (bool)this["override"]; }
            set { this["override"] = value; }
        }
    }
}
