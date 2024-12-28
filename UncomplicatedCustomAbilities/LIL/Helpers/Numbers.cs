using System;
using System.Linq;

namespace UncomplicatedCustomAbilities.LIL.Helpers
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
    }
}
