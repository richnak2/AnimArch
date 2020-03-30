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
        public void Execute_Normal_12()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("SwampCreature");
            Animation.ExecutionSpace.SpawnClass("SwampSlinger");
            Animation.ExecutionSpace.SpawnClass("Tretch");

            List<EXECommandQueryCreate> CreateCommands = new List<EXECommandQueryCreate>( new EXECommandQueryCreate[] {
                new EXECommandQueryCreate("SwampCreature", "swamp_creature1"),
                new EXECommandQueryCreate("SwampCreature", "swamp_creature2"),
                new EXECommandQueryCreate("SwampCreature", "swamp_creature3"),
                new EXECommandQueryCreate("SwampCreature", "swamp_creature4"),
                new EXECommandQueryCreate("SwampCreature", "swamp_creature5"),
                new EXECommandQueryCreate("SwampCreature", "swamp_creature6"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger1"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger2"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger3"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger4"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger5"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger6"),
                new EXECommandQueryCreate("Tretch", "tretch1"),
                new EXECommandQueryCreate("Tretch", "tretch2"),
                new EXECommandQueryCreate("Tretch", "tretch3"),
                new EXECommandQueryCreate("Tretch", "tretch4"),
                new EXECommandQueryCreate("Tretch", "tretch5"),
                new EXECommandQueryCreate("Tretch", "tretch6")
            });

            List<EXECommandQueryDelete> DeleteCommands = new List<EXECommandQueryDelete>(new EXECommandQueryDelete[] {
                new EXECommandQueryDelete("swamp_creature1"),
                new EXECommandQueryDelete("swamp_creature2"),
                new EXECommandQueryDelete("swamp_creature3"),
                new EXECommandQueryDelete("swamp_creature4"),
                new EXECommandQueryDelete("swamp_creature5"),
                new EXECommandQueryDelete("swamp_creature6"),
                new EXECommandQueryDelete("swamp_slinger1"),
                new EXECommandQueryDelete("swamp_slinger2"),
                new EXECommandQueryDelete("swamp_slinger3"),
                new EXECommandQueryDelete("swamp_slinger4"),
                new EXECommandQueryDelete("swamp_slinger5"),
                new EXECommandQueryDelete("swamp_slinger6"),
                new EXECommandQueryDelete("tretch1"),
                new EXECommandQueryDelete("tretch2"),
                new EXECommandQueryDelete("tretch3"),
                new EXECommandQueryDelete("tretch4"),
                new EXECommandQueryDelete("tretch5"),
                new EXECommandQueryDelete("tretch6")
            });

            for (int i = 0; i < 1000; i++)
            {
                TestUtil.ShuffleList<EXECommandQueryCreate>(CreateCommands);
                TestUtil.ShuffleList<EXECommandQueryDelete>(DeleteCommands);

                foreach (EXECommandQueryCreate Command in CreateCommands)
                {
                    Animation.SuperScope.AddCommand(Command);
                }
                foreach (EXECommandQueryDelete Command in DeleteCommands)
                {
                    Animation.SuperScope.AddCommand(Command);
                }
            }
            
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
                { "swamp_creature2", "SwampCreature"},
                { "swamp_creature3", "SwampCreature"},
                { "swamp_creature4", "SwampCreature"},
                { "swamp_creature5", "SwampCreature"},
                { "swamp_creature6", "SwampCreature"},
                { "swamp_slinger1", "SwampSlinger"},
                { "swamp_slinger2", "SwampSlinger"},
                { "swamp_slinger3", "SwampSlinger"},
                { "swamp_slinger4", "SwampSlinger"},
                { "swamp_slinger5", "SwampSlinger"},
                { "swamp_slinger6", "SwampSlinger"},
                { "tretch1", "Tretch"},
                { "tretch2", "Tretch"},
                { "tretch3", "Tretch"},
                { "tretch4", "Tretch"},
                { "tretch5", "Tretch"},
                { "tretch6", "Tretch"}
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
        public void Execute_Normal_13()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("SwampCreature");
            Animation.ExecutionSpace.SpawnClass("SwampSlinger");
            Animation.ExecutionSpace.SpawnClass("Tretch");

            List<EXECommandQueryCreate> CreateCommands = new List<EXECommandQueryCreate>(new EXECommandQueryCreate[] {
                new EXECommandQueryCreate("SwampCreature", "swamp_creature1"),
                new EXECommandQueryCreate("SwampCreature", "swamp_creature2"),
                new EXECommandQueryCreate("SwampCreature", "swamp_creature3"),
                new EXECommandQueryCreate("SwampCreature", "swamp_creature4"),
                new EXECommandQueryCreate("SwampCreature", "swamp_creature5"),
                new EXECommandQueryCreate("SwampCreature", "swamp_creature6"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger1"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger2"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger3"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger4"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger5"),
                new EXECommandQueryCreate("SwampSlinger", "swamp_slinger6"),
                new EXECommandQueryCreate("Tretch", "tretch1"),
                new EXECommandQueryCreate("Tretch", "tretch2"),
                new EXECommandQueryCreate("Tretch", "tretch3"),
                new EXECommandQueryCreate("Tretch", "tretch4"),
                new EXECommandQueryCreate("Tretch", "tretch5"),
                new EXECommandQueryCreate("Tretch", "tretch6")
            });

            List<EXECommandQueryDelete> DeleteCommands = new List<EXECommandQueryDelete>(new EXECommandQueryDelete[] {
                new EXECommandQueryDelete("swamp_creature1"),
                new EXECommandQueryDelete("swamp_creature2"),
                new EXECommandQueryDelete("swamp_creature3"),
                new EXECommandQueryDelete("swamp_creature4"),
                new EXECommandQueryDelete("swamp_creature5"),
                new EXECommandQueryDelete("swamp_creature6"),
                new EXECommandQueryDelete("swamp_slinger1"),
                new EXECommandQueryDelete("swamp_slinger2"),
                new EXECommandQueryDelete("swamp_slinger3"),
                new EXECommandQueryDelete("swamp_slinger4"),
                new EXECommandQueryDelete("swamp_slinger5"),
                new EXECommandQueryDelete("swamp_slinger6"),
                new EXECommandQueryDelete("tretch1"),
                new EXECommandQueryDelete("tretch2"),
                new EXECommandQueryDelete("tretch3"),
                new EXECommandQueryDelete("tretch4"),
                new EXECommandQueryDelete("tretch5"),
                new EXECommandQueryDelete("tretch6")
            });

            for (int i = 0; i < 1000; i++)
            {
                TestUtil.ShuffleList<EXECommandQueryCreate>(CreateCommands);
                TestUtil.ShuffleList<EXECommandQueryDelete>(DeleteCommands);

                foreach (EXECommandQueryCreate Command in CreateCommands)
                {
                    Animation.SuperScope.AddCommand(Command);
                }
                foreach (EXECommandQueryDelete Command in DeleteCommands)
                {
                    Animation.SuperScope.AddCommand(Command);
                }
            }
            TestUtil.ShuffleList<EXECommandQueryCreate>(CreateCommands);
            foreach (EXECommandQueryCreate Command in CreateCommands)
            {
                Animation.SuperScope.AddCommand(Command);
            }

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampCreature", 6},
                { "SwampSlinger", 6},
                { "Tretch", 6}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_creature1", "SwampCreature"},
                { "swamp_creature2", "SwampCreature"},
                { "swamp_creature3", "SwampCreature"},
                { "swamp_creature4", "SwampCreature"},
                { "swamp_creature5", "SwampCreature"},
                { "swamp_creature6", "SwampCreature"},
                { "swamp_slinger1", "SwampSlinger"},
                { "swamp_slinger2", "SwampSlinger"},
                { "swamp_slinger3", "SwampSlinger"},
                { "swamp_slinger4", "SwampSlinger"},
                { "swamp_slinger5", "SwampSlinger"},
                { "swamp_slinger6", "SwampSlinger"},
                { "tretch1", "Tretch"},
                { "tretch2", "Tretch"},
                { "tretch3", "Tretch"},
                { "tretch4", "Tretch"},
                { "tretch5", "Tretch"},
                { "tretch6", "Tretch"}
            };
            int ExpectedValidRefVarCount = 18;

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