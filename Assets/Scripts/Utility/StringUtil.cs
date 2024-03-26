using UnityEngine;

namespace Utility
{
    public static class StringUtil
    {
        public static string GetSubstringAfterLast(string originalString, char separator)
        {
            // Check if the originalString is not null and contains the separator
            if (!string.IsNullOrEmpty(originalString) && originalString.Contains(separator))
            {
                // Use LastIndexOf to find the last occurrence of the separator
                int lastIndexOfSeparator = originalString.LastIndexOf(separator);

                // Use Substring to get the portion after the last occurrence
                return originalString.Substring(lastIndexOfSeparator + 1);
            }
            else
            {
                return "00";
            }
        }
    }
}