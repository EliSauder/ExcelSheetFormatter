using Formatter.Configuration;
using Formatter.Utility;

namespace Formatter.Processing
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemRemove'
    public class CleanupItemRemove : CleanupItem
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemRemove'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemRemove.CleanupItemRemove(ConfigurationCleanupActions.CleanupAction, ConfigurationCleanupActions.CleanupScope, StringEvaluation.StringEvalCondition, string[], string, string, bool)'
        public CleanupItemRemove(ConfigurationCleanupActions.CleanupAction action, ConfigurationCleanupActions.CleanupScope scope, StringEvaluation.StringEvalCondition condition, string[] identifiers, string valueCategory, string triggerValue, bool report) :
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CleanupItemRemove.CleanupItemRemove(ConfigurationCleanupActions.CleanupAction, ConfigurationCleanupActions.CleanupScope, StringEvaluation.StringEvalCondition, string[], string, string, bool)'
            base(action, scope, condition, "Removed because value = " + triggerValue + " in column" + valueCategory, identifiers, report)
        {
        }
    }
}
