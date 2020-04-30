namespace Formatter.Data {
    public class BomDataCell {
        public object Value { get; set; }
        public BomDataRowHolder Row { get; }
        public BomDataColumn Column { get; }

        public BomDataCell(BomDataRowHolder row, BomDataColumn column, object value) {
            Column = column;
            Row = row;
            Value = value;
        }
    }
}
