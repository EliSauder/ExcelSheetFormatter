using Formatter.Configuration;
using Formatter.Utility;
using System;
using System.Text;

namespace Formatter.Processing {
    public class CleanupItem : IComparable<CleanupItem>, IComparable {
        public ConfigurationCleanupActions.CleanupAction Action { get; }
        public ConfigurationCleanupActions.CleanupScope Scope { get; }

        public string[] Categories { get; }
        public StringEvaluation.StringEvalCondition Condition { get; } = StringEvaluation.StringEvalCondition.ANY;
        public string Message { get; } = "";
        public bool Report { get; } = false;

        public CleanupItem(ConfigurationCleanupActions.CleanupAction action, ConfigurationCleanupActions.CleanupScope scope, StringEvaluation.StringEvalCondition condition, string message, string[] categories, bool report) {
            this.Action = action;
            this.Condition = condition;
            this.Message = message;
            this.Scope = scope;
            this.Report = report;
            this.Categories = categories;
        }

        public int CompareTo(CleanupItem other) {
            if (other.Action > this.Action) return 1;
            else if (other.Action < this.Action) return -1;
            return 0;//this.Message.CompareTo(other.Message);
        }

        public int CompareTo(object obj) {
            if (obj.GetType() == typeof(CleanupItem))
                return CompareTo((CleanupItem)obj);
            return -1;
        }
    }
}
