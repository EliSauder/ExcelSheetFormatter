using System;

namespace Formatter.Utility
{
    public class ErrorFormating
    {

        public static string FormatException(Exception e)
        {
            int indexOfParan = e.Message.IndexOf("(");
            int messageEnd = indexOfParan == -1 ? e.Message.Length : indexOfParan;
            int indexOfLine = e.Message.IndexOf("line");

            string lineNumber = indexOfLine == -1 ? "" : e.Message.Substring(indexOfLine);
            string lineError = indexOfLine == -1 ? "" : lineNumber.Substring(0, lineNumber.Length - 1);

            return e.Message.Substring(0, messageEnd) + (indexOfLine == -1 ? "" : "\n\nOn Line: ") + lineError;
        }

    }
}
