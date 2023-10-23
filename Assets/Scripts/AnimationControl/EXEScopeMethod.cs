using System;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class EXEScopeMethod : EXEScope
    {
        public CDMethod MethodDefinition;
        public EXEASTNodeMethodCall MethodCallOrigin;

        public EXEScopeMethod() : base()
        {
            this.MethodCallOrigin = null;
        }
        public EXEScopeMethod(CDMethod methodDefinition) : base()
        {
            this.MethodDefinition = methodDefinition;
        }
        public override bool CollectReturn(EXEValueBase returnedValue, OALProgram programInstance)
        {
            // Check compatibility of types
            if (!EXETypes.CanBeAssignedTo(this.MethodCallOrigin.EvaluationResult.ReturnedOutput, this.MethodDefinition.ReturnType, programInstance.ExecutionSpace))
            {
                return false;
            }

            // This actually performs the assignment
            this.MethodCallOrigin.EvaluationResult = Success();
            this.MethodCallOrigin.EvaluationResult.ReturnedOutput = returnedValue;
            return true;
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
            return new EXEScopeMethod(this.MethodDefinition);
        }
    }
}