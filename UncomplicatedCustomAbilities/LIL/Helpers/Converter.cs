using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UncomplicatedCustomAbilities.LIL.Features.Script;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;

namespace UncomplicatedCustomAbilities.LIL.Helpers
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

        public static Tuple<Result, StackMember> ConvertTo(Script script, StackMember original, string newType)
        {
            if (!StackTypes.TryGetValue(newType.ToLower(), out Type target))
                return new(new Error($"Can't find a StackMember w/ type {newType.ToLower()}!"), null);

            
        }
    }
}
