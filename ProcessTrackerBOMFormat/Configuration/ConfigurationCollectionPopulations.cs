using System;
using System.Configuration;
using System.Xml;

namespace Formatter.Configuration {

    /// <summary>
    /// Class <c>ConfigurationCollectionPopulations</c> defines a 
    /// custom configuration element collection for App.config 
    /// that defines a collection of bom field populations.
    /// </summary>
    /// <see cref="ConfigurationElementCollection"/>
    public class ConfigurationCollectionPopulations : ConfigurationElementCollection {

        public ConfigurationCollectionPopulations() {
            ConfigurationElementPopulation bom = (ConfigurationElementPopulation)CreateNewElement();
            if (!bom.Name.Equals("")) {
                BaseAdd(bom);
            }
        }

        public ConfigurationCollectionPopulations(XmlNode node) {
            foreach (XmlAttribute attribute in node.Attributes) {
                if (Properties.Contains(attribute.Name))
                    this[this.Properties[attribute.Name]] = this.Properties[attribute.Name].Converter.ConvertFrom(attribute.Value);
            }
            foreach (XmlNode childNode in node.ChildNodes) {
                ConfigurationElementPopulation bom = (ConfigurationElementPopulation)Activator.CreateInstance(typeof(ConfigurationElementPopulation), childNode);
                if (bom.Name.Length != 0) this.BaseAdd(bom);
            }
        }

        /// <value>Property <c>CollectionType</c> Represents the type of the current collection.</value>
        public override ConfigurationElementCollectionType CollectionType {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        /// <summary>
        /// Creates a new ConfigurationElement to add to the collection.
        /// </summary>
        /// <remarks>It will create the element as a <c>ConfigurationElementPopulation</c></remarks>
        /// <returns>A new configuration element of type ConfigurationElementPopulation</returns>
        protected override ConfigurationElement CreateNewElement() {
            return new ConfigurationElementPopulation();
        }

        /// <summary>
        /// Gets the key of the element provided.
        /// </summary>
        /// <param name="element">The elment that you want the key of.</param>
        /// <returns>The key of the element.</returns>
        protected override object GetElementKey(ConfigurationElement element) {
            return ((ConfigurationElementPopulation)element).Name;
        }


        /// <summary>
        /// Gets the element at the index specified.
        /// </summary>
        /// <param name="index">The index of the elment</param>
        /// <seealso cref="[]"/>
        /// <returns>The bom configuration element from the index specified.</returns>
        public ConfigurationElementPopulation this[int index] {
            get { return (ConfigurationElementPopulation)BaseGet(index); }
            set {
                if (BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets the element at the key specified.
        /// </summary>
        /// <param name="name">The key of the element you wish to find</param>
        /// <returns>The element at the key given</returns>
        new public ConfigurationElementPopulation this[string name] {
            get { return (ConfigurationElementPopulation)BaseGet(name); }
        }

        /// <summary>
        /// The index of the element provided.
        /// </summary>
        /// <param name="field">The element you wish to find the index of.</param>
        /// <returns>The index of the element.</returns>
        public int IndexOf(ConfigurationElementPopulation field) {
            return BaseIndexOf(field);
        }

        /// <summary>
        /// The index of the element provided.
        /// </summary>
        /// <param name="name">the key you want the index of</param>
        /// <returns>The index of the element</returns>
        public int IndexOf(string name) {
            name = name.ToLower();
            for (int idx = 0; idx < base.Count; idx++) {
                if (this[idx].Name.ToLower() == name) return idx;
            }
            return -1;
        }

        /// <summary>
        /// Gets the name of the elements expected. This is the inner xml titles.
        /// </summary>
        /// <example>
        ///     Such as:
        ///     <code>
        ///         &lt;base&gt;
        ///             &lt;population/%gt;
        ///         &lt;/base&gt;    
        ///     </code>
        /// </example>
        protected override string ElementName {
            get { return "population"; }
        }
    }
}