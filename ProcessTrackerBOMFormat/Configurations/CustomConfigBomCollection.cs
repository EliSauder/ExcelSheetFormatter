using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBomCollection : ConfigurationElementCollection {

        public CustomConfigBomCollection() {
            CustomConfigBomElement bom = (CustomConfigBomElement)CreateNewElement();
            if(!bom.Name.Equals("")) {
                Add(bom);
            }
        }

        public override ConfigurationElementCollectionType CollectionType {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement() {
            return new CustomConfigBomElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((CustomConfigBomElement)element).Name;
        }

        public CustomConfigBomElement this[int index] {
            get { return (CustomConfigBomElement)BaseGet(index); }
            set {
                if (BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        new public CustomConfigBomElement this[string name] {
            get { return (CustomConfigBomElement)BaseGet(name); }
        }

        public int IndexOf(CustomConfigBomElement field) {
            return BaseIndexOf(field);
        }

        public void Add(CustomConfigBomElement field) {
            BaseAdd(field);
        }

        protected override void BaseAdd(ConfigurationElement element) {
            BaseAdd(element, false);
        }

        //protected override string ElementName {
        //    get { return "bom"; }
        //}

        public void Remove(CustomConfigBomElement field) {
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
