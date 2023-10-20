namespace OALProgramControl
{
    public class EXEASTNodeAccessChainElement
    {
        public readonly EXEASTNodeBase NodeValue;

        public EXEASTNodeAccessChainElement PreviousNode;
        public EXEASTNodeAccessChainElement NextNode;
        public EXEValueBase EvaluationResult;
        public bool CreateNewVariableIfItDoesNotExist;

        public EXEASTNodeAccessChainElement(EXEASTNodeBase nodeValue)
        {
            this.NodeValue = nodeValue;
            this.PreviousNode = null;
            this.NextNode = null;
            this.EvaluationResult = null;
            this.CreateNewVariableIfItDoesNotExist = false;
        }

        public EXEExecutionResult Evaluate(EXEScope currentScope, OALProgram currentProgramInstance)
        {
            EXEASTNodeAccessChainContext evaluationContext
                = new EXEASTNodeAccessChainContext()
                {
                    CreateVariableIfItDoesNotExist = this.CreateNewVariableIfItDoesNotExist,
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

            return this.NextNode.Evaluate(currentScope, currentProgramInstance);
        }
    }
}