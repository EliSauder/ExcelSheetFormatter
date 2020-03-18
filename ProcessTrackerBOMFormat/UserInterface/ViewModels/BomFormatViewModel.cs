using Caliburn.Micro;
using ProcessTrackerBOMFormat.UserInterface.Interfaces;
using ProcessTrackerBOMFormat.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProcessTrackerBOMFormat.UserInterface.ViewModels {

    [Obsolete("This is not being used in favor of a simpler system", false)]
    public class BomFormatViewModel : Conductor<IBomFormatChild>.Collection.AllActive, IBomFormat {

        private ProductNumberModel _productNumber = null;
        private DataSet _excelData = null;

        public BomFormatViewModel() {
            Items.Add(new BomFormatFormViewModel());
            Items.Add(new BomFormatDataDisplayViewModel());
        }

        public BomFormatFormViewModel Form {
            get => (BomFormatFormViewModel)Items[0];
        }

        public BomFormatDataDisplayViewModel DataDisplay {
            get => (BomFormatDataDisplayViewModel)Items[1];
        }

        public BomExcelModel GetBomExcelModel() {
            throw new NotImplementedException();
        }

        public ProductNumberModel GetProductNumber() {
            return _productNumber;
        }

        public void SetProductNumber(string value) {
            this._productNumber.ProductNumber = value;
        }
    }
}
