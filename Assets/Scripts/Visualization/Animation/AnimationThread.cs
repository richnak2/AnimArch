using OALProgramControl;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Visualization.Animation
{
    public class AnimationThread
    {
        private readonly EXEExecutionStack CommandStack;
        private readonly OALProgram CurrentProgramInstance;
        private readonly Animation Animation;
        public EXEExecutionResult ExecutionSuccess;
        protected AnimationThread ParentThread;
        protected readonly List<AnimationThread> ChildThreads;

        protected bool IsOver;

        public AnimationThread(EXEExecutionStack executionStack, OALProgram currentProgramInstance, Animation animation)
        {
            this.CommandStack = executionStack;
            this.CurrentProgramInstance = currentProgramInstance;
            this.Animation = animation;
            this.ExecutionSuccess = EXEExecutionResult.Success();
            this.IsOver = false;
            this.ParentThread = null;
            this.ChildThreads = new List<AnimationThread>();
        }

        public IEnumerator Start()
        {
            int i = 0;
            while (ExecutionSuccess.IsSuccess && CommandStack.HasNext())
            {
                EXECommand CurrentCommand = CommandStack.Next();
                ExecutionSuccess = CurrentCommand.PerformExecution(CurrentProgramInstance);

                Debug.Log("Command " + i++ + ExecutionSuccess.ToString());

                if (!ExecutionSuccess.IsSuccess)
                {
                    ShowError(ExecutionSuccess);
                    break;
                }

                if (!ExecutionSuccess.IsDone)
                {
                    continue;
                }

                CurrentCommand.ToggleActiveRecursiveBottomUp(true);

                if (!(CurrentCommand is EXECommandMulti))
                {
                    EXEScopeMethod CurrentMethodScope = CurrentCommand.GetCurrentMethodScope();

                    UI.MenuManager.Instance.AnimateSourceCodeAtMethodStart(CurrentMethodScope);
                }
                yield return Animation.AnimateCommand(CurrentCommand, this);

                CurrentCommand.ToggleActiveRecursiveBottomUp(false);

                if (CurrentCommand is EXEScopeParallel)
                {
                    Fork(CurrentCommand as EXEScopeParallel);
                    yield return Join();
                }
            }

            IsOver = true;
        }

        private void Fork(EXEScopeParallel forkingCommand)
        {
            foreach (EXEScope threadScope in forkingCommand.Threads)
            {
                EXEExecutionStack ChildThreadExecutionStack = new EXEExecutionStack();
                ChildThreadExecutionStack.Enqueue(threadScope);
                threadScope.CommandStack = ChildThreadExecutionStack;

                AnimationThread ChildThread = new AnimationThread(ChildThreadExecutionStack, CurrentProgramInstance, Animation);
                this.ChildThreads.Add(ChildThread);
                ChildThread.ParentThread = this;

                Animation.StartCoroutine(ChildThread.Start());
            }
        }

        private IEnumerator Join()
        {
            yield return new WaitUntil(() => !ChildThreads.Any(thread => !thread.IsOver));
        }

        private void ShowError(EXEExecutionResult executionSuccess)
        {
            UI.MenuManager.Instance.ShowErrorPanel(executionSuccess);
        }
    }
}