using System;
using LIL.Enums;
using LIL.Helpers;

namespace LIL.Features.Script.EvaluationStack
{
    public abstract class StackMember(Script script, Type type = null)
    {
        public abstract StackMemberType Type { get; }

        public readonly Script Script = script;

        public readonly Type RefType = type;

        public abstract bool IsQuantifiable { get; }

        public virtual Type[] CanBeConvertedTo { get; } = [];

        public abstract object Evaluate(Type requiredType = null);

        public static StackMember CreateFromGenericType(Type type, object raw, Script script)
        {
            if (type == typeof(string))
                return new String(raw.ToString(), script);
            else if (type == typeof(bool))
                return new Boolean(bool.Parse(raw.ToString()), script);
            else if (type.IsNumber())
                return new Number(raw.ToString(), script, type);
            else if (type.IsClass)
                return new Class(type, raw, script);
            else
                return new Generic(raw, script);

        }

        public virtual decimal Quantify() => 0;

        public virtual StackMember ConvertTo(Type newType) => throw new NotImplementedException();
    }
}
