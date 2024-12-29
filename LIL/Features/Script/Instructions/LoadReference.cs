using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Results;
using LIL.Helpers;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Ldref)]
    internal class LoadReference(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            Script.EvaluationStack.Add(new Class(ObjectHandler.LoadType(Script, Raw), null, Script));

            return new Success();
        }
    }
}