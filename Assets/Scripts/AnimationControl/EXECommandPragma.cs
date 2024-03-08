using UnityEditor;
using UnityEngine;
using Visualization.Animation;

namespace OALProgramControl
{
    public class EXECommandPragma : EXECommand
    {
        public string Pragma { get; }
        public AnimationThread CurrentThread { get; set; }

        public EXECommandPragma(string pragma)
        {
            this.Pragma = pragma;
            this.CurrentThread = null;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            string pragma = Pragma.ToLower().Trim();

            switch (pragma)
            {
                case "noanim": CurrentThread.ToggleAnimate(false); break;
                case "doanim": CurrentThread.ToggleAnimate(true); break;
                case "nonewobjs": CurrentThread.ToggleNewObjectAnimate(false); break;
                case "donewobjs": CurrentThread.ToggleNewObjectAnimate(true); break;
                default: return EXEExecutionResult.Error(string.Format("Unknown pragma option: '{0}'.", Pragma), "XEC2049");
            }

            return Success();
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandPragma(Pragma);
        }
    }
}