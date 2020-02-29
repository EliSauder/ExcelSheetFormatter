using System.Configuration;

namespace ProcessTrackerBOMFormat.Configuration {
    public class ConfigurationCollectionPopulations : ConfigurationElementCollection{
        public override ConfigurationElementCollectionType CollectionType {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement() {
            return new ConfigurationElementPopulation();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((ConfigurationElementPopulation)element).Name;
        }

        public ConfigurationElementPopulation this[int index] {
            get { return (ConfigurationElementPopulation)BaseGet(index); }
            set {
                if (BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        new public ConfigurationElementPopulation this[string name] {
            get { return (ConfigurationElementPopulation)BaseGet(name); }
        }

        public int IndexOf(ConfigurationElementPopulation field) {
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
            get { return "populate"; }
        }
    }
}
