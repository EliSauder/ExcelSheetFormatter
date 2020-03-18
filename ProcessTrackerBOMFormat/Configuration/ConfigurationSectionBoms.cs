using ProcessTrackerBOMFormat.Utility;
using System.Configuration;

namespace ProcessTrackerBOMFormat.Configuration {

    /// <summary>
    /// Class <c>ConfigurationSectionBoms</c> defines the bom section. This section will contain any bom types that the program will look for.
    /// It also contains how the ouput will be format.
    /// </summary>
    public class ConfigurationSectionBoms : ConfigurationSection {

        /// <value>Property <c>BomCollection</c> is the collection of boms in the config file.</value>
        /// <remarks>
        /// <para>It is the default collection.</para>
        /// </remarks>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ConfigurationCollectionBoms BomCollection {
            get {
                ConfigurationCollectionBoms bomCollection = (ConfigurationCollectionBoms)base[""];
                return bomCollection; 
            }
        }

        /// <value>Proeprty <c>OutputType</c> is the type of output the program will use in the output file.</value>
        /// <remarks>
        /// <para>It is required.</para>
        /// <para>Default value: individual</para>
        /// <para>Must matche the regex: "^individual|compact$"</para>
        /// </remarks>
        //TODO: Convert OutputType to an enum output. 
        [ConfigurationProperty("outputType", DefaultValue = "INDIVIDUAL", IsRequired = true)]
        public BomOutputType OutputType {
            get { return (BomOutputType)base["outputType"]; }
            set { base["outputType"] = value; }
        }

    }
}
