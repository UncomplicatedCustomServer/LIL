using System;
using System.Collections.Generic;
using System.ComponentModel;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Instructions;
using LIL.Features.Script.Results;
using LIL.Features.Script.Variables;

namespace LIL.Features.Script
{
    public class Script : ICloneable
    {
        public readonly List<StackMember> EvaluationStack = [];

        public readonly Dictionary<int, Script> Rcp = [];

        public readonly List<Instruction> Instructions = [];

        public Dictionary<string, Variable> Variables => Parent is null ? _variables : Parent._variables;

        public Dictionary<string, Variable> _variables = [];

        public readonly Dictionary<string, string> GenericSettings = [];

        public bool CanOverrideVars => !(GenericSettings.ContainsKey("secure_vars") && GenericSettings["secure_vars"] == "true");

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
                Result result = Instructions[i].Execute();
                if (result is Goto gt)
                    i = gt.Line;
                /*else if (result is ExecuteRcp rcp && Rcp.ContainsKey(rcp.Id))
                    if (Rcp[rcp.Id].Execute();*/
                else if (result is Results.Return)
                    return new Results.Return();
                else if (result is Results.Break && IsInsideLoop)
                    return new Results.Break();
                else if (result is Results.Continue && IsInsideLoop)
                    return new Success(); // Skip without breaking everything
                else if (result is Error)
                    return new Results.Return();
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
