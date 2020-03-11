using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEThreadSynchronizatorTests
    {
        [TestMethod]
        public void EXEThreadSynchronizatorTest_01()
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
                while (endedCount != 3)
                {
                    Monitor.Wait(OverLocker);
                }
            }

            StringAssert.Matches(ActualResult, new Regex(@"^(?:([ABC])(?!\1)([ABC])(?!\1)(?!\2)[ABC])*$", RegexOptions.Compiled | RegexOptions.IgnoreCase));
        }
        [TestMethod]
        public void EXEThreadSynchronizatorTest_02()
        {
            EXEThreadSynchronizator Syncer = new EXEThreadSynchronizator();
            Syncer.RegisterThread(3);

            object Locker = new object();
            string ActualResult = "";
            object OverLocker = new object();
            int endedCount = 0;
            new Thread(() => { for (int i = 0; i < 15; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "A"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();
            new Thread(() => { for (int i = 0; i < 15; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "B"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();
            new Thread(() => { for (int i = 0; i < 15; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "C"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();

            lock (OverLocker)
            {
                while (endedCount != 3)
                {
                    Monitor.Wait(OverLocker);
                }
            }

            StringAssert.Matches(ActualResult, new Regex(@"^(?:([ABC])(?!\1)([ABC])(?!\1)(?!\2)[ABC])*$", RegexOptions.Compiled | RegexOptions.IgnoreCase));
        }
        [TestMethod]
        public void EXEThreadSynchronizatorTest_03()
        {
            EXEThreadSynchronizator Syncer = new EXEThreadSynchronizator();
            Syncer.RegisterThread(3);

            object Locker = new object();
            string ActualResult = "";
            object OverLocker = new object();
            int endedCount = 0;
            new Thread(() => { for (int i = 0; i < 100; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "A"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();
            new Thread(() => { for (int i = 0; i < 100; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "B"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();
            new Thread(() => { for (int i = 0; i < 100; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "C"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();

            lock (OverLocker)
            {
                while (endedCount != 3)
                {
                    Monitor.Wait(OverLocker);
                }
            }

            StringAssert.Matches(ActualResult, new Regex(@"^(?:([ABC])(?!\1)([ABC])(?!\1)(?!\2)[ABC])*$", RegexOptions.Compiled | RegexOptions.IgnoreCase));
        }
        [TestMethod]
        public void EXEThreadSynchronizatorTest_04()
        {
            EXEThreadSynchronizator Syncer = new EXEThreadSynchronizator();
            Syncer.RegisterThread(3);

            object Locker = new object();
            string ActualResult = "";
            object OverLocker = new object();
            int endedCount = 0;
            new Thread(() => { for (int i = 0; i < 10000; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "A"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();
            new Thread(() => { for (int i = 0; i < 10000; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "B"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();
            new Thread(() => { for (int i = 0; i < 10000; i++) { Syncer.RequestStep(); lock (Locker) { ActualResult += "C"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();

            lock (OverLocker)
            {
                while (endedCount != 3)
                {
                    Monitor.Wait(OverLocker);
                }
            }

            StringAssert.Matches(ActualResult, new Regex(@"^(?:([ABC])(?!\1)([ABC])(?!\1)(?!\2)[ABC])*$", RegexOptions.Compiled | RegexOptions.IgnoreCase));
        }
        [TestMethod]
        public void EXEThreadSynchronizatorTest_05()
        {
            EXEThreadSynchronizator Syncer = new EXEThreadSynchronizator();
            Syncer.RegisterThread(3);

            object Locker = new object();
            string ActualResult = "";
            object OverLocker = new object();
            int endedCount = 0;
            new Thread(() => { for (int i = 0; i < 100000; i++) { Console.Write("A" + i + "\n");  Syncer.RequestStep(); lock (Locker) { ActualResult += "A"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();
            new Thread(() => { for (int i = 0; i < 100000; i++) { Console.Write("B" + i + "\n");  Syncer.RequestStep(); lock (Locker) { ActualResult += "B"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();
            new Thread(() => { for (int i = 0; i < 100000; i++) { Console.Write("C" + i + "\n");  Syncer.RequestStep(); lock (Locker) { ActualResult += "C"; } } lock (OverLocker) { endedCount++; Monitor.Pulse(OverLocker); } }).Start();

            lock (OverLocker)
            {
                while (endedCount != 3)
                {
                    Monitor.Wait(OverLocker);
                }
            }

            StringAssert.Matches(ActualResult, new Regex(@"^(?:([ABC])(?!\1)([ABC])(?!\1)(?!\2)[ABC])*$", RegexOptions.Compiled | RegexOptions.IgnoreCase));
        }
    }
}