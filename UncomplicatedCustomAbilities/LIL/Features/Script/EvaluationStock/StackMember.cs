using System;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Helpers;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack
{
    public abstract class StackMember(Script script, Type type = null)
    {
        public abstract StackMemberType Type { get; }

        public readonly Script Script = script;

        public readonly Type RefType = type;

        public abstract object Evaluate(Type requiredType = null);

        public static StackMember CreateFromGenericType(Type type, object raw, Script script)
        {
            if (type.IsClass)
                return new Class(type, raw, script);
            else if (type.IsNumber())
                return new Number(raw.ToString(), script, type);
            else if (type == typeof(string))
                return new String(raw.ToString(), script);
            else if (type == typeof(bool))
                return new EvaluationStock.Boolean(bool.Parse(raw.ToString()), script);
            else
                return new Generic(raw, script);

        }
    }
}
