using Formatter.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Formatter.UserInterface.Models {
    public class BomSelectionModel {

        private readonly Dictionary<string, string> _boms = new Dictionary<string, string>();
        private readonly List<string> _bomKeyList = new List<string>();
        private KeyValuePair<string, string> _selectedBom = new KeyValuePair<string, string>(null, null);
        public int NumberBoms { get; } = 0;

        public BomSelectionModel(IFormatterConfiguration formatter) {
            ConfigurationSectionBoms configuration = formatter.BomConfiguration;

            foreach (ConfigurationElementBom bom in configuration.BomCollection) {
                if (bom.Enabled) {
                    _boms.Add(bom.Name, bom.DisplayName);
                    _bomKeyList.Add(bom.Name);
                    NumberBoms++;
                }
            }
        }

        public Dictionary<string, string> Boms {
            get { return _boms; }
        }

        public KeyValuePair<string, string> this[string key] {
            get { return new KeyValuePair<string, string>(key, _boms[key]); }
        }

        public KeyValuePair<string, string> this[int number] {
            get { return new KeyValuePair<string, string>(_bomKeyList[number], _boms[_bomKeyList[number]]); }
        }

        public void select(string key) {
            if (!_boms.ContainsKey(key)) throw new ArgumentException("Value pair provided is not found in the list of BOMs.");
            _selectedBom = new KeyValuePair<string, string>(key, _boms[key]);
        }

        public void select(int number) {
            if (!_boms.ContainsKey(_bomKeyList[number])) throw new ArgumentException("Value pair provided is not found in the list of BOMs.");
            _selectedBom = new KeyValuePair<string, string>(_bomKeyList[number], _boms[_bomKeyList[number]]);
        }

        public bool HasSelectedItem() {
            return _selectedBom.Key != null;
        }

        public KeyValuePair<string, string> SelectedItem {
            get { return _selectedBom; }
            set {
                if (!_boms.ContainsKey(value.Key)) throw new ArgumentException("Value pair provided is not found in the list of BOMs.");
                _selectedBom = value;
            }
        }
    }
}
