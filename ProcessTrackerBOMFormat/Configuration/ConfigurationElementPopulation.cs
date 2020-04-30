using System.Configuration;
using Formatter.Utility;

namespace Formatter.Configuration {

    /// <summary>
    /// <para>Class <c>ConfigurationElementPopulation</c> defines a bom population event.</para>  
    /// <para>Each populations configuration is used to define how the program will behave 
    /// when a paticular input is found in one column.</para>
    /// </summary>
    /// <see cref="ConfigurationElement"/>
    public class ConfigurationElementPopulation : ConfigurationElement {

        /// <value>Property <c>Name</c> is the name of the population as well as the key for the collection</value>
        /// <remarks>
        /// <para>It is required.</para>
        /// <para>It is a key.</para>
        /// </remarks>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <value>Property <c>Condition</c> is the condition that will be performed on the column values.</value>
        /// <remarks>
        /// <para>Default value: EQUALS</para>
        /// </remarks>
        /// <see cref="StringEvaluation.StringEvalCondition"/>
        [ConfigurationProperty("condition", IsRequired = false, DefaultValue = "EQUALS")]
        public StringEvaluation.StringEvalCondition Condition {
            get { return (StringEvaluation.StringEvalCondition)this["condition"]; }
            set { this["condition"] = value; }
        }

        /// <value>Property <c>FindValue</c> is the value that the column value will be compared against.</value>
        /// <remarks>
        /// <para>Default value is "".</para>
        /// </remarks>
        [ConfigurationProperty("findValue", IsRequired = false, DefaultValue = "")]
        public string FindValue {
            get { return (string)this["findValue"]; }
            set { this["findValue"] = value; }
        }

        /// <value>Property <c>SetValue</c> is the value that the result column will be set to.</value>
        /// <remarks>
        /// <para>It is required.</para>
        /// </remarks>
        [ConfigurationProperty("setValue", IsRequired = true)]
        public string SetValue {
            get { return (string)this["setValue"]; }
            set { this["setValue"] = value; }
        }

        /// <value>
        /// Property <c>ToColumn</c> is the column that the program will set with 
        /// <c>SetValue</c> when the column value and <c>FindValue</c> match the <c>Condition</c>.
        /// </value>
        /// <remarks>
        /// <para>It is required.</para>
        /// </remarks>
        [ConfigurationProperty("toColumn", DefaultValue = "")]
        public string ToColumn {
            get { return (string)this["toColumn"]; }
            set { this["toColumn"] = value; }
        }

        /// <value>Property <c>Active</c> defines whether or not the program will utilize this population.</value>
        /// <remarks>
        /// <para>Default value: true</para>
        /// </remarks>
        [ConfigurationProperty("active", IsRequired = false, DefaultValue = true)]
        public bool Active {
            get { return (bool)this["active"]; }
            set { this["active"] = value; }
        }
    }
}
