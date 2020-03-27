using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQueryDeleteTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature", "Creature"}
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_02()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature1"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature", "Creature"},
                { "swamp_creature1", "Creature"}
            };
            int ExpectedValidRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_03()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature", "Creature"},
            };
            int ExpectedValidRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_04()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 3}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature", "Creature"},
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_05()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature", "Creature"},
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_06()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 4}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature", "Creature"},
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_07()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 3}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature1", "Creature"},
                { "swamp_creature2", "Creature"},
                { "swamp_creature3", "Creature"}
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_08()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("SwampCreature");
            Animation.ExecutionSpace.SpawnClass("SwampSlinger");
            Animation.ExecutionSpace.SpawnClass("Tretch");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampCreature", "swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampSlinger", "swamp_creature2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Tretch", "swamp_creature3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature1"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampCreature", 0},
                { "SwampSlinger", 1},
                { "Tretch", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature1", "SwampCreature"},
                { "swamp_creature2", "SwampSlinger"},
                { "swamp_creature3", "Tretch"}
            };
            int ExpectedValidRefVarCount = 2;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_09()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("SwampCreature");
            Animation.ExecutionSpace.SpawnClass("SwampSlinger");
            Animation.ExecutionSpace.SpawnClass("Tretch");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampCreature", "swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampSlinger", "swamp_creature2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Tretch", "swamp_creature3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature1"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampCreature", 0},
                { "SwampSlinger", 0},
                { "Tretch", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature1", "SwampCreature"},
                { "swamp_creature2", "SwampSlinger"},
                { "swamp_creature3", "Tretch"}
            };
            int ExpectedValidRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_10()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("SwampCreature");
            Animation.ExecutionSpace.SpawnClass("SwampSlinger");
            Animation.ExecutionSpace.SpawnClass("Tretch");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampCreature", "swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampSlinger", "swamp_creature2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Tretch", "swamp_creature3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature3"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampCreature", 0},
                { "SwampSlinger", 0},
                { "Tretch", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature1", "SwampCreature"},
                { "swamp_creature2", "SwampSlinger"},
                { "swamp_creature3", "Tretch"}
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_11()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("SwampCreature");
            Animation.ExecutionSpace.SpawnClass("SwampSlinger");
            Animation.ExecutionSpace.SpawnClass("Tretch");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampCreature", "swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampSlinger", "swamp_slinger1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Tretch", "tretch1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_slinger1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("tretch1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampCreature", "swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampSlinger", "swamp_slinger1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Tretch", "tretch1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_slinger1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("tretch1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampCreature", "swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampSlinger", "swamp_slinger1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Tretch", "tretch1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_slinger1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("tretch1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampCreature", "swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampSlinger", "swamp_slinger1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Tretch", "tretch1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_slinger1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("tretch1"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampCreature", 0},
                { "SwampSlinger", 0},
                { "Tretch", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature1", "SwampCreature"},
                { "swamp_slinger1", "SwampSlinger"},
                { "tretch1", "Tretch"}
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature", "Creature"}
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_02()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");

            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_03()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("swamp_creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_04()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");
            Animation.SuperScope.AddVariable(new EXEReferencingSetVariable("x", "Creature"));
            
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("x"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_05()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Creature");
            Animation.SuperScope.AddVariable(new EXEPrimitiveVariable("x", "15"));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("x"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Creature", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_06()
        {
            Animation Animation = new Animation();
            Animation.SuperScope.AddVariable(new EXEPrimitiveVariable("x", "15"));

            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("x"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Creature", "swamp_creature"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_07()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("x"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
    }
}