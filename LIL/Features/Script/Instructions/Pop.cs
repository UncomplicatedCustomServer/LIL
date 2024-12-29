﻿using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.Results;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Pop)]
    internal class Pop(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Script.EvaluationStack.Count > 0)
                Script.RemoveLastStackMember();

            return new Success();
        }
    }
}
