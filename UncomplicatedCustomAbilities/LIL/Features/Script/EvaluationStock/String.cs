using System;
using UncomplicatedCustomAbilities.LIL.Enums;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack
{
    internal class String(string str, Script script) : StackMember(script, typeof(string))
    {
        public override StackMemberType Type => StackMemberType.String;

        public readonly string Content = str;

        public override object Evaluate(Type requiredType = null) => Content;
    }
}
