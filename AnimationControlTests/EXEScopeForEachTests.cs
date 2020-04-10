using Microsoft.VisualStudio.TestTools.UnitTesting;
using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace OALProgramControl.Tests
{
    [TestClass()]
    public class EXEScopeForEachTests
    {
        [TestMethod]
        public void EXEScopeForEach_Normal_01()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                OALProgram.SuperScope,
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
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
                   new EXECommand[]
                   {
                        new EXECommandQueryDelete("observer")
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Subject", "s"),
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        OALProgram.SuperScope,
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
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "subjects"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
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
                            OALProgram.SuperScope,
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

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Subject", "s"),
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        OALProgram.SuperScope,
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
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "subjects"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
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
                            OALProgram.SuperScope,
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

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Subject", "s"),
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        OALProgram.SuperScope,
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
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "subjects"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("observer_count", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
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

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
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

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Subject", "s"),
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        OALProgram.SuperScope,
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
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "subjects"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("observer_count", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "subject",
                   "subjects"
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryDelete("o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryDelete("o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryDelete("o3"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
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

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            CDClass ClassO = OALProgram.ExecutionSpace.SpawnClass("Observer");
            ClassO.AddAttribute(new CDAttribute("value", EXETypes.IntegerTypeName));

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryDelete("o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryDelete("o3"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
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

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetAllHandleStateAttrsDictRecursive(OALProgram.ExecutionSpace);
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observeri"
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "x",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "x"
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "observers"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "observers"));
            OALProgram.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   OALProgram.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = OALProgram.Execute();

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

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = OALProgram.SuperScope.GetSetRefStateAttrsDictRecursive(OALProgram.ExecutionSpace, "observers");
            int ActualValidRefVarCount = OALProgram.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = OALProgram.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

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