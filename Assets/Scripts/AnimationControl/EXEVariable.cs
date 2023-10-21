namespace OALProgramControl
{
    public class EXEVariable
    {
        public readonly string Name;
        public readonly EXEValueBase Value;

        public EXEVariable(string name) : this(name, new EXEValueUnitialized()) {}
        public EXEVariable(string name, EXEValueBase value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}