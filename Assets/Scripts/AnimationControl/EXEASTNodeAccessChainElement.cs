namespace OALProgramControl
{
    public class EXEASTNodeAccessChainElement
    {
        public readonly EXEASTNodeBase NodeValue;

        public EXEASTNodeAccessChainElement PreviousNode;
        public EXEASTNodeAccessChainElement NextNode;
        public EXEValueBase EvaluationResult;

        public EXEASTNodeAccessChainElement(EXEASTNodeBase nodeValue)
        {
            this.NodeValue = nodeValue;
            this.PreviousNode = null;
            this.NextNode = null;
            this.EvaluationResult = null;
        }

        public EXEExecutionResult Evaluate(EXEScope currentScope, OALProgram currentProgramInstance, EXEASTNodeAccessChainContext valueContext)
        {
            EXEASTNodeAccessChainContext evaluationContext
                = new EXEASTNodeAccessChainContext()
                {
                    CreateVariableIfItDoesNotExist = valueContext.CreateVariableIfItDoesNotExist,
                    CurrentValue = this.PreviousNode == null ? null : PreviousNode.EvaluationResult
                };

            EXEExecutionResult executionResult = this.NodeValue.Evaluate(currentScope, currentProgramInstance, evaluationContext);

            if (!executionResult.IsSuccess || !executionResult.IsDone)
            {
                return executionResult;
            }

            this.EvaluationResult = executionResult.ReturnedOutput;

            if (this.NextNode == null)
            {
                return executionResult;
            }

            return this.NextNode.Evaluate(currentScope, currentProgramInstance, evaluationContext);
        }

        public EXEASTNodeAccessChainElement Clone()
        {
            return new EXEASTNodeAccessChainElement(this.NodeValue.Clone());
        }
    }
}