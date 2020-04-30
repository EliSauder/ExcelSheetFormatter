using Formatter.Configuration;
using Formatter.Utility;

namespace Formatter.Processing {
    public class CleanupItemRemove : CleanupItem {
        public CleanupItemRemove(ConfigurationCleanupActions.CleanupAction action, ConfigurationCleanupActions.CleanupScope scope, StringEvaluation.StringEvalCondition condition, string[] identifiers, string valueCategory, string triggerValue, bool report) : 
            base(action, scope, condition, "Removed because value = " + triggerValue + " in column" + valueCategory, identifiers, report) {
        }
    }
}
