using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LIL.Features.Script;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Results;

namespace LIL.Helpers
{
    internal static class Converter
    {
        public static readonly Dictionary<string, Type> StackTypes = [];

        public static void Init()
        {
            if (StackTypes.Count > 0)
                return;

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsClass && !type.IsAbstract && typeof(StackMember).IsAssignableFrom(type)))
                StackTypes.Add(type.Name.ToLower(), type);

        }

        public static Tuple<Result, StackMember> ConvertTo(StackMember original, string newType)
        {
            Init();

            if (!StackTypes.TryGetValue(newType.ToLower(), out Type target))
                return new(new Error($"Can't find a StackMember w/ type {newType.ToLower()}!"), null);

            StackMember newStackMember = null;

            if (original.CanBeConvertedTo.Length > 0)
                newStackMember = original.ConvertTo(target);

            if (newStackMember is null)
                return new(new Error($"Can't convert {original.GetType().Name} to {target.Name}!"), null);

            return new(new Success(), newStackMember);
        }
    }
}
