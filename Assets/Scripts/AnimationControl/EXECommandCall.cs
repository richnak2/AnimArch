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
            throw new System.NotImplementedException();
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            if (this.MethodAccessChain.EvaluationState == EEvaluationState.HasBeenEvaluated)
            {
                CreateExeCommandCallPreEvaluated(OALProgram);
                Success();
            }

            if (!this.MethodAccessChain.EvaluationResult.IsDone)
            {
                // It's a stack-like structure, so we enqueue the current command first, then the pending command.
                this.CommandStack.Enqueue(this);
                this.CommandStack.Enqueue(this.MethodAccessChain.EvaluationResult.PendingCommand);
                return this.MethodAccessChain.EvaluationResult;
            }

            if (!this.MethodAccessChain.EvaluationResult.IsSuccess)
            {
                this.MethodAccessChain.EvaluationResult.OwningCommand = this;
                return this.MethodAccessChain.EvaluationResult;
            }

            CreateExeCommandCallPreEvaluated(OALProgram);

            return Success();
        }
        private void CreateExeCommandCallPreEvaluated(OALProgram programInstance)
        {
            EXECommandCallPreEvaluated newCommand
                = new EXECommandCallPreEvaluated(this.MethodAccessChain.EvaluationResult.ReturnedOutput, this.MethodCall);
            newCommand.SetSuperScope(this.SuperScope);

            this.CommandStack.Enqueue(newCommand);
        }
    }
}