using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Formatter.Configuration {
    public class ConfigurationSectionParsing : ConfigurationSection {

        private XmlNode _xmlNode;

        public ConfigurationSectionParsing() { }

        public ConfigurationSectionParsing(XmlNode node) {
            foreach (XmlAttribute attribute in node.Attributes) {
                if (Properties.Contains(attribute.Name))
                    this[this.Properties[attribute.Name]] = this.Properties[attribute.Name].Converter.ConvertFrom(attribute.Value);
            }

            foreach (XmlNode childNode in node.ChildNodes) {
                if (this.Properties.Contains(childNode.Name))
                    this[childNode.Name] = Activator.CreateInstance(this[childNode.Name].GetType(), childNode);
            }

            _xmlNode = node["parsing"];
        }

        [ConfigurationProperty("productRegex", IsRequired = true)]
        public string ProdutRegex {
            get { return (string)this["productRegex"]; }
            set {
                base["productRegex"] = value;
            }
        }
    }
}
