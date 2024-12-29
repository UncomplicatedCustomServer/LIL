using System.Collections;
using System.Linq;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;
using UncomplicatedCustomAbilities.LIL.Features.Script.Variables;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions.Operators
{
    [OpCodeReference(OpCodeType.Evalfr)]
    internal class Foreach(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Script.EvaluationStack.Last() is not Class cl)
                return new Error($"Last evaluation Stack is not a class but a {Script.EvaluationStack.Last().GetType().FullName}");

            if (!Script.Rcp.TryGetValue(int.Parse(Raw), out Script rcp))
                return new Error($"RCP {int.Parse(Raw)} not found inside the script file(s)!");

            if (cl.RefType is IEnumerable enumerable)
                foreach (object item in enumerable)
                {
                    Script scr = (Script)rcp.Clone();
                    if (scr is null)
                        continue;

                    scr.IsInsideLoop = true;
                    scr.Parent = Script;
                    scr.Variables.Add(Raw, Variable.Initialize(Raw, StackMember.CreateFromGenericType(item.GetType(), item, Script)));

                    Result status = scr.Execute();
                    if (status is Error)
                        return new Results.Return();
                    else if (status is Results.Break)
                        return new Results.Break(); // Break is recursive
                }

            return new Success();
        }
    }
}
