using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTrackerBOMFormat.Configurations {
    public static class StringEvaluation {

        public enum StringEvalCondition {
            BEGINS_WITH,
            ENDS_WITH,
            EQUALS,
            CONTAINS
        }

        public static bool eval(StringEvalCondition condition, string input, string lookFor) {
            switch (condition) {
                case StringEvalCondition.BEGINS_WITH:
                    return input.StartsWith(lookFor);
                case StringEvalCondition.ENDS_WITH:
                    return input.EndsWith(lookFor);
                case StringEvalCondition.EQUALS:
                    return input.Equals(lookFor);
                case StringEvalCondition.CONTAINS:
                    return input.Contains(lookFor);
                default:
                    return false;
            }
        }

        public static StringEvalCondition getCondition(string condition) {
            foreach(StringEvalCondition evalCondition in (StringEvalCondition[]) Enum.GetValues(typeof(StringEvalCondition))) {
                if (evalCondition.ToString().Equals(condition.ToUpper())) return evalCondition;
            }
            return StringEvalCondition.EQUALS;
        }
    }
}
