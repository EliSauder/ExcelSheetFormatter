using System.Configuration;

namespace ProcessTrackerBOMFormat.Configuration {
    public class ConfigurationElementColumn : ConfigurationElement {
        public ConfigurationElementColumn() { }
        public ConfigurationElementColumn(string name, bool enabled, string header, string output, int order, bool oRide, bool required) {
            this.Name = name;
            this.Header = header;
            this.Enabled = enabled;
            this.Output = output;
            this.Order = order;
            this.Override = oRide;
            this.Required = required;
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

        [ConfigurationProperty("required", IsRequired = false, DefaultValue = true)]
        public bool Required {
            get { return (bool)this["required"]; }
            set { this["required"] = value; }
        }
    }
}
