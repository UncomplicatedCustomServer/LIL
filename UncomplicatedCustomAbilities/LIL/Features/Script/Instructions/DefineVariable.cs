using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
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
