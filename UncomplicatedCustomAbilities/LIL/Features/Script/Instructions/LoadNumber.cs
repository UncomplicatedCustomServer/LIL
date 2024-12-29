using System;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Ldnum)]
    internal class LoadNumber(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Raw.Length == 0)
                return new Error("Can't load an empty number into the evaluation stack!");

            Script.EvaluationStack.Add(new Number(Raw, Script));

            return new Success();
        }
    }
}
