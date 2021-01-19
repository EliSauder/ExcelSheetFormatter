using Formatter.Configuration;
using Formatter.Utility;

namespace Formatter.Processing
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemUpdate'
    public class CleanupItemUpdate : CleanupItem
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemUpdate'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemUpdate.CleanupItemUpdate(ConfigurationCleanupActions.CleanupAction, ConfigurationCleanupActions.CleanupScope, StringEvaluation.StringEvalCondition, string, string, string[], bool)'
        public CleanupItemUpdate(ConfigurationCleanupActions.CleanupAction action, ConfigurationCleanupActions.CleanupScope scope, StringEvaluation.StringEvalCondition condition, string originalValue, string updatedValue, string[] identifiers, bool report) :
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemUpdate.CleanupItemUpdate(ConfigurationCleanupActions.CleanupAction, ConfigurationCleanupActions.CleanupScope, StringEvaluation.StringEvalCondition, string, string, string[], bool)'
            base(action, scope, condition, "Updated " + originalValue + " -> " + updatedValue, identifiers, report)
        {
        }
    }
}
