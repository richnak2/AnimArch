using System.Collections.Generic;

namespace OALProgramControl
{
    public class EXECommandMulti : EXECommand
    {
        public List<EXECommand> Commands;

        public EXECommandMulti(List<EXECommand> callCommands)
        {
            this.Commands = callCommands;
        }

        public override EXECommand CreateClone()
        {
            throw new System.NotImplementedException();
        }

        protected override bool Execute(OALProgram OALProgram)
        {
            foreach (EXECommand command in this.Commands)
            {
                if (!command.PerformExecution(OALProgram))
                {
                    return false;
                }
            }

            return true;
        }
    }
}