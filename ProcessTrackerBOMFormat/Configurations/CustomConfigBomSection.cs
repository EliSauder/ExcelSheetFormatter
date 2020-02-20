using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBomSection : ConfigurationSection {

        public CustomConfigBomSection() {
            CustomConfigBom columnField = new CustomConfigBom();
            BomCollection.Add(columnField);
        }

        [ConfigurationProperty("bom", IsRequired = true, IsDefaultCollection = true)]
        public CustomConfigBomCollection BomCollection {
            get { return (CustomConfigBomCollection)base[""]; }
            //set { CustomConfigBomCollection columnFields = value; }
        }

    }
}
