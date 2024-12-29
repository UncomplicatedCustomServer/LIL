using System;
using LIL.Features.Script.EvaluationStack;

namespace LIL.Features.Script.Variables
{
    public class Variable(string name)
    {
        public string Name { get; internal set; } = name;

        public StackMember Content { get; internal set; }

        public Type DynamicType => Content.GetType();

        public object RawContent => Content.Evaluate();

        public bool IsInitialized => Content != null;

        public static Variable Initialize(string name, StackMember content)
        {
            Variable var = new(name);
            var.Update(content);
            return var;
        }

        public void Update(StackMember content) => Content = content;
    }
}
