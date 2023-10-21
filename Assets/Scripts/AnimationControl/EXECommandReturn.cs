
namespace OALProgramControl
{
    public class EXECommandReturn : EXECommand
    {
        private EXEASTNodeBase Expression { get; }

        public EXECommandReturn(EXEASTNodeBase Expression)
        {
            this.Expression = Expression;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult returnedExpressionEvaluationResult = null;

            if (Expression != null)
            {
                returnedExpressionEvaluationResult = Expression.Evaluate(this.SuperScope, OALProgram);

                if (!returnedExpressionEvaluationResult.IsDone)
                {
                    // It's a stack-like structure, so we enqueue the current command first, then the pending command.
                    this.CommandStack.Enqueue(this);
                    this.CommandStack.Enqueue(returnedExpressionEvaluationResult.PendingCommand);
                    return returnedExpressionEvaluationResult;
                }

                if (!returnedExpressionEvaluationResult.IsSuccess)
                {
                    returnedExpressionEvaluationResult.OwningCommand = this;
                    return returnedExpressionEvaluationResult;
                }
            }

            EXEScopeMethod owningMethodScope = GetCurrentMethodScope();
            if (!owningMethodScope.CollectReturn(returnedExpressionEvaluationResult.ReturnedOutput, OALProgram))
            {
                return Error("Failed to deliver return value to owning scope.", "XEC2014");
            }

            return Success();
        }

        public override string ToCodeSimple()
        {
            return this.Expression == null ? "return" : ("return " + this.Expression.ToCode());
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandReturn(Expression);
        }
    }
}
