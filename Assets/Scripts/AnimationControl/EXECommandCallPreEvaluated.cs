using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXECommandCallPreEvaluated : EXECommandCallBase
    {
        public readonly EXEValueBase OwningObject;
        public MethodInvocationInfo CallInfo { get; private set;}

        public EXECommandCallPreEvaluated(EXEValueBase owningObject, EXEASTNodeMethodCall methodCall) : base(methodCall)
        {
            this.OwningObject = owningObject;
            this.CallInfo = null;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandCallPreEvaluated(this.OwningObject, this.MethodCall.Clone() as EXEASTNodeMethodCall);
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

            if (!HandleRepeatableASTEvaluation(methodCallResolvingResult))
            {
                return methodCallResolvingResult;
            }

            // We are here, which means that all parameter values have been resolved successfully

            EXEScopeMethod MethodCode = this.MethodCall.Method.ExecutableCode;
            MethodCode.OwningObject = (this.OwningObject as EXEValueReference)?.ClassInstance;
            MethodCode.SetSuperScope(null);
            MethodCode.CommandStack = this.CommandStack;
            MethodCode.MethodCallOrigin = this.MethodCall;
            this.CommandStack.Enqueue(MethodCode);

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
                    GetCalledObject()
                );

            return Success();
        }

        public CDClassInstance GetCalledObject()
        {
            return (this.OwningObject as EXEValueReference)?.ClassInstance;
        }
        private CDRelationship CallRelationshipInfo(string CallerMethod, string CalledMethod)
        {
            return OALProgram.Instance.RelationshipSpace.GetRelationshipByClasses(CallerMethod, CalledMethod);
        }
    }
}