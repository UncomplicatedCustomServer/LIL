using System;
using UncomplicatedCustomAbilities.LIL.Enums;

namespace UncomplicatedCustomAbilities.LIL.Features.Script.EvaluationStack
{
    internal class TempSetting(string raw, Script script) : StackMember(script)
    {
        public override StackMemberType Type => StackMemberType.TempSetting;

        public readonly string Content = raw;

        public readonly string[] Args = raw.Split(' ');

        public override bool IsQuantifiable => false;

        public override object Evaluate(Type requiredType = null) => Content;
    }
}