using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Results;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Ldstr)]
    internal class LoadString(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            Script.EvaluationStack.Add(new String(Raw, Script));
            return new Success();
        }
    }
}
