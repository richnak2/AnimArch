using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQueryCreateTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_02()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Form");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_03()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Form");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_04()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Form");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 0},
                { "Form", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_05()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Form");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 0},
                { "Form", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_06()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Form");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5},
                { "Subject", 0},
                { "Form", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_07()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Form");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Normal_08()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");          

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 2}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Bad_02()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Bad_03()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.SuperScope.AddVariable(new EXEPrimitiveVariable("s", "12"));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Bad_04()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Form");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("ConcreteObserver"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Form"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
        [TestMethod]
        public void Execute_Bad_05()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "s"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Subject", 1},
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "s", "Subject"}
            };

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
        }
    }
}