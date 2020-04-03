using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQuerySelectRelatedByTests
    {
        private void Setup_Observer_Classes(Animation Animation, int flag = 0)
        {
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            if (flag == 1)
            {
                ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));
            }
            Animation.ExecutionSpace.SpawnClass("Subject");

            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");
        }
        private void Setup_Observer_Commands(Animation Animation)
        {
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer", "subject", "R1"));
        }
        private EXERelationshipSelection Setup_Observer_RelationshipNavigation(int flag)
        {
            EXERelationshipSelection RelSel = null;
            switch (flag)
            {

                case 0:
                    RelSel = new EXERelationshipSelection(
                        "subject",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "Observer")
                        }
                    );
                    break;
                case 1:
                    RelSel = new EXERelationshipSelection(
                        "observer",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "Subject")
                        }
                    );
                    break;
            }

            return RelSel;
        }
        private void Setup_Monster_Classes(Animation Animation, int flag = 0)
        {
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));
            switch (flag)
            {
                case 1:
                    Class1.AddAttribute(new CDAttribute("stunned", EXETypes.BooleanTypeName));
                    break;
                default:
                    break;
            }

            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Item");
            Class2.AddAttribute(new CDAttribute("type", EXETypes.StringTypeName));
            Class2.AddAttribute(new CDAttribute("screen_name", EXETypes.StringTypeName));
            Class2.AddAttribute(new CDAttribute("gold_value", EXETypes.IntegerTypeName));

            Animation.RelationshipSpace.SpawnRelationship("SwampMonster", "Item");
        }
        private EXERelationshipSelection Setup_Monster_RelationshipNavigation(int flag)
        {
            EXERelationshipSelection RelSel = null;
            switch (flag)
            {

                case 0:
                    RelSel = new EXERelationshipSelection(
                        "monster",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "Item")
                        }
                    );
                    break;
                case 1:
                    RelSel = new EXERelationshipSelection(
                        "item",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "SwampMonster")
                        }
                    );
                    break;
            }

            return RelSel;
        }
        private void Setup_Troll_Classes(Animation Animation, int flag = 0)
        {
            CDClass Class1= Animation.ExecutionSpace.SpawnClass("Troll");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Item");
            Class2.AddAttribute(new CDAttribute("type", EXETypes.StringTypeName));
            Class2.AddAttribute(new CDAttribute("screen_name", EXETypes.StringTypeName));
            Class2.AddAttribute(new CDAttribute("gold_value", EXETypes.IntegerTypeName));

            Animation.RelationshipSpace.SpawnRelationship("Troll", "Item");
        }
        [TestMethod]
        public void Execute_Normal_Any_01()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Setup_Observer_Commands(Animation);
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
            (
                EXECommandQuerySelect.CardinalityAny,
                "selected_o",
                null,
                Setup_Observer_RelationshipNavigation(0)
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "selected_o", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 3;

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
            Setup_Monster_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "monster"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("monster", "item", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
            (
                EXECommandQuerySelect.CardinalityAny,
                "selected_monster",
                null,
                Setup_Monster_RelationshipNavigation(1)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 1},
                { "Item", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "monster", "SwampMonster"},
                { "selected_monster", "SwampMonster"},
                { "item", "Item"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "selected_monster.health", EXETypes.UnitializedName},
                { "selected_monster.max_health", EXETypes.UnitializedName},
                { "selected_monster.mana", EXETypes.UnitializedName},
                { "selected_monster.max_mana", EXETypes.UnitializedName}
            };
            int ExpectedValidRefVarCount = 3;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "selected_monster");
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
            Setup_Monster_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "monster"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("monster", "health", new EXEASTNodeLeaf("50")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("monster", "item", "R1"));
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
                    EXECommandQuerySelect.CardinalityAny,
                    "selected_monster",
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
                    ),
                    Setup_Monster_RelationshipNavigation(1)
                )
            );
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 1},
                { "Item", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "monster", "SwampMonster"},
                { "selected_monster", "SwampMonster"},
                { "item", "Item"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "selected_monster.health", "50.0"},
                { "selected_monster.max_health", EXETypes.UnitializedName},
                { "selected_monster.mana", EXETypes.UnitializedName},
                { "selected_monster.max_mana", EXETypes.UnitializedName}
            };
            int ExpectedValidRefVarCount = 3;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "selected_monster");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Normal_Any_04()
        {
            Animation Animation = new Animation();
            Setup_Monster_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "monster"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("monster", "item", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("monster", "health", new EXEASTNodeLeaf("50")));
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
                    EXECommandQuerySelect.CardinalityAny,
                    "selected_monster",
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
                    ),
                    Setup_Monster_RelationshipNavigation(1)
                )
            );
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 1},
                { "Item", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "monster", "SwampMonster"},
                { "selected_monster", "SwampMonster"},
                { "item", "Item"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 2;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "selected_monster");
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
            Setup_Monster_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm", "health", new EXEASTNodeLeaf("50")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm1", "health", new EXEASTNodeLeaf("60")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm1", "mana", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("sm", "item", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("sm1", "item", "R1"));
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
                    EXECommandQuerySelect.CardinalityAny,
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
                    ),
                    new EXERelationshipSelection(
                        "item",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "SwampMonster")
                        }
                    )
                )
            );
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster", "mana", new EXEASTNodeLeaf("10")));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 2},
                { "Item", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                { "sm", "SwampMonster"},
                { "sm1", "SwampMonster"},
                { "item", "Item"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "50.0"},
                { "swamp_monster.max_health", EXETypes.UnitializedName},
                { "swamp_monster.mana", "10.0"},
                { "swamp_monster.max_mana", EXETypes.UnitializedName}
            };
            int ExpectedValidRefVarCount = 4;

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
            Setup_Monster_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm", "health", new EXEASTNodeLeaf("50")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm", "mana", new EXEASTNodeLeaf("10")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm1", "health", new EXEASTNodeLeaf("60")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("sm1", "mana", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("sm", "item", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("sm1", "item", "R1"));
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
                    EXECommandQuerySelect.CardinalityAny,
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
                    ),
                    new EXERelationshipSelection(
                        "item",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "SwampMonster")
                        }
                    )
                )
            );
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster", "mana", new EXEASTNodeLeaf("30")));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 2},
                { "Item", 1},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                { "sm", "SwampMonster"},
                { "sm1", "SwampMonster"},
                { "item", "Item"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "60.0"},
                { "swamp_monster.max_health", EXETypes.UnitializedName},
                { "swamp_monster.mana", "30.0"},
                { "swamp_monster.max_mana", EXETypes.UnitializedName}
            };
            int ExpectedValidRefVarCount = 4;

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
            Setup_Monster_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandQueryRelate("sm" + i.ToString(), "item", "R1"));
            }
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
                    EXECommandQuerySelect.CardinalityAny,
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
                    ),
                    new EXERelationshipSelection(
                        "item",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "SwampMonster")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5000},
                { "Item", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                { "item", "Item"}
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "5050.0"},
                { "swamp_monster.mana", "5010.0"},
                { "swamp_monster.max_health", "5050.0"},
                { "swamp_monster.max_mana", "5010.0"}
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
        public void Execute_Normal_Any_08()
        {
            Animation Animation = new Animation();
            Setup_Monster_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item")); 
            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandQueryRelate("sm" + i.ToString(), "item", "R1"));
            }
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
                    EXECommandQuerySelect.CardinalityAny,
                    "swamp_monster",
                    new EXEASTNodeComposite(
                        "==",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ".",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("health")
                                }
                            ),
                            new EXEASTNodeLeaf("3050")
                        }
                    ),
                    new EXERelationshipSelection(
                        "item",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "SwampMonster")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5000},
                { "Item", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                { "item", "Item"}
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "3050.0"},
                { "swamp_monster.mana", "3010.0"},
                { "swamp_monster.max_health", "3050.0"},
                { "swamp_monster.max_mana", "3010.0"}
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
        public void Execute_Normal_Any_09()
        {
            Animation Animation = new Animation();
            Setup_Monster_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandQueryRelate("sm" + i.ToString(), "item", "R1"));

                if (i == 2950)
                {
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "max_health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "max_mana", new EXEASTNodeLeaf("5")));
                    Animation.SuperScope.AddCommand(new EXECommandQueryRelate("smx", "item", "R1"));
                }
            }
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
                    EXECommandQuerySelect.CardinalityAny,
                    "swamp_monster",
                    new EXEASTNodeComposite(
                        ">=",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeComposite(
                                ".",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf("selected"),
                                    new EXEASTNodeLeaf("health")
                                }
                            ),
                            new EXEASTNodeLeaf("12000")
                        }
                    ),
                    new EXERelationshipSelection(
                        "item",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "SwampMonster")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5001},
                { "Item", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                 { "smx", "SwampMonster"},
                { "item", "Item"}
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "12000.0"},
                { "swamp_monster.mana", "5.0"},
                { "swamp_monster.max_health", "12000.0"},
                { "swamp_monster.max_mana", "5.0"}
            };
            int ExpectedValidRefVarCount = 5003;

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
            Setup_Monster_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandQueryRelate("sm" + i.ToString(), "item", "R1"));

                if (i == 2950)
                {
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "max_health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "max_mana", new EXEASTNodeLeaf("5")));
                    Animation.SuperScope.AddCommand(new EXECommandQueryRelate("smx", "item", "R1"));
                }
            }
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
                    EXECommandQuerySelect.CardinalityAny,
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
                    ),
                    new EXERelationshipSelection(
                        "item",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "SwampMonster")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5001},
                { "Item", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                 { "smx", "SwampMonster"},
                { "item", "Item"}
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "12000.0"},
                { "swamp_monster.mana", "5.0"},
                { "swamp_monster.max_health", "12000.0"},
                { "swamp_monster.max_mana", "5.0"}
            };
            int ExpectedValidRefVarCount = 5003;

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
            Setup_Monster_Classes(Animation, 1);

            Random rand = new Random();
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandQueryRelate("sm" + i.ToString(), "item", "R1"));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "stunned", new EXEASTNodeLeaf(rand.NextDouble() >= 0.5 ? EXETypes.BooleanTrue : EXETypes.BooleanFalse)));


                if (i == 2950)
                {
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "max_health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "max_mana", new EXEASTNodeLeaf("5")));
                    Animation.SuperScope.AddCommand(new EXECommandQueryRelate("smx", "item", "R1"));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "stunned", new EXEASTNodeLeaf(EXETypes.BooleanFalse)));
                }
            }
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
                    EXECommandQuerySelect.CardinalityAny,
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
                    ),
                    new EXERelationshipSelection(
                        "item",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "SwampMonster")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5001},
                { "Item", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                 { "smx", "SwampMonster"},
                { "item", "Item"}
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "swamp_monster.health", "12000.0"},
                { "swamp_monster.mana", "5.0"},
                { "swamp_monster.max_health", "12000.0"},
                { "swamp_monster.max_mana", "5.0" },
                { "swamp_monster.stunned", EXETypes.BooleanFalse}
            };
            int ExpectedValidRefVarCount = 5003;

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
            Setup_Monster_Classes(Animation, 1);

            Random rand = new Random();
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "max_mana", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandQueryRelate("sm" + i.ToString(), "item", "R1"));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "stunned", new EXEASTNodeLeaf(rand.NextDouble() >= 0.5 ? EXETypes.BooleanTrue : EXETypes.BooleanFalse)));


                if (i == 2950)
                {
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "smx"));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "mana", new EXEASTNodeLeaf("5")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "max_health", new EXEASTNodeLeaf("12000")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "max_mana", new EXEASTNodeLeaf("5")));
                    Animation.SuperScope.AddCommand(new EXECommandQueryRelate("smx", "item", "R1"));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("smx", "stunned", new EXEASTNodeLeaf(EXETypes.BooleanFalse)));
                }
            }
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
                    EXECommandQuerySelect.CardinalityAny,
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
                    ),
                    new EXERelationshipSelection(
                        "item",
                        new EXERelationshipLink[]
                        {
                            new EXERelationshipLink("R1", "SwampMonster")
                        }
                    )
                )
            );
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "SwampMonster", 5001},
                { "Item", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "swamp_monster", "SwampMonster"},
                 { "smx", "SwampMonster"},
                { "item", "Item"}
            };
            for (int i = 1; i <= 5000; i++)
            {
                ExpectedScopeVars["sm" + i.ToString()] = "SwampMonster";
            }

            int ExpectedValidRefVarCount = 5003;

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
            Setup_Observer_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityAny,
                "o",
                null,
                Setup_Observer_RelationshipNavigation(0)
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

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
        public void Execute_Normal_Any_14()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityAny,
                "o",
                null,
                Setup_Observer_RelationshipNavigation(0)
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "observer", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 2;

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
        public void Execute_Normal_Any_15()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityAny,
                "o",
                null,
                Setup_Observer_RelationshipNavigation(0)
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "o_empty",
                new EXEASTNodeComposite
                (
                    "empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("o")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "o_not_empty",
                new EXEASTNodeComposite
                (
                    "not_empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("o")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "o_cardinality",
                new EXEASTNodeComposite
                (
                    "cardinality",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("o")
                    }
                )
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"o_empty", EXETypes.BooleanTrue},
                {"o_not_empty", EXETypes.BooleanFalse},
                {"o_cardinality", "0"},
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "observer", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 2;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_01()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityAny,
                "selected_observer",
                null,
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
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
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "selected_observer");
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
            Setup_Observer_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityAny,
                "selected_observer",
                null,
                Setup_Observer_RelationshipNavigation(1)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
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
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "selected_observer");
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
            Setup_Observer_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityAny,
                "selected_observer",
                null,
                Setup_Observer_RelationshipNavigation(1)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0},
                { "Subject", 0}
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
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "selected_observer");
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
            Setup_Observer_Classes(Animation);

            Setup_Observer_Commands(Animation);
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityAny,
                "selected_observer",
                null,
                new EXERelationshipSelection
                (
                    "subject",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink
                        (
                            "R1",
                            "ConcreteObserver"
                        )
                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 2;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "selected_observer");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_05()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Setup_Observer_Commands(Animation);
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityAny,
                "selected_observer",
                null,
                new EXERelationshipSelection
                (
                    "subject",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink
                        (
                            "R2",
                            "Observer"
                        )
                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 2;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "selected_observer");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_06()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityAny,
                "selected_observer",
                null,
                new EXERelationshipSelection
                (
                    "subject",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink
                        (
                            "R2",
                            "Observer"
                        )
                    }
                )
            ));
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
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "selected_observer");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Any_07()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityAny,
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
                ),
                Setup_Observer_RelationshipNavigation(0)
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "o_empty",
                new EXEASTNodeComposite
                (
                    "empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("o")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "o_not_empty",
                new EXEASTNodeComposite
                (
                    "not_empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("o")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "o_cardinality",
                new EXEASTNodeComposite
                (
                    "cardinality",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("o")
                    }
                )
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {

            };
            int ExpectedValidRefVarCount = 2;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_01()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Setup_Observer_Commands(Animation);
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "o",
                null,
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "subject", "Subject"},
                { "o[1]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 2;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer1", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer2", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer3", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer4", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer5", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "o",
                null,
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer1", "Observer"},
                { "observer2", "Observer"},
                { "observer3", "Observer"},
                { "observer4", "Observer"},
                { "observer5", "Observer"},
                { "subject", "Subject"},
                { "o[5]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 6;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "o",
                null,
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "subject", "Subject"},
                { "o[0]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_04()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation, 1);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("subject", "observer", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "o",
                null,
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "subject", "Subject"},
                { "o[1]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "o[0].value", "3"}
            };
            int ExpectedValidRefVarCount = 2;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_05()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation, 1);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer2", "value", new EXEASTNodeLeaf("4")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer3", "value", new EXEASTNodeLeaf("5")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer4"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer4", "value", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer5"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer5", "value", new EXEASTNodeLeaf("7")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("subject", "observer1", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer2", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer3", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer4", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer5", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "observers",
                null,
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer1", "Observer"},
                { "observer2", "Observer"},
                { "observer3", "Observer"},
                { "observer4", "Observer"},
                { "observer5", "Observer"},
                { "subject", "Subject"},
                { "observers[5]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "observers[0].value", "3" },
                { "observers[1].value", "4" },
                { "observers[2].value", "5" },
                { "observers[3].value", "6" },
                { "observers[4].value", "7" },
            };
            int ExpectedValidRefVarCount = 6;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_06()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation, 1);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer2", "value", new EXEASTNodeLeaf("4")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer3", "value", new EXEASTNodeLeaf("5")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer4"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer4", "value", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer5"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer5", "value", new EXEASTNodeLeaf("7")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("subject", "observer1", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer2", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer3", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer4", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer5", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                ),
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer1", "Observer"},
                { "observer2", "Observer"},
                { "observer3", "Observer"},
                { "observer4", "Observer"},
                { "observer5", "Observer"},
                { "subject", "Subject"},
                { "observers[2]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "observers[0].value", "6" },
                { "observers[1].value", "7" },
            };
            int ExpectedValidRefVarCount = 6;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_07()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation, 1);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer2", "value", new EXEASTNodeLeaf("4")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer3", "value", new EXEASTNodeLeaf("5")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer4"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer4", "value", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer5"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer5", "value", new EXEASTNodeLeaf("7")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("subject", "observer1", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer2", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer3", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer4", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer5", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                        new EXEASTNodeLeaf("7")
                    }
                ),
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer1", "Observer"},
                { "observer2", "Observer"},
                { "observer3", "Observer"},
                { "observer4", "Observer"},
                { "observer5", "Observer"},
                { "subject", "Subject"},
                { "observers[1]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "observers[0].value", "7" }
            };
            int ExpectedValidRefVarCount = 6;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_08()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation, 1);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer2", "value", new EXEASTNodeLeaf("4")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer3", "value", new EXEASTNodeLeaf("5")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer4"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer4", "value", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer5"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer5", "value", new EXEASTNodeLeaf("7")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("subject", "observer1", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer2", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer3", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer4", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer5", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                        new EXEASTNodeLeaf("7")
                    }
                ),
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer1", "Observer"},
                { "observer2", "Observer"},
                { "observer3", "Observer"},
                { "observer4", "Observer"},
                { "observer5", "Observer"},
                { "subject", "Subject"},
                { "observers[0]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 6;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_09()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation, 1);

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer2", "value", new EXEASTNodeLeaf("4")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer3", "value", new EXEASTNodeLeaf("5")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer4"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer4", "value", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer5"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer5", "value", new EXEASTNodeLeaf("7")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "subject"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("subject", "observer1", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer2", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer3", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer4", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("observer5", "subject", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                ),
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 5},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer1", "Observer"},
                { "observer2", "Observer"},
                { "observer3", "Observer"},
                { "observer4", "Observer"},
                { "observer5", "Observer"},
                { "subject", "Subject"},
                { "observers[3]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "observers[0].value", "5" },
                { "observers[1].value", "6" },
                { "observers[2].value", "7" }
            };
            int ExpectedValidRefVarCount = 6;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_10()
        {
            Animation Animation = new Animation();
            Setup_Troll_Classes(Animation);

            int ItemCount = 1;
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item" + ItemCount.ToString()));
            for (int i = 1; i <= 200; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandQueryRelate("troll" + i.ToString(), "item" + ItemCount.ToString(), "R1"));

                if(i % 20 == 0)
                {
                    ItemCount++;
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item" + ItemCount.ToString()));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "type", new EXEASTNodeLeaf("\"Sword\"")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "screen_name", new EXEASTNodeLeaf("\"Butchering Sword\"")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "gold_value", new EXEASTNodeLeaf((ItemCount + 1000).ToString())));
                }
            }
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                ),
                new EXERelationshipSelection
                (
                    "item2",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                ),
                new EXERelationshipSelection
                (
                    "item9",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Troll", 200},
                { "Item", 11},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "weak_trolls[20]", "Troll"},
                { "strong_trolls[20]", "Troll"}
            };
            for (int i = 1; i <= 200; i++)
            {
                ExpectedScopeVars["troll" + i.ToString()] = "Troll";
            }
            for (int i = 1; i <= 11; i++)
            {
                ExpectedScopeVars["item" + i.ToString()] = "Item";
            }
            Dictionary<String, String> ExpectedCreatedVarState1 = new Dictionary<String, String>()
            {
            };
            for (int i = 0; i <= 19; i++)
            {
                ExpectedCreatedVarState1["weak_trolls[" + i.ToString() + "].health"] = (50 + i + 21).ToString();
                ExpectedCreatedVarState1["weak_trolls[" + i.ToString() + "].attack"] = (10 + i + 21).ToString();
            }
            Dictionary<String, String> ExpectedCreatedVarState2 = new Dictionary<String, String>()
            {
            };
            for (int i = 0; i <= 19; i++)
            {
                ExpectedCreatedVarState2["strong_trolls[" + i.ToString() + "].health"] = (i + 211).ToString();
                ExpectedCreatedVarState2["strong_trolls[" + i.ToString() + "].attack"] = (i + 171).ToString();
            }
            int ExpectedValidRefVarCount = 211;
            int ExpectedValidSetRefVarCount = 2;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState1, ActualCreatedVarState1);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState2, ActualCreatedVarState2);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_11()
        {
            Animation Animation = new Animation();
            Setup_Troll_Classes(Animation);

            int ItemCount = 1;
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item" + ItemCount.ToString()));
            for (int i = 1; i <= 200; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandQueryRelate("troll" + i.ToString(), "item" + ItemCount.ToString(), "R1"));

                if (i % 20 == 0)
                {
                    ItemCount++;
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item" + ItemCount.ToString()));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "type", new EXEASTNodeLeaf("\"Sword\"")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "screen_name", new EXEASTNodeLeaf("\"Butchering Sword\"")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "gold_value", new EXEASTNodeLeaf((ItemCount + 1000).ToString())));
                }
            }
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                ),
                new EXERelationshipSelection
                (
                    "item2",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                ),
                new EXERelationshipSelection
                (
                    "item9",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "no_trolls",
                null,
                new EXERelationshipSelection
                (
                    "item11",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "no_troll_was_selected",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("weak_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("strong_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("no_trolls")
                            }
                        )
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "a_troll_was_selected",
                new EXEASTNodeComposite
                (
                    "or",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "not_empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("weak_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "not_empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("strong_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "not_empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("no_trolls")
                            }
                        )
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "selected_troll_count",
                new EXEASTNodeComposite
                (
                    "+",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "cardinality",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("weak_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "cardinality",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("strong_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "cardinality",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("no_trolls")
                            }
                        )
                    }
                )
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"no_troll_was_selected", EXETypes.BooleanFalse },
                {"a_troll_was_selected", EXETypes.BooleanTrue },
                {"selected_troll_count", "4" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Troll", 200},
                { "Item", 11},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "weak_trolls[2]", "Troll"},
                { "strong_trolls[2]", "Troll"},
                { "no_trolls[0]", "Troll"}
            };
            for (int i = 1; i <= 200; i++)
            {
                ExpectedScopeVars["troll" + i.ToString()] = "Troll";
            }
            for (int i = 1; i <= 11; i++)
            {
                ExpectedScopeVars["item" + i.ToString()] = "Item";
            }
            Dictionary<String, String> ExpectedCreatedVarState1 = new Dictionary<String, String>()
            {
                { "weak_trolls[0].health", "80"},
                { "weak_trolls[0].attack", "40"},
                { "weak_trolls[1].health", "90"},
                { "weak_trolls[1].attack", "50"}
            };
            Dictionary<String, String> ExpectedCreatedVarState2 = new Dictionary<String, String>()
            {
                { "strong_trolls[0].health", "220"},
                { "strong_trolls[0].attack", "180"},
                { "strong_trolls[1].health", "230"},
                { "strong_trolls[1].attack", "190"}
            };
            int ExpectedValidRefVarCount = 211;
            int ExpectedValidSetRefVarCount = 2;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
        public void Execute_Good_Many_12()
        {
            Animation Animation = new Animation();
            Setup_Troll_Classes(Animation);

            int ItemCount = 1;
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item" + ItemCount.ToString()));
            for (int i = 1; i <= 200; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandQueryRelate("troll" + i.ToString(), "item" + ItemCount.ToString(), "R1"));

                if (i % 20 == 0)
                {
                    ItemCount++;
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item" + ItemCount.ToString()));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "type", new EXEASTNodeLeaf("\"Sword\"")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "screen_name", new EXEASTNodeLeaf("\"Butchering Sword\"")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "gold_value", new EXEASTNodeLeaf((ItemCount + 1000).ToString())));
                }
            }
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                                new EXEASTNodeLeaf("10")
                            }
                        )
                    }
                ),
                new EXERelationshipSelection
                (
                    "item2",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                ),
                new EXERelationshipSelection
                (
                    "item9",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "no_trolls",
                null,
                new EXERelationshipSelection
                (
                    "item11",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "no_troll_was_selected",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("weak_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("strong_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("no_trolls")
                            }
                        )
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "a_troll_was_selected",
                new EXEASTNodeComposite
                (
                    "or",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "not_empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("weak_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "not_empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("strong_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "not_empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("no_trolls")
                            }
                        )
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "selected_troll_count",
                new EXEASTNodeComposite
                (
                    "+",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "cardinality",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("weak_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "cardinality",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("strong_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "cardinality",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("no_trolls")
                            }
                        )
                    }
                )
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"no_troll_was_selected", EXETypes.BooleanTrue },
                {"a_troll_was_selected", EXETypes.BooleanFalse },
                {"selected_troll_count", "0" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Troll", 200},
                { "Item", 11},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "weak_trolls[0]", "Troll"},
                { "strong_trolls[0]", "Troll"},
                { "no_trolls[0]", "Troll"}
            };
            for (int i = 1; i <= 200; i++)
            {
                ExpectedScopeVars["troll" + i.ToString()] = "Troll";
            }
            for (int i = 1; i <= 11; i++)
            {
                ExpectedScopeVars["item" + i.ToString()] = "Item";
            }
            Dictionary<String, String> ExpectedCreatedVarState1 = new Dictionary<String, String>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState2 = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 211;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
        public void Execute_Good_Many_13()
        {
            Animation Animation = new Animation();
            Setup_Troll_Classes(Animation);

            int ItemCount = 1;
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item" + ItemCount.ToString()));
            for (int i = 1; i <= 200; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandQueryRelate("troll" + i.ToString(), "item" + ItemCount.ToString(), "R1"));

                if (i % 20 == 0)
                {
                    ItemCount++;
                    Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item" + ItemCount.ToString()));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "type", new EXEASTNodeLeaf("\"Sword\"")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "screen_name", new EXEASTNodeLeaf("\"Butchering Sword\"")));
                    Animation.SuperScope.AddCommand(new EXECommandAssignment("item" + ItemCount.ToString(), "gold_value", new EXEASTNodeLeaf((ItemCount + 1000).ToString())));
                }
            }
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                                new EXEASTNodeLeaf("80")
                            }
                        ),
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
                                new EXEASTNodeLeaf("75")
                            }
                        )
                    }
                ),
                new EXERelationshipSelection
                (
                    "item2",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
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
                ),
                new EXERelationshipSelection
                (
                    "item9",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "no_trolls",
                null,
                new EXERelationshipSelection
                (
                    "item11",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Troll")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "no_troll_was_selected",
                new EXEASTNodeComposite
                (
                    "and",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("weak_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("strong_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("no_trolls")
                            }
                        )
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "a_troll_was_selected",
                new EXEASTNodeComposite
                (
                    "or",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "not_empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("weak_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "not_empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("strong_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "not_empty",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("no_trolls")
                            }
                        )
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "selected_troll_count",
                new EXEASTNodeComposite
                (
                    "+",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite
                        (
                            "cardinality",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("weak_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "cardinality",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("strong_trolls")
                            }
                        ),
                        new EXEASTNodeComposite
                        (
                            "cardinality",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("no_trolls")
                            }
                        )
                    }
                )
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"no_troll_was_selected", EXETypes.BooleanFalse },
                {"a_troll_was_selected", EXETypes.BooleanTrue},
                {"selected_troll_count", "6" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Troll", 200},
                { "Item", 11},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "weak_trolls[6]", "Troll"},
                { "strong_trolls[0]", "Troll"},
                { "no_trolls[0]", "Troll"}
            };
            for (int i = 1; i <= 200; i++)
            {
                ExpectedScopeVars["troll" + i.ToString()] = "Troll";
            }
            for (int i = 1; i <= 11; i++)
            {
                ExpectedScopeVars["item" + i.ToString()] = "Item";
            }
            Dictionary<String, String> ExpectedCreatedVarState1 = new Dictionary<String, String>()
            {
            };
            for (int i = 0; i < 6; i++)
            {
                ExpectedCreatedVarState1["weak_trolls[" + i.ToString() + "].health"] = (50 + i + 25).ToString();
                ExpectedCreatedVarState1["weak_trolls[" + i.ToString() + "].attack"] = (10 + i + 25).ToString();
            }
            Dictionary<String, String> ExpectedCreatedVarState2 = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 211;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero", "inv", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv", "inv_slot", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv_slot", "item", "R3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod", "stat", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "stats",
                null,
                new EXERelationshipSelection
                (
                    "hero",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Inventory"),
                        new EXERelationshipLink("R2", "Item_Slot"),
                        new EXERelationshipLink("R3", "Item"),
                        new EXERelationshipLink("R4", "Modifier"),
                        new EXERelationshipLink("R5", "Stat")

                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 1},
                { "Inventory", 1},
                { "Item_Slot", 1},
                { "Item", 1},
                { "Modifier", 1},
                { "Stat", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero", "Hero"},
                { "inv", "Inventory"},
                { "inv_slot", "Item_Slot"},
                { "item", "Item"},
                { "mod", "Modifier"},
                { "stat", "Stat"},
                { "stats[1]", "Stat"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 6;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_15()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero", "inv", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv", "inv_slot", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv_slot", "item", "R3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod", "stat", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod1", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat1", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "stats",
                null,
                new EXERelationshipSelection
                (
                    "hero",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Inventory"),
                        new EXERelationshipLink("R2", "Item_Slot"),
                        new EXERelationshipLink("R3", "Item"),
                        new EXERelationshipLink("R4", "Modifier"),
                        new EXERelationshipLink("R5", "Stat")

                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 1},
                { "Inventory", 1},
                { "Item_Slot", 1},
                { "Item", 1},
                { "Modifier", 2},
                { "Stat", 2}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero", "Hero"},
                { "inv", "Inventory"},
                { "inv_slot", "Item_Slot"},
                { "item", "Item"},
                { "mod", "Modifier"},
                { "stat", "Stat"},
                { "mod1", "Modifier"},
                { "stat1", "Stat"},
                { "stats[2]", "Stat"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 8;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_16()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero", "inv", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv", "inv_slot", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv_slot", "item", "R3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod", "stat", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod1", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat1", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat2", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "stats",
                null,
                new EXERelationshipSelection
                (
                    "hero",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Inventory"),
                        new EXERelationshipLink("R2", "Item_Slot"),
                        new EXERelationshipLink("R3", "Item"),
                        new EXERelationshipLink("R4", "Modifier"),
                        new EXERelationshipLink("R5", "Stat")

                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 1},
                { "Inventory", 1},
                { "Item_Slot", 1},
                { "Item", 1},
                { "Modifier", 2},
                { "Stat", 3}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero", "Hero"},
                { "inv", "Inventory"},
                { "inv_slot", "Item_Slot"},
                { "item", "Item"},
                { "mod", "Modifier"},
                { "stat", "Stat"},
                { "mod1", "Modifier"},
                { "stat1", "Stat"},
                { "stat2", "Stat"},
                { "stats[3]", "Stat"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 9;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_17()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero", "inv", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv", "inv_slot", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv_slot", "item", "R3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod", "stat", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod1", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat1", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat2", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "stats",
                null,
                new EXERelationshipSelection
                (
                    "inv_slot",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R3", "Item"),
                        new EXERelationshipLink("R4", "Modifier"),
                        new EXERelationshipLink("R5", "Stat")

                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 1},
                { "Inventory", 1},
                { "Item_Slot", 1},
                { "Item", 1},
                { "Modifier", 2},
                { "Stat", 3}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero", "Hero"},
                { "inv", "Inventory"},
                { "inv_slot", "Item_Slot"},
                { "item", "Item"},
                { "mod", "Modifier"},
                { "stat", "Stat"},
                { "mod1", "Modifier"},
                { "stat1", "Stat"},
                { "stat2", "Stat"},
                { "stats[3]", "Stat"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 9;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_18()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero", "inv", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv", "inv_slot", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            // No relationship between item and slot
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod", "stat", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod1", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat1", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat2", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "stats",
                null,
                new EXERelationshipSelection
                (
                    "hero",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Inventory"),
                        new EXERelationshipLink("R2", "Item_Slot"),
                        new EXERelationshipLink("R3", "Item"),
                        new EXERelationshipLink("R4", "Modifier"),
                        new EXERelationshipLink("R5", "Stat")
                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 1},
                { "Inventory", 1},
                { "Item_Slot", 1},
                { "Item", 1},
                { "Modifier", 2},
                { "Stat", 3}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero", "Hero"},
                { "inv", "Inventory"},
                { "inv_slot", "Item_Slot"},
                { "item", "Item"},
                { "mod", "Modifier"},
                { "stat", "Stat"},
                { "mod1", "Modifier"},
                { "stat1", "Stat"},
                { "stat2", "Stat"},
                { "stats[0]", "Stat"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 9;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_19()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero", "inv", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv", "inv_slot", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "stats",
                null,
                new EXERelationshipSelection
                (
                    "hero",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Inventory"),
                        new EXERelationshipLink("R2", "Item_Slot"),
                        new EXERelationshipLink("R3", "Item"),
                        new EXERelationshipLink("R4", "Modifier"),
                        new EXERelationshipLink("R5", "Stat")
                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 1},
                { "Inventory", 1},
                { "Item_Slot", 1},
                { "Item", 1},
                { "Modifier", 0},
                { "Stat", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero", "Hero"},
                { "inv", "Inventory"},
                { "inv_slot", "Item_Slot"},
                { "item", "Item"},
                { "stats[0]", "Stat"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 4;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_20()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero1", "inv1", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv1", "inv_slot1", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv_slot1", "item1", "R3"));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero2", "inv2", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv2", "inv_slot2", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv_slot2", "item2", "R3"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(
                EXECommandQuerySelect.CardinalityMany,
                "Hero",
                "heroes"
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "items",
                null,
                new EXERelationshipSelection
                (
                    "heroes",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R1", "Inventory"),
                        new EXERelationshipLink("R2", "Item_Slot"),
                        new EXERelationshipLink("R3", "Item")
                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 2},
                { "Inventory", 2},
                { "Item_Slot", 2},
                { "Item", 2},
                { "Modifier", 0},
                { "Stat", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero1", "Hero"},
                { "inv1", "Inventory"},
                { "inv_slot1", "Item_Slot"},
                { "item1", "Item"},
                { "hero2", "Hero"},
                { "inv2", "Inventory"},
                { "inv_slot2", "Item_Slot"},
                { "item2", "Item"},
                { "heroes[2]", "Hero"},
                { "items[2]", "Item"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 8;
            int ExpectedValidSetRefVarCount = 2;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_21()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero", "inv", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv", "inv_slot", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv_slot", "item", "R3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod", "stat", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "heroes",
                null,
                new EXERelationshipSelection
                (
                    "stat",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R5", "Modifier"),
                        new EXERelationshipLink("R4", "Item"),
                        new EXERelationshipLink("R3", "Item_Slot"),
                        new EXERelationshipLink("R2", "Inventory"),
                        new EXERelationshipLink("R1", "Hero")
                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 1},
                { "Inventory", 1},
                { "Item_Slot", 1},
                { "Item", 1},
                { "Modifier", 1},
                { "Stat", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero", "Hero"},
                { "inv", "Inventory"},
                { "inv_slot", "Item_Slot"},
                { "item", "Item"},
                { "mod", "Modifier"},
                { "stat", "Stat"},
                { "heroes[1]", "Hero"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 6;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Many_01()
        {
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Setup_Observer_Commands(Animation);
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "o",
                null,
                new EXERelationshipSelection
                (
                    "observer",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink
                        (
                            "R1",
                            "Visitor"
                        )
                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 2;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "o",
                null,
                new EXERelationshipSelection
                (
                    "observer",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink
                        (
                            "R1",
                            "Visitor"
                        )
                    }
                )
            ));
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
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation);

            Setup_Observer_Commands(Animation);
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "o",
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite(
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("count")
                            }
                        )
                    }
                ),
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 2;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation, 1);

            Setup_Observer_Commands(Animation);
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "o",
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite(
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("count")
                            }
                        )
                    }
                ),
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 2;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            Setup_Observer_Classes(Animation, 1);

            Setup_Observer_Commands(Animation);
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
                "observer",
                "value",
                new EXEASTNodeLeaf("5")
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "o",
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeComposite(
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected"),
                                new EXEASTNodeLeaf("count")
                            }
                        )
                    }
                ),
                Setup_Observer_RelationshipNavigation(0)
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "subject", "Subject"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 2;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero", "inv", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv", "inv_slot", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv_slot", "item", "R3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod", "stat", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod1", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat1", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat2", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "stats",
                null,
                new EXERelationshipSelection
                (
                    "item",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R3", "Item"),
                        new EXERelationshipLink("R4", "Modifier"),
                        new EXERelationshipLink("R5", "Stat")

                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 1},
                { "Inventory", 1},
                { "Item_Slot", 1},
                { "Item", 1},
                { "Modifier", 2},
                { "Stat", 3}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero", "Hero"},
                { "inv", "Inventory"},
                { "inv_slot", "Item_Slot"},
                { "item", "Item"},
                { "mod", "Modifier"},
                { "stat", "Stat"},
                { "mod1", "Modifier"},
                { "stat1", "Stat"},
                { "stat2", "Stat"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 9;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero", "inv", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv", "inv_slot", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv_slot", "item", "R3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod", "stat", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod1", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat1", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat2", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "stats",
                null,
                new EXERelationshipSelection
                (
                    "item",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R3", "Item"),
                        new EXERelationshipLink("R4", "Modifier"),
                        new EXERelationshipLink("R6", "Stat")

                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 1},
                { "Inventory", 1},
                { "Item_Slot", 1},
                { "Item", 1},
                { "Modifier", 2},
                { "Stat", 3}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero", "Hero"},
                { "inv", "Inventory"},
                { "inv_slot", "Item_Slot"},
                { "item", "Item"},
                { "mod", "Modifier"},
                { "stat", "Stat"},
                { "mod1", "Modifier"},
                { "stat1", "Stat"},
                { "stat2", "Stat"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 9;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void Execute_Bad_Many_08()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Hero");
            Animation.ExecutionSpace.SpawnClass("Inventory");
            Animation.ExecutionSpace.SpawnClass("Item_Slot");
            Animation.ExecutionSpace.SpawnClass("Item");
            Animation.ExecutionSpace.SpawnClass("Modifier");
            Animation.ExecutionSpace.SpawnClass("Stat");
            Animation.RelationshipSpace.SpawnRelationship("Hero", "Inventory");
            Animation.RelationshipSpace.SpawnRelationship("Inventory", "Item_Slot");
            Animation.RelationshipSpace.SpawnRelationship("Item_Slot", "Item");
            Animation.RelationshipSpace.SpawnRelationship("Item", "Modifier");
            Animation.RelationshipSpace.SpawnRelationship("Modifier", "Stat");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Hero", "hero"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Inventory", "inv"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("hero", "inv", "R1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item_Slot", "inv_slot"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv", "inv_slot", "R2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Item", "item"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("inv_slot", "item", "R3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod", "stat", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Modifier", "mod1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("item", "mod1", "R4"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat1", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Stat", "stat2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("mod1", "stat2", "R5"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(
                EXECommandQuerySelect.CardinalityMany,
                "stats",
                null,
                new EXERelationshipSelection
                (
                    "item",
                    new EXERelationshipLink[]
                    {
                        new EXERelationshipLink("R3", "Item"),
                        new EXERelationshipLink("R4", "Modifier"),
                        new EXERelationshipLink("R5", "Hero")
                    }
                )
            ));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Hero", 1},
                { "Inventory", 1},
                { "Item_Slot", 1},
                { "Item", 1},
                { "Modifier", 2},
                { "Stat", 3}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "hero", "Hero"},
                { "inv", "Inventory"},
                { "inv_slot", "Item_Slot"},
                { "item", "Item"},
                { "mod", "Modifier"},
                { "stat", "Stat"},
                { "mod1", "Modifier"},
                { "stat1", "Stat"},
                { "stat2", "Stat"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 9;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "stats");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
    }
}