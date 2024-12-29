using System.Collections.Generic;

namespace LIL.Helpers
{
    internal static class Strings
    {
        public static string ToFirstCharUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public static bool AbsEquals(this string[] a, string[] b)
        {
            if (a.Length != b.Length)
                return false;

            bool allowed = true;
            for (int index = 0; index < a.Length; index++)
                allowed &= a[index] == b[index];

            return allowed;
        }
    }
}
