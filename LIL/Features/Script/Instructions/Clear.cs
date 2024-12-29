using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Results;

namespace LIL.Features.Script.Instructions
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
