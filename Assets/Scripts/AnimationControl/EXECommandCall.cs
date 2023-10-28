using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXECommandCall : EXECommand
    {
        public readonly EXEASTNodeAccessChain MethodAccessChain;
        public readonly EXEASTNodeMethodCall MethodCall;
        public EXEValueBase ReturnedValue;
        public MethodInvocationInfo CallInfo { get; private set; }
        private EXEValueBase CalledObject { get; set; }
        private string MethodAccessChainS { get; }
        public EXEScopeMethod InvokedMethod { get; private set; }

        public EXECommandCall(EXEASTNodeAccessChain methodAccessChain, EXEASTNodeMethodCall methodCall)
        {
            this.MethodAccessChain = methodAccessChain;
            this.MethodCall = methodCall;
            this.ReturnedValue = null;
            this.CallInfo = null;
            this.CalledObject = null;
            this.MethodAccessChainS = methodAccessChain.ToCode();
        }
        public EXECommandCall(EXEValueBase methodOwningObject, string accessChain, EXEASTNodeMethodCall methodCall)
        {
            this.MethodAccessChain = null;
            this.MethodCall = methodCall;
            this.ReturnedValue = null;
            this.CallInfo = null;
            this.CalledObject = methodOwningObject;
            this.MethodAccessChainS = accessChain;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandCall(this.MethodAccessChain, this.MethodCall);
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            if (this.CalledObject == null)
            {
                EXEExecutionResult methodOwningObjectEvaluationResult = this.MethodAccessChain.Evaluate(this.SuperScope, OALProgram);

                if (!HandleRepeatableASTEvaluation(methodOwningObjectEvaluationResult))
                {
                    return methodOwningObjectEvaluationResult;
                }

                this.CalledObject = methodOwningObjectEvaluationResult.ReturnedOutput.GetCurrentValue();
            }

            EXEExecutionResult methodCallResolvingResult
                = this.MethodCall.Evaluate
                (
                    this.SuperScope,
                    OALProgram,
                    new EXEASTNodeAccessChainContext() { CurrentValue = this.CalledObject }
                );

            if (this.MethodCall.EvaluationState != EEvaluationState.HasBeenEvaluated)
            {
                if (!HandleRepeatableASTEvaluation(methodCallResolvingResult))
                {
                    return methodCallResolvingResult;
                }
            }

            // We are here, which means that all parameter values have been resolved successfully

            EXEScopeMethod MethodCode = this.MethodCall.Method.ExecutableCode;
            MethodCode.OwningObject = (this.CalledObject.GetCurrentValue() as EXEValueReference).ClassInstance;
            MethodCode.SetSuperScope(null);
            MethodCode.CommandStack = this.CommandStack;
            MethodCode.MethodCallOrigin = this.MethodCall;
            this.CommandStack.Enqueue(MethodCode);

            this.InvokedMethod = MethodCode;

            EXEExecutionResult variableCreationResult
                = MethodCode.AddVariable(new EXEVariable(EXETypes.SelfReferenceName, this.MethodCall.OwningObject));

            if (!HandleSingleShotASTEvaluation(variableCreationResult))
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

                if (!HandleSingleShotASTEvaluation(variableCreationResult))
                {
                    return variableCreationResult;
                }
            }

            this.CallInfo
                = new MethodInvocationInfo
                (
                    GetCurrentMethodScope().MethodDefinition,
                    MethodCode.MethodDefinition,
                    OALProgram
                        .RelationshipSpace
                        .GetRelationshipByClasses
                        (
                            GetCurrentMethodScope().MethodDefinition.OwningClass.Name,
                            MethodCode.MethodDefinition.OwningClass.Name
                        ),
                    GetCurrentMethodScope().OwningObject,
                    (this.CalledObject.GetCurrentValue() as EXEValueReference).ClassInstance
                );

            return Success();
        }
        public override string ToCodeSimple()
        {
            return this.MethodAccessChainS + "." + this.MethodCall.ToCode();
        }
    }
}