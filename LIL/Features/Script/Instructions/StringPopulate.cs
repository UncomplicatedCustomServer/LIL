using System.Linq;
using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Results;
using LIL.Features.Script.Variables;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Strpop)]
    internal class StringPopulate(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Script.EvaluationStack.Count == 0)
                return new Error("Can't populate a string that has not been loaded into the evaluation stack!");

            if (Script.EvaluationStack.Last() is not String str)
                return new Error($"Cannot accept a {Script.EvaluationStack.Last().GetType().FullName} as a string!");

            string content = str.Evaluate().ToString();

            foreach (Variable var in Script.Variables.Values)
                content = content.Replace($"{{{var.Name}}}", var.Content.Evaluate().ToString());

            // Push the result into the evaluation stack - before we need to remove the old one
            Script.RemoveLastStackMember();
            Script.EvaluationStack.Add(new String(content, Script));

            return new Success();
        }
    }
}
