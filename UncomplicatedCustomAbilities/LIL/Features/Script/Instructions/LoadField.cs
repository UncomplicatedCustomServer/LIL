﻿using System.Linq;
using System.Reflection;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;
namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Ldfld)]
    internal class LoadField(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Script.EvaluationStack.Last() is not Class cl)
                return new Error($"Last evaluation Stack is not a class but a {Script.EvaluationStack.Last().GetType().FullName}");

            Script.RemoveLastStackMember();

            FieldInfo field = cl.RefType.GetField(Raw);

            if (field == null)
                return new Error($"Field {Raw} does not exists inside the class {cl.RefType.FullName}");

            object data = field?.GetValue(cl.Instance);
            StackMember member = StackMember.CreateFromGenericType(field.FieldType, data, Script);

            Script.EvaluationStack.Add(member);

            return new Success();
        }
    }
}