using System;
using UncomplicatedCustomAbilities.LIL.Enums;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack
{
    internal class Operator(OperatorType type, Script script) : StackMember(script)
    {
        public override StackMemberType Type => StackMemberType.Operator;

        public readonly OperatorType RefOperator = type;

        public override bool IsQuantifiable => false;

        public override object Evaluate(Type requiredType = null) => null;
    }
}
