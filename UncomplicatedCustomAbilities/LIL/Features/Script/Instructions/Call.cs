using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;
using UncomplicatedCustomAbilities.LIL.Helpers;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Call)]
    internal class Call(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute() => Executor.Execute(Raw, Script).Item1;
    }
}
