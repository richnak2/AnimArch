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

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEExecutionResult result;

            foreach (EXECommand command in this.Commands)
            {
                result = command.PerformExecution(OALProgram);

                if (!result.IsSuccess)
                {
                    result.OwningCommand = command;
                    return result;
                }
            }

            return Success();
        }
    }
}