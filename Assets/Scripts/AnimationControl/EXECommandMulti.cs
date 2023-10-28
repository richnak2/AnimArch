using System.Collections.Generic;
using System.Linq;

namespace OALProgramControl
{
    public class EXECommandMulti : EXECommand
    {
        public List<EXECommand> Commands;
        private int CurrentCommand;

        public EXECommandMulti(List<EXECommand> callCommands)
        {
            this.CurrentCommand = 0;
            this.Commands = callCommands;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandMulti(this.Commands.Select(command => command.CreateClone()).ToList());
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            if (this.CurrentCommand < this.Commands.Count)
            {
                this.CommandStack.Enqueue(Commands[this.CurrentCommand]);
                this.CurrentCommand++;
            }

            return Success();
        }
    }
}