using System.Configuration;

namespace ProcessTrackerBOMFormat.Configurations {
    public class CustomConfigBomUniqueKey : ConfigurationSection {

        [ConfigurationProperty("whereLook", IsRequired = true)]
        [RegexStringValidator("^[A-Z]+[0-9]+(?:(?:,[A-Z]+[0-9]+)+)?$")]
        public string WhereLook { get; set; }

        [ConfigurationProperty("valueFind", IsRequired = true)]
        public string ValueFind { get; set; }

    }
}
