using System;
using System.Linq;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;
using UncomplicatedCustomAbilities.LIL.Helpers;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Convto)]
    internal class ConvertTo(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Script.EvaluationStack.Count == 0)
                return new Error("Can't convert an non-existing StackMember (EMPTY)!");

            Tuple<Result, StackMember> result = Converter.ConvertTo(Script.EvaluationStack.Last(), Raw);

            if (result.Item1 is not Success success)
                return new Error($"Failed to convert StackMember to {Raw}! - Please check the COMPATIBILITY");

            Script.RemoveLastStackMember();
            Script.EvaluationStack.Add(result.Item2);

            return success;
        }
    }
}
