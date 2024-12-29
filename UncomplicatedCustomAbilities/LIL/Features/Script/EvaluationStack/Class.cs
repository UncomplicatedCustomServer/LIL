using System;
using UncomplicatedCustomAbilities.LIL.Enums;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack
{
    internal class Class(Type type, object instance, Script script) : StackMember(script, type)
    {
        public override StackMemberType Type => StackMemberType.Class;

        public readonly new Type RefType = type;

        public readonly object Instance = instance;

        public override bool IsQuantifiable => false;

        public bool IsStatic => RefType is not null && Instance is null;

        public override object Evaluate(Type requiredType = null) => Instance;
    }
}
