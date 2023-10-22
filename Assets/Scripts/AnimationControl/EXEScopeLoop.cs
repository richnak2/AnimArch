using System.Linq;

namespace OALProgramControl
{
    public abstract class EXEScopeLoop : EXEScope
    {
        protected abstract EXEExecutionResult HandleIterationStart(out bool startNewIteration);
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            bool startNewIteration;
            EXEExecutionResult iterationStartResult = HandleIterationStart(out startNewIteration);

            // If it is null, there is no need for further evaluation
            if (iterationStartResult != null && !HandleRepeatableASTEvaluation(iterationStartResult))
            {
                return iterationStartResult;
            }

            if (startNewIteration)
            {
                OALProgram.CommandStack.Enqueue(this);
                AddCommandsToStack
                (
                    this.Commands.Select(command => command.CreateClone()).ToList()
                );
                this.ClearVariables();
            }

            return Success();
        }
    }
}