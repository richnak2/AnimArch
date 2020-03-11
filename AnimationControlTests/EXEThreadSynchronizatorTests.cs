using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Threading;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEThreadSynchronizatorTests
    {
        [TestMethod]
        public void EXEThreadSynchronizatorTest()
        {
            EXEThreadSynchronizator Syncer = new EXEThreadSynchronizator();
            Syncer.RegisterThread(3);

            object Locker = new object();
            string ActualResult = "";
            object OverLocker = new object();
            int endedCount = 0;
            new Thread(() => { for (int i = 0; i < 1; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "A"; }} lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();
            new Thread(() => { for (int i = 0; i < 1; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "B"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();
            new Thread(() => { for (int i = 0; i < 1; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "C"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();

            lock (OverLocker)
            {
                while (endedCount != 0)
                {
                    Monitor.Wait(OverLocker);
                }
            }

            StringAssert.Matches(ActualResult, new Regex(@"", RegexOptions.Compiled | RegexOptions.IgnoreCase));
        }
    }
}