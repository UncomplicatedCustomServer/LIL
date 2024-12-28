using System.Collections.Generic;

namespace UncomplicatedCustomAbilities.LIL.Helpers
{
    internal static class Strings
    {
        public static string ToFirstCharUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
