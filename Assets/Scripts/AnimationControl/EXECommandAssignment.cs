using System;
using System.Linq;

namespace OALProgramControl
{
    public class EXECommandAssignment : EXECommand
    {
        public EXEASTNodeAccessChain AssignmentTarget { get; }
        public EXEASTNodeBase AssignedExpression { get; }

        public EXECommandAssignment(EXEASTNodeAccessChain assignmentTarget, EXEASTNodeBase assignedExpression)
        {
            this.AssignmentTarget = assignmentTarget;
            this.AssignmentTarget.CreateVariableIfItDoesNotExist = true;
            this.AssignedExpression = assignedExpression;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult evaluationResultOfAssignmentTarget = this.AssignmentTarget.Evaluate(this.SuperScope, OALProgram);

            if (!evaluationResultOfAssignmentTarget.IsDone)
            {
                // It's a stack-like structure, so we enqueue the current command first, then the pending command.
                this.CommandStack.Enqueue(this);
                this.CommandStack.Enqueue(evaluationResultOfAssignmentTarget.PendingCommand);
                return evaluationResultOfAssignmentTarget;
            }

            if (!evaluationResultOfAssignmentTarget.IsSuccess)
            {
                evaluationResultOfAssignmentTarget.OwningCommand = this;
                return evaluationResultOfAssignmentTarget;
            }

            EXEExecutionResult evaluationResultOfAssignedExpression = this.AssignedExpression.Evaluate(this.SuperScope, OALProgram);
            if (!evaluationResultOfAssignedExpression.IsDone)
            {
                // It's a stack-like structure, so we enqueue the current command first, then the pending command.
                this.CommandStack.Enqueue(this);
                this.CommandStack.Enqueue(evaluationResultOfAssignedExpression.PendingCommand);
                return evaluationResultOfAssignedExpression;
            }

            if (!evaluationResultOfAssignedExpression.IsSuccess)
            {
                evaluationResultOfAssignedExpression.OwningCommand = this;
                return evaluationResultOfAssignedExpression;
            }

            EXEExecutionResult assignmentResult
                = evaluationResultOfAssignmentTarget
                    .ReturnedOutput
                    .AssignValueFrom(evaluationResultOfAssignedExpression.ReturnedOutput);
            assignmentResult.OwningCommand = this;

            return assignmentResult;
        }

        public override String ToCodeSimple()
        {
            String Result = this.VariableName;
            if (this.AttributeName != null)
            {
                Result += "." + this.AttributeName;
            }
            Result += " = " + this.AssignedExpression.ToCode();
            return Result;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandAssignment(VariableName, AttributeName, AssignedExpression);
        }
    }
}
