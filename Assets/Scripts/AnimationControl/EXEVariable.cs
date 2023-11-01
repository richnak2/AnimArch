namespace OALProgramControl
{
    public class EXEVariable
    {
        public readonly string Name;
        public readonly EXEValueBase Value;

        public EXEVariable(string name, EXEValueBase value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}