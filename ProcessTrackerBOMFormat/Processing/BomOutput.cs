using ClosedXML.Excel;
using Formatter.Configuration;
using Formatter.Data;
using System;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace Formatter.Processing
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomOutput'
    public class BomOutput
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomOutput'

        private BomDataTable _bomDataTable = new BomDataTable();
        private Excel.Application _excelAppInstance = null;
        private ConfigurationSectionFiles _fileConfig = null;
        private BomInput _bomInput = null;
        private ConfigurationElementBom _bomConfig = null;
        private XLWorkbook _outputData = null;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.TemplateRow'
        public BomDataRowHolder TemplateRow { get; } = null;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.TemplateRow'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.BomDataTable'
        public BomDataTable BomDataTable {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.BomDataTable'
            get { return _bomDataTable; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.BomOutput(Application, BomInput, ConfigurationElementBom, ConfigurationSectionFiles)'
        public BomOutput(Excel.Application excelInstance, BomInput bomInput, ConfigurationElementBom bomElement, ConfigurationSectionFiles fileConfig)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.BomOutput(Application, BomInput, ConfigurationElementBom, ConfigurationSectionFiles)'
            _excelAppInstance = excelInstance;
            _fileConfig = fileConfig;
            _bomInput = bomInput;
            _bomConfig = bomElement;

            foreach (ConfigurationElementColumn column in bomElement.ColumnCollection)
            {
                _bomDataTable.AddColumn(new BomDataColumn(column));
            }

            TemplateRow = new BomDataRowHolder(_bomDataTable);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.CopyDataToExcel()'
        public void CopyDataToExcel()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.CopyDataToExcel()'

            //Excel.Workbook wb = _excelAppInstance.Workbooks.Add();
            // Excel.Worksheet ws = wb.ActiveSheet;

            foreach (BomDataColumn column in _bomDataTable.Data.Columns)
            {
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

            foreach (BomDataColumn column in _bomDataTable.Data.Columns)
            {
                string temp = column.OutputName;
                column.OutputName = column.ColumnName;
                column.ColumnName = temp;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.SaveWorkbook()'
        public void SaveWorkbook()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.SaveWorkbook()'
            if (_outputData == null) throw new NullReferenceException("Data has not been copied to workbook.");

            StringBuilder outputFileName = new StringBuilder();

            outputFileName.Append(_fileConfig.RootDirectory)
                .Append(_fileConfig.OutputFolder)
                .Append(_bomInput.ProductNumber.ProductNumber)
                .Append("-output")
                .Append(Properties.Resources.OUTPUTFILE_EXTENTION);

            _outputData.SaveAs(outputFileName.ToString());

            _excelAppInstance.Workbooks.Open(outputFileName.ToString());
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.GetBomDataColumn(ConfigurationElementColumn)'
        public BomDataColumn GetBomDataColumn(ConfigurationElementColumn configColumn)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.GetBomDataColumn(ConfigurationElementColumn)'
            return _bomDataTable.GetColumn(configColumn.Name);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.GetBomDataColumn(string)'
        public BomDataColumn GetBomDataColumn(string configColumn)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.GetBomDataColumn(string)'
            return _bomDataTable.GetColumn(configColumn);
        }

#pragma warning disable CS0114 // 'BomOutput.ToString()' hides inherited member 'object.ToString()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.ToString()'
        public string ToString()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomOutput.ToString()'
#pragma warning restore CS0114 // 'BomOutput.ToString()' hides inherited member 'object.ToString()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
            StringBuilder returnVal = new StringBuilder();
            foreach (BomDataColumn column in _bomDataTable.GetColumns())
            {
                returnVal.Append(column.ColumnName).Append(", ");
            }

            return returnVal.ToString();
        }
    }
}
