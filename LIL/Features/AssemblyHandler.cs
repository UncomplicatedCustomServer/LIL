using System;
using System.Collections.Generic;
using System.Reflection;

namespace LIL.Features
{
    public static class AssemblyHandler
    {
        internal static readonly Dictionary<string, Assembly> assembliesRef = [];

        internal static HashSet<Assembly> Assemblies => [.. assembliesRef.Values];

        public static void LoadAssembly(Assembly assembly)
        {
            if (!assembliesRef.ContainsKey(assembly.GetName().Name))
                assembliesRef.Add(assembly.GetName().Name, assembly);
        }

        public static void LoadAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (Assembly assembly in assemblies)
                LoadAssembly(assembly);
        }

        public static void TryAutoLoad() => LoadAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    }
}
