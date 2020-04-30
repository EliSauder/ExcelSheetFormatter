using Formatter.Utility;
using System.Configuration;

namespace Formatter.Configuration {

    /// <summary>
    /// <para>Class <c>ConfigurationElementBom</c> defines a bom configuration element.</para>  
    /// <para>Each bom configuration is used to define how the program will take the input bom 
    /// and convert it to the specified output type.</para>
    /// <para>It contains the name of the bom, wether it is active or not and the columns to 
    /// look for/replacesments that need to be made while formatting the bom.</para>
    /// </summary>
    /// <see cref="ConfigurationElement"/>
    public class ConfigurationElementCleanUp : ConfigurationElement {

        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("action", IsRequired = true)]
        public ConfigurationCleanupActions.CleanupAction Action {
            get { return (ConfigurationCleanupActions.CleanupAction)this["action"]; }
            set { this["action"] = value; }
        }

        [ConfigurationProperty("report", IsRequired = false, DefaultValue = true)]
        public bool Report {
            get { return (bool)this["report"]; }
            set { this["report"] = value; }
        }

        [ConfigurationProperty("active", IsRequired = false, DefaultValue = true)]
        public bool Active {
            get { return (bool)this["active"]; }
            set { this["active"] = value; }
        }

        [ConfigurationProperty("scope", IsRequired = true)]
        public ConfigurationCleanupActions.CleanupScope Scope {
            get { return (ConfigurationCleanupActions.CleanupScope)this["scope"]; }
            set { this["scope"] = value; }
        }

        [ConfigurationProperty("condition", IsRequired = true)]
        public StringEvaluation.StringEvalCondition Condition {
            get { return (StringEvaluation.StringEvalCondition)this["condition"]; }
            set { this["condition"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = false, DefaultValue = "")]
        public string Value {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }


    }
}
