using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace ProcessTrackerBOMFormat.UserInterface.Models {
    public class BomExcelModel {

        public const string CELL_REGEX = @"^([A-Z]+)([0-9]+)$";

        private string _filePath = "";
        private Regex _cellRegex = null;
        private _Application _excel = new _Excel.Application();
        private Workbook _workbook = null;
        private Worksheet _worksheet = null;

        public BomExcelModel(string filePath) {
            this._filePath = filePath;
            if (!File.Exists(filePath)) throw new FileNotFoundException("File requrested does not exist.");

            this._workbook = _excel.Workbooks.Open(this._filePath);
            this._worksheet = _excel.Worksheets[0];

            _cellRegex = new Regex(CELL_REGEX);
        }

        private Range this[object row, object column] {
            get { return _worksheet.Cells[row, column].Value2; }
            set { _worksheet.Cells[row, column].Value2 = value; }
        }

        public Range this[int row, int column] {
            get { return _worksheet.Cells[++row, ++column].Value2; }
            set { _worksheet.Cells[++row, ++column].Value2 = value; }
        }

        public Range this[int row, string column] {
            get { return _worksheet.Cells[++row, column].Value2; }
            set { _worksheet.Cells[++row, column].Value2 = value; }
        }

        public Range this[string cell] {
            get {
                Match match = _cellRegex.Match(cell);
                if (!match.Success) throw new FormatException("Cell name was invalid.");

                string column = match.Groups[0].Value;
                int row = int.Parse(match.Groups[1].Value);

                return _worksheet.Cells[++row, column];
            }
        }

        public ListColumn GetColumn(int column) {
            return _worksheet.Columns[1, ++column].Column;
        }
    }
}
