using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Results;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Ldbool)]
    internal class LoadBool(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Raw.Length == 0)
                return new Error("Can't load an empty number into the evaluation stack!");

            Script.EvaluationStack.Add(new Boolean(bool.Parse(Raw), Script));

            return new Success();
        }
    }
}
