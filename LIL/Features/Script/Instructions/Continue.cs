using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.Results;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Cont)]
    internal class Continue(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute() => new Results.Continue();
    }
}
