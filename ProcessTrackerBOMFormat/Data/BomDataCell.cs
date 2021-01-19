namespace Formatter.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataCell'
    public class BomDataCell
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataCell'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataCell.Value'
        public object Value { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataCell.Value'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataCell.Row'
        public BomDataRowHolder Row { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataCell.Row'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataCell.Column'
        public BomDataColumn Column { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataCell.Column'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataCell.BomDataCell(BomDataRowHolder, BomDataColumn, object)'
        public BomDataCell(BomDataRowHolder row, BomDataColumn column, object value)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataCell.BomDataCell(BomDataRowHolder, BomDataColumn, object)'
            Column = column;
            Row = row;
            Value = value;
        }
    }
}
