namespace AnimationControl
{
    public class EXECommandContinue : EXECommand
    {
        public override bool Execute(Animation Animation, EXEScope Scope)
        {
            return Scope.PropagateControlCommand(LoopControlStructure.Continue);
        }
        public override string ToCodeSimple()
        {
            return "continue";
        }
    }
}
