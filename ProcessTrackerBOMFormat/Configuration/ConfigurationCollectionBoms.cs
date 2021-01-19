using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace Formatter.Configuration {

    /// <summary>
    /// Class <c>ConfigurationCollectionBoms</c> defines a 
    /// custom configuration element collection for App.config 
    /// that defines a collection of BOMs.
    /// </summary>
    /// <see cref="ConfigurationElementCollection"/>
    [ConfigurationCollection(typeof(ConfigurationElementBom), CollectionType = ConfigurationElementCollectionType.BasicMapAlternate)]
    public class ConfigurationCollectionBoms : ConfigurationElementCollection {

        /// <summary>
        /// Creates a collection with a single element as long 
        /// as the name is not and empty string.
        /// </summary>
        public ConfigurationCollectionBoms() {
            ConfigurationElementBom bom = (ConfigurationElementBom)CreateNewElement();
            if (!bom.Name.Equals("")) {
                Add(bom);
            }
        }

        /// <summary>
        /// Creates collection of boms from an xml node.
        /// </summary>
        /// <param name="node">The XML node containing the collection of BOMS</param>
        public ConfigurationCollectionBoms(XmlNode node) {
            foreach (XmlAttribute attribute in node.Attributes) {
                if (Properties.Contains(attribute.Name))
                    this[this.Properties[attribute.Name]] = this.Properties[attribute.Name].Converter.ConvertFrom(attribute.Value);
            }
            foreach (XmlNode childNode in node.ChildNodes) {
                ConfigurationElementBom bom = (ConfigurationElementBom)Activator.CreateInstance(typeof(ConfigurationElementBom), childNode);
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
        /// <remarks>It will create the element as a <c>ConfigurationElementBom</c></remarks>
        /// <returns>A new configuration element of type ConfigurationElementBom</returns>
        protected override ConfigurationElement CreateNewElement() {
            return new ConfigurationElementBom();
        }

        /// <summary>
        /// Gets the key of the element provided.
        /// </summary>
        /// <param name="element">The elment that you want the key of.</param>
        /// <returns>The key of the element.</returns>
        protected override object GetElementKey(ConfigurationElement element) {
            return ((ConfigurationElementBom)element).Name;
        }


        /// <summary>
        /// Gets the element at the index specified.
        /// </summary>
        /// <param name="index">The index of the elment</param>
        /// <seealso cref="[]"/>
        /// <returns>The bom configuration element from the index specified.</returns>
        public ConfigurationElementBom this[int index] {
            get { return (ConfigurationElementBom)BaseGet(index); }
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
        new public ConfigurationElementBom this[string name] {
            get { return (ConfigurationElementBom)BaseGet(name); }
        }

        /// <summary>
        /// The index of the element provided.
        /// </summary>
        /// <param name="field">The element you wish to find the index of.</param>
        /// <returns>The index of the element.</returns>
        public int IndexOf(ConfigurationElementBom field) {
            return BaseIndexOf(field);
        }

        /// <summary>
        /// Adds an element to the collection
        /// </summary>
        /// <param name="field">The element you wish to add</param>
        public void Add(ConfigurationElementBom field) {
            BaseAdd(field);
        }

        /// <summary>
        /// Adds the element provided to the collection.
        /// Overrides the <c>BaseAdd</c> method.
        /// </summary>
        /// <param name="element">The element you wish to add</param>
        /// <see cref="ConfigurationElement"/>
        protected override void BaseAdd(ConfigurationElement element) {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Gets the name of the elements expected. This is the inner xml titles.
        /// </summary>
        /// <example>
        ///     Such as:
        ///     <code>
        ///         &lt;base&gt;
        ///             &lt;bom/%gt;
        ///         &lt;/base&gt;    
        ///     </code>
        /// </example>
        protected override string ElementName => "bom";
    }
}
