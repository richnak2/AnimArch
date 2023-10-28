using System;
using UnityEngine;

namespace OALProgramControl
{
    public class EXECommandQueryCreate : EXECommand
    {
        public String ClassName { get; }
        public EXEASTNodeAccessChain AssignmentTarget { get; }
        //private CDClassInstance CreatedInstance { get; set; }

        public EXECommandQueryCreate(String ClassName) : this(ClassName, null)
        {
        }
        public EXECommandQueryCreate(String ClassName, EXEASTNodeAccessChain assignmentTarget)
        {
            this.ClassName = ClassName;
            this.AssignmentTarget = assignmentTarget;
            //this.CreatedInstance = null;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult assignmentTargetEvaluationResult = null;

            if (this.AssignmentTarget != null)
            {
                assignmentTargetEvaluationResult
                    = this.AssignmentTarget.Evaluate(this.SuperScope, OALProgram, new EXEASTNodeAccessChainContext() { CreateVariableIfItDoesNotExist = true });

                if (!HandleRepeatableASTEvaluation(assignmentTargetEvaluationResult))
                {
                    return assignmentTargetEvaluationResult;
                }
            }

            EXEExecutionResult classInstanceCreationResult = OALProgram.ExecutionSpace.CreateInstance(this.ClassName);

            if (!HandleSingleShotASTEvaluation(classInstanceCreationResult))
            {
                return classInstanceCreationResult;
            }

            //this.CreatedInstance = (classInstanceCreationResult.ReturnedOutput as EXEValueReference).ClassInstance;

            if (this.AssignmentTarget != null)
            {
                EXEExecutionResult assignmentResult
                    = assignmentTargetEvaluationResult.ReturnedOutput.AssignValueFrom(classInstanceCreationResult.ReturnedOutput);

                if (!HandleSingleShotASTEvaluation(assignmentResult))
                {
                    return assignmentResult;
                }
            }

            return Success();
        }

        public override string ToCodeSimple()
        {
            return "create object instance " + (this.AssignmentTarget?.ToCode() ?? string.Empty) + " of " + this.ClassName;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandQueryCreate(this.ClassName, this.AssignmentTarget?.Clone() as EXEASTNodeAccessChain);
        }
        public CDClassInstance GetCreatedInstance()
        {
            return (this.AssignmentTarget.LastElement.EvaluationResult.GetCurrentValue() as EXEValueReference)?.ClassInstance;
        }
    }
}