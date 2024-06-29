using System;

namespace Nemuge.Core {
    public static class StringUtility {
        public static string SubstringBetweenStrings(string fullString, string startString, string endString) {
            var startIndex = fullString.IndexOf(startString, StringComparison.Ordinal);
            var endIndex = fullString.IndexOf(endString, startIndex, StringComparison.Ordinal);
            var resultIndex = (startIndex + startString.Length);
            var resultLength = endIndex - resultIndex;
            return fullString.Substring(resultIndex, resultLength);
        }
    }
}