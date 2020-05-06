using Formatter.Configuration;
using System;
using System.Data;

namespace Formatter.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn'
    public class BomDataColumn : DataColumn
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.Overrides'
        public bool Overrides { get; set; } = false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.Overrides'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.Position'
        public int Position { get; set; } = 0;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.Position'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.OutputName'
        public string OutputName { get; set; } = "";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.OutputName'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.HeaderName'
        public string HeaderName { get; set; } = "";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.HeaderName'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.IsSplit'
        public bool IsSplit { get; set; } = false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.IsSplit'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.Delimeter'
        public string Delimeter { get; set; } = "";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.Delimeter'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.IsQuantity'
        public bool IsQuantity { get; set; } = false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.IsQuantity'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn()'
        public BomDataColumn() : base() { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn()'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn(ConfigurationElementColumn)'
        public BomDataColumn(ConfigurationElementColumn configColumn) : this(configColumn.Output.Length == 0 ? configColumn.Header : configColumn.Output, configColumn.DataType, configColumn.Override, configColumn.Order)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn(ConfigurationElementColumn)'
            this.ColumnName = configColumn.Name;
            this.OutputName = configColumn.Output.Length == 0 ? configColumn.Header : configColumn.Output;
            this.HeaderName = configColumn.Header;
            this.IsSplit = configColumn.IsSplit;
            this.Delimeter = configColumn.Delimiter;
            this.DataType = configColumn.DataType;
            this.IsQuantity = configColumn.IsQuantity;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn(string, bool, int)'
        public BomDataColumn(string columnName, bool overrides, int position) : base(columnName)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn(string, bool, int)'
            Overrides = overrides;
            Position = position;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn(string, Type, bool, int)'
        public BomDataColumn(string columnName, Type dataType, bool overrides, int position) : base(columnName, dataType)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn(string, Type, bool, int)'
            Overrides = overrides;
            Position = position;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn(string, Type, string, bool, int)'
        public BomDataColumn(string columnName, Type dataType, string expr, bool overrides, int position) : base(columnName, dataType, expr)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn(string, Type, string, bool, int)'
            Overrides = overrides;
            Position = position;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn(string, Type, string, MappingType, bool, int)'
        public BomDataColumn(string columnName, Type dataType, string expr, MappingType type, bool overrides, int position) : base(columnName, dataType, expr, type)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomDataColumn.BomDataColumn(string, Type, string, MappingType, bool, int)'
            Overrides = overrides;
            Position = position;
        }
    }
}
