using System;
using UncomplicatedCustomAbilities.LIL.Attributes;
using UncomplicatedCustomAbilities.LIL.Enums;
using UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack;
using UncomplicatedCustomAbilities.LIL.Features.Script.Results;
using UncomplicatedCustomAbilities.LIL.Helpers;

/*
 * THE SYNTAX OF THE IF CLAUSE IS REALLY STRANGE
 * This is because you CANT SIMPLY LEAVE THINGS "ad cazzorum"
 * The IF clase will read the WHOLE evaluation stack
 * The length of it MUST BE a multiplier of 3!
 * This is because EVERY CLAUSE will HAVE TO HAVE TWO MEMBERS and then we have the COMPARATOR
 * Every COUPLE of VALUES is a CLAUSE and a comparator must go at the END! (yeah)
 * so this is an example
 * A
 * B
 * ==
 * C
 * D
 * !=
 * EVERYTHING WILL BE LINKED WITH THE &&!!!
 * 
 * To redirect the IF supports the straight code and the else code -> evalif <true> <false>
 * You can also choose only the true one, no problem at all!
 */

namespace UncomplicatedCustomAbilities.LIL.Features.Script.Instructions.Operators
{
    [OpCodeReference(OpCodeType.Evalif)]
    internal class If(string raw, Script script) : Instruction(raw, script)
    {
        public override Result Execute()
        {
            if (!Script.EvaluationStack.Count.IsMultiplier(3))
                return new Error($"Can't initialize an IF clause without the proper number of multipliers! - Found {Script.EvaluationStack.Count} elements inside the stack!");

            bool result = true;

            for (int i = 0; i < Script.EvaluationStack.Count; i += 3)
            {
                if (Script.EvaluationStack[i] is StackMember m1 && Script.EvaluationStack[i + 1] is StackMember m2 && Script.EvaluationStack[i + 2] is Operator op)
                {
                    bool? localRes = HandleClause(m1, m2, op);
                    if (localRes is null)
                        return new Error($"Cannot apply the IF clause to the {i / 3} member -> {m1.GetType().Name} {op.RefType} {m2.GetType().Name} resulted in an error!");
                    result &= (bool)localRes;
                }
            }

            // Now elaborate the args
            string[] args = Raw.Contains(" ") ? Raw.Split(' ') : [Raw, null];

            string target = result ? args[0] : args[1];

            if (target is null)
                return new Error("Unhandled case of an IF clause (NOT FATAL!)", false);

            int res = Convert.ToInt32(target, 16);

            if (!Script.Rcp.TryGetValue(res, out Script action))
                return new Error($"Failed to parse rcp of an IF clause: {target} -> NOTFOUND!");

            Script.EvaluationStack.Clear();

            action.Clone();
            action.Parent = Script;
            return action.Execute();
        }

        private bool? HandleClause(StackMember a, StackMember b, Operator @operator)
        {
            OperatorType op = @operator.RefOperator;

            if (a.IsQuantifiable && b.IsQuantifiable)
            {
                decimal qA = a.Quantify();
                decimal qB = b.Quantify();
                return op switch
                {
                    OperatorType.Eq => qA == qB,
                    OperatorType.Neq => qA != qB,
                    OperatorType.Grt => qA > qB,
                    OperatorType.Grtoe => qA >= qB,
                    OperatorType.Lst => qA < qB,
                    OperatorType.Lstoe => qA <= qB,
                    _ => null
                };
            }
            else
            {
                return op switch
                {
                    OperatorType.Eq => a.Evaluate() == b.Evaluate(),
                    OperatorType.Neq => a.Evaluate() != b.Evaluate(),
                    _ => null
                };
            }
        }
    }
}
