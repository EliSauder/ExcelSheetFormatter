using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBom : ConfigurationElement {

        public CustomConfigBom() {
            CustomConfigBomColumnField columnField = new CustomConfigBomColumnField();
            BomColumnFieldCollection.Add(columnField);
        }

        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name { get; set; }

        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
        public bool Enabled { get; set; }

        [ConfigurationProperty("uniqueKey", IsRequired = true)]
        public CustomConfigBomUniqueKey UniqueKey {get; set;}

        [ConfigurationProperty("fields", IsRequired = true)]
        [ConfigurationCollection(typeof(CustomConfigBomColumnFieldCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public CustomConfigBomColumnFieldCollection BomColumnFieldCollection {
            get { return (CustomConfigBomColumnFieldCollection)base["bom"]; }
            set { CustomConfigBomColumnFieldCollection columnFields = value; }
        }

    }
}
