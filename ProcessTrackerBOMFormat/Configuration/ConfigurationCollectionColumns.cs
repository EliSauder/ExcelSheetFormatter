using System.Configuration;

namespace ProcessTrackerBOMFormat.Configuration {
    public class ConfigurationCollectionColumns : ConfigurationElementCollection {

        public override ConfigurationElementCollectionType CollectionType {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement() {
            return new ConfigurationElementColumn();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((ConfigurationElementColumn)element).Name;
        }

        public ConfigurationElementColumn this[int index] {
            get { return (ConfigurationElementColumn)BaseGet(index); }
            set {
                if (BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        new public ConfigurationElementColumn this[string name] {
            get { return (ConfigurationElementColumn)BaseGet(name); }
        }

        public int IndexOf(ConfigurationElementColumn field) {
            return BaseIndexOf(field);
        }

        public int IndexOf(string name) {
            name = name.ToLower();
            for (int idx = 0; idx < base.Count; idx++) {
                if (this[idx].Name.ToLower() == name) return idx;
            }
            return -1;
        }

        protected override string ElementName {
            get { return "field"; }
        }
    }
}
