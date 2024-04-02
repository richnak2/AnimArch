using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.AnimationControl.BuiltIn;

namespace OALProgramControl
{
    public class EXEScopeBuiltInMethod : EXEScopeMethod
    {
        private readonly BuiltInMethodBase MethodExecution;

        public EXEScopeBuiltInMethod(CDMethod methodDefinition, BuiltInMethodBase methodExecution) : base()
        {
            this.MethodDefinition = methodDefinition;
            this.MethodCallOrigin = null;
            this.OwningObject = null;
            this.MethodExecution = methodExecution;
        }
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            List<EXEValueBase> parameters
                = this.MethodDefinition.Parameters
                    .Select(parameter => this.FindVariable(parameter.Name).Value)
                    .ToList();

            EXEExecutionResult result = MethodExecution.Evaluate(OwningObject, parameters);
            if (!HandleRepeatableASTEvaluation(result))
            {
                return result;
            }

            if (!this.SubmitReturn(result.ReturnedOutput, OALProgram))
            {
                return Error("XEC2044", "Failed to deliver return value to owning scope.");
            }

            return Success();
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeScopeMethod(this);
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeBuiltInMethod(this.MethodDefinition, this.MethodExecution.Clone()); ;
        }
    }
}