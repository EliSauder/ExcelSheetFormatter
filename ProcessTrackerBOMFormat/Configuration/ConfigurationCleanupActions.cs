using System;
using System.Data;

namespace Formatter.Configuration {

    /// <summary>
    /// List of actions to perform after loading the data.
    /// </summary>
    public class ConfigurationCleanupActions {

        /// <summary>
        /// The different types of actions that can be performed.
        /// </summary>
        public enum CleanupActionType {
            /// <summary>
            /// Remove some value.
            /// </summary>
            REMOVAL,
            /// <summary>
            /// Modify some value.
            /// </summary>
            MODIFICATION,
            /// <summary>
            /// Provide a status report of some value.
            /// </summary>
            STATS,
            /// <summary>
            /// No action performed.
            /// </summary>
            NULL
        }

        /// <summary>
        /// The individual actions that can be performed on the data.
        /// </summary>
        public enum CleanupAction {
            /// <summary>
            /// Remove a value.
            /// </summary>
            REMOVE,
            /// <summary>
            /// Transform the text into uppercase.
            /// </summary>
            UPPERCASE,
            /// <summary>
            /// Transform the text into lowercase.
            /// </summary>
            LOWERCASE,
            /// <summary>
            /// Report out on the data.
            /// </summary>
            REPORT
        }

        /// <summary>
        /// The scope for performing the action, how wide reaching is the action.
        /// </summary>
        public enum CleanupScope {
            /// <summary>
            /// The change is applied to the entire row.
            /// </summary>
            ROW,
            /// <summary>
            /// The change is applied only to the individual cell (column in the current row).
            /// </summary>
            CELL
        }

        /// <summary>
        /// Get the action type from the action provided.
        /// </summary>
        /// <param name="action">The action being performed</param>
        /// <returns>The type of the action.</returns>
        public static CleanupActionType GetCleanUpActionType(CleanupAction action) {
            switch (action) {
                case CleanupAction.LOWERCASE: return CleanupActionType.MODIFICATION;
                case CleanupAction.UPPERCASE: return CleanupActionType.MODIFICATION;
                case CleanupAction.REMOVE: return CleanupActionType.REMOVAL;
                case CleanupAction.REPORT: return CleanupActionType.STATS;
                default: return CleanupActionType.NULL;
            }
        }

        /// <summary>
        /// Performs the clean up action on the provided datarow.
        /// </summary>
        /// <param name="action">The action to perform</param>
        /// <param name="input">The datarow to perform the action on.</param>
        public static object PerformCleanupAction(CleanupAction action, DataRow input) {
            switch (action) {
                case CleanupAction.LOWERCASE:
                    foreach (DataColumn column in input.Table.Columns) {
                        if (column.DataType == typeof(string))
                            input[column] = input[column].ToString().ToLower();
                    }
                    return null;
                case CleanupAction.UPPERCASE:
                    foreach (DataColumn column in input.Table.Columns) {
                        if (column.DataType == typeof(string))
                            input[column] = input[column].ToString().ToUpper();
                    }
                    return null;
                case CleanupAction.REMOVE:
                    input.Delete();
                    return null;
                case CleanupAction.REPORT:
                    string value = "";
                    foreach(DataColumn column in input.Table.Columns) {
                        value += input[column].ToString();
                    }
                    return value;
                default: return null;
            }
        }

        /// <summary>
        /// Performs the clean up action on some sort of data value (for performing actions on the cell)
        /// </summary>
        /// <param name="action">The action to perform</param>
        /// <param name="input">The input to perform the action on</param>
        /// <param name="dataType">The data type of the input</param>
        /// <returns>The input value with the changes applied to it.</returns>
        public static object PerformCleanupAction(CleanupAction action, object input, Type dataType) {
            switch (action) {
                case CleanupAction.LOWERCASE:
                    return input.GetType() == typeof(string) ? ((string)input).ToLower() : input;
                case CleanupAction.UPPERCASE:
                    return input.GetType() == typeof(string) ? ((string)input).ToUpper() : input;
                case CleanupAction.REPORT: return input;
                case CleanupAction.REMOVE:
                default: return Activator.CreateInstance(dataType);
            }
        }
    }
}