using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;
using UncomplicatedCustomAbilities.LIL.Features.Script.Variables;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
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
