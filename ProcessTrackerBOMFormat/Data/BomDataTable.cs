using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Formatter.Data {
    public class BomDataTable {

        private DataTable _dataTable = null;

        public DataTable DataTableWithColumns {
            get {
                DataTable copiedData = _dataTable.Clone();
                FixCopyColumnValues(copiedData);
                return copiedData; } 
        }

        public DataTable GetDataCopy() {
            DataTable copiedData = _dataTable.Copy();
            FixCopyColumnValues(copiedData);

            return copiedData;
        }

        public DataTable Data {
            get { return _dataTable; }
        }
        
        private void FixCopyColumnValues(DataTable copiedTable) {
            foreach (BomDataColumn copyColumn in copiedTable.Columns) {
                BomDataColumn originalColumn = (BomDataColumn)_dataTable.Columns[copyColumn.ColumnName];
                copyColumn.OutputName = originalColumn.OutputName;
                copyColumn.Overrides = originalColumn.Overrides;
                copyColumn.Position = originalColumn.Position;
                copyColumn.ColumnName = originalColumn.ColumnName;
                copyColumn.HeaderName = originalColumn.HeaderName;
                copyColumn.IsSplit = originalColumn.IsSplit;
                copyColumn.Delimeter = originalColumn.Delimeter;
                copyColumn.IsQuantity = originalColumn.IsQuantity;
            }
        }

        public DataRowCollection Rows { get { return _dataTable.Rows; } }

        public BomDataTable() {
            _dataTable = new DataTable();
        }

        public BomDataColumn GetColumn(int number) {
            return (BomDataColumn)_dataTable.Columns[number];
        }

        public int GetColumnPosition(int number) {
            return ((BomDataColumn)_dataTable.Columns[number]).Position;
        }

        public DataColumnCollection GetColumns() {
            return _dataTable.Columns;
        }

        public BomDataColumn GetColumn(string key) {
            foreach(BomDataColumn column in GetColumns()) {
                if (column.ColumnName.Equals(key)) return column;
            }
            return null;
        }
        
        public int GetColumnCount() {
            return _dataTable.Columns.Count;
        }

        public void AddColumn(BomDataColumn column) {

            if (column.Position < 0 || column.Position > GetColumnCount()) {
                _addColumn(column);
            } else if (column.Position == GetColumnCount()) {
                _addColumn(column);

                for(int i = GetColumnCount() - 1; i >= 0; i--) {
                    BomDataColumn bomColumn = GetColumn(i);
                    if (column.Position < bomColumn.Position || bomColumn.Position < 0) {
                        MoveColumn(column, bomColumn.Ordinal);
                    }
                }
            } else {
                if(GetColumnPosition(column.Position) == column.Position) {
                    if (column.Overrides) {
                        RemoveColumn(column.Position);

                        _addColumn(column);
                        MoveColumn(GetColumnCount() - 1, column.Position);
                    }
                } else {
                    _addColumn(column);
                    MoveColumn(GetColumnCount() - 1, column.Position);
                }
            }
        }

        private void RemoveColumn(BomDataColumn column) {
            _dataTable.Columns.Remove(column);
        }

        private void RemoveColumn(int index) {
            _dataTable.Columns.RemoveAt(index);
        }

        private void _addColumn(BomDataColumn column) {
            _dataTable.Columns.Add(column);
        }

        private void MoveColumn(BomDataColumn columnToMove, int ordinal) {
            MoveColumn(_dataTable.Columns.IndexOf(columnToMove), ordinal);
        }

        private void MoveColumn(int columnToMove, int ordinal) {
            _dataTable.Columns[columnToMove].SetOrdinal(ordinal);
        }

        public void AddRowCollection(BomDataRowCollection rowCollection) {
            foreach (BomDataRowHolder row in rowCollection) {
                AddRow(row);
            }
        }

        public BomDataRowHolder GetNewRow() {
            return new BomDataRowHolder(this);
        }

        public void AddRow(BomDataRowHolder row) {
            Collection<object> values = new Collection<object>();

            foreach (BomDataColumn column in GetColumns()) {
                if (column.IsQuantity && !double.TryParse(row[column].Value.ToString(), out double outValue)) {
                    values.Add(outValue);
                }
                else values.Add(row[column].Value);
            }

            _dataTable.Rows.Add(values.ToArray());
        }
    }
}
