using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXECommandPragma : EXECommand
    {
        public string Pragma { get; }

        public EXECommandPragma(string pragma)
        {
            this.Pragma = pragma;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            string pragma = Pragma.ToLower().Trim();

            switch (pragma)
            {
                default: return EXEExecutionResult.Error(string.Format("Unknown pragram option: '{0}'.", Pragma), "XEC2049");
            }

            return Success();
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandPragma(Pragma);
        }
    }
}