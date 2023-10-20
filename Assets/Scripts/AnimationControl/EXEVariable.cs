namespace OALProgramControl
{
    public class EXEVariable
    {
        public readonly string Name;
        public readonly EXEValueBase Value;

        public EXEVariable(string name)
        {
            this.Name = name;
            this.Value = new EXEValueUnitialized();
        }
    }
}