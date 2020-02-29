using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
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

        /*public void Add(CustomConfigBomColumnField field) {
            BaseAdd(field);
        }

        protected override void BaseAdd(ConfigurationElement element) {
            BaseAdd(element, false);
        }

        public void Remove(CustomConfigBomColumnField field) {
            if (BaseIndexOf(field) >= 0) {
                BaseRemove(field.Name);
            }
        }

        public void RemoveAt(int index) {
            BaseRemoveAt(index);
        }

        public void Remove(string name) {
            BaseRemove(name);
        }

        public void Clear() {
            BaseClear();
        }*/

        protected override string ElementName {
            get { return "field"; }
        }
    }
}
