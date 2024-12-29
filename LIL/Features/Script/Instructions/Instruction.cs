using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.Results;

namespace LIL.Features.Script.Instructions
{
    public abstract class Instruction(string raw, Script script)
    {
        public static readonly Dictionary<OpCodeType, Type> InstructionsAssociator = [];

        public readonly string Raw = raw;

        public readonly Script Script = script;
        public abstract Result Execute();

        public static void Init()
        {
            if (InstructionsAssociator.Count > 0)
                return;

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.GetCustomAttribute<OpCodeReference>() != null))
            {
                OpCodeReference reference = type.GetCustomAttribute<OpCodeReference>();
                InstructionsAssociator.Add(reference.OpCode, type);
            }
        }
    }
}
