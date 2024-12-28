using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions
{
    [OpCodeReference(OpCodeType.Callvir)]
    internal class CallVir(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (Script.EvaluationStack.Last() is not Class cl)
                return new Error($"Last evaluation Stack is not a class but a {Script.EvaluationStack.Last().GetType().FullName}");

            Script.RemoveLastStackMember();

            // We load the method BEFORE and then we put the arguments, we prob will have to cast them? idk we'll see
            List<MethodInfo> methodInfo = [.. cl.RefType.GetMethods(BindingFlags.Public).Where(m => m.Name == Raw)];

            if (methodInfo.Count < 1)
                return new Error($"Method {Raw} not found inside class {cl.RefType.FullName}!");

            if (methodInfo.Count == 1)
                return MethodRunner(methodInfo[0], cl.Instance);

            return new Error($"Method {Raw} has multiple overloads ({methodInfo.Count})");

            //return new Success();
        }

        public Result MethodRunner(MethodInfo method, object cl)
        {
            if (Script.EvaluationStack.Count < method.GetParameters().Count(p => !p.IsOptional))
                return new Error($"Method {method.GetType().FullName} requires a minimum of {method.GetParameters().Count(p => !p.IsOptional)}");

            int argc = Script.EvaluationStack.Count < method.GetParameters().Count() ? Script.EvaluationStack.Count : method.GetParameters().Count();

            List<StackMember> args = Script.EvaluationStack.Skip(argc).ToList();

            for (int index = 0; index < argc; index++)
                if (args[index].RefType != method.GetParameters()[index].ParameterType)
                    return new Error($"Parameter type mismatch! - Parameter {method.GetParameters()[index].Name} requires type {method.GetParameters()[index].ParameterType.FullName} but a {args[index].RefType.FullName} was given!");

            object result = method.Invoke(cl, [.. args]);

            Script.EvaluationStack.RemoveRange(Script.EvaluationStack.IndexOf(Script.EvaluationStack.Skip(argc).ToList()[0]), argc);
            Script.EvaluationStack.Add(StackMember.CreateFromGenericType(result.GetType(), result, Script));
          
            return new Success();
        }
    }
}
