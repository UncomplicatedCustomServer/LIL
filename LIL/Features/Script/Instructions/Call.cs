using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.Results;
using LIL.Helpers;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Call)]
    internal class Call(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute() => Executor.Execute(Raw, Script).Item1;
    }
}
