using System;
using UncomplicatedCustomAbilities.LIL.Enums;

namespace UncomplicatedCustomAbilities.LIL.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    internal class OpCodeReference(OpCodeType generic) : Attribute
    {
        public OpCodeType OpCode { get; } = generic;
    }
}
