using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AnimationControl
{
    public class EXEThreadSynchronizator
    {
        private object Syncer { get; }
        private int ThreadCount { get; set; }
        private int UntilLockResetCount { get; set; }
        private int InLockCount { get; set; }
        private Boolean blocked { get; set; }
        private List<int> ChangeQueue { get; }
        public EXEThreadSynchronizator()
        {
            this.Syncer = new object();
            this.ThreadCount = 0;
            this.UntilLockResetCount = 0;
            this.InLockCount = 0;
            this.blocked = false;
            this.ChangeQueue = new List<int>();
        }

        public void RegisterThread(uint count)
        {
            lock (this.Syncer)
            {
                ChangeQueue.Add((int)count);
            }
        }
        public void UnregisterThread()
        {
            lock (this.Syncer)
            {
                ChangeQueue.Add(-1);
            }
        }
        public void RequestStep()
        {
            lock (this.Syncer)
            {
                //If the room has been entered previously and has not been emptied yet, we must wait until it's emptied
                while (this.blocked)
                {
                    Monitor.Wait(this.Syncer);
                }

                //If someone changed number of threads, let's do it
                if (this.ChangeQueue.Any())
                {
                    int change = this.ChangeQueue.Sum();
                    this.ThreadCount += change;
                    this.UntilLockResetCount += change;
                    ChangeQueue.Clear();
                }

                //Let us let know everyone of our presence
                this.InLockCount++;
                this.UntilLockResetCount--;
                //If we have to wait for more threads, let's sleep until they come
                while (this.UntilLockResetCount > 0)
                {
                    //If someone changed number of threads, let's do it
                    if (this.ChangeQueue.Any())
                    {
                        int change = this.ChangeQueue.Sum();
                        this.ThreadCount += change;
                        this.UntilLockResetCount += change;
                        ChangeQueue.Clear();
                    }

                    Monitor.Wait(this.Syncer);
                }
                //If we are the last thread to come, let's block the room until everyone leaves and wake everyone up
                if (this.UntilLockResetCount == 0)
                {
                    this.blocked = true;
                    Monitor.PulseAll(this.Syncer);
                }

                //Let know that we are leaving
                this.InLockCount--;
                //If we are the last one leaving, unblock the room and reset counters. And wake up possible sleepers
                if (this.InLockCount == 0)
                {
                    this.blocked = false;
                    this.UntilLockResetCount = this.ThreadCount;
                    Monitor.PulseAll(this.Syncer);
                }
            }
        }
    }
}
