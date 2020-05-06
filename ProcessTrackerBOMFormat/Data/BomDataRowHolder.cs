using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text.RegularExpressions;

namespace Formatter.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder'
    public class BomDataRowHolder : Collection<BomDataCell>
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.BomDataRowHolder(BomDataRowHolder)'
        public BomDataRowHolder(BomDataRowHolder copyRow)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.BomDataRowHolder(BomDataRowHolder)'
            foreach (BomDataCell cell in copyRow)
            {
                this.Add(new BomDataCell(this, cell.Column, null));
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.BomDataRowHolder(BomDataRowHolder, BomDataColumn, object)'
        public BomDataRowHolder(BomDataRowHolder copyRow, BomDataColumn replaceColumn, object newValue)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.BomDataRowHolder(BomDataRowHolder, BomDataColumn, object)'
            foreach (BomDataCell cell in copyRow)
            {
                if (cell.Column.Equals(replaceColumn))
                {
                    this.Add(new BomDataCell(this, replaceColumn, newValue));
                }
                else
                {
                    this.Add(new BomDataCell(this, cell.Column, cell.Value));
                }
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.BomDataRowHolder(BomDataTable)'
        public BomDataRowHolder(BomDataTable dataTable)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.BomDataRowHolder(BomDataTable)'
            DataColumnCollection columns = dataTable.GetColumns();
            foreach (BomDataColumn column in columns)
            {
                this.Add(new BomDataCell(this, column, null));
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.this[BomDataCell]'
        public BomDataCell this[BomDataCell cell] {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.this[BomDataCell]'
            get { return this[this.IndexOf(cell)]; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.this[BomDataColumn]'
        public BomDataCell this[BomDataColumn column] {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.this[BomDataColumn]'
            get {
                foreach (BomDataCell cell in this)
                {
                    if (column.Equals(cell.Column)) return cell;
                }
                return null;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.ExpandRow()'
        public BomDataRowCollection ExpandRow()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataRowHolder.ExpandRow()'

            BomDataRowCollection rowCollection = new BomDataRowCollection();
            Collection<BomDataColumn> quantityColumnCollection = new Collection<BomDataColumn>();

            int rowsAdded = 0;

            foreach (BomDataCell cell in this)
            {
                if (cell.Column.IsSplit)
                {
                    string[] values;

                    if (cell.Value == null)
                    {
                        values = new string[] { "" };
                    }
                    else
                    {
                        Regex test = new Regex(cell.Column.Delimeter);
                        values = test.Split(cell.Value.ToString()); // cell.Value.ToString().Split(new[] { cell.Column.Delimeter }, StringSplitOptions.None);
                    }

                    rowsAdded += values.Length;

                    foreach (string value in values)
                    {
                        rowCollection.Add(new BomDataRowHolder(cell.Row, cell.Column, value));
                    }
                }

                if (cell.Column.IsQuantity) quantityColumnCollection.Add(cell.Column);

            }

            if (rowsAdded == 0)
            {
                rowCollection.Add(this);
                rowsAdded++;
            }

            if (quantityColumnCollection.Count > 0)
            {
                foreach (BomDataColumn column in quantityColumnCollection)
                {
                    foreach (BomDataRowHolder row in rowCollection)
                    {
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
