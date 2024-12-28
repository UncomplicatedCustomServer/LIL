using System;
using System.Collections.Generic;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Instructions;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;
using UncomplicatedCustomAbilities.LIL.Features.Script.Variables;

namespace UncomplicatedCustomAbilities.LIL.Features.Script
{
    public class Script : ICloneable
    {
        public readonly List<StackMember> EvaluationStack = [];

        public readonly Dictionary<int, Script> Rcp = [];

        public readonly List<Instruction> Instructions = [];

        public readonly Dictionary<string, Variable> Variables = [];

        public readonly Dictionary<string, string> GenericSettings = [];

        public bool IsInsideLoop { get; internal set; } = false;

        public Script Parent { get; internal set; } = null;

        public bool IsChild => Parent != null;

        internal void RemoveLastStackMember()
        {
            EvaluationStack.RemoveAt(EvaluationStack.Count - 1);
        }

        public Result Execute()
        {
            for (int i = 0; i < Instructions.Count; i++)
            {
                Console.WriteLine($"Executing Instruction {i} - {Instructions[i].")
                Result result = Instructions[i].Execute();
                if (result is Goto gt)
                    i = gt.Line;
                /*else if (result is ExecuteRcp rcp && Rcp.ContainsKey(rcp.Id))
                    if (Rcp[rcp.Id].Execute();*/
                else if (result is Return)
                    return new Return();
                else if (result is Break && IsInsideLoop)
                    return new Break();
                else if (result is Continue && IsInsideLoop)
                    return new Success(); // Skip without breaking everything
                else if (result is Error)
                    return new Return();
            }

            return new Success();
        }

        #region ICloneable Members
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
    }
}
