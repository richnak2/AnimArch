
namespace OALProgramControl
{
    public class EXECommandReturn : EXECommand
    {
        private EXEASTNodeBase Expression { get; }

        public EXECommandReturn(EXEASTNodeBase Expression)
        {
            this.Expression = Expression;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            return Success();
        }

        public override string ToCodeSimple()
        {
            return this.Expression == null ? "return" : ("return " + this.Expression.ToCode());
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandReturn(Expression);
        }
    }
}
