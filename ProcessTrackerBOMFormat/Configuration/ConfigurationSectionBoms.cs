using System.Configuration;

namespace ProcessTrackerBOMFormat.Configuration {
    public class ConfigurationSectionBoms : ConfigurationSection {

        //public CustomConfigBomSection() {
        //    CustomConfigBom columnField = new CustomConfigBom();
        //    BomCollection.Add(columnField);
        //}

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ConfigurationCollectionBoms BomCollection {
            get {
                ConfigurationCollectionBoms bomCollection = (ConfigurationCollectionBoms)base[""];
                return bomCollection; 
            }
            //set { CustomConfigBomCollection columnFields = value; }
        }

        [ConfigurationProperty("outputType", DefaultValue = "individual", IsRequired = true)]
        [RegexStringValidator("^individual|compact$")]
        public string OutputType {
            get { return (string)base["outputType"]; }
            set { base["outputType"] = value; }
        }

    }
}
