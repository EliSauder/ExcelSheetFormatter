using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Formatter.Configuration;
using Formatter.Data;
using System;
using System.IO;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace Formatter.Processing {
    public class BomOutput {

        private BomDataTable _bomDataTable = new BomDataTable();
        private Excel.Application _excelAppInstance = null;
        private IFormatterConfiguration _formatterConfiguration;
        private BomInput _bomInput = null;
        private ConfigurationElementBom _bomConfig = null;
        private XLWorkbook _outputData = null;

        public BomDataRowHolder TemplateRow { get; } = null;

        public BomDataTable BomDataTable {
            get { return _bomDataTable; }
        }

        public BomOutput(Excel.Application excelInstance, BomInput bomInput, ConfigurationElementBom bomElement, IFormatterConfiguration formatterConfiguration) {
            _excelAppInstance = excelInstance;
            _formatterConfiguration = formatterConfiguration;
            _bomInput = bomInput;
            _bomConfig = bomElement;

            foreach (ConfigurationElementColumn column in bomElement.ColumnCollection) {
                _bomDataTable.AddColumn(new BomDataColumn(column));
            }

            TemplateRow = new BomDataRowHolder(_bomDataTable);
        }

        public void CopyDataToExcel() {

            //Excel.Workbook wb = _excelAppInstance.Workbooks.Add();
            // Excel.Worksheet ws = wb.ActiveSheet;

            foreach (BomDataColumn column in _bomDataTable.Data.Columns) {
                string temp = column.OutputName;
                column.OutputName = column.ColumnName;
                column.ColumnName = temp;
            }

            XLWorkbook xlWb = new XLWorkbook();

            xlWb.Worksheets.Add(_bomDataTable.Data, _bomConfig.OutputSheetName);

            /*ws.Name = _bomConfig.OutputSheetName;

            for(int i = 0; i < _bomDataTable.Data.Columns.Count; i++) {
                ws.Cells[1, i + 1].Value2 = ((BomDataColumn)_bomDataTable.Data.Columns[i]).OutputName;
            }

            for(int i = 0; i < _bomDataTable.Data.Columns.Count; i++) {
                for (int j = 0; j < _bomDataTable.Data.Rows.Count; j++) {
                    ws.Cells[j + 2, i + 1] = _bomDataTable.Data.Rows[j][i];
                }
            }*/

            _outputData = xlWb;

            foreach (BomDataColumn column in _bomDataTable.Data.Columns) {
                string temp = column.OutputName;
                column.OutputName = column.ColumnName;
                column.ColumnName = temp;
            }
        }

        public void SaveWorkbook() {
            if (_outputData == null) throw new NullReferenceException("Data has not been copied to workbook.");

            //StringBuilder outputFileName = new StringBuilder();

            //outputFileName.Append(_fileConfig.RootDirectory)
            //    .Append(_fileConfig.OutputFolder)
            //    .Append(_bomInput.ProductNumber.ProductNumber)
            //    .Append("-output")
            //    .Append(Properties.Resources.OUTPUTFILE_EXTENTION);

            string outputFilePath = Path.Combine(_formatterConfiguration.OutputFolderPath, _bomInput.ProductNumber.ProductNumber + "-output");
            outputFilePath = Path.ChangeExtension(outputFilePath, Properties.Resources.OUTPUTFILE_EXTENTION);

            _outputData.SaveAs(outputFilePath);

            _excelAppInstance.Workbooks.Open(outputFilePath);
        }

        public BomDataColumn GetBomDataColumn(ConfigurationElementColumn configColumn) {
            return _bomDataTable.GetColumn(configColumn.Name);
        }

        public BomDataColumn GetBomDataColumn(string configColumn) {
            return _bomDataTable.GetColumn(configColumn);
        }

        public string ToString() {
            StringBuilder returnVal = new StringBuilder();
            foreach (BomDataColumn column in _bomDataTable.GetColumns()) {
                returnVal.Append(column.ColumnName).Append(", ");
            }

            return returnVal.ToString();
        }
    }
}