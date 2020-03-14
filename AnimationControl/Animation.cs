using System.Threading;

namespace AnimationControl
{
    public class Animation
    {
        public CDClassPool ExecutionSpace { get; }
        public CDRelationshipPool RelationshipSpace { get; }
        public EXEScope SuperScope { get; }
        private int AllowedStepCount { get; set; }
        private bool ContinuousExecution { get; set; }

        private readonly object StepCountLock = new object();

        public readonly EXEThreadSynchronizator ThreadSyncer = new EXEThreadSynchronizator();
        public Animation()
        {
            this.ExecutionSpace = new CDClassPool();
            this.RelationshipSpace = new CDRelationshipPool();
            this.SuperScope = new EXEScope();

            this.AllowedStepCount = int.MaxValue;
            this.ContinuousExecution = true;
        }

        public bool Execute()
        {
            bool Result = false;

            Result = this.SuperScope.Execute(this, null);

            return Result;
        }

        public void RequestNextStep()
        {
            lock (StepCountLock)
            {
                if (!ContinuousExecution)
                {
                    while (AllowedStepCount <= 0)
                    {
                        Monitor.Wait(StepCountLock);
                    }
                    --AllowedStepCount;
                }
            }
        }
        public void PermitNextStep()
        {
            lock (StepCountLock)
            {
                ++AllowedStepCount;
                Monitor.PulseAll(StepCountLock);
            }
        }
        public void ToggleContinuousExecution()
        {
            lock (StepCountLock)
            {
                ContinuousExecution = !ContinuousExecution;
                if (!ContinuousExecution)
                {
                    AllowedStepCount = 0;
                }
            }
        }
    }
}
