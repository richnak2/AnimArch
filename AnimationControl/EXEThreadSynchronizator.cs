using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OALProgramControl
{
    public class EXEThreadSynchronizator
    {
        private object Syncer { get; }
        private int ThreadCount { get; set; }
        private int UntilLockResetCount { get; set; }
        private int InLockCount { get; set; }
        private Boolean Blocked { get; set; }
        private int Change { get; set; }

        private OALProgram OALProgram { get; }
        public EXEThreadSynchronizator(OALProgram OALProgram)
        {
            this.Syncer = new object();
            this.ThreadCount = 0;
            this.UntilLockResetCount = 0;
            this.InLockCount = 0;
            this.Blocked = false;
            this.Change = 0;
            this.OALProgram = OALProgram;
        }

        public void RegisterThread(uint count)
        {
            lock (this.Syncer)
            {
                this.Change += (int)count;
            }
        }
        public void UnregisterThread()
        {
            lock (this.Syncer)
            {
                this.Change -= 1;
                Monitor.PulseAll(this.Syncer);
            }
        }
        public void RequestStep()
        {
            lock (this.Syncer)
            {
                //If the room has been entered previously and has not been emptied yet, we must wait until it's emptied
                while (this.Blocked)
                {
                    Monitor.Wait(this.Syncer);
                }

                //If someone changed number of threads, let's do it
                PerformThreadCountChange();

                //Let us let know everyone of our presence
                this.InLockCount++;
                this.UntilLockResetCount--;
                //If we have to wait for more threads, let's sleep until they come
                while (this.UntilLockResetCount > 0)
                {
                    //If someone changed number of threads, let's do it
                    PerformThreadCountChange();
                    Monitor.Wait(this.Syncer);
                }
                //If we are the last thread to come, let's block the room until everyone leaves and wake everyone up
                if (this.UntilLockResetCount == 0)
                {
                    this.Blocked = true;
                    OALProgram.RequestNextStep();
                    Monitor.PulseAll(this.Syncer);
                }

                //Let know that we are leaving
                this.InLockCount--;
                //If we are the last one leaving, unblock the room and reset counters. And wake up possible sleepers
                if (this.InLockCount == 0)
                {
                    this.Blocked = false;
                    this.UntilLockResetCount = this.ThreadCount;
                    Monitor.PulseAll(this.Syncer);
                }
            }
        }

        private void PerformThreadCountChange()
        {
            if (this.Change != 0)
            {
                this.ThreadCount += this.Change;
                this.UntilLockResetCount += this.Change;
                this.Change = 0;
            }

            if (this.ThreadCount < 0 || this.UntilLockResetCount < 0)
            {
                throw new Exception();
            }
        }
    }
}
