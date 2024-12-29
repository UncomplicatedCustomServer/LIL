using System;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;
using UncomplicatedCustomAbilities.LIL.Helpers;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Callvir)]
    internal class CallVir(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            Tuple<Result, object> result = Executor.Execute(Raw, Script);
            if (result.Item2 is not null && result.Item2 is StackMember stackMember)
                Script.EvaluationStack.Add(stackMember);
            return result.Item1;
        }
    }
}
