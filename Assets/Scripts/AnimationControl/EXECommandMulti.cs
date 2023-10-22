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
            EXEExecutionResult commandExecutionResult;

            while (this.CurrentCommand < this.Commands.Count)
            {
                commandExecutionResult = this.Commands[this.CurrentCommand].PerformExecution(OALProgram);

                if (!HandleSingleShotASTEvaluation(commandExecutionResult))
                {
                    return commandExecutionResult;
                }

                this.CurrentCommand++;
            }

            return Success();
        }
    }
}