using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBomCollection : ConfigurationElementCollection {

        public CustomConfigBomCollection() { }

        public override ConfigurationElementCollectionType CollectionType {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        protected override ConfigurationElement CreateNewElement() {
            return new CustomConfigBom();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((CustomConfigBom)element).Name;
        }

        public CustomConfigBom this[int index] {
            get { return (CustomConfigBom)BaseGet(index); }
            set {
                if (BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        new public CustomConfigBom this[string name] {
            get { return (CustomConfigBom)BaseGet(name); }
        }

        public int IndexOf(CustomConfigBom field) {
            return BaseIndexOf(field);
        }

        public void Add(CustomConfigBom field) {
            BaseAdd(field);
        }

        protected override void BaseAdd(ConfigurationElement element) {
            BaseAdd(element, false);
        }

        protected override string ElementName {
            get { return "bom"; }
        }

        public void Remove(CustomConfigBom field) {
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
        }
    }
}
