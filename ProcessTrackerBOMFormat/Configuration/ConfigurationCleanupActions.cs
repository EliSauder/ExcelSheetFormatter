using System;
using System.Data;

namespace Formatter.Configuration {

    public class ConfigurationCleanupActions {

        public enum CleanupActionType {
            REMOVAL,
            MODIFICATION,
            STATS,
            NULL
        }

        public enum CleanupAction {
            REMOVE,
            UPPERCASE,
            LOWERCASE,
            REPORT
        }

        public enum CleanupScope {
            ROW,
            CELL
        }

        public static CleanupActionType GetCleanUpActionType(CleanupAction action) {
            switch(action) {
                case CleanupAction.LOWERCASE: return CleanupActionType.MODIFICATION;
                case CleanupAction.UPPERCASE: return CleanupActionType.MODIFICATION;
                case CleanupAction.REMOVE: return CleanupActionType.REMOVAL;
                case CleanupAction.REPORT: return CleanupActionType.STATS;
                default: return CleanupActionType.NULL;
            }
        }

        public static void PerformCleanupAction(CleanupAction action, DataRow input) {
            switch(action) {
                case CleanupAction.LOWERCASE: 
                    foreach(DataColumn column in input.Table.Columns) {
                        if (column.DataType == typeof(string))
                            input[column] = input[column].ToString().ToLower();
                    }
                    break;
                case CleanupAction.UPPERCASE:
                    foreach (DataColumn column in input.Table.Columns) {
                        if (column.DataType == typeof(string))
                            input[column] = input[column].ToString().ToUpper();
                    }
                    break;
                case CleanupAction.REMOVE:
                    input.Delete();
                    break;
                default: break;
            }
        }

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
