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

        public BomFormatFormViewModel() {
            _bomSelectionModel.SelectedItem = _bomSelectionModel[0];
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
            get { return _bomSelectionModel.SelectedItem; }
            set { 
                _bomSelectionModel.SelectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        public void Process(string productNumber) {

        }

    }
}
