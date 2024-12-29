using System.Linq;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;
using UncomplicatedCustomAbilities.LIL.Features.Script.Variables;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Stloc)]
    internal class SaveLocalVar(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Script.EvaluationStack.Count == 0)
                return new Error("Can't save an non-existing StackMember inside a var (EMPTY)!");

            if (Script.Variables.ContainsKey(Raw))
            {
                if (!Script.CanOverrideVars)
                    return new Error($"Can't override var {Raw}! - Please check the SCRIPT SETTINGS! [H]");
                else
                    Script.Variables[Raw].Update(Script.EvaluationStack.Last());
            }
            else
                Script.Variables.Add(Raw, Variable.Initialize(Raw, Script.EvaluationStack.Last()));

            Script.RemoveLastStackMember();

            return new Success();
        }
    }
}