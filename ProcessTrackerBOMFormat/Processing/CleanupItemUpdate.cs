using Formatter.Configuration;
using Formatter.Utility;

namespace Formatter.Processing {
    public class CleanupItemUpdate : CleanupItem {
        public CleanupItemUpdate(ConfigurationCleanupActions.CleanupAction action, ConfigurationCleanupActions.CleanupScope scope, StringEvaluation.StringEvalCondition condition, string originalValue, string updatedValue, string[] identifiers, bool report) : 
            base(action, scope, condition, "Updated " + originalValue + " -> " + updatedValue, identifiers, report) {
        }
    }
}
