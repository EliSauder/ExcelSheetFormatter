﻿using Formatter.Configuration;
using Formatter.Data;
using Formatter.Utility;
using System;
using System.Data;

namespace Formatter.Processing
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomLoad'
    public class BomLoad
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomLoad'

        private ConfigurationElementBom _bomConfig = null;
        private BomOutputType _outputType = BomOutputType.INDIVIDUAL;
        private BomInput _input = null;
        private BomOutput _output = null;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomLoad.InputBom'
        public BomInput InputBom { get { return _input; } }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomLoad.InputBom'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomLoad.OutputBom'
        public BomOutput OutputBom { get { return _output; } }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomLoad.OutputBom'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomLoad.OutputType'
        public BomOutputType OutputType { get { return _outputType; } }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomLoad.OutputType'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomLoad.BomLoad(ConfigurationElementBom, BomOutputType, BomInput, BomOutput)'
        public BomLoad(ConfigurationElementBom bomConfig, BomOutputType outputType, BomInput input, BomOutput output)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomLoad.BomLoad(ConfigurationElementBom, BomOutputType, BomInput, BomOutput)'
            _bomConfig = bomConfig;
            _outputType = outputType;
            _input = input;
            _output = output;

            ValidateInput();
            LoadInput();
        }

        private void ValidateInput()
        {

            foreach (ConfigurationElementColumn columnConfig in _bomConfig.ColumnCollection)
            {
                if (columnConfig.Enabled && columnConfig.Required && !InputColumnExists(columnConfig))
                {
                    throw new ConfigurationElementColumnException("Required column " + columnConfig.Header + " not found.");
                }
            }
        }

        private bool InputColumnExists(ConfigurationElementColumn column)
        {
            int index = _input.InputData.Columns.IndexOf(column.Header);

            if (index == -1) return false;

            return _input.InputData.Columns[index].ColumnName.Equals(column.Header);
        }

        private int DataColumnIndex(DataColumn column)
        {
            return _input.InputData.Columns.IndexOf(column);
        }

        private ConfigurationElementColumn GetConfigColumn(DataColumn column)
        {
            foreach (ConfigurationElementColumn configColumn in _bomConfig.ColumnCollection)
            {
                if (configColumn.Header.Equals(column.ColumnName)) return configColumn;
            }
            return null;
        }

        private void LoadInput()
        {
            foreach (DataRow row in _input.InputData.Rows)
            {
                BomDataRowHolder newRow = LoadRow(row);

                if (_outputType == BomOutputType.INDIVIDUAL)
                {
                    _output.BomDataTable.AddRowCollection(newRow.ExpandRow());
                }
                else
                {
                    _output.BomDataTable.AddRow(newRow);
                }
            }
        }

        private BomDataRowHolder LoadRow(DataRow row)
        {
            BomDataRowHolder newRow = new BomDataRowHolder(_output.TemplateRow);

            foreach (DataColumn column in row.Table.Columns)
            {
                ConfigurationElementColumn columnConfig = GetConfigColumn(column);
                if (columnConfig != null)
                {
                    //bool valueSet = false;

                    foreach (BomDataCell cell in newRow)
                    {
                        if (cell.Column.Position == columnConfig.Order /*&& cell.Column.ColumnName.Equals(columnConfig.Name)*/)
                        {
                            object rowValue = row.ItemArray[DataColumnIndex(column)];
                            rowValue = rowValue.Equals(DBNull.Value) ? null : rowValue;

                            if ((columnConfig.Override && rowValue != null) || cell.Value == null) cell.Value = rowValue;
                            //valueSet = true;
                        }
                    }

                    //if (!valueSet) 
                    //    newRow.Add(new BomDataCell(
                    //        newRow,
                    //        _output.BomDataTable.GetColumn(columnConfig.Name),
                    //        row.ItemArray[DataColumnIndex(column)]));

                    //newRow.Add(new BomDataCell(
                    //    newRow,
                    //    _output.BomDataTable.GetColumn(columnConfig.Name), 
                    //    row.ItemArray[DataColumnIndex(column)]));
                }
                else if (columnConfig != null && columnConfig.Enabled && columnConfig.Required)
                {
                    throw new ConfigurationElementColumnException("Required column " + columnConfig.Header + " not found.");
                }
            }

            return newRow;
        }


    }
}
