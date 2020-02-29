namespace ProcessTrackerBOMFormat.Utility {
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
    }
}
