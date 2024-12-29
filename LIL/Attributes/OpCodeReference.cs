using System;
using LIL.Enums;

namespace LIL.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    internal class OpCodeReference(OpCodeType generic) : Attribute
    {
        public OpCodeType OpCode { get; } = generic;
    }
}
