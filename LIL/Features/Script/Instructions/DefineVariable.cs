using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.Results;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Defvar)]
    internal class DefineVariable(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            Script.Variables.Add(Raw, new(Raw));
            return new Success();
        }
    }
}
