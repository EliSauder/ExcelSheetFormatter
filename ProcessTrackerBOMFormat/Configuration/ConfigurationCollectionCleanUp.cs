using System.Configuration;

namespace Formatter.Configuration
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCollectionCleanUp'
    public class ConfigurationCollectionCleanUp : ConfigurationElementCollection
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCollectionCleanUp'

        /// <summary>
        /// Creates a collection with a single element as long 
        /// as the name is not and empty string.
        /// </summary>
        public ConfigurationCollectionCleanUp()
        {
            ConfigurationElementCleanUp bom = (ConfigurationElementCleanUp)CreateNewElement();
            if (!bom.Name.Equals(""))
            {
                Add(bom);
            }
        }

        /// <value>Property <c>CollectionType</c> Represents the type of the current collection.</value>
        public override ConfigurationElementCollectionType CollectionType {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        /// <summary>
        /// Creates a new ConfigurationElement to add to the collection.
        /// </summary>
        /// <remarks>It will create the element as a <c>ConfigurationElementCleanUp</c></remarks>
        /// <returns>A new configuration element of type ConfigurationElementCleanUp</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationElementCleanUp();
        }

        /// <summary>
        /// Gets the key of the element provided.
        /// </summary>
        /// <param name="element">The elment that you want the key of.</param>
        /// <returns>The key of the element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigurationElementCleanUp)element).Name;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCollectionCleanUp.this[int]'
        public ConfigurationElementCleanUp this[int index] {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCollectionCleanUp.this[int]'
            get { return (ConfigurationElementCleanUp)BaseGet(index); }
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
        new public ConfigurationElementCleanUp this[string name] {
            get { return (ConfigurationElementCleanUp)BaseGet(name); }
        }

        /// <summary>
        /// The index of the element provided.
        /// </summary>
        /// <param name="field">The element you wish to find the index of.</param>
        /// <returns>The index of the element.</returns>
        public int IndexOf(ConfigurationElementCleanUp field)
        {
            return BaseIndexOf(field);
        }

        /// <summary>
        /// Adds an element to the collection
        /// </summary>
        /// <param name="field">The element you wish to add</param>
        public void Add(ConfigurationElementCleanUp field)
        {
            BaseAdd(field);
        }

        /// <summary>
        /// Adds the element provided to the collection.
        /// Overrides the <c>BaseAdd</c> method.
        /// </summary>
        /// <param name="element">The element you wish to add</param>
        /// <see cref="ConfigurationElement"/>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCollectionCleanUp.ElementName'
        protected override string ElementName {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCollectionCleanUp.ElementName'
            get { return "cleanup"; }
        }
    }
}
