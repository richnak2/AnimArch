namespace AnimationControl
{
    class Animation
    {
        public CDClassPool ExecutionSpace { get; }
        public CDRelationshipPool RelationshipSpace { get; }
        public EXEScope SuperScope { get; }
        private int AllowedStepCount { get; set; }
        private bool ContinuousExecution { get; set; }

        private readonly object StepCountLock = new object();
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

            //Result = this.SuperScope.Execute(this, null);

            return Result;
        }

        public void RequestExecution()
        {
            lock (StepCountLock)
            {
                if (!ContinuousExecution)
                {
                    --AllowedStepCount;
                }
            }
        }
        public void PermitNextStep()
        {
            lock (StepCountLock)
            {
                ++AllowedStepCount;
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
