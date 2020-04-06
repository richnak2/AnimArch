﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace AnimationControl
{
    public class EXEScopeParallel : EXEScope
    {
        private List<EXEScope> Threads { get; }
        private object MyThreadEndSyncer { get; }
        private int ActiveThreadCount;
        public EXEScopeParallel() : base()
        {
            this.Threads = new List<EXEScope>();
            this.MyThreadEndSyncer = new object();
            this.ActiveThreadCount = 0;
        }

        public void AddThread(EXEScope Thread)
        {
            this.Threads.Add(Thread);
        }
        public override Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Boolean Success = this.Execute(Animation, Scope);
            return Success;
        }
        public override Boolean Execute(Animation Animation, EXEScope Scope)
        {
            this.Animation = Animation;
            this.Animation.ThreadSyncer.RegisterThread((uint)this.Threads.Count);
            EXEScopeParallel ParallelScope = this;
            Boolean Success = true;


            lock (this.MyThreadEndSyncer)
            {
                this.ActiveThreadCount = this.Threads.Count;
            }

            foreach (EXEScope ThreadScope in this.Threads)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = false;

                    Boolean MySuccess = ThreadScope.SynchronizedExecute(Animation, null);

                    Animation.ThreadSyncer.UnregisterThread();

                    lock (ParallelScope.MyThreadEndSyncer)
                    {
                        Success &= MySuccess;
                        ParallelScope.ActiveThreadCount--;
                        if (ParallelScope.ActiveThreadCount == 0)
                        {
                            Monitor.PulseAll(this.MyThreadEndSyncer);
                        }
                    }
                }).Start();
            }

            lock (this.MyThreadEndSyncer)
            {
                while (this.ActiveThreadCount > 0)
                {
                    Monitor.Wait(this.MyThreadEndSyncer);
                }
            }

            return Success;
        }
        public override bool PropagateControlCommand(LoopControlStructure PropagatedCommand)
        {
            return false;
        }
    }
}