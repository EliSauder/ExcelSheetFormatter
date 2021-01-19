using Formatter.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Formatter.UserInterface.Models {
    public class ProductNumberModel {

        public const string PRODUCT_NUMBER_REGEX = @"^((?:G|T|K)\d{5}(?:(?=-)-\d{1,3}(?:(?=[A-Z])[A-Z]\d|)|)|(?:V)?\d{6,7}Z)$";

        private readonly Regex _regex = null;

        public ProductNumberModel(IFormatterConfiguration configuration) {
            _regex = new Regex(configuration.ParsingConfiguration.ProdutRegex);
        }

        [Required]
        public string ProductNumber { get; set; } = "";

        public bool validateProductNumber() {
            return _regex.IsMatch(ProductNumber);
        }
    }
}