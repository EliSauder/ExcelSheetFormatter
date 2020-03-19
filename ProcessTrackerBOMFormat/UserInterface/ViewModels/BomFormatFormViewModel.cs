using Caliburn.Micro;
using ProcessTrackerBOMFormat.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProcessTrackerBOMFormat.UserInterface.ViewModels {
    public class BomFormatFormViewModel : Screen, IBomFormatChild{

        private BomSelectionModel _bomSelectionModel = new BomSelectionModel();
        private ProductNumberModel _productNumber = new ProductNumberModel();
        public bool _bomIsSelected = false;

        public bool BomIsSelected {
            get { return _bomIsSelected; }
            set {
                _bomIsSelected = value;
                NotifyOfPropertyChange(() => BomIsSelected);
            }
        }

        public void bomSelectedIsValid() {
            BomIsSelected = !string.IsNullOrWhiteSpace(SelectedItem.Key);
        }

        public string ProductNumber {
            get { return _productNumber.ProductNumber; }
            set { 
                _productNumber.ProductNumber = value;
                NotifyOfPropertyChange(() => ProductNumber);
            }
        }

        public BomSelectionModel BomSelectionModel {
            get { return _bomSelectionModel; }
        }

        public Dictionary<string, string> Boms {
            get { return _bomSelectionModel.Boms; }
        }

        public KeyValuePair<string, string> SelectedItem {
            get { return _bomSelectionModel.SelectedValue; }
            set { 
                _bomSelectionModel.SelectedValue = value;
                SelectedKey = value.Key;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        private string SelectedKey {
            get { return _bomSelectionModel.SelectedValue.Key; }
            set { NotifyOfPropertyChange(() => SelectedKey); }
        }

        public bool CanProcess(string productNumber, string selectedKey) {
            MessageBox.Show("Calling CanProcess();");
            return _productNumber.validateProductNumber() && !string.IsNullOrWhiteSpace(selectedKey);
        }

        public void Process(string productNumber, string selectedKey) {

        }

    }
}
