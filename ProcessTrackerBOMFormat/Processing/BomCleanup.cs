using Formatter.Configuration;
using Formatter.Data;
using Formatter.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;

namespace Formatter.Processing {
    public class BomCleanup {

        private CleanupItemCollection _cleanups = new CleanupItemCollection();

        private Collection<ConfigurationElementColumn> _columnsWithCleanup = new Collection<ConfigurationElementColumn>();
        private SortedList<int, ConfigurationElementColumn> _columnsForIdentifier = new SortedList<int, ConfigurationElementColumn>(new DuplicateKeyComparer<int>());

        private BomPopulations _populatedOutput = null;
        private ConfigurationElementBom _bomConfig = null;

        public BomCleanup(ConfigurationElementBom bomConfig, BomPopulations populatedOutput) {
            _populatedOutput = populatedOutput;
            _bomConfig = bomConfig;

            foreach (ConfigurationElementColumn evaluateColumn in _bomConfig.ColumnCollection) {
                ConfigurationElementColumn value = null;
                foreach (BomDataColumn checkColumn in populatedOutput.PopulatedDataTable.Columns) {
                    if (evaluateColumn.Order == checkColumn.Ordinal && !checkColumn.ColumnName.Equals(evaluateColumn.Name)) {
                        value = new ConfigurationElementColumn();
                        value.Name = checkColumn.ColumnName;
                        value.Order = evaluateColumn.Order;
                        value.Output = checkColumn.OutputName;
                        value.Override = evaluateColumn.Override;
                        value.Required = evaluateColumn.Required;
                        value.PopulationCollection = evaluateColumn.PopulationCollection;
                        value.CleanupCollection = evaluateColumn.CleanupCollection;
                        value.DataType = evaluateColumn.DataType;
                        value.Delimiter = evaluateColumn.Delimiter;
                        value.Enabled = evaluateColumn.Enabled;
                        value.Header = checkColumn.HeaderName;
                        value.IdentifierOrder = evaluateColumn.IdentifierOrder;
                        value.IsQuantity = evaluateColumn.IsQuantity;
                        value.IsSplit = evaluateColumn.IsSplit;
                        break;
                    }
                }

                if (value == null) {
                    value = evaluateColumn;
                }

                if (value.CleanupCollection.Count > 0) _columnsWithCleanup.Add(value);
                if (value.IdentifierOrder != -1) _columnsForIdentifier.Add(value.IdentifierOrder, evaluateColumn);
            }

            PerformCleanup();
        }

        private void PerformCleanup() {
            for (int i = _populatedOutput.PopulatedDataTable.Rows.Count - 1; i >= 0; i--) {
                //foreach (DataRow row in _populatedOutput.PopulatedDataTable.Rows) {
                DataRow row = _populatedOutput.PopulatedDataTable.Rows[i];

                foreach (ConfigurationElementColumn column in _columnsWithCleanup) {
                    object cell = row[column.Name];
                    string cellVal = cell.ToString();
                    object originalCell = cell;

                    foreach (ConfigurationElementCleanUp cleanup in column.CleanupCollection) {
                        if (cleanup.Active && StringEvaluation.eval(cleanup.Condition, cellVal, cleanup.Value)) {
                            CleanupItem cleanupItem;

                            ConfigurationCleanupActions.CleanupActionType actionType = ConfigurationCleanupActions.GetCleanUpActionType(cleanup.Action);

                            string[] identifiers = GetRowIdentifier(row);

                            if (actionType == ConfigurationCleanupActions.CleanupActionType.REMOVAL) {
                                cleanupItem = new CleanupItemRemove(cleanup.Action, cleanup.Scope, cleanup.Condition, identifiers, column.Name, cellVal, cleanup.Report);
                            } else if (actionType == ConfigurationCleanupActions.CleanupActionType.MODIFICATION) {
                                cleanupItem = new CleanupItemUpdate(cleanup.Action, cleanup.Scope, cleanup.Condition, originalCell.ToString(), cellVal, identifiers, cleanup.Report);
                            } else if (actionType == ConfigurationCleanupActions.CleanupActionType.STATS) {
                                cleanupItem = new CleanupItem(cleanup.Action, cleanup.Scope, cleanup.Condition, "Value = " + cellVal, identifiers, cleanup.Report);
                            } else {
                                cleanupItem = new CleanupItem(cleanup.Action, cleanup.Scope, cleanup.Condition, "Unknown Cleanup, no cleanup performed", identifiers, cleanup.Report);
                            }

                            if (cleanup.Scope == ConfigurationCleanupActions.CleanupScope.ROW) {
                                ConfigurationCleanupActions.PerformCleanupAction(cleanup.Action, row);
                            } else {
                                row[column.Name] = ConfigurationCleanupActions.PerformCleanupAction(cleanup.Action, cell, column.DataType);
                            }

                            _cleanups.Add(cleanupItem);
                        }
                    }
                }
            }

            _populatedOutput.PopulatedDataTable.AcceptChanges();
        }

        private TreeViewItem GetSubItem(TreeViewItem item, string header) {
            foreach (TreeViewItem subItem in item.Items) {
                if (header.Equals(subItem.Header)) return subItem;
            }
            return null;
        }

        private TreeViewItem GetSubItem(Collection<TreeViewItem> items, string header) {
            foreach (TreeViewItem subItem in items) {
                if (header.Equals(subItem.Header)) return subItem;
            }
            return null;
        }

        private TreeViewItem CreateTreeViewItem(string header, bool expanded) {
            return new TreeViewItem() { Header = header, IsExpanded = expanded };
        }

        private void AddTreeViewSubItem(TreeViewItem item, TreeViewItem subItem) {
            if (subItem.Parent == null) item.Items.Add(subItem);
        }

        private void AddTreeViewSubItem(Collection<TreeViewItem> items, TreeViewItem subItem) {
            if (items.IndexOf(subItem) == -1) items.Add(subItem);
        }

        public Collection<TreeViewItem> OutputResults() {

            Collection<TreeViewItem> treeViewItems = new Collection<TreeViewItem>();

            foreach (CleanupItem cleanupItem in _cleanups) {

                string header = "Action: " + cleanupItem.Action.ToString();

                TreeViewItem treeItemForAction =
                    GetSubItem(treeViewItems, header) ??
                    CreateTreeViewItem(header, cleanupItem.Report);

                AddTreeViewSubItem(treeViewItems, treeItemForAction);

                header = "Scope: " + cleanupItem.Scope.ToString();

                TreeViewItem treeItemForScope =
                    GetSubItem(treeItemForAction, header) ??
                    CreateTreeViewItem(header, cleanupItem.Report);

                AddTreeViewSubItem(treeItemForAction, treeItemForScope);

                header = "Condition: " + cleanupItem.Condition.ToString();

                TreeViewItem treeItemForPreMessage =
                    GetSubItem(treeItemForScope, header) ??
                    CreateTreeViewItem(header, cleanupItem.Report);

                AddTreeViewSubItem(treeItemForScope, treeItemForPreMessage);

                TreeViewItem treeItemForPreviousValue = null;

                for (int i = 0; i < cleanupItem.Categories.Length; i++) {
                    string value = cleanupItem.Categories[i];
                    bool expand = i == cleanupItem.Categories.Length - 1 ? false : true;
                    TreeViewItem treeItemForValue = null;

                    if (treeItemForPreviousValue == null) {
                        treeItemForValue =
                            GetSubItem(treeItemForPreMessage, value) ??
                            CreateTreeViewItem(value, expand);

                        AddTreeViewSubItem(treeItemForPreMessage, treeItemForValue);

                    } else {
                        treeItemForValue =
                            GetSubItem(treeItemForPreviousValue, value) ??
                            CreateTreeViewItem(value, expand);

                        AddTreeViewSubItem(treeItemForPreviousValue, treeItemForValue);
                    }

                    treeItemForPreviousValue = treeItemForValue;
                }

                if (cleanupItem.Message.Length != 0) {
                    TreeViewItem treeItemForPostMessage =
                                GetSubItem(treeItemForPreviousValue, cleanupItem.Message) ??
                                CreateTreeViewItem(cleanupItem.Message, false);

                    AddTreeViewSubItem(treeItemForPreviousValue, treeItemForPostMessage);
                }
            }

            return treeViewItems;
        }

        private string[] GetRowIdentifier(DataRow row) {

            Collection<string> identifiers = new Collection<string>();

            ConfigurationElementColumn previousColumn = null;

            foreach (KeyValuePair<int, ConfigurationElementColumn> column in _columnsForIdentifier) {

                if (previousColumn != null && previousColumn.IdentifierOrder == column.Value.IdentifierOrder) {
                    string delimiter = identifiers[identifiers.Count - 1].Trim().Length == 0 ? "" : " - ";
                    identifiers[identifiers.Count - 1] = row[column.Value.Name].ToString() + delimiter + identifiers[identifiers.Count - 1];
                } else {
                    string value = row[column.Value.Name].ToString();
                    identifiers.Add(value);
                }

                previousColumn = column.Value;
            }

            for (int i = 0; i < identifiers.Count; i++) {
                string value = identifiers[i];
                if (value.Replace("-", "").Trim().Length == 0) identifiers[i] = "(BLANK)";
            }

            string[] identifiersReturn = new string[identifiers.Count];

            identifiers.CopyTo(identifiersReturn, 0);

            return identifiersReturn;
        }
    }
}