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
        private void Setup_Observer_Classes(Animation Animation)
        {
            Animation.ExecutionSpace.SpawnClass("Observer");
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
            }

            return RelSel;
        }
        private void Setup_Monster_Classes(Animation Animation)
        {
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.RealTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.RealTypeName));

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
        }/*
        [TestMethod]
        public void Execute_Normal_Any_08()
        {
            Animation Animation = new Animation();
            Setup_Monster_Classes(Animation);

            for (int i = 1; i <= 5000; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "sm" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("sm" + i.ToString(), "mana", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            Animation.SuperScope.AddCommand(
                new EXECommandQuerySelectRelatedBy(
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
                new EXECommandQuerySelectRelatedBy(
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
                new EXECommandQuerySelectRelatedBy(
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
                new EXECommandQuerySelectRelatedBy(
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
                new EXECommandQuerySelectRelatedBy(
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

            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityAny, "Observer", "o"));

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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityAny, "Subject", "s"));
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

            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityAny, "Subject", "s"));
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

            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityAny, "Subject", "s"));
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
                new EXECommandQuerySelectRelatedBy
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
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
        }
        [TestMethod]
        public void Execute_Good_Many_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityMany, "Observer", "o"));
            Boolean ExecutionSuccess = Animation.Execute();

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
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityMany, "Observer", "o"));
            Boolean ExecutionSuccess = Animation.Execute();

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
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityMany, "Observer", "o"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "o");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualNonEmptySetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o2", "value", new EXEASTNodeLeaf("4")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o3", "value", new EXEASTNodeLeaf("5")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o4"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o4", "value", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o5"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o5", "value", new EXEASTNodeLeaf("7")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o2", "value", new EXEASTNodeLeaf("4")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o3", "value", new EXEASTNodeLeaf("5")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o4"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o4", "value", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o5"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o5", "value", new EXEASTNodeLeaf("7")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o2", "value", new EXEASTNodeLeaf("4")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o3", "value", new EXEASTNodeLeaf("5")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o4"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o4", "value", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o5"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o5", "value", new EXEASTNodeLeaf("7")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o2", "value", new EXEASTNodeLeaf("4")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o3", "value", new EXEASTNodeLeaf("5")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o4"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o4", "value", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o5"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o5", "value", new EXEASTNodeLeaf("7")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o1", "value", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o2", "value", new EXEASTNodeLeaf("4")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o3", "value", new EXEASTNodeLeaf("5")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o4"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o4", "value", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o5"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o5", "value", new EXEASTNodeLeaf("7")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Boolean ExecutionSuccess = Animation.Execute();

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
                ExpectedCreatedVarState1["weak_trolls[" + i.ToString() + "].health"] = (51 + i).ToString();
                ExpectedCreatedVarState1["weak_trolls[" + i.ToString() + "].attack"] = (11 + i).ToString();
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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState1 = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "weak_trolls");
            Dictionary<String, String> ActualCreatedVarState2 = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "strong_trolls");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Boolean ExecutionSuccess = Animation.Execute();

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
        public void Execute_Good_Many_12()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Boolean ExecutionSuccess = Animation.Execute();

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
        public void Execute_Good_Many_13()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
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
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
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

            Boolean ExecutionSuccess = Animation.Execute();

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
        public void Execute_Good_Many_15()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
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
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
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

            Boolean ExecutionSuccess = Animation.Execute();

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
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Troll");
            ClassObserver.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            ClassObserver.AddAttribute(new CDAttribute("attack", EXETypes.IntegerTypeName));

            for (int i = 1; i <= 200; i++)
            {
                Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Troll", "troll" + i.ToString()));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "health", new EXEASTNodeLeaf((50 + i).ToString())));
                Animation.SuperScope.AddCommand(new EXECommandAssignment("troll" + i.ToString(), "attack", new EXEASTNodeLeaf((10 + i).ToString())));
            }
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
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
            Animation.SuperScope.AddCommand(new EXECommandAssignment(
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

            Boolean ExecutionSuccess = Animation.Execute();

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
        public void Execute_Bad_Many_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityMany, "Subject", "s"));
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
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "s");
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

            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityMany, "Observer", "s"));
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
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "s");
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
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "s");
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
            CDClass Class = Animation.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "s");
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
            CDClass Class = Animation.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o", "count", new EXEASTNodeLeaf("50")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "s");
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
            CDClass Class = Animation.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("o", "count", new EXEASTNodeLeaf("50")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "s");
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
            CDClass Class = Animation.ExecutionSpace.SpawnClass("Observer");
            Class.AddAttribute(new CDAttribute("count", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelectRelatedBy
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
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "s");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }*/
    }
}