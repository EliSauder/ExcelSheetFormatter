using System;
using System.Text.RegularExpressions;

namespace Formatter.Utility {
    /// <summary>
    /// Class <c>StringEvaluation</c> is resposible for evaluating string comparisions.
    /// </summary>
    public static class StringEvaluation {

        /// <summary>
        /// Enum <c>StringEvalCondition</c> are the different evaluations that can be performed on the strings.
        /// </summary>
        public enum StringEvalCondition {
            /// <summary>
            /// One string begins with another.
            /// </summary>
            BEGINS_WITH,
            /// <summary>
            /// One string ends with another.
            /// </summary>
            ENDS_WITH,
            /// <summary>
            /// One string equals another.
            /// </summary>
            EQUALS,
            /// <summary>
            /// One string contains another.
            /// </summary>
            CONTAINS,
            ANY,
            MATCHES,
            ISNUMBER,
            ISINT,
            ISFLOAT,
            ISEMPTY
        }

        /// <summary>
        /// Performs the actual string evaluation based on the information provided.
        /// </summary>
        /// <param name="condition">The condition that will be evaluated.</param>
        /// <param name="input">The input string, this will be the string the other is compared against.</param>
        /// <param name="lookFor">The string that will be compared to the <c>input</c> string based on the <c>condition</c>.</param>
        /// <returns>True if the condition is met with the two strings and False if the condition is not met.</returns>
        public static bool eval(StringEvalCondition condition, string input, string lookFor) {
            switch (condition) {
                case StringEvalCondition.ISEMPTY:
                    return input.Length == 0;
                case StringEvalCondition.BEGINS_WITH:
                    return input.StartsWith(lookFor);
                case StringEvalCondition.ENDS_WITH:
                    return input.EndsWith(lookFor);
                case StringEvalCondition.EQUALS:
                    return input.Equals(lookFor);
                case StringEvalCondition.CONTAINS:
                    return input.Contains(lookFor);
                case StringEvalCondition.MATCHES:
                    try {
                        bool value = (new Regex(lookFor)).IsMatch(input);
                        return value;
                    } catch(Exception e) {
                        throw new Exception("Issue encountered when using regex for match of " + input + ", check config.\n" + "Original Error: " + e.Message);
                    } 
                case StringEvalCondition.ANY:
                    return true;
                default:
                    return eval(condition, input);
            }
        }

        public static bool eval(StringEvalCondition condition, string input) {
            switch (condition) {
                case StringEvalCondition.ISNUMBER:
                    return double.TryParse(input, out _);
                case StringEvalCondition.ISINT:
                    return int.TryParse(input, out _);
                case StringEvalCondition.ISFLOAT:
                    double.TryParse(input, out double outValue);
                    return outValue != (int)outValue;
                default:
                    return false;
            }
        }
    }
}
