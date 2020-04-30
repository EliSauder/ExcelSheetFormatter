using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Formatter.Data {
    public class BomDataRowHolder : Collection<BomDataCell> {

        public BomDataRowHolder(BomDataRowHolder copyRow) {
            foreach(BomDataCell cell in copyRow) {
                this.Add(new BomDataCell(this, cell.Column, null));
            }
        }

        public BomDataRowHolder(BomDataRowHolder copyRow, BomDataColumn replaceColumn, object newValue) {
            foreach(BomDataCell cell in copyRow) {
                if(cell.Column.Equals(replaceColumn)) {
                    this.Add(new BomDataCell(this, replaceColumn, newValue));
                } else {
                    this.Add(new BomDataCell(this, cell.Column, cell.Value));
                }
            }
        }

        public BomDataRowHolder(BomDataTable dataTable) {
            DataColumnCollection columns = dataTable.GetColumns();
            foreach (BomDataColumn column in columns) {
                this.Add(new BomDataCell(this, column, null));
            }
        }

        public BomDataCell this[BomDataCell cell] {
            get { return this[this.IndexOf(cell)]; }
        }

        public BomDataCell this[BomDataColumn column] {
            get { 
                foreach(BomDataCell cell in this) {
                    if (column.Equals(cell.Column)) return cell;
                }
                return null;
            }
        }

        public BomDataRowCollection ExpandRow() {

            BomDataRowCollection rowCollection = new BomDataRowCollection();
            Collection<BomDataColumn> quantityColumnCollection = new Collection<BomDataColumn>();

            int rowsAdded = 0;

            foreach (BomDataCell cell in this) {
                if (cell.Column.IsSplit) {
                    string[] values;

                    if (cell.Value == null) {
                        values = new string[] { "" };
                    } else {
                        values = cell.Value.ToString().Split(new[] { cell.Column.Delimeter }, StringSplitOptions.None);
                    }
                    
                    rowsAdded += values.Length;

                    foreach (string value in values) {
                        rowCollection.Add(new BomDataRowHolder(cell.Row, cell.Column, value));
                    }
                }

                if (cell.Column.IsQuantity) quantityColumnCollection.Add(cell.Column);

            }

            if (rowsAdded == 0) {
                rowCollection.Add(this);
                rowsAdded++;
            }

            if (quantityColumnCollection.Count > 0) {
                foreach (BomDataColumn column in quantityColumnCollection) {
                    foreach (BomDataRowHolder row in rowCollection) {
                        if (row[column].Value != null && double.TryParse(row[column].Value.ToString(), out double valueToDouble))
                            row[column].Value = valueToDouble / rowsAdded;
                        else row[column].Value = 0;
                    }
                }
            }

            return rowCollection;
        }
    }
}
