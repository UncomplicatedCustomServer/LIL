using System;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStock
{
    internal class Boolean(bool status, Script script) : StackMember(script, typeof(bool))
    {
        public override StackMemberType Type => StackMemberType.Boolean;

        public readonly bool Content = status;

        public override object Evaluate(Type requiredType = null) => Content;
    }
}
