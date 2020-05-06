using System;
using System.Data;

namespace Formatter.Configuration
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions'
    public class ConfigurationCleanupActions
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupActionType'
        public enum CleanupActionType
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupActionType'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupActionType.REMOVAL'
            REMOVAL,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupActionType.REMOVAL'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupActionType.MODIFICATION'
            MODIFICATION,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupActionType.MODIFICATION'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupActionType.STATS'
            STATS,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupActionType.STATS'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupActionType.NULL'
            NULL
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupActionType.NULL'
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupAction'
        public enum CleanupAction
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupAction'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupAction.REMOVE'
            REMOVE,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupAction.REMOVE'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupAction.UPPERCASE'
            UPPERCASE,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupAction.UPPERCASE'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupAction.LOWERCASE'
            LOWERCASE,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupAction.LOWERCASE'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupAction.REPORT'
            REPORT
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupAction.REPORT'
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupScope'
        public enum CleanupScope
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupScope'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupScope.ROW'
            ROW,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupScope.ROW'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupScope.CELL'
            CELL
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.CleanupScope.CELL'
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.GetCleanUpActionType(ConfigurationCleanupActions.CleanupAction)'
        public static CleanupActionType GetCleanUpActionType(CleanupAction action)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.GetCleanUpActionType(ConfigurationCleanupActions.CleanupAction)'
            switch (action)
            {
                case CleanupAction.LOWERCASE: return CleanupActionType.MODIFICATION;
                case CleanupAction.UPPERCASE: return CleanupActionType.MODIFICATION;
                case CleanupAction.REMOVE: return CleanupActionType.REMOVAL;
                case CleanupAction.REPORT: return CleanupActionType.STATS;
                default: return CleanupActionType.NULL;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.PerformCleanupAction(ConfigurationCleanupActions.CleanupAction, DataRow)'
        public static void PerformCleanupAction(CleanupAction action, DataRow input)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.PerformCleanupAction(ConfigurationCleanupActions.CleanupAction, DataRow)'
            switch (action)
            {
                case CleanupAction.LOWERCASE:
                    foreach (DataColumn column in input.Table.Columns)
                    {
                        if (column.DataType == typeof(string))
                            input[column] = input[column].ToString().ToLower();
                    }
                    break;
                case CleanupAction.UPPERCASE:
                    foreach (DataColumn column in input.Table.Columns)
                    {
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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.PerformCleanupAction(ConfigurationCleanupActions.CleanupAction, object, Type)'
        public static object PerformCleanupAction(CleanupAction action, object input, Type dataType)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ConfigurationCleanupActions.PerformCleanupAction(ConfigurationCleanupActions.CleanupAction, object, Type)'
            switch (action)
            {
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
