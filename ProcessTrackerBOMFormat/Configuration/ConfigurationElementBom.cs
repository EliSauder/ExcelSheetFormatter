using System.Configuration;

namespace ProcessTrackerBOMFormat.Configuration {

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
            set { this["name"] = value; } 
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
        /// Property <c>UniqueKey</c> defines where in the excel file the program will 
        /// look to find the value specified indicating that the file input is of the current bom type configured.
        /// </value>
        [ConfigurationProperty("uniqueKey")]
        public ConfigurationElementUniqueKey UniqueKey {
            get { return (ConfigurationElementUniqueKey)this["uniqueKey"]; }
        }

        /// <value>
        /// <para>Property <c>ColumnCollection</c> is a collection of columns that the program will look for.</para>
        /// <para>Each column will contain the information needed for the program to perform the defined actions</para>
        /// </value>
        [ConfigurationProperty("fields", IsDefaultCollection = false)]
        public ConfigurationCollectionColumns ColumnCollection {
            get { return (ConfigurationCollectionColumns)base["fields"]; }
        }

        /// <value>
        /// <para>Property <c>PopulationCollection</c> is a collection of population definitions that the program will execute.</para>
        /// <para>Each population set one columns value to the value provided as long as a condition is true.</para>
        /// </value>
        [ConfigurationProperty("populations", IsDefaultCollection = false)]
        public ConfigurationCollectionPopulations PopulationCollection {
            get { return (ConfigurationCollectionPopulations)base["populations"]; }
        }
    }
}
