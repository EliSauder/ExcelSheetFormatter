using ExcelDataReader;
using Formatter.UserInterface.Models;
using System.Data;
using System.IO;

namespace Formatter.Processing
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomInput'
    public class BomInput
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomInput'

        private string _sheetName = "";
        private DataTable _inputData = null;
        private ProductNumberModel _productNumber = null;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomInput.BomInput(ProductNumberModel, string, string)'
        public BomInput(ProductNumberModel productNumber, string absoluteInputFolderPath, string fileExtention)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomInput.BomInput(ProductNumberModel, string, string)'

            _productNumber = productNumber;

            string filePath = absoluteInputFolderPath + _productNumber.ProductNumber + fileExtention;
            //_sheetName = GetSheetName(filePath, 1);

            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IExcelDataReader excelFile = ExcelReaderFactory.CreateReader(fileStream);

            DataSet dataSet = excelFile.AsDataSet(new ExcelDataSetConfiguration()
            {
                UseColumnDataType = true,
                FilterSheet = (tableReader, sheetIndex) => true,
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                    EmptyColumnNamePrefix = "Column",
                    UseHeaderRow = true,
                    FilterRow = (rowReader) =>
                    {
                        return true;
                    },
                    FilterColumn = (rowReader, columnIndex) =>
                    {
                        return true;
                    }
                }
            });

            _inputData = dataSet.Tables[0];
            _sheetName = _inputData.TableName;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomInput.InputData'
        public DataTable InputData {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomInput.InputData'
            get { return _inputData; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomInput.SheetName'
        public string SheetName {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomInput.SheetName'
            get { return _sheetName; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomInput.ProductNumber'
        public ProductNumberModel ProductNumber {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomInput.ProductNumber'
            get { return _productNumber; }
        }
    }
}
