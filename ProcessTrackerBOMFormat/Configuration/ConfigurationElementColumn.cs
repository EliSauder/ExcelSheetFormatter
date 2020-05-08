using System;
using System.ComponentModel;
using System.Configuration;
using System.Xml;

namespace Formatter.Configuration {

    /// <summary>
    /// <para>Class <c>ConfigurationElementColumn</c> defines a bom column element.</para>  
    /// <para>Each column configuration is used to define how the program will scan the input 
    /// file and decide what actions to perform on each column.</para>
    /// </summary>
    /// <see cref="ConfigurationElement"/>
    public class ConfigurationElementColumn : ConfigurationElement {

        public ConfigurationElementColumn() { }

        public ConfigurationElementColumn(XmlNode node) {
            foreach (XmlAttribute attribute in node.Attributes) {
                if (Properties.Contains(attribute.Name))
                    this[Properties[attribute.Name]] = Properties[attribute.Name].Converter.ConvertFrom(attribute.Value);
            }
            foreach (XmlNode childNode in node.ChildNodes) {
                if (Properties.Contains(childNode.Name))
                    this[childNode.Name] = Activator.CreateInstance(this[childNode.Name].GetType(), childNode);
            }
        }

        /// <value>Property <c>Name</c> defines the name of the column and is the key for the collection</value>
        /// <remarks>
        /// <para>It is a key.</para>
        /// <para>It is required.</para>
        /// </remarks>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get { return (string)this["name"]; }
            set {
                this["name"] = value;
                if (Output.Length == 0) Output = value;
                if (Header.Length == 0) Header = value;
            }
        }

        [ConfigurationProperty("identifierOrder", IsRequired = false, DefaultValue = -1)]
        public int IdentifierOrder {
            get { return (int)this["identifierOrder"]; }
            set { this["identifierOrder"] = value; }
        }

        /// <value>Property <c>IsQuantity</c> defines whether or not the column is a quantity.</value>
        /// <remarks>
        /// <para>Default value is False</para>
        /// </remarks>
        [ConfigurationProperty("isQuantity", DefaultValue = false, IsRequired = false)]
        public bool IsQuantity {
            get { return (bool)this["isQuantity"]; }
            set { this["isQuantity"] = value; }
        }

        /// <value>Property <c>DataType</c> defines the datatype that the column will contain.</value>
        /// <remarks>
        /// <para>It is required.</para>
        /// <para>Default value is typeof(string)</para>
        /// </remarks>
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("dataType", DefaultValue = typeof(string), IsRequired = true)]
        public Type DataType {
            get { return (Type)this["dataType"]; }
            set { this["dataType"] = value; }
        }

        /// <value>Property <c>IsSplit</c> defines whether or not the column values need to be split based on the delimiter.</value>
        /// <remarks>
        /// <para>Default value is false.</para>
        /// </remarks>
        [ConfigurationProperty("isSplit", DefaultValue = false)]
        public bool IsSplit {
            get { return (bool)this["isSplit"]; }
            set { this["isSplit"] = value; }
        }

        /// <value>Property <c>Delimiter</c> defines the delimiter that will be used to split the column value.</value>
        /// <remarks>
        /// <para>Default value is " ".</para>
        /// </remarks>
        [ConfigurationProperty("delimiter", DefaultValue = " ")]
        public string Delimiter {
            get { return (string)this["delimiter"]; }
            set { this["delimiter"] = value; }
        }

        /// <value>Property <c>Header</c> is the header inside of the input file that the program will look for.</value>
        /// <remarks>
        /// <para>It is required.</para>
        /// </remarks>
        [ConfigurationProperty("header", IsRequired = true)]
        public string Header {
            get { return (string)this["header"]; }
            set { this["header"] = value; }
        }

        /// <value>Property <c>Output</c> is the name of the column that the output file will contain instead of the one in the input file.</value>
        /// <remarks>
        /// <para>If it is not provided, the program will use the input file column name.</para>
        /// </remarks>
        [ConfigurationProperty("output", IsRequired = false, DefaultValue = "")]
        public string Output {
            get { return (string)this["output"]; }
            set {
                if (value.Length == 0) this["output"] = Name;
                else this["output"] = value;
            }
        }

        /// <value>Property <c>Order</c> is zero based order in which the columns will be ordered.</value>
        /// <remarks>
        /// <para>The values accepted are 0 and above.</para>
        /// <para>If two column configurations have the same value, the one that was encountered 
        /// first will override the other unless <c>Override</c> is defined on one.</para>
        /// </remarks>
        [ConfigurationProperty("order", DefaultValue = -1, IsRequired = false)]
        public int Order {
            get { return (int)this["order"]; }
            set { this["order"] = value; }
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

        /// <value>
        /// <para>Property <c>PopulationCollection</c> is a collection of population definitions that the program will execute.</para>
        /// <para>Each population set one columns value to the value provided as long as a condition is true.</para>
        /// </value>
        [ConfigurationProperty("populations", IsDefaultCollection = false)]
        public ConfigurationCollectionPopulations PopulationCollection {
            get { return (ConfigurationCollectionPopulations)base["populations"]; }
            set { base["populations"] = value; }
        }

        [ConfigurationProperty("cleanupActions", IsDefaultCollection = false)]
        public ConfigurationCollectionCleanUp CleanupCollection {
            get { return (ConfigurationCollectionCleanUp)base["cleanupActions"]; }
            set { base["cleanupActions"] = value; }
        }
    }
}