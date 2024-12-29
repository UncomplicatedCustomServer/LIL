using System;
using System.Linq;

namespace LIL.Helpers
{
    internal static class Numbers
    {
        private static readonly Type[] numericTypes = [
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double), 
            typeof(decimal)
        ];

        public static bool IsNumber(this Type type) => numericTypes.Contains(type);

        public static bool IsMultiplier(this int number, uint @base)
        {
            if (@base == 0)
                return false;

            return !(number / @base).ToString().Contains(".");
        }
    }
}
