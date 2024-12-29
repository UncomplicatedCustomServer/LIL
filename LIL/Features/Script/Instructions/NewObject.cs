using LIL.Attributes;
using LIL.Enums;
using LIL.Features.Script.EvaluationStack;
using LIL.Features.Script.Results;
using LIL.Helpers;
using System;

namespace LIL.Features.Script.Instructions
{
#nullable enable
    [OpCodeReference(OpCodeType.Newobj)]
    internal class NewObject(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            // This MUST NOT be used with ldref! - This CAN handle the 2nd argument!
            string classRef = Raw.Contains(" ") ? Raw.Split(' ')[0] : Raw;
            string? types = Raw.Contains(" ") ? Raw.Split(' ')[1] : null;

            Type target = ObjectHandler.LoadType(Script, classRef);

            if (target.GetConstructors().Length == 1 && target.GetConstructors()[0].GetParameters().Length == 0)
            {
                // We don't have to execute a constructor - contructor-free class!
                object result = Activator.CreateInstance(target);
                Script.EvaluationStack.Add(new Class(target, result, Script));
                return new Success();
            }

            // Class does have a constructor
            Script.EvaluationStack.Add(new Class(target, null, Script)); // Temp addition to let Executor.Execute(...) works

            if (target is null)
                return new Error("Failed to load type! (newobj exec)");

            string input = "..ctor";

            if (types is not null)
                input += $" {types}";

            Tuple<Result, object> data = Executor.Execute(input, Script, true);

            if (data.Item1 is Success && data.Item2 is not null)
                Script.EvaluationStack.Add(new Class(target, data.Item2, Script));

            return data.Item1;
        }
    }
}
