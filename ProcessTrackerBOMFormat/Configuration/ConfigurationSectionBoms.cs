using System.Configuration;

namespace Formatter.Configuration
{

    /// <summary>
    /// Class <c>ConfigurationSectionBoms</c> defines the bom section. This section will contain any bom types that the program will look for.
    /// It also contains how the ouput will be format.
    /// </summary>
    public class ConfigurationSectionBoms : ConfigurationSection
    {

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
    }
}
