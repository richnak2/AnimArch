namespace OALProgramControl
{
    public class EXECommandContinue : EXECommand
    {
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            return Success();//return SuperScope.PropagateControlCommand(LoopControlStructure.Continue);
        }
        public override string ToCodeSimple()
        {
            return "continue";
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandContinue();
        }
    }
}
