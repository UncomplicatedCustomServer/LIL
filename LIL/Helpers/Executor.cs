using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Results;
using LIL.Features.Script;
using System;

namespace LIL.Helpers
{
    internal class Executor
    {
        public static Tuple<Result, object> Execute(string raw, Script script, bool targetConstructor = false) => InternalExecute(raw, script, targetConstructor);

        private static Tuple<Result, object> InternalExecute(string Raw, Script Script, bool targetConstructor = false)
        {
            if (Script.EvaluationStack.Last() is not Class cl)
                return new(new Error($"Last evaluation Stack is not a class but a {Script.EvaluationStack.Last().GetType().FullName}"), null);

            if (cl.RefType is null)
                return new(new Error($"Given class in the evaluation stack is null!"), null);

            Script.RemoveLastStackMember();

            string name = Raw.Split(' ')[0];

            // We load the method BEFORE and then we put the arguments, we prob will have to cast them? idk we'll see
            List<MethodBase> methodInfo = targetConstructor ? [.. cl.RefType.GetConstructors()] : [.. cl.RefType.GetMethods().Where(m => m.Name == name)];

            if (methodInfo.Count < 1)
                return new(new Error($"Method {name} not found inside class {cl.RefType.FullName}!"), null);

            if (methodInfo.Count == 1)
                return MethodRunner(Script, methodInfo[0], cl.Instance);

            if (Raw.Split(' ').Length > 2)
                return new(new Error($"OpCode call/callvir accept a maximum number of 2 args!"), null);

            string[] required;

            if (Raw.Split(' ').Length == 1)
                required = [];
            else
                required = Raw.Split(' ')[1].Split(',');

            foreach (MethodInfo method in methodInfo)
                if (required.AbsEquals(MethodTypes(method)))
                    return MethodRunner(Script, method, cl.Instance);

            // You HAVE TO specify the method by using the 2nd arg of the OpCode like callvir WriteLine string,object -- only mandatory things must be signed here!
            return new(new Error($"Method {Raw} has multiple overloads ({methodInfo.Count}), but no one with the required args: {Raw.Split(' ')[1]}"), null);


            //return new Success();
        }

        private static Tuple<Result, object> MethodRunner(Script Script, MethodBase method, object cl)
        {
            if (Script.EvaluationStack.Count < method.GetParameters().Count(p => !p.IsOptional))
                return new(new Error($"Method {method.GetType().FullName} requires a minimum of {method.GetParameters().Count(p => !p.IsOptional)}"), null);

            int argc = Script.EvaluationStack.Count < method.GetParameters().Count() ? Script.EvaluationStack.Count : method.GetParameters().Count();

            List<object> rawArgs = [];
            if (argc > 0)
            {
                List<StackMember> args = Script.EvaluationStack.Skip(argc - 1).ToList();

                for (int index = 0; index < args.Count; index++)
                    if (args[index].RefType != method.GetParameters()[index].ParameterType)
                        return new(new Error($"Parameter type mismatch! - Parameter {method.GetParameters()[index].Name} requires type {method.GetParameters()[index].ParameterType.FullName} but a {args[index].RefType.Name} was given!"), null);

                foreach (StackMember arg in args)
                    rawArgs.Add(arg.Evaluate(arg.RefType));

                Script.EvaluationStack.RemoveRange(Script.EvaluationStack.IndexOf(args[0]), argc);
            }

            object result = method.Invoke(cl, [.. rawArgs]);

            if (result is null)
                return new(new Success(), null);

            // Script.EvaluationStack.Add(StackMember.CreateFromGenericType(result.GetType(), result, Script)); - Do not add now to the evaluation stack as this is used also by call!

            return new(new Success(), StackMember.CreateFromGenericType(result.GetType(), result, Script));
        }

        private static string[] MethodTypes(MethodInfo method)
        {
            List<string> types = [];

            foreach (ParameterInfo methodType in method.GetParameters().Where(p => !p.IsOptional))
                types.Add(methodType.ParameterType.Name);

            return [.. types];
        }
    }
}
