using System;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class EXEScopeMethod : EXEScope
    {
        public MethodCallRecord MethodDefinition;

        public EXEScopeMethod() : base()
        {
        }
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            AddCommandsToStack(this.Commands);
            return Success();
        }
        public override string ToFormattedCode(string Indent = "")
        {
            String Result = "";
            foreach (EXECommand Command in this.Commands)
            {
                Result += Command.ToFormattedCode(Indent);
            }
            return Result;
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeMethod() { MethodDefinition = MethodDefinition };
        }
    }
}