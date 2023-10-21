using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXECommandCallPreEvaluated : EXECommandCallBase
    {
        public readonly EXEValueBase OwningObject;

        public EXECommandCallPreEvaluated(EXEValueBase owningObject, EXEASTNodeMethodCall methodCall) : base(methodCall)
        {
            this.OwningObject = owningObject;
        }

        public override EXECommand CreateClone()
        {
            throw new System.NotImplementedException();
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult methodCallResolvingResult
                = this.MethodCall.Evaluate
                (
                    this.SuperScope,
                    OALProgram,
                    new EXEASTNodeAccessChainContext() { CurrentValue = this.OwningObject }
                );

            if (!methodCallResolvingResult.IsDone)
            {
                // It's a stack-like structure, so we enqueue the current command first, then the pending command.
                this.CommandStack.Enqueue(this);
                this.CommandStack.Enqueue(methodCallResolvingResult.PendingCommand);
                return methodCallResolvingResult;
            }

            if (!methodCallResolvingResult.IsSuccess)
            {
                methodCallResolvingResult.OwningCommand = this;
                return methodCallResolvingResult;
            }

            // We are here, which means that all parameter values have been resolved successfully

            EXEScopeMethod MethodCode = this.MethodCall.Method.ExecutableCode;
            MethodCode.SetSuperScope(null);
            MethodCode.CommandStack = this.CommandStack;
            MethodCode.MethodCallOrigin = this.MethodCall;
            this.CommandStack.Enqueue(MethodCode);

            EXEExecutionResult variableCreationResult
                = MethodCode.AddVariable(new EXEVariable(EXETypes.SelfReferenceName, this.MethodCall.OwningObject));

            if (!variableCreationResult.IsSuccess)
            {
                return variableCreationResult;
            }
            
            for (int i = 0; i < this.MethodCall.Arguments.Count; i++)
            {
                variableCreationResult =
                    MethodCode.AddVariable
                    (
                        new EXEVariable
                        (
                            this.MethodCall.Method.Parameters[i].Name,
                            this.MethodCall.Arguments[i].EvaluationResult.ReturnedOutput
                        )
                    );

                if (!variableCreationResult.IsSuccess)
                {
                    return variableCreationResult;
                }
            }

            return Success();
        }
    }
}