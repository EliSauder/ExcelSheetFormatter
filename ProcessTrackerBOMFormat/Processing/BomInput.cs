using ExcelDataReader;
using Formatter.UserInterface.Models;
using System.Data;
using System.IO;

namespace Formatter.Processing {
    public class BomInput {

        private string _sheetName = "";
        private DataTable _inputData = null;
        private ProductNumberModel _productNumber = null;

        public BomInput(ProductNumberModel productNumber, string absoluteInputFolderPath, string fileExtention) {

            _productNumber = productNumber;

            string filePath = absoluteInputFolderPath + _productNumber.ProductNumber + fileExtention;
            //_sheetName = GetSheetName(filePath, 1);

            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IExcelDataReader excelFile = ExcelReaderFactory.CreateReader(fileStream);
            
            DataSet dataSet = excelFile.AsDataSet(new ExcelDataSetConfiguration() {
                UseColumnDataType = true,
                FilterSheet = (tableReader, sheetIndex) => true,
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration() {
                    EmptyColumnNamePrefix = "Column",
                    UseHeaderRow = true,
                    FilterRow = (rowReader) => {
                        return true;
                    },
                    FilterColumn = (rowReader, columnIndex) => {
                        return true;
                    }
                }
            });

            _inputData = dataSet.Tables[0];
            _sheetName = _inputData.TableName;
        }

        public DataTable InputData {
            get { return _inputData; }
        }

        public string SheetName {
            get { return _sheetName; }
        }

        public ProductNumberModel ProductNumber {
            get { return _productNumber; }
        }
    }
}
