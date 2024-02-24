using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.AnimationControl.BuiltIn;

namespace OALProgramControl
{
    public class EXEScopeBuiltInMethod : EXEScopeMethod
    {
        private readonly BuiltInMethodString MethodExecution;

        public EXEScopeBuiltInMethod(CDMethod methodDefinition, BuiltInMethodString methodExecution) : base()
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

            EXEExecutionResult result = MethodExecution.Evaluate(OwningObject as EXEValueString, parameters);
            if (!HandleSingleShotASTEvaluation(result))
            {
                return result;
            }

            if (!this.CollectReturn(result.ReturnedOutput, OALProgram))
            {
                return Error("Failed to deliver return value to owning scope.", "XEC2044");
            }

            return Success();
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeScopeMethod(this);
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeBuiltInMethod(this.MethodDefinition, this.MethodExecution);
        }
    }
}