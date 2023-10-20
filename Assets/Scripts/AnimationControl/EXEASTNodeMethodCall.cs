using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEASTNodeMethodCall : EXEASTNodeBase
    {
        public readonly string MethodName;
        public readonly List<EXEASTNodeBase> Arguments;

        public EXEASTNodeMethodCall(string methodName)
        {
            this.MethodName = methodName;
            this.Arguments = new List<EXEASTNodeBase>();
        }

        public override EXEExecutionResult Evaluate(EXEScope currentScope, OALProgram currentProgramInstance, EXEASTNodeAccessChainContext valueContext = null)
        {
            if (this.EvaluationState == EEvaluationState.HasBeenEvaluated)
            {
                return this.EvaluationResult;
            }

            this.EvaluationState = EEvaluationState.IsBeingEvaluated;

            CDMethod method = null;

            if (valueContext == null || valueContext.CurrentValue == null)
            {
                // We want to access a method of the current method owning object
                EXEVariable selfVariable = currentScope.FindVariable(EXETypes.SelfReferenceName);
                if (selfVariable.Value.MethodExists(this.MethodName))
                {
                    // Method exists
                    method = selfVariable.Value.FindMethod(this.MethodName);
                }
                else
                {
                   // Method does not exist, time to raise an error
                   Error
                }
            }
            else
            {
                // We want to access a method of an object given by the context
                if (valueContext.CurrentValue.MethodExists(this.MethodName))
                {
                    // Method exists
                    method = valueContext.CurrentValue.FindMethod(this.MethodName);
                }
                else
                {
                    // Method does not exist, time to raise an error
                    Error
                }
            }

            // Now, let us evaluate the args and invoke the method
            EXEExecutionResult argumentExecutionResult;
            foreach (EXEASTNodeBase argument in this.Arguments)
            {
                argumentExecutionResult = argument.Evaluate(currentScope, currentProgramInstance);

                if (!argumentExecutionResult.IsSuccess)
                {
                    this.EvaluationState = EEvaluationState.HasBeenEvaluated;
                    this.EvaluationResult = argumentExecutionResult;
                }

                if (!argumentExecutionResult.IsDone || !argumentExecutionResult.IsSuccess)
                {
                    // Either current argument evaluation did not finish or produced an error
                    return argumentExecutionResult;
                }

                // Current argument evaluation finished and did not produce an error
                Check match of argument and parameter type
            }

            // All arguments have been evaluated and without an error
            this.EvaluationResult = EXEExecutionResult.Success();
            this.EvaluationResult.PendingCommand = new EXECommandCallPreEvaluated(this);
            return this.EvaluationResult;
        }

        public override void PrintPretty(string indent, bool last)
        {
            throw new System.NotImplementedException();
        }

        public override string ToCode()
        {
            throw new System.NotImplementedException();
        }
    }
}
