using System;
using System.Linq;
using LIL.Enums;

namespace LIL.Features.Script.EvaluationStack
{
    internal class Boolean(bool status, Script script) : StackMember(script, typeof(bool))
    {
        public override StackMemberType Type => StackMemberType.Boolean;

        public readonly bool Content = status;

        public override bool IsQuantifiable => false;

        public override Type[] CanBeConvertedTo => [typeof(String)];

        public override StackMember ConvertTo(Type newType)
        {
            if (!CanBeConvertedTo.Contains(newType))
                return null;

            if (newType == typeof(String))
                return new String(Content ? "true" : "false", Script);

            return null;
        }

        public override object Evaluate(Type requiredType = null) => Content;
    }
}
