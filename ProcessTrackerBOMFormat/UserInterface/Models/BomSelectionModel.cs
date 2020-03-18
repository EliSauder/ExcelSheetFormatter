using ProcessTrackerBOMFormat.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTrackerBOMFormat.UserInterface.Models {
    public class BomSelectionModel {

        private readonly Dictionary<string, string> _boms = null;
        private KeyValuePair<string, string> _selectedBom = new KeyValuePair<string, string>(null, null);

        public int NumberBoms { get; } = 0;

        public BomSelectionModel() {
            ConfigurationSectionBoms configuration = (ConfigurationSectionBoms)ConfigurationManager.GetSection(Properties.Resources.BOM_CONFIGURATION_SECTION)

            foreach (ConfigurationElementBom bom in configuration.BomCollection) {
                _boms.Add(bom.Name, bom.DisplayName);
                NumberBoms++;
            }
        }

        public Dictionary<string, string> Boms {
            get { return _boms; }
        }

        public KeyValuePair<string, string> this[string key] {
            get { return new KeyValuePair<string, string>(key, _boms[key]); }
        }

        public void select(string key) {
            if (!_boms.ContainsKey(key)) throw new ArgumentException("Value pair provided is not found in the list of BOMs.");
            _selectedBom = new KeyValuePair<string, string>(key, _boms[key]);
        }

        public KeyValuePair<string, string> SelectedValue {
            get { return _selectedBom; }
            set {
                if (!_boms.ContainsKey(value.Key)) throw new ArgumentException("Value pair provided is not found in the list of BOMs.");
                _selectedBom = value;
            }
        }
    }
}
