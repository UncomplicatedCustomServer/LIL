using System;
using System.Linq;
using UncomplicatedCustomAbilities.LIL.Enums;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack
{
    internal class Number(string num, Script script, Type type = null) : StackMember(script, type)
    {
        public override StackMemberType Type => StackMemberType.Number;

        public readonly string Content = num;

        public override bool IsQuantifiable => true;

        public override Type[] CanBeConvertedTo => [typeof(String)];

        public override StackMember ConvertTo(Type newType)
        {
            if (!CanBeConvertedTo.Contains(newType))
                return null;

            if (newType == typeof(String))
                return new String(Content, Script);

            return null;
        }

        public override object Evaluate(Type requiredType = null)
        {
            if (requiredType is null)
                return TryCast();
            return Convert.ChangeType(Content, requiredType);
        }

        public object TryCast()
        {
            if (int.TryParse(Content, out int value))
                return value;
            if (float.TryParse(Content, out float value2))
                return value2;
            if (long.TryParse(Content, out long value3))
                return value3;
            if (ulong.TryParse(Content, out ulong value4))
                return value4;
            if (double.TryParse(Content, out double value5))
                return value5;
            if (decimal.TryParse(Content, out decimal value6))
                return value6;
            if (byte.TryParse(Content, out byte value7))
                return value7;
            return 0;
        }

        public override decimal Quantify() => (decimal)Evaluate(typeof(decimal));
    }
}
