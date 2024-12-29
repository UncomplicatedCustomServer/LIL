using System;
using System.Linq;
using UncomplicatedCustomAbilities.LIL.Enums;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack
{
    internal class String(string str, Script script) : StackMember(script, typeof(string))
    {
        public override StackMemberType Type => StackMemberType.String;

        public readonly string Content = str;

        public override bool IsQuantifiable => true;

        public override Type[] CanBeConvertedTo => [typeof(Number), typeof(Boolean)];

        public override StackMember ConvertTo(Type newType)
        {
            if (!CanBeConvertedTo.Contains(newType))
                return null;

            if (newType == typeof(Number))
                return new Number(Content, Script);
            else if (newType == typeof(Boolean))
                return new Boolean(bool.Parse(Content), Script);

            return null;
        }

        public override object Evaluate(Type requiredType = null) => Content;

        public override decimal Quantify() => Convert.ToDecimal(Content.Length);
    }
}
