using System;
using UncomplicatedCustomAbilities.LIL.Enums;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack
{
    internal class Generic(object content, Script script, Type type = null) : StackMember(script, type)
    {
        public override StackMemberType Type => StackMemberType.Generic;

        public readonly object Content = content;

        public override object Evaluate(Type requiredType = null) => Content;
    }
}
