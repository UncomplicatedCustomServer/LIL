using LIL.Features;
using LIL.Features.Script;
using LIL.Features.Script.EvaluationStack;
using System;
using System.Linq;
using System.Reflection;

namespace LIL.Helpers
{
    internal static class ObjectHandler
    {
        public static Type LoadType(Script script, string name)
        {
            Assembly assembly = null;

            if (script.EvaluationStack.Count > 0 && script.EvaluationStack.Last() is TempSetting tempSetting && tempSetting.Args[0] == "load_assembly")
            {
                script.RemoveLastStackMember();
                assembly = AssemblyHandler.Assemblies.FirstOrDefault(assembly => assembly.GetName().Name == name);
            }

            Type def;

            if (assembly is not null)
                def = assembly.GetType(name);
            else
                def = Type.GetType(name);

            if (def is null)
            {
                foreach (Assembly assemb in AssemblyHandler.Assemblies)
                {
                    def = assemb.GetType(name);
                    if (def is not null)
                        break;
                }
            }

            if (def is null)
                throw new MemberAccessException($"Assembly NOT FOUND or {assembly?.GetName().Name} does not contains the {name} prt!");

            return def;
        }
    }
}
