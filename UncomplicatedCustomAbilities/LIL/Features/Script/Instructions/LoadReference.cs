using System;
using System.Linq;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Ldref)]
    internal class LoadReference(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            string assembly = string.Empty;

            if (Script.EvaluationStack.Count > 0 && Script.EvaluationStack.Last() is TempSetting tempSetting && tempSetting.Args[0] == "load_assembly") {
                Script.RemoveLastStackMember();
                assembly = $", {tempSetting.Args[1]}";
            }

            Script.EvaluationStack.Add(new Class(Type.GetType($"{Raw}{assembly}"), null, Script));

            return new Success();
        }
    }
}