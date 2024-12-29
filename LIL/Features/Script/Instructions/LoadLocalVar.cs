using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.Results;
using LIL.Features.Script.Variables;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Ldloc)]
    internal class LoadLocalVar(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (!Script.Variables.TryGetValue(Raw, out Variable var))
                return new Error($"Failed to load the local variable (locvar) {Raw}!");

            Script.EvaluationStack.Add(var.Content);

            return new Success();
        }
    }
}
