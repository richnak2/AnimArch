using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{
    public abstract class EXEASTNodeBase
    {
        public EEvaluationState EvaluationState;
        protected EXEExecutionResult EvaluationResult;

        public EXEASTNodeBase()
        {
            this.EvaluationState = EEvaluationState.NotYetEvaluated;
            this.EvaluationResult = null;
        }

        public abstract EXEExecutionResult Evaluate(EXEScope currentScope, OALProgram currentProgramInstance, EXEASTNodeAccessChainContext valueContext = null);
        public abstract void PrintPretty(string indent, bool last);
        public abstract string ToCode();
    }
}
