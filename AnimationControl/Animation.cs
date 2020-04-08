using System;
using System.Threading;

namespace AnimationControl
{
    public class Animation
    {
        public CDClassPool ExecutionSpace { get; }
        public CDRelationshipPool RelationshipSpace { get; }
        private bool InDatabase;
        private readonly object InstanceDatabaseLock = new object();

        public EXEScope SuperScope { get; }
        private int AllowedStepCount { get; set; }
        private bool ContinuousExecution { get; set; }

        private readonly object StepCountLock = new object();

        public EXEThreadSynchronizator ThreadSyncer { get; }

        public EXETestGUI Visualizer = new EXETestGUIImplementation();
        
        private int command_counter = 0;

        public int HighlightDuration { get; set; }
        public Animation(int HighlightDuration = 5)
        {
            this.ExecutionSpace = new CDClassPool();
            this.RelationshipSpace = new CDRelationshipPool();
            this.SuperScope = new EXEScope();

            this.AllowedStepCount = int.MaxValue;
            this.ContinuousExecution = true;
            this.ThreadSyncer = new EXEThreadSynchronizator(this);

            this.InDatabase = false;

            this.HighlightDuration = HighlightDuration;
        }

        public void AccessInstanceDatabase()
        {
            Console.WriteLine("Accessing Instance DB");
            lock (this.InstanceDatabaseLock)
            {
                
                while (this.InDatabase)
                {
                    Monitor.Wait(this.InstanceDatabaseLock);
                }
                this.InDatabase = true;
            }
            Console.WriteLine("Accessed Instance DB");
            //Console.WriteLine("Executing command no. " + ++this.command_counter);
        }
        public void LeaveInstanceDatabase()
        {
            Console.WriteLine("Leaving Instance DB");
            lock (this.InstanceDatabaseLock)
            {
                Monitor.PulseAll(this.InstanceDatabaseLock);
                this.InDatabase = false;
            }
            Console.WriteLine("Left Instance DB");
        }
        public bool Execute()
        {
            bool Result = false;

            this.ThreadSyncer.RegisterThread(1);
            Result = this.SuperScope.SynchronizedExecute(this, null);

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
                Monitor.PulseAll(StepCountLock);
            }
        }
    }
}
