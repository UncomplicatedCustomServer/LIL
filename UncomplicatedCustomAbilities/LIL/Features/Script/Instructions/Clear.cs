using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Clear)]
    internal class Clear(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            Script.EvaluationStack.Clear();

            return new Success();
        }
    }
}
