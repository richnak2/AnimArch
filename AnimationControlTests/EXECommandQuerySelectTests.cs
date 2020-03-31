using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQuerySelectTests
    {
        [TestMethod]
        public void Execute_Normal_Any_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "o"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>(){
                
            };
            int ExpectedValidRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_02()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "SwampMonster", "swamp_monster1"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "swamp_monster1");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_03()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm", "health", new EXEASTNodeLeaf("50")));
            Animation.SuperScope.AddCommand(
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "swamp_monster1");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

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
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm", "health", new EXEASTNodeLeaf("50")));
            Animation.SuperScope.AddCommand(
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "swamp_monster1");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_05()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm", "health", new EXEASTNodeLeaf("50")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm", "mana", new EXEASTNodeLeaf("10")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm1", "health", new EXEASTNodeLeaf("60")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm1", "mana", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_06()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm", "health", new EXEASTNodeLeaf("50")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm", "mana", new EXEASTNodeLeaf("10")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm1", "health", new EXEASTNodeLeaf("60")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm1", "mana", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_07()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));

            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            Animation.SuperScope.AddCommand(
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_08()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));

            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            Animation.SuperScope.AddCommand(
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_09()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));

            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));

                if (i == 2950)
                {
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                }
            }
            Animation.SuperScope.AddCommand(
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_10()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));

            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));

                if (i == 2950)
                {
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                }
            }
            Animation.SuperScope.AddCommand(
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_11()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("stunned", EXETypes.BooleanTypeName));

            Random rand = new Random();
            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "stunned", new EXEASTNodeLeaf(rand.NextDouble() >= 0.5 ? EXETypes.BooleanTrue : EXETypes.BooleanFalse)));

                if (i == 2950)
                {
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "stunned", new EXEASTNodeLeaf(EXETypes.BooleanFalse)));

                }
            }
            Animation.SuperScope.AddCommand(
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "swamp_monster");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_12()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("stunned", EXETypes.BooleanTypeName));

            Random rand = new Random();
            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "stunned", new EXEASTNodeLeaf(rand.NextDouble() >= 0.5 ? EXETypes.BooleanTrue : EXETypes.BooleanFalse)));

                if (i == 2950)
                {
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "stunned", new EXEASTNodeLeaf(EXETypes.BooleanFalse)));

                }
            }
            Animation.SuperScope.AddCommand(
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_13()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "o"));

            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Subject", "s"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_02()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Subject", "s"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_03()
        {
            Animation Animation = new Animation();
           
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Subject", "s"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_04()
        {
            Animation Animation = new Animation();
            CDClass Class = Animation.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand
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

            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            foreach (var i in ActualScopeVars) Console.WriteLine(i.Key + " : " + i.Value);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
    }
}

