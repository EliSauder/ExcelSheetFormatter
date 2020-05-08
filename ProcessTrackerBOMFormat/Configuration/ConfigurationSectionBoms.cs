using System;
using System.Configuration;
using System.Xml;

namespace Formatter.Configuration {

    /// <summary>
    /// Class <c>ConfigurationSectionBoms</c> defines the bom section. This section will contain any bom types that the program will look for.
    /// It also contains how the ouput will be format.
    /// </summary>
    public class ConfigurationSectionBoms : ConfigurationSection {

        public ConfigurationSectionBoms() {}

        public ConfigurationSectionBoms(XmlNode node) {
            foreach (XmlAttribute attribute in node.Attributes) {
                if (Properties.Contains(attribute.Name))
                    this[this.Properties[attribute.Name]] = this.Properties[attribute.Name].Converter.ConvertFrom(attribute.Value);
            }

            foreach (XmlNode childNode in node.ChildNodes) {
                if(this.Properties.Contains(childNode.Name))
                    this[childNode.Name] = Activator.CreateInstance(this[childNode.Name].GetType(), childNode);
            }
        }

        /// <value>Property <c>BomCollection</c> is the collection of boms in the config file.</value>
        /// <remarks>
        /// <para>It is the default collection.</para>
        /// </remarks>
        [ConfigurationProperty("boms", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ConfigurationCollectionBoms))]
        public ConfigurationCollectionBoms BomCollection {
            get {
                ConfigurationCollectionBoms bomCollection = (ConfigurationCollectionBoms)base["boms"];
                return bomCollection;
            }
            private set {
                base["boms"] = value;
            }
        }
    }
}
