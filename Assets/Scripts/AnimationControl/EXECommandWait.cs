namespace OALProgramControl
{
    public class EXECommandWait : EXECommand
    {
        public EXEASTNodeBase WaitTime { get; }

        public EXECommandWait(EXEASTNodeBase waitTime)
        {
            this.WaitTime = waitTime;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult waitTimeEvaluationResult = WaitTime.Evaluate(this.SuperScope, OALProgram);

            if (!HandleRepeatableASTEvaluation(waitTimeEvaluationResult))
            {
                return waitTimeEvaluationResult;
            }

            return Success();
        }
        public override void Accept(Visitor v)
        {
            v.VisitExeCommandWait(this);
        }
        public override EXECommand CreateClone()
        {
            return new EXECommandWait(WaitTime.Clone());
        }
    }
}