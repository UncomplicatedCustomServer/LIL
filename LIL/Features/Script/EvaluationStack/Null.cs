using System;
using LIL.Enums;

namespace LIL.Features.Script.EvaluationStack
{
    internal class Null(Script script) : StackMember(script)
    {
        public override bool IsQuantifiable => false;

        public override StackMemberType Type => StackMemberType.Null;

        public override object Evaluate(Type requiredType = null) => null;
    }
}
