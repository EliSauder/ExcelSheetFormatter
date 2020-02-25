using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBomSection : ConfigurationSection {

        //public CustomConfigBomSection() {
        //    CustomConfigBom columnField = new CustomConfigBom();
        //    BomCollection.Add(columnField);
        //}

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public CustomConfigBomCollection BomCollection {
            get {
                CustomConfigBomCollection bomCollection = (CustomConfigBomCollection)base[""];
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
