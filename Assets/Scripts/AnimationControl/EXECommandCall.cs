using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXECommandCall : EXECommandCallBase
    {
        public readonly EXEASTNodeAccessChain MethodAccessChain;

        public EXECommandCall(EXEASTNodeAccessChain methodAccessChain, EXEASTNodeMethodCall methodCall) : base(methodCall)
        {
            this.MethodAccessChain = methodAccessChain;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandCall(this.MethodAccessChain, this.MethodCall);
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult methodOwningObjectEvaluationResult = this.MethodAccessChain.Evaluate(this.SuperScope, OALProgram);

            if (!HandleRepeatableASTEvaluation(methodOwningObjectEvaluationResult))
            {
                return methodOwningObjectEvaluationResult;
            }

            CreateExeCommandCallPreEvaluated();

            return Success();
        }
        private void CreateExeCommandCallPreEvaluated()
        {
            EXECommandCallPreEvaluated newCommand
                = new EXECommandCallPreEvaluated(this.MethodAccessChain.EvaluationResult.ReturnedOutput, this.MethodCall);
            newCommand.SetSuperScope(this.SuperScope);

            this.CommandStack.Enqueue(newCommand);
        }
    }
}