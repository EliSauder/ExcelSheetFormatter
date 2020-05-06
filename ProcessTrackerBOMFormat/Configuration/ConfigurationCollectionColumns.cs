using System.Configuration;

namespace Formatter.Configuration
{

    /// <summary>
    /// Class <c>ConfigurationCollectionColumns</c> defines a 
    /// custom configuration element collection for App.config 
    /// that defines a collection of BOM columns.
    /// </summary>
    /// <see cref="ConfigurationElementCollection"/>
    public class ConfigurationCollectionColumns : ConfigurationElementCollection
    {

        /// <value>Property <c>CollectionType</c> Represents the type of the current collection.</value>
        public override ConfigurationElementCollectionType CollectionType {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        /// <summary>
        /// Creates a new ConfigurationElement to add to the collection.
        /// </summary>
        /// <remarks>It will create the element as a <c>ConfigurationElementColumn</c></remarks>
        /// <returns>A new configuration element of type ConfigurationElementColumn</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationElementColumn();
        }

        /// <summary>
        /// Gets the key of the element provided.
        /// </summary>
        /// <param name="element">The elment that you want the key of.</param>
        /// <returns>The key of the element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigurationElementColumn)element).Name;
        }


#pragma warning disable CS1658 // Identifier expected. See also error CS1001.
#pragma warning disable CS1584 // XML comment has syntactically incorrect cref attribute '[]'
        /// <summary>
        /// Gets the element at the index specified.
        /// </summary>
        /// <param name="index">The index of the elment</param>
        /// <seealso cref="[]"/>
        /// <returns>The bom column element from the index specified.</returns>
        public ConfigurationElementColumn this[int index] {
#pragma warning restore CS1584 // XML comment has syntactically incorrect cref attribute '[]'
#pragma warning restore CS1658 // Identifier expected. See also error CS1001.
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
        public int IndexOf(ConfigurationElementColumn field)
        {
            return BaseIndexOf(field);
        }

        /// <summary>
        /// The index of the element provided.
        /// </summary>
        /// <param name="name">the key you want the index of</param>
        /// <returns>The index of the element</returns>
        public int IndexOf(string name)
        {
            name = name.ToLower();
            for (int idx = 0; idx < base.Count; idx++)
            {
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
        ///             &lt;field/%gt;
        ///         &lt;/base&gt;    
        ///     </code>
        /// </example>
        protected override string ElementName {
            get { return "field"; }
        }
    }
}
