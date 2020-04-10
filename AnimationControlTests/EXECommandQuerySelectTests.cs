using Microsoft.VisualStudio.TestTools.UnitTesting;
using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace OALProgramControl.Tests
{
    [TestClass]
    public class EXECommandQuerySelectTests
    {
        [TestMethod]
        public void Execute_Normal_Any_01()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "o"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "o");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_02()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "SwampMonster", "swamp_monster1"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster1", "SwampMonster"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster1.health", EXETypes.UnitializedName},
                { "swamp_monster1.max_health", EXETypes.UnitializedName},
                { "swamp_monster1.mana", EXETypes.UnitializedName},
                { "swamp_monster1.max_mana", EXETypes.UnitializedName}
            };
            int ExpectedValidRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "swamp_monster1");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_03()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm", "health", new EXEASTNodeLeaf("50")));
            OALProgram.SuperScope.AddCommand(
                new EXECommandQuerySelect(
                    EXECommandQuerySelect.CardinalityAny,
                    "SwampMonster",
                    "swamp_monster1",
                    new EXEASTNodeComposite(
                        "==",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ".",
                                new EXEASTNode []
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("health")
                                }
                            ),
                            new EXEASTNodeLeaf("50")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster1", "SwampMonster"},
                { "sm", "SwampMonster"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster1.health", "50.0"},
                { "swamp_monster1.max_health", EXETypes.UnitializedName},
                { "swamp_monster1.mana", EXETypes.UnitializedName},
                { "swamp_monster1.max_mana", EXETypes.UnitializedName}
            };
            int ExpectedValidRefVarCount = 2;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "swamp_monster1");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            foreach (var i in ActualCreatedVarState) Console.WriteLine(i.Key + ":" + i.Value);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_04()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm", "health", new EXEASTNodeLeaf("50")));
            OALProgram.SuperScope.AddCommand(
                new EXECommandQuerySelect(
                    EXECommandQuerySelect.CardinalityAny,
                    "SwampMonster",
                    "swamp_monster1",
                    new EXEASTNodeComposite(
                        ">",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ".",
                                new EXEASTNode []
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("health")
                                }
                            ),
                            new EXEASTNodeLeaf("50")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster1", "SwampMonster"},
                { "sm", "SwampMonster"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "swamp_monster1");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_05()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm", "health", new EXEASTNodeLeaf("50")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm", "mana", new EXEASTNodeLeaf("10")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm1"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm1", "health", new EXEASTNodeLeaf("60")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm1", "mana", new EXEASTNodeLeaf("20")));
            OALProgram.SuperScope.AddCommand(
                new EXECommandQuerySelect(
                    EXECommandQuerySelect.CardinalityAny,
                    "SwampMonster",
                    "swamp_monster",
                    new EXEASTNodeComposite(
                        "==",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ".",
                                new EXEASTNode []
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("health")
                                }
                            ),
                            new EXEASTNodeLeaf("50")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 2}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                { "sm", "SwampMonster"},
                { "sm1", "SwampMonster"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "50.0"},
                { "swamp_monster.max_health", EXETypes.UnitializedName},
                { "swamp_monster.mana", "10.0"},
                { "swamp_monster.max_mana", EXETypes.UnitializedName}
            };
            int ExpectedValidRefVarCount = 3;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_06()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm", "health", new EXEASTNodeLeaf("50")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm", "mana", new EXEASTNodeLeaf("10")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm1"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm1", "health", new EXEASTNodeLeaf("60")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm1", "mana", new EXEASTNodeLeaf("20")));
            OALProgram.SuperScope.AddCommand(
                new EXECommandQuerySelect(
                    EXECommandQuerySelect.CardinalityAny,
                    "SwampMonster",
                    "swamp_monster",
                    new EXEASTNodeComposite(
                        "==",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ".",
                                new EXEASTNode []
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("health")
                                }
                            ),
                            new EXEASTNodeLeaf("60")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 2}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                { "sm", "SwampMonster"},
                { "sm1", "SwampMonster"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "60.0"},
                { "swamp_monster.max_health", EXETypes.UnitializedName},
                { "swamp_monster.mana", "20.0"},
                { "swamp_monster.max_mana", EXETypes.UnitializedName}
            };
            int ExpectedValidRefVarCount = 3;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_07()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));

            for (int i = 1; i <= 5000; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            OALProgram.SuperScope.AddCommand(
                new EXECommandQuerySelect(
                    EXECommandQuerySelect.CardinalityAny,
                    "SwampMonster",
                    "swamp_monster",
                    new EXEASTNodeComposite(
                        "==",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ".",
                                new EXEASTNode []
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("health")
                                }
                            ),
                            new EXEASTNodeLeaf("5050")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5000}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "5050.0"},
                { "swamp_monster.mana", "5010.0"}
            };
            int ExpectedValidRefVarCount = 5001;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_08()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));

            for (int i = 1; i <= 5000; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            OALProgram.SuperScope.AddCommand(
                new EXECommandQuerySelect(
                    EXECommandQuerySelect.CardinalityAny,
                    "SwampMonster",
                    "swamp_monster",
                    new EXEASTNodeComposite(
                        "==",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ".",
                                new EXEASTNode []
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("health")
                                }
                            ),
                            new EXEASTNodeLeaf("3050")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5000}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "3050.0"},
                { "swamp_monster.mana", "3010.0"}
            };
            int ExpectedValidRefVarCount = 5001;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_09()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));

            for (int i = 1; i <= 5000; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));

                if (i == 2950)
                {
                    OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    OALProgram.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    OALProgram.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                }
            }
            OALProgram.SuperScope.AddCommand(
                new EXECommandQuerySelect(
                    EXECommandQuerySelect.CardinalityAny,
                    "SwampMonster",
                    "swamp_monster",
                    new EXEASTNodeComposite(
                        ">=",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ".",
                                new EXEASTNode []
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("health")
                                }
                            ),
                            new EXEASTNodeLeaf("12000")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5001}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                { "smx", "SwampMonster"}
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "12000.0"},
                { "swamp_monster.mana", "5.0"}
            };
            int ExpectedValidRefVarCount = 5002;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_10()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));

            for (int i = 1; i <= 5000; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));

                if (i == 2950)
                {
                    OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    OALProgram.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    OALProgram.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                }
            }
            OALProgram.SuperScope.AddCommand(
                new EXECommandQuerySelect(
                    EXECommandQuerySelect.CardinalityAny,
                    "SwampMonster",
                    "swamp_monster",
                    new EXEASTNodeComposite(
                        "and",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ">",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeComposite(
                                        ".",
                                        new EXEASTNode []
                                        {
                                            new EXEASTNodeLeaf("selected"),
                                            new EXEASTNodeLeaf("health")
                                        }
                                    ),
                                    new EXEASTNodeLeaf("10000")
                                }
                            ),
                            new EXEASTNodeComposite(
                                "<",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeComposite(
                                        ".",
                                        new EXEASTNode []
                                        {
                                            new EXEASTNodeLeaf("selected"),
                                            new EXEASTNodeLeaf("mana")
                                        }
                                    ),
                                    new EXEASTNodeLeaf("10")
                                }
                            )
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5001}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                { "smx", "SwampMonster"}
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "12000.0"},
                { "swamp_monster.mana", "5.0"}
            };
            int ExpectedValidRefVarCount = 5002;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_11()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("stunned", EXETypes.BooleanTypeName));

            Random rand = new Random();
            for (int i = 1; i <= 5000; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "stunned", new EXEASTNodeLeaf(rand.NextDouble() >= 0.5 ? EXETypes.BooleanTrue : EXETypes.BooleanFalse)));

                if (i == 2950)
                {
                    OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    OALProgram.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    OALProgram.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                    OALProgram.SuperScope.AddCommand(new EXECommandAssignment("smx", "stunned", new EXEASTNodeLeaf(EXETypes.BooleanFalse)));

                }
            }
            OALProgram.SuperScope.AddCommand(
                new EXECommandQuerySelect(
                    EXECommandQuerySelect.CardinalityAny,
                    "SwampMonster",
                    "swamp_monster",
                    new EXEASTNodeComposite(
                        "and",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ">",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeComposite(
                                        ".",
                                        new EXEASTNode []
                                        {
                                            new EXEASTNodeLeaf("selected"),
                                            new EXEASTNodeLeaf("health")
                                        }
                                    ),
                                    new EXEASTNodeLeaf("10000")
                                }
                            ),
                            new EXEASTNodeComposite(
                                "<",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeComposite(
                                        ".",
                                        new EXEASTNode []
                                        {
                                            new EXEASTNodeLeaf("selected"),
                                            new EXEASTNodeLeaf("mana")
                                        }
                                    ),
                                    new EXEASTNodeLeaf("10")
                                }
                            ),
                            new EXEASTNodeComposite(
                                "not",
                                new EXEASTNode []
                                {
                                    new EXEASTNodeComposite(
                                        ".",
                                        new EXEASTNode []
                                        {
                                            new EXEASTNodeLeaf("selected"),
                                            new EXEASTNodeLeaf("stunned")
                                        }
                                    )
                                }
                            )
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5001}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                { "smx", "SwampMonster"}
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "12000.0"},
                { "swamp_monster.mana", "5.0"},
                { "swamp_monster.stunned", EXETypes.BooleanFalse}
            };
            int ExpectedValidRefVarCount = 5002;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_12()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class1 = OALProgram.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("stunned", EXETypes.BooleanTypeName));

            Random rand = new Random();
            for (int i = 1; i <= 5000; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "stunned", new EXEASTNodeLeaf(rand.NextDouble() >= 0.5 ? EXETypes.BooleanTrue : EXETypes.BooleanFalse)));

                if (i == 2950)
                {
                    OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    OALProgram.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    OALProgram.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                    OALProgram.SuperScope.AddCommand(new EXECommandAssignment("smx", "stunned", new EXEASTNodeLeaf(EXETypes.BooleanFalse)));

                }
            }
            OALProgram.SuperScope.AddCommand(
                new EXECommandQuerySelect(
                    EXECommandQuerySelect.CardinalityAny,
                    "SwampMonster",
                    "swamp_monster",
                    new EXEASTNodeComposite(
                        "not",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ">",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeComposite(
                                        ".",
                                        new EXEASTNode []
                                        {
                                            new EXEASTNodeLeaf("selected"),
                                            new EXEASTNodeLeaf("health")
                                        }
                                    ),
                                    new EXEASTNodeLeaf("10000")
                                }
                            )
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5001}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                { "smx", "SwampMonster"}
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            int ExpectedValidRefVarCount = 5002;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_13()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "o"));

            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "o");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_01()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Subject", "s"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "o");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_02()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Subject", "s"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "o");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_03()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Subject", "s"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "o");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_04()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class = OALProgram.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand
            (
                new EXECommandQuerySelect
                (
                    EXECommandQuerySelect.CardinalityAny,
                    "Observer",
                    "o",
                    new EXEASTNodeComposite
                    (
                        "==",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite
                            (
                                ".",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("count")
                                }
                            ),
                            new EXEASTNodeLeaf("3")
                        }
                    )
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "o");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_05()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class = OALProgram.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o", "count", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand
            (
                new EXECommandQuerySelect
                (
                    EXECommandQuerySelect.CardinalityAny,
                    "Observer",
                    "o",
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite
                            (
                                ".",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("count")
                                }
                            ),
                            new EXEASTNodeLeaf("3")
                        }
                    )
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer" }
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "o.count", "0" }
            };
            int ExpectedValidRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "o");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_01()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "o"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o[1]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "o");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_02()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "o"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o[5]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "o");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_03()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "o"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o[0]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedNonEmptySetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "o");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualNonEmptySetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedNonEmptySetRefVarCount, ActualNonEmptySetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_04()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers[1]", "Observer"},
                { "o1", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "observers[0].value", "3" }
            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            foreach (var Item in ActualCreatedVarState) Console.WriteLine(Item.Key + ":" + Item.Value);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_05()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o2", "value", new EXEASTNodeLeaf("4")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o3", "value", new EXEASTNodeLeaf("5")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o4"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o4", "value", new EXEASTNodeLeaf("6")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o5"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o5", "value", new EXEASTNodeLeaf("7")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers[5]", "Observer"},
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "o4", "Observer"},
                { "o5", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "observers[0].value", "3" },
                { "observers[1].value", "4" },
                { "observers[2].value", "5" },
                { "observers[3].value", "6" },
                { "observers[4].value", "7" },
            };
            int ExpectedValidRefVarCount = 5;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            foreach (var Item in ActualCreatedVarState) Console.WriteLine(Item.Key + ":" + Item.Value);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_06()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o2", "value", new EXEASTNodeLeaf("4")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o3", "value", new EXEASTNodeLeaf("5")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o4"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o4", "value", new EXEASTNodeLeaf("6")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o5"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o5", "value", new EXEASTNodeLeaf("7")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Observer",
                "observers",
                new EXEASTNodeComposite
                (
                    ">",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("value")
                            } 
                        ),
                        new EXEASTNodeLeaf("5")
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers[2]", "Observer"},
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "o4", "Observer"},
                { "o5", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "observers[0].value", "6" },
                { "observers[1].value", "7" }
            };
            int ExpectedValidRefVarCount = 5;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            foreach (var Item in ActualCreatedVarState) Console.WriteLine(Item.Key + ":" + Item.Value);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_07()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o2", "value", new EXEASTNodeLeaf("4")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o3", "value", new EXEASTNodeLeaf("5")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o4"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o4", "value", new EXEASTNodeLeaf("6")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o5"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o5", "value", new EXEASTNodeLeaf("7")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Observer",
                "observers",
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("value")
                            }
                        ),
                        new EXEASTNodeLeaf("5")
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers[1]", "Observer"},
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "o4", "Observer"},
                { "o5", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "observers[0].value", "5" }
            };
            int ExpectedValidRefVarCount = 5;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            foreach (var Item in ActualCreatedVarState) Console.WriteLine(Item.Key + ":" + Item.Value);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_08()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o2", "value", new EXEASTNodeLeaf("4")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o3", "value", new EXEASTNodeLeaf("5")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o4"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o4", "value", new EXEASTNodeLeaf("6")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o5"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o5", "value", new EXEASTNodeLeaf("7")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Observer",
                "observers",
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("value")
                            }
                        ),
                        new EXEASTNodeLeaf("2")
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers[0]", "Observer"},
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "o4", "Observer"},
                { "o5", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 5;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            foreach (var Item in ActualCreatedVarState) Console.WriteLine(Item.Key + ":" + Item.Value);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_09()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o2", "value", new EXEASTNodeLeaf("4")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o3", "value", new EXEASTNodeLeaf("5")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o4"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o4", "value", new EXEASTNodeLeaf("6")));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o5"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o5", "value", new EXEASTNodeLeaf("7")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Observer",
                "observers",
                new EXEASTNodeComposite
                (
                    ">=",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("value")
                            }
                        ),
                        new EXEASTNodeLeaf("5")
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers[3]", "Observer"},
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "o4", "Observer"},
                { "o5", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "observers[0].value", "5" },
                { "observers[1].value", "6" },
                { "observers[2].value", "7" }
            };
            int ExpectedValidRefVarCount = 5;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            foreach (var Item in ActualCreatedVarState) Console.WriteLine(Item.Key + ":" + Item.Value);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_10()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "weak_trolls",
                new EXEASTNodeComposite
                (
                    "<=",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("health")
                            }
                        ),
                        new EXEASTNodeLeaf("100")
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "strong_trolls",
                new EXEASTNodeComposite
                (
                    ">=",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("health")
                            }
                        ),
                        new EXEASTNodeLeaf("200")
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Troll", 200}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "weak_trolls[50]", "Troll"},
                { "strong_trolls[51]", "Troll"}
            };
            for (int i = 1; i <= 200; i++)
            {
                ExpectedScopeVars["troll" + i.ToString()] = "Troll";
            }
            Dictionary<String, String> ExpectedCreatedVarState1 = new Dictionary<String, String>()
            {
            };
            for (int i = 0; i < 50; i++)
            {
                ExpectedCreatedVarState1["weak_trolls[" + i.ToString() + "].health"] = (51 + i ).ToString();
                ExpectedCreatedVarState1["weak_trolls[" + i.ToString() + "].attack"] = (11 + i ).ToString();
            }
            Dictionary<String, String> ExpectedCreatedVarState2 = new Dictionary<String, String>()
            {
            };
            for (int i = 0; i <= 50; i++)
            {
                ExpectedCreatedVarState2["strong_trolls[" + i.ToString() + "].health"] = (200 + i).ToString();
                ExpectedCreatedVarState2["strong_trolls[" + i.ToString() + "].attack"] = (160 + i).ToString();
            }
            int ExpectedValidRefVarCount = 200;
            int ExpectedValidSetRefVarCount = 2;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            //foreach (var Item in ActualCreatedVarState) Console.WriteLine(Item.Key + ":" + Item.Value);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState1, ActualCreatedVarState1);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState2, ActualCreatedVarState2);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_11()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "weak_trolls",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "<=",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    ".",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("selected"),
                                        new EXEASTNodeLeaf("health")
                                    }
                                ),
                                new EXEASTNodeLeaf("100")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    "%",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected"),
                                                new EXEASTNodeLeaf("attack")
                                            }
                                        ),
                                        new EXEASTNodeLeaf("10")
                                    }
                                ),
                                new EXEASTNodeLeaf("0")
                            }
                        )
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "strong_trolls",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ">=",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    ".",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("selected"),
                                        new EXEASTNodeLeaf("health")
                                    }
                                ),
                                new EXEASTNodeLeaf("200")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    "%",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected"),
                                                new EXEASTNodeLeaf("attack")
                                            }
                                        ),
                                        new EXEASTNodeLeaf("10")
                                    }
                                ),
                                new EXEASTNodeLeaf("0")
                            }
                        )
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Troll", 200}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "weak_trolls[5]", "Troll"},
                { "strong_trolls[6]", "Troll"}
            };
            for (int i = 1; i <= 200; i++)
            {
                ExpectedScopeVars["troll" + i.ToString()] = "Troll";
            }
            Dictionary<String, String> ExpectedCreatedVarState1 = new Dictionary<String, String>()
            {
                { "weak_trolls[0].health", "60"},
                { "weak_trolls[0].attack", "20"},
                { "weak_trolls[1].health", "70"},
                { "weak_trolls[1].attack", "30"},
                { "weak_trolls[2].health", "80"},
                { "weak_trolls[2].attack", "40"},
                { "weak_trolls[3].health", "90"},
                { "weak_trolls[3].attack", "50"},
                { "weak_trolls[4].health", "100"},
                { "weak_trolls[4].attack", "60"}
            };
            Dictionary<String, String> ExpectedCreatedVarState2 = new Dictionary<String, String>()
            {
                { "strong_trolls[0].health", "200"},
                { "strong_trolls[0].attack", "160"},
                { "strong_trolls[1].health", "210"},
                { "strong_trolls[1].attack", "170"},
                { "strong_trolls[2].health", "220"},
                { "strong_trolls[2].attack", "180"},
                { "strong_trolls[3].health", "230"},
                { "strong_trolls[3].attack", "190"},
                { "strong_trolls[4].health", "240"},
                { "strong_trolls[4].attack", "200"},
                { "strong_trolls[5].health", "250"},
                { "strong_trolls[5].attack", "210"}
            };
            int ExpectedValidRefVarCount = 200;
            int ExpectedValidSetRefVarCount = 2;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState1, ActualCreatedVarState1);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState2, ActualCreatedVarState2);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_12()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "weak_trolls",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "<=",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    ".",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("selected"),
                                        new EXEASTNodeLeaf("health")
                                    }
                                ),
                                new EXEASTNodeLeaf("240")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    "%",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected"),
                                                new EXEASTNodeLeaf("attack")
                                            }
                                        ),
                                        new EXEASTNodeLeaf("10")
                                    }
                                ),
                                new EXEASTNodeLeaf("11")
                            }
                        )
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "strong_trolls",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ">=",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    ".",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("selected"),
                                        new EXEASTNodeLeaf("health")
                                    }
                                ),
                                new EXEASTNodeLeaf("200")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            ">",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    "%",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected"),
                                                new EXEASTNodeLeaf("attack")
                                            }
                                        ),
                                        new EXEASTNodeLeaf("10")
                                    }
                                ),
                                new EXEASTNodeLeaf("10")
                            }
                        )
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Troll", 200}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "weak_trolls[0]", "Troll"},
                { "strong_trolls[0]", "Troll"}
            };
            for (int i = 1; i <= 200; i++)
            {
                ExpectedScopeVars["troll" + i.ToString()] = "Troll";
            }
            Dictionary<String, String> ExpectedCreatedVarState1 = new Dictionary<String, String>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState2 = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 200;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState1, ActualCreatedVarState1);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState2, ActualCreatedVarState2);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_13()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "weak_trolls",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "<=",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    ".",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("selected"),
                                        new EXEASTNodeLeaf("health")
                                    }
                                ),
                                new EXEASTNodeLeaf("100")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    "%",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected"),
                                                new EXEASTNodeLeaf("attack")
                                            }
                                        ),
                                        new EXEASTNodeLeaf("10")
                                    }
                                ),
                                new EXEASTNodeLeaf("11")
                            }
                        )
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "strong_trolls",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ">=",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    ".",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("selected"),
                                        new EXEASTNodeLeaf("health")
                                    }
                                ),
                                new EXEASTNodeLeaf("200")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    "%",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected"),
                                                new EXEASTNodeLeaf("attack")
                                            }
                                        ),
                                        new EXEASTNodeLeaf("10")
                                    }
                                ),
                                new EXEASTNodeLeaf("0")
                            }
                        )
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment(
                "weak_are_empty",
                new EXEASTNodeComposite
                (
                    "empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("weak_trolls")
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment(
                "strong_are_empty",
                new EXEASTNodeComposite
                (
                    "empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("strong_trolls")
                    }
                )
            ));

            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"weak_are_empty", EXETypes.BooleanTrue },
                {"strong_are_empty", EXETypes.BooleanFalse }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Troll", 200}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "weak_trolls[0]", "Troll"},
                { "strong_trolls[6]", "Troll"}
            };
            for (int i = 1; i <= 200; i++)
            {
                ExpectedScopeVars["troll" + i.ToString()] = "Troll";
            }
            Dictionary<String, String> ExpectedCreatedVarState1 = new Dictionary<String, String>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState2 = new Dictionary<String, String>()
            {
                { "strong_trolls[0].health", "200"},
                { "strong_trolls[0].attack", "160"},
                { "strong_trolls[1].health", "210"},
                { "strong_trolls[1].attack", "170"},
                { "strong_trolls[2].health", "220"},
                { "strong_trolls[2].attack", "180"},
                { "strong_trolls[3].health", "230"},
                { "strong_trolls[3].attack", "190"},
                { "strong_trolls[4].health", "240"},
                { "strong_trolls[4].attack", "200"},
                { "strong_trolls[5].health", "250"},
                { "strong_trolls[5].attack", "210"}
            };
            int ExpectedValidRefVarCount = 200;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState1, ActualCreatedVarState1);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState2, ActualCreatedVarState2);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_15()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "weak_trolls",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "<=",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    ".",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("selected"),
                                        new EXEASTNodeLeaf("health")
                                    }
                                ),
                                new EXEASTNodeLeaf("100")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    "%",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected"),
                                                new EXEASTNodeLeaf("attack")
                                            }
                                        ),
                                        new EXEASTNodeLeaf("10")
                                    }
                                ),
                                new EXEASTNodeLeaf("11")
                            }
                        )
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "strong_trolls",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ">=",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    ".",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("selected"),
                                        new EXEASTNodeLeaf("health")
                                    }
                                ),
                                new EXEASTNodeLeaf("200")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    "%",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected"),
                                                new EXEASTNodeLeaf("attack")
                                            }
                                        ),
                                        new EXEASTNodeLeaf("10")
                                    }
                                ),
                                new EXEASTNodeLeaf("0")
                            }
                        )
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment(
                "weak_count",
                new EXEASTNodeComposite
                (
                    "cardinality",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("weak_trolls")
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment(
                "strong_count",
                new EXEASTNodeComposite
                (
                    "cardinality",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("strong_trolls")
                    }
                )
            ));

            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"weak_count", "0" },
                {"strong_count", "6" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Troll", 200}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "weak_trolls[0]", "Troll"},
                { "strong_trolls[6]", "Troll"}
            };
            for (int i = 1; i <= 200; i++)
            {
                ExpectedScopeVars["troll" + i.ToString()] = "Troll";
            }
            Dictionary<String, String> ExpectedCreatedVarState1 = new Dictionary<String, String>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState2 = new Dictionary<String, String>()
            {
                { "strong_trolls[0].health", "200"},
                { "strong_trolls[0].attack", "160"},
                { "strong_trolls[1].health", "210"},
                { "strong_trolls[1].attack", "170"},
                { "strong_trolls[2].health", "220"},
                { "strong_trolls[2].attack", "180"},
                { "strong_trolls[3].health", "230"},
                { "strong_trolls[3].attack", "190"},
                { "strong_trolls[4].health", "240"},
                { "strong_trolls[4].attack", "200"},
                { "strong_trolls[5].health", "250"},
                { "strong_trolls[5].attack", "210"}
            };
            int ExpectedValidRefVarCount = 200;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState1, ActualCreatedVarState1);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState2, ActualCreatedVarState2);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_14()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass ClassObserver = OALProgram.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                OALProgram.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "weak_trolls",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "<=",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    ".",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("selected"),
                                        new EXEASTNodeLeaf("health")
                                    }
                                ),
                                new EXEASTNodeLeaf("100")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    "%",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected"),
                                                new EXEASTNodeLeaf("attack")
                                            }
                                        ),
                                        new EXEASTNodeLeaf("10")
                                    }
                                ),
                                new EXEASTNodeLeaf("11")
                            }
                        )
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Troll",
                "strong_trolls",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ">=",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    ".",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("selected"),
                                        new EXEASTNodeLeaf("health")
                                    }
                                ),
                                new EXEASTNodeLeaf("200")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeComposite
                                (
                                    "%",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected"),
                                                new EXEASTNodeLeaf("attack")
                                            }
                                        ),
                                        new EXEASTNodeLeaf("10")
                                    }
                                ),
                                new EXEASTNodeLeaf("0")
                            }
                        )
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment(
                "weak_are_empty",
                new EXEASTNodeComposite
                (
                    "not_empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("weak_trolls")
                    }
                )
            ));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment(
                "strong_are_empty",
                new EXEASTNodeComposite
                (
                    "not_empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("strong_trolls")
                    }
                )
            ));

            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"weak_are_empty", EXETypes.BooleanFalse },
                {"strong_are_empty", EXETypes.BooleanTrue }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Troll", 200}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "weak_trolls[0]", "Troll"},
                { "strong_trolls[6]", "Troll"}
            };
            for (int i = 1; i <= 200; i++)
            {
                ExpectedScopeVars["troll" + i.ToString()] = "Troll";
            }
            Dictionary<String, String> ExpectedCreatedVarState1 = new Dictionary<String, String>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState2 = new Dictionary<String, String>()
            {
                { "strong_trolls[0].health", "200"},
                { "strong_trolls[0].attack", "160"},
                { "strong_trolls[1].health", "210"},
                { "strong_trolls[1].attack", "170"},
                { "strong_trolls[2].health", "220"},
                { "strong_trolls[2].attack", "180"},
                { "strong_trolls[3].health", "230"},
                { "strong_trolls[3].attack", "190"},
                { "strong_trolls[4].health", "240"},
                { "strong_trolls[4].attack", "200"},
                { "strong_trolls[5].health", "250"},
                { "strong_trolls[5].attack", "210"}
            };
            int ExpectedValidRefVarCount = 200;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState1, ActualCreatedVarState1);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState2, ActualCreatedVarState2);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Many_01()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "s"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "s");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Many_02()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "s"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "s");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Many_03()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Observer",
                "observers",
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("value")
                            }
                        ),
                        new EXEASTNodeLeaf("2")
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "s");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Many_04()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class = OALProgram.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Observer",
                "observers",
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("value")
                            }
                        ),
                        new EXEASTNodeLeaf("2")
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "s");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Many_05()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class = OALProgram.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o", "count", new EXEASTNodeLeaf("50")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Observer",
                "observers",
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("value")
                            }
                        ),
                        new EXEASTNodeLeaf("2")
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "s");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Many_06()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class = OALProgram.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("o", "count", new EXEASTNodeLeaf("50")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Observer",
                "observers",
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("value")
                            }
                        ),
                        new EXEASTNodeLeaf("2")
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "s");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Many_07()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass Class = OALProgram.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect
            (
                EXECommandQuerySelect.CardinalityMany,
                "Observer",
                "observers",
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("count")
                            }
                        ),
                        new EXEASTNodeLeaf("2")
                    }
                )
            ));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "s");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
    }
}