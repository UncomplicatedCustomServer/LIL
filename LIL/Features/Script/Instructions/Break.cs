using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.Results;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Brk)]
    internal class Break(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute() => new Results.Break();
    }
}
