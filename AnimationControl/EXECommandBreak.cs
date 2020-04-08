namespace AnimationControl
{
    public class EXECommandBreak : EXECommand
    {
        public override bool Execute(Animation Animation, EXEScope Scope)
        {
            return Scope.PropagateControlCommand(LoopControlStructure.Break);
        }
        public override string ToCodeSimple()
        {
            return "break";
        }
    }
}
