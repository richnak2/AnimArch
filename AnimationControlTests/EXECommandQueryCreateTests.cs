using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace OALProgramControl.Tests
{
    [TestClass]
    public class EXECommandQueryCreateTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_02()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Form");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 0},
                { "Form", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_03()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Form");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 3},
                { "Subject", 1},
                { "Form", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"}
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_04()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Form");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 0},
                { "Form", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_05()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Form");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 0},
                { "Form", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_06()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Form");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5},
                { "Subject", 0},
                { "Form", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_07()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Form");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5},
                { "Subject", 3},
                { "Form", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "s1", "Subject"}
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_08()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");          

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 2}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Bad_02()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Bad_03()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.SuperScope.AddVariable(new EXEPrimitiveVariable("s", "12"));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Bad_04()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Form");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("ConcreteObserver"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Form"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 4},
                { "Subject", 3},
                { "Form", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "s1", "Subject"}
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Bad_05()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "s"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Subject", 1},
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "s", "Subject"}
            };

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
    }
}