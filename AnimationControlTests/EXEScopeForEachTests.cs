using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass()]
    public class EXEScopeForEachTests
    {
        [TestMethod]
        public void EXEScopeForEach_Normal_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "observers[1]", "Observer"},
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
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
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Normal_02()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 2}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "observers[2]", "Observer"},
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
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
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Normal_03()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Observer"),
                    new EXECommandAssignment(
                        "x",
                        new EXEASTNodeComposite(
                            "+",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("1")
                            }
                        )
                    )
                },
                new EXEASTNodeComposite
                (
                    "<",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                        new EXECommandQueryDelete("observer")
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"}
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "observers[0]", "Observer"},
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
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
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Normal_04()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Subject", "s"),
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandQueryCreate("Observer", "o"),
                            new EXECommandQueryRelate("o", "s", "R1"),
                            new EXECommandAssignment(
                                "y",
                                new EXEASTNodeComposite(
                                    "+",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("y"),
                                        new EXEASTNodeLeaf("1")
                                    }
                                )
                            )
                        },
                        new EXEASTNodeComposite
                        (
                            "<",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("y"),
                                new EXEASTNodeLeaf("x")
                            }
                        )
                    ),
                    new EXECommandAssignment(
                        "x",
                        new EXEASTNodeComposite(
                            "+",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("1")
                            }
                        )
                    )
                },
                new EXEASTNodeComposite
                (
                    "<",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "subjects"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                        new EXECommandQuerySelectRelatedBy
                        (
                            EXECommandQuerySelect.CardinalityMany,
                            "observers",
                            null,
                            new EXERelationshipSelection
                            (
                                "subject",
                                new EXERelationshipLink[]
                                {
                                    new EXERelationshipLink("R1", "Observer")
                                }
                            )
                        ),
                        new EXEScopeForEach
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandQueryUnrelate("observer", "subject", "R1"),
                            },
                            "observer",
                            "observers"
                        )
                   },
                   "subject",
                   "subjects"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Subject", 10 },
                { "Observer", 45 }
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "subject", "Subject" },
                { "subjects[10]", "Subject" }
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
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
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Normal_05()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Subject", "s"),
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandQueryCreate("Observer", "o"),
                            new EXECommandQueryRelate("o", "s", "R1"),
                            new EXECommandAssignment(
                                "y",
                                new EXEASTNodeComposite(
                                    "+",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("y"),
                                        new EXEASTNodeLeaf("1")
                                    }
                                )
                            )
                        },
                        new EXEASTNodeComposite
                        (
                            "<",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("y"),
                                new EXEASTNodeLeaf("x")
                            }
                        )
                    ),
                    new EXECommandAssignment(
                        "x",
                        new EXEASTNodeComposite(
                            "+",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("1")
                            }
                        )
                    )
                },
                new EXEASTNodeComposite
                (
                    "<",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "subjects"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                        new EXECommandQuerySelectRelatedBy
                        (
                            EXECommandQuerySelect.CardinalityMany,
                            "observers",
                            null,
                            new EXERelationshipSelection
                            (
                                "subject",
                                new EXERelationshipLink[]
                                {
                                    new EXERelationshipLink("R1", "Observer")
                                }
                            )
                        ),
                        new EXEScopeForEach
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandQueryUnrelate("observer", "subject", "R1"),
                                new EXECommandQueryDelete("observer")
                            },
                            "observer",
                            "observers"
                        ),
                        new EXECommandQueryDelete("subject")
                   },
                   "subject",
                   "subjects"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Subject", 0 },
                { "Observer", 0 }
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "subject", "Subject" },
                { "subjects[0]", "Subject" }
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
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
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Normal_06()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Subject", "s"),
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandQueryCreate("Observer", "o"),
                            new EXECommandQueryRelate("o", "s", "R1"),
                            new EXECommandAssignment(
                                "y",
                                new EXEASTNodeComposite(
                                    "+",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("y"),
                                        new EXEASTNodeLeaf("1")
                                    }
                                )
                            )
                        },
                        new EXEASTNodeComposite
                        (
                            "<",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("y"),
                                new EXEASTNodeLeaf("x")
                            }
                        )
                    ),
                    new EXECommandAssignment
                    (
                        "x",
                        new EXEASTNodeComposite(
                            "+",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("1")
                            }
                        )
                    )
                },
                new EXEASTNodeComposite
                (
                    "<",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "subjects"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer_count", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                        new EXECommandQuerySelectRelatedBy
                        (
                            EXECommandQuerySelect.CardinalityMany,
                            "observers",
                            null,
                            new EXERelationshipSelection
                            (
                                "subject",
                                new EXERelationshipLink[]
                                {
                                    new EXERelationshipLink("R1", "Observer")
                                }
                            )
                        ),
                        new EXECommandAssignment
                        (
                            "observer_count",
                            new EXEASTNodeComposite
                            (
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf("observer_count"),
                                    new EXEASTNodeComposite
                                    (
                                        "cardinality",
                                        new EXEASTNode[]
                                        {
                                            new EXEASTNodeLeaf("observers")
                                        }
                                    )
                                }
                            )
                        )
                   },
                   "subject",
                   "subjects"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10" },
                {"observer_count", "45" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Subject", 10 },
                { "Observer", 45 }
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "subject", "Subject" },
                { "subjects[10]", "Subject" }
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
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
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Normal_07()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                        new EXECommandAssignment
                        (
                            "x",
                            new EXEASTNodeComposite(
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf("x"),
                                    new EXEASTNodeLeaf("1")
                                }
                            )
                        )
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                { "x", "0" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "observers[0]", "Observer"},
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
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
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Normal_08()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Subject", "s"),
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandQueryCreate("Observer", "o"),
                            new EXECommandQueryRelate("o", "s", "R1"),
                            new EXECommandAssignment(
                                "y",
                                new EXEASTNodeComposite(
                                    "+",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("y"),
                                        new EXEASTNodeLeaf("1")
                                    }
                                )
                            )
                        },
                        new EXEASTNodeComposite
                        (
                            "<",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("y"),
                                new EXEASTNodeLeaf("x")
                            }
                        )
                    ),
                    new EXECommandAssignment
                    (
                        "x",
                        new EXEASTNodeComposite(
                            "+",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("1")
                            }
                        )
                    )
                },
                new EXEASTNodeComposite
                (
                    "<",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "subjects"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer_count", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "subject",
                   "subjects"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10" },
                {"observer_count", "0" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Subject", 10 },
                { "Observer", 45 }
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "subject", "Subject" },
                { "subjects[10]", "Subject" }
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
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
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Normal_09()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("o1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("o2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("o3"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                        new EXECommandAssignment
                        (
                            "x",
                            new EXEASTNodeComposite(
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf("x"),
                                    new EXEASTNodeLeaf("1")
                                }
                            )
                        )
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                { "x", "0" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "observers[0]", "Observer"},
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
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
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Normal_10()
        {
            Animation Animation = new Animation();
            CDClass ClassO = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassO.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("o1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("o3"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                       new EXECommandAssignment
                        (
                            "observer",
                            "value",
                            new EXEASTNodeComposite(
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf("x"),
                                    new EXEASTNodeLeaf("4")
                                }
                            )
                        ),
                        new EXECommandAssignment
                        (
                            "x",
                            new EXEASTNodeComposite(
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf("x"),
                                    new EXEASTNodeLeaf("1")
                                }
                            )
                        )
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                { "x", "1" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "observers[1]", "Observer"},
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
                { "o2.value", "4" },
                { "observer.value", "4" },
                { "observers[0].value", "4"}
            };
            int ExpectedValidRefVarCount = 2;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetAllHandleStateAttrsDictRecursive(Animation.ExecutionSpace);
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Bad_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observeri"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers[1]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Bad_02()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "x",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                { "x", "0" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers[1]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Bad_03()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "x"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                { "x", "0" }
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers[1]", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 1;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Bad_04()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };
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

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Bad_05()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeForEach_Bad_06()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
    }
}