using Formatter.Configuration;
using System;
using System.Data;

namespace Formatter.Data {
    public class BomDataColumn : DataColumn {
        public bool Overrides { get; set; } = false;
        public int Position { get; set; } = 0;
        public string OutputName { get; set; } = "";
        public string HeaderName { get; set; } = "";
        public bool IsSplit { get; set; } = false;
        public string Delimeter { get; set; } = "";
        public bool IsQuantity { get; set; } = false;
        public int SplitInto { get; set; } = -1;

        public BomDataColumn() : base() { }

        public BomDataColumn(ConfigurationElementColumn configColumn) : this(configColumn.Output.Length == 0 ? configColumn.Header : configColumn.Output, configColumn.DataType, configColumn.Override, configColumn.Order) {
            this.ColumnName = configColumn.Name;
            this.OutputName = configColumn.Output.Length == 0 ? configColumn.Header : configColumn.Output;
            this.HeaderName = configColumn.Header;
            this.IsSplit = configColumn.IsSplit;
            this.Delimeter = configColumn.Delimiter;
            this.DataType = configColumn.DataType;
            this.IsQuantity = configColumn.IsQuantity;
            this.SplitInto = configColumn.SplitInto;
        }

        public BomDataColumn(string columnName, bool overrides, int position) : base(columnName) {
            Overrides = overrides;
            Position = position;
        }

        public BomDataColumn(string columnName, Type dataType, bool overrides, int position) : base(columnName, dataType) {
            Overrides = overrides;
            Position = position;
        }

        public BomDataColumn(string columnName, Type dataType, string expr, bool overrides, int position) : base(columnName, dataType, expr) {
            Overrides = overrides;
            Position = position;
        }

        public BomDataColumn(string columnName, Type dataType, string expr, MappingType type, bool overrides, int position) : base(columnName, dataType, expr, type) {
            Overrides = overrides;
            Position = position;
        }
    }
}