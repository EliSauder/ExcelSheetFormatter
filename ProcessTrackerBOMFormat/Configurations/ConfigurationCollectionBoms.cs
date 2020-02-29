using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class ConfigurationCollectionBoms : ConfigurationElementCollection {

        public ConfigurationCollectionBoms() {
            ConfigurationElementBom bom = (ConfigurationElementBom)CreateNewElement();
            if(!bom.Name.Equals("")) {
                Add(bom);
            }
        }

        public override ConfigurationElementCollectionType CollectionType {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement() {
            return new ConfigurationElementBom();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((ConfigurationElementBom)element).Name;
        }

        public ConfigurationElementBom this[int index] {
            get { return (ConfigurationElementBom)BaseGet(index); }
            set {
                if (BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        new public ConfigurationElementBom this[string name] {
            get { return (ConfigurationElementBom)BaseGet(name); }
        }

        public int IndexOf(ConfigurationElementBom field) {
            return BaseIndexOf(field);
        }

        public void Add(ConfigurationElementBom field) {
            BaseAdd(field);
        }

        protected override void BaseAdd(ConfigurationElement element) {
            BaseAdd(element, false);
        }

        //protected override string ElementName {
        //    get { return "bom"; }
        //}

        public void Remove(ConfigurationElementBom field) {
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

        protected override string ElementName {
            get { return "bom"; }
        }
    }
}
