using Formatter.Configuration;
using Formatter.Utility;
using System;

namespace Formatter.Processing
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem'
    public class CleanupItem : IComparable<CleanupItem>, IComparable
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Action'
        public ConfigurationCleanupActions.CleanupAction Action { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Action'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Scope'
        public ConfigurationCleanupActions.CleanupScope Scope { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Scope'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Categories'
        public string[] Categories { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Categories'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Condition'
        public StringEvaluation.StringEvalCondition Condition { get; } = StringEvaluation.StringEvalCondition.ANY;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Condition'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Message'
        public string Message { get; } = "";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Message'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Report'
        public bool Report { get; } = false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.Report'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.CleanupItem(ConfigurationCleanupActions.CleanupAction, ConfigurationCleanupActions.CleanupScope, StringEvaluation.StringEvalCondition, string, string[], bool)'
        public CleanupItem(ConfigurationCleanupActions.CleanupAction action, ConfigurationCleanupActions.CleanupScope scope, StringEvaluation.StringEvalCondition condition, string message, string[] categories, bool report)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.CleanupItem(ConfigurationCleanupActions.CleanupAction, ConfigurationCleanupActions.CleanupScope, StringEvaluation.StringEvalCondition, string, string[], bool)'
            this.Action = action;
            this.Condition = condition;
            this.Message = message;
            this.Scope = scope;
            this.Report = report;
            this.Categories = categories;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.CompareTo(CleanupItem)'
        public int CompareTo(CleanupItem other)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.CompareTo(CleanupItem)'
            if (other.Action > this.Action) return 1;
            else if (other.Action < this.Action) return -1;
            return 0;//this.Message.CompareTo(other.Message);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.CompareTo(object)'
        public int CompareTo(object obj)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItem.CompareTo(object)'
            if (obj.GetType() == typeof(CleanupItem))
                return CompareTo((CleanupItem)obj);
            return -1;
        }
    }
}
