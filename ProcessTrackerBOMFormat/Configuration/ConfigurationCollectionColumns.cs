﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml;

namespace Formatter.Configuration {

    /// <summary>
    /// Class <c>ConfigurationCollectionColumns</c> defines a 
    /// custom configuration element collection for App.config 
    /// that defines a collection of BOM columns.
    /// </summary>
    /// <see cref="ConfigurationElementCollection"/>
    public class ConfigurationCollectionColumns : ConfigurationElementCollection, ICollection<ConfigurationElementColumn>, IEnumerator<ConfigurationElementColumn> {

        public ConfigurationCollectionColumns() { }

        public ConfigurationCollectionColumns(XmlNode node) {
            foreach (XmlAttribute attribute in node.Attributes) {
                if (Properties.Contains(attribute.Name))
                    this[this.Properties[attribute.Name]] = this.Properties[attribute.Name].Converter.ConvertFrom(attribute.Value);
            }
            foreach (XmlNode childNode in node.ChildNodes) {
                ConfigurationElementColumn bom = (ConfigurationElementColumn)Activator.CreateInstance(typeof(ConfigurationElementColumn), childNode);
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
        /// <remarks>It will create the element as a <c>ConfigurationElementColumn</c></remarks>
        /// <returns>A new configuration element of type ConfigurationElementColumn</returns>
        protected override ConfigurationElement CreateNewElement() {
            return new ConfigurationElementColumn();
        }

        /// <summary>
        /// Gets the key of the element provided.
        /// </summary>
        /// <param name="element">The elment that you want the key of.</param>
        /// <returns>The key of the element.</returns>
        protected override object GetElementKey(ConfigurationElement element) {
            return ((ConfigurationElementColumn)element).Name;
        }

        /// <summary>
        /// Gets the element at the index specified.
        /// </summary>
        /// <param name="index">The index of the elment</param>
        /// <seealso cref="[]"/>
        /// <returns>The bom column element from the index specified.</returns>
        public ConfigurationElementColumn this[int index] {
            get { return (ConfigurationElementColumn)BaseGet(index); }
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
        new public ConfigurationElementColumn this[string name] {
            get { return (ConfigurationElementColumn)BaseGet(name); }
        }

        /// <summary>
        /// The index of the element provided.
        /// </summary>
        /// <param name="field">The element you wish to find the index of.</param>
        /// <returns>The index of the element.</returns>
        public int IndexOf(ConfigurationElementColumn field) {
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

        public void Add(ConfigurationElementColumn item) => BaseAdd(item);

        public void Clear() => BaseClear();

        public bool Contains(ConfigurationElementColumn item) => IndexOf(item) != -1;

        public void CopyTo(ConfigurationElementColumn[] array, int arrayIndex) {
            for (int i = 0; i < this.Count; i++) 
                array[i + arrayIndex] = this[i];
        }

        public bool Remove(ConfigurationElementColumn item) {
            if (Contains(item)) {
                BaseRemove(item);
                return true;
            }
            return false;
        }

        IEnumerator<ConfigurationElementColumn> IEnumerable<ConfigurationElementColumn>.GetEnumerator() {
            return this;
        }

        public void Dispose() { }

        public bool MoveNext() {
            if (_currentElement + 1 > this.Count - 1) return false;
            _currentElement++;
            return true;
        }

        public void Reset() {
            _currentElement = 0;
        }

        /// <summary>
        /// Gets the name of the elements expected. This is the inner xml titles.
        /// </summary>
        /// <example>
        ///     Such as:
        ///     <code>
        ///         &lt;base&gt;
        ///             &lt;field/%gt;
        ///         &lt;/base&gt;    
        ///     </code>
        /// </example>
        protected override string ElementName {
            get { return "field"; }
        }

        private int _currentElement = 0;

        bool ICollection<ConfigurationElementColumn>.IsReadOnly => false;

        public ConfigurationElementColumn Current => this[_currentElement];

        object IEnumerator.Current => Current;
    }
}
