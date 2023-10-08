namespace OALProgramControl
{
    public class EXECommandBreak : EXECommand
    {
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            return Success();//return SuperScope.PropagateControlCommand(LoopControlStructure.Break);
        }
        public override string ToCodeSimple()
        {
            return "break";
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandBreak();
        }
    }
}
