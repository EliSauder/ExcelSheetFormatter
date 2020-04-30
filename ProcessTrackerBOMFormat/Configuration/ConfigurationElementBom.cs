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
    public class ConfigurationElementBom : ConfigurationElement {

        /// <value>
        /// Property <c>Name</c> defines the name of the bom. 
        /// It is also the key for the bom collection
        /// </value>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get { return (string)this["name"]; } 
            set {
                if (string.IsNullOrEmpty(DisplayName)) DisplayName = value;
                this["name"] = value; 
            } 
        }

        /// <value>Proeprty <c>OutputType</c> is the type of output the program will use in the output file.</value>
        /// <remarks>
        /// <para>It is required.</para>
        /// <para>Default value: INDIVIDUAL</para>
        /// </remarks>
        //TODO: Convert OutputType to an enum output. 
        [ConfigurationProperty("outputType", DefaultValue = "INDIVIDUAL", IsRequired = true)]
        public BomOutputType OutputType {
            get { return (BomOutputType)base["outputType"]; }
            set { base["outputType"] = value; }
        }

        /// <value>
        /// Property <c>Name</c> defines the name of the bom. 
        /// It is also the key for the bom collection
        /// </value>
        [ConfigurationProperty("displayName", IsKey = true, DefaultValue = "")]
        public string DisplayName {
            get { return (string)this["displayName"]; }
            set { this["displayName"] = value; }
        }

        [ConfigurationProperty("outputSheetName", DefaultValue = "")]
        public string OutputSheetName {
            get { return (string)this["outputSheetName"]; }
            set { this["outputSheetName"] = value; }
        }

        [ConfigurationProperty("inputFileExtention", IsRequired = true, DefaultValue = ".xlsx")]
        [RegexStringValidator(@"\.xlsx|\.csv|\.xls")]
        public string InputFileExtention {
            get { return (string)this["inputFileExtention"]; }
            set { this["inputFileExtention"] = value; }
        }

        /// <value>
        /// Property <c>Enabled</c> defines wether or not the bom in configuration will be checked/used or not.
        /// </value>
        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
        public bool Enabled {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        /// <value>
        /// <para>Property <c>ColumnCollection</c> is a collection of columns that the program will look for.</para>
        /// <para>Each column will contain the information needed for the program to perform the defined actions</para>
        /// </value>
        [ConfigurationProperty("fields", IsDefaultCollection = false)]
        public ConfigurationCollectionColumns ColumnCollection {
            get { return (ConfigurationCollectionColumns)base["fields"]; }
        }
    }
}
