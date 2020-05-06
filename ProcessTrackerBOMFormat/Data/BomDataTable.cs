using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Formatter.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable'
    public class BomDataTable
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable'

        private DataTable _dataTable = null;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.DataTableWithColumns'
        public DataTable DataTableWithColumns {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.DataTableWithColumns'
            get {
                DataTable copiedData = _dataTable.Clone();
                FixCopyColumnValues(copiedData);
                return copiedData;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetDataCopy()'
        public DataTable GetDataCopy()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetDataCopy()'
            DataTable copiedData = _dataTable.Copy();
            FixCopyColumnValues(copiedData);

            return copiedData;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.Data'
        public DataTable Data {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.Data'
            get { return _dataTable; }
        }

        private void FixCopyColumnValues(DataTable copiedTable)
        {
            foreach (BomDataColumn copyColumn in copiedTable.Columns)
            {
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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.Rows'
        public DataRowCollection Rows { get { return _dataTable.Rows; } }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.Rows'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.BomDataTable()'
        public BomDataTable()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.BomDataTable()'
            _dataTable = new DataTable();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetColumn(int)'
        public BomDataColumn GetColumn(int number)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetColumn(int)'
            return (BomDataColumn)_dataTable.Columns[number];
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetColumnPosition(int)'
        public int GetColumnPosition(int number)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetColumnPosition(int)'
            return ((BomDataColumn)_dataTable.Columns[number]).Position;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetColumns()'
        public DataColumnCollection GetColumns()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetColumns()'
            return _dataTable.Columns;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetColumn(string)'
        public BomDataColumn GetColumn(string key)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetColumn(string)'
            foreach (BomDataColumn column in GetColumns())
            {
                if (column.ColumnName.Equals(key)) return column;
            }
            return null;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetColumnCount()'
        public int GetColumnCount()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetColumnCount()'
            return _dataTable.Columns.Count;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.AddColumn(BomDataColumn)'
        public void AddColumn(BomDataColumn column)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.AddColumn(BomDataColumn)'

            if (column.Position < 0 || column.Position > GetColumnCount())
            {
                _addColumn(column);
            }
            else if (column.Position == GetColumnCount())
            {
                _addColumn(column);

                for (int i = GetColumnCount() - 1; i >= 0; i--)
                {
                    BomDataColumn bomColumn = GetColumn(i);
                    if (column.Position < bomColumn.Position || bomColumn.Position < 0)
                    {
                        MoveColumn(column, bomColumn.Ordinal);
                    }
                }
            }
            else
            {
                if (GetColumnPosition(column.Position) == column.Position)
                {
                    if (column.Overrides)
                    {
                        RemoveColumn(column.Position);

                        _addColumn(column);
                        MoveColumn(GetColumnCount() - 1, column.Position);
                    }
                }
                else
                {
                    _addColumn(column);
                    MoveColumn(GetColumnCount() - 1, column.Position);
                }
            }
        }

        private void RemoveColumn(BomDataColumn column)
        {
            _dataTable.Columns.Remove(column);
        }

        private void RemoveColumn(int index)
        {
            _dataTable.Columns.RemoveAt(index);
        }

        private void _addColumn(BomDataColumn column)
        {
            _dataTable.Columns.Add(column);
        }

        private void MoveColumn(BomDataColumn columnToMove, int ordinal)
        {
            MoveColumn(_dataTable.Columns.IndexOf(columnToMove), ordinal);
        }

        private void MoveColumn(int columnToMove, int ordinal)
        {
            _dataTable.Columns[columnToMove].SetOrdinal(ordinal);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.AddRowCollection(BomDataRowCollection)'
        public void AddRowCollection(BomDataRowCollection rowCollection)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.AddRowCollection(BomDataRowCollection)'
            foreach (BomDataRowHolder row in rowCollection)
            {
                AddRow(row);
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetNewRow()'
        public BomDataRowHolder GetNewRow()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.GetNewRow()'
            return new BomDataRowHolder(this);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.AddRow(BomDataRowHolder)'
        public void AddRow(BomDataRowHolder row)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataTable.AddRow(BomDataRowHolder)'
            Collection<object> values = new Collection<object>();

            foreach (BomDataColumn column in GetColumns())
            {
                if (column.IsQuantity && !double.TryParse(row[column].Value.ToString(), out double outValue))
                {
                    values.Add(outValue);
                }
                else values.Add(row[column].Value);
            }

            _dataTable.Rows.Add(values.ToArray());
        }
    }
}
