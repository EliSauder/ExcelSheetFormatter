using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessTrackerBOMFormat.UserInterface.Models {
    public class ProductNumberModel {

        public const string PRODUCT_NUMBER_REGEX = @"^((?:G|T|K)\d{5}(?:(?=-)-\d{1,3}(?:(?=[A-Z])[A-Z]\d|)|)|(?:V)?\d{6,7}Z)$";

        private string _productNumber = "";
        private Regex _regex = null;

        public ProductNumberModel() {
            this._regex = new Regex(PRODUCT_NUMBER_REGEX);
        }

        [Required]
        [RegularExpression(PRODUCT_NUMBER_REGEX, 
            ErrorMessage = "Product is not valid. Product number must match the following regex: " + PRODUCT_NUMBER_REGEX)]
        public string ProductNumber {
            get { return this._productNumber; }
            set { this._productNumber = value; }
        }
        
        public bool validateProductNumber(string productNumber) {
            return this._regex.IsMatch(productNumber);
        }

    }
}
