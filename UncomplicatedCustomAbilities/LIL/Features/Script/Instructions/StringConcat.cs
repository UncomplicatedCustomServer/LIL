using System.Collections.Generic;
using System.Linq;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;
using UncomplicatedCustomAbilities.LIL.Helpers;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Strc)]
    internal class StringConcat(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            // W/ this method we join EVERY string that we can find inside the evaluation stack so check it!
            List<String> strings = [];
            foreach (StackMember str in Script.EvaluationStack)
                if (str is not String realStr)
                    return new Error("In the evaluation stack there's a non-string that was evoked w/ the strconcat OpCode!");
                else
                    strings.Add(realStr);

            if (strings.Count == 0)
                return new Error("The evaluation stack is empty!");

            string result = string.Empty;
            foreach (String str in strings)
                result += str.Evaluate().ToString();

            Script.EvaluationStack.Clear();
            Script.EvaluationStack.Add(new String(result, Script));

            return new Success();
        }
    }
}
