using System;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Ldop)]
    internal class LoadOperator(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Raw.Length < 2)
                return new Error($"You can't load an empty operator inside the evaluation stack!");

            if (!Enum.TryParse(Raw, out OperatorType opType))
                return new Error($"The operator {Raw} does NOT exists!");

            Script.EvaluationStack.Add(new Operator(opType, Script));

            return new Success();
        }
    }
}
