using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Results;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Defass)]
    internal class DefineAssembly(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            Script.EvaluationStack.Add(new TempSetting($"load_assembly {Raw}", Script));

            return new Success();
        }
    }
}
