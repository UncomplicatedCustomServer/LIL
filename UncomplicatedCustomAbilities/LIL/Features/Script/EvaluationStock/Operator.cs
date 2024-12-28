using System;
using UncomplicatedCustomAbilities.LIL.Enums;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack
{
    internal class Operator(Script script) : StackMember(script)
    {
        public override StackMemberType Type => StackMemberType.Operator;

        public override object Evaluate(Type requiredType = null) => null;
    }
}
