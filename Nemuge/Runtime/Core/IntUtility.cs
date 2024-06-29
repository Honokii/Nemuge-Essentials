namespace Nemuge.Core {
    public static class IntUtility {
        public static int IntFromString(string intString, int defaultVal) {
            var success = int.TryParse(intString, out var result);
            return !success ? defaultVal : result;
        }
    }
}