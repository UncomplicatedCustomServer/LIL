using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Results;
using System.Linq;
using System.Reflection;

namespace LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Stfld)]
    internal class SaveField(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Script.EvaluationStack.Last() is not Class cl)
                return new Error($"Last evaluation Stack is not a class but a {Script.EvaluationStack.Last().GetType().FullName}");

            Script.RemoveLastStackMember();

            FieldInfo field = cl.RefType.GetField(Raw);

            if (field == null)
                return new Error($"Field {Raw} does not exists inside the class {cl.RefType.FullName}");

            StackMember load;

            if (Script.EvaluationStack.Count == 0)
                load = new Null(Script);
            else
                load = Script.EvaluationStack.Last();

            object data = load.Evaluate();

            if (field.FieldType != data.GetType())
                return new Error($"Field {Raw} is of type {field.FieldType.FullName} but the data is of type {data.GetType().FullName}");

            field.SetValue(cl.Instance, data);
            Script.RemoveLastStackMember();

            return new Success();
        }
    }
}
