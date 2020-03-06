using System.Configuration;

namespace ProcessTrackerBOMFormat.Configuration {

    /// <summary>
    /// <para>Class <c>ConfigurationElementColumn</c> defines a bom column element.</para>  
    /// <para>Each column configuration is used to define how the program will scan the input 
    /// file and decide what actions to perform on each column.</para>
    /// </summary>
    /// <see cref="ConfigurationElement"/>
    public class ConfigurationElementColumn : ConfigurationElement {

        /// <value>Property <c>Name</c> defines the name of the column and is the key for the collection</value>
        /// <remarks>
        /// <para>It is a key.</para>
        /// <para>It is required.</para>
        /// </remarks>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get {return (string)this["name"];}
            set {this["name"] = value;}
        }
        /// <value>Property <c>Header</c> is the header inside of the input file that the program will look for.</value>
        /// <remarks>
        /// <para>It is required.</para>
        /// </remarks>
        [ConfigurationProperty("header", IsRequired = true)]
        public string Header {
            get {return (string)this["header"];}
            set {this["header"] = value;}
        }

        /// <value>Property <c>Output</c> is the name of the column that the output file will contain instead of the one in the input file.</value>
        /// <remarks>
        /// <para>If it is not provided, the program will use the input file column name.</para>
        /// </remarks>
        [ConfigurationProperty("output", IsRequired = false, DefaultValue = "")]
        public string Output {
            get { return (string)this["output"]; }
            set { this["output"] = value; }
        }

        /// <value>Property <c>Order</c> is the order in which the columns will be ordered.</value>
        /// <remarks>
        /// <para>The values accepted are 0 and above.</para>
        /// <para>If two column configurations have the same value, the one that was encountered 
        /// first will override the other unless <c>Override</c> is defined on one.</para>
        /// </remarks>
        [ConfigurationProperty("order", IsRequired = false)]
        [IntegerValidator(MinValue = 0, ExcludeRange = false)]
        public int Order {
            get {return (int)this["order"];}
            set {this["order"] = value;}
        }

        /// <value>Property <c>Enabled</c> defines whether the program will actually check for/use this column.</value>
        /// <remarks>
        /// <para>Default value: false</para>
        /// </remarks>
        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = false)]
        public bool Enabled {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        /// <value>Property <c>Override</c> whether this column will override any other column with the same <c>Order</c></value>
        /// <remarks>
        /// <para>Default value: false</para>
        /// </remarks>
        [ConfigurationProperty("override", IsRequired = false, DefaultValue = false)]
        public bool Override {
            get { return (bool)this["override"]; }
            set { this["override"] = value; }
        }

        /// <value>Property <c>Required</c> indicates whether the column is required in the input file or not.</value>
        /// <remarks>
        /// <para>Default value: true</para>
        /// </remarks>
        [ConfigurationProperty("required", IsRequired = false, DefaultValue = true)]
        public bool Required {
            get { return (bool)this["required"]; }
            set { this["required"] = value; }
        }
    }
}
