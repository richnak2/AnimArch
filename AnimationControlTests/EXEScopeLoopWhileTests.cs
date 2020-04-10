using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace OALProgramControl.Tests
{
    [TestClass]
    public class EXEScopeLoopWhileTests
    {
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_01()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
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

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_02()
        {
            OALProgram OALProgram = new OALProgram();


            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("1")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("i", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment
                    (
                        "x",
                        new EXEASTNodeComposite
                        (
                            "*",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("2")
                            }
                        )
                    ),
                    new EXECommandAssignment(
                        "i",
                        new EXEASTNodeComposite(
                            "+",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("i"),
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
                        new EXEASTNodeLeaf("i"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"i", "10"},
                {"x", "1024"},
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_03()
        {
            OALProgram OALProgram = new OALProgram();


            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("1")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("i", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXEScopeCondition
                    (
                        null,
                        new EXECommand[]
                        {
                            new EXECommandAssignment
                            (
                                "x",
                                new EXEASTNodeComposite
                                (
                                    "*",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("x"),
                                        new EXEASTNodeLeaf("2")
                                    }
                                )
                            )
                        },
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
                                        new EXEASTNodeLeaf("i"),
                                        new EXEASTNodeLeaf("2")
                                    }
                                ),
                                new EXEASTNodeLeaf("0")
                            }
                        ),
                        new EXEScope
                        (
                            null,
                            new EXECommand[]
                            {
                                new EXECommandAssignment
                                (
                                    "x",
                                    new EXEASTNodeComposite
                                    (
                                        "*",
                                        new EXEASTNode[]
                                        {
                                            new EXEASTNodeLeaf("x"),
                                            new EXEASTNodeLeaf("4")
                                        }
                                    )
                                )
                            }
                        )
                    ),
                    new EXECommandAssignment
                    (
                        "i",
                        new EXEASTNodeComposite
                        (
                            "+",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("i"),
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
                        new EXEASTNodeLeaf("i"),
                        new EXEASTNodeLeaf("5")
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"i", "5"},
                {"x", "128"},
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_04()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
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

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_05()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
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

            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"}
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 10}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observers[10]", "Observer"}
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

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_06()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Observer");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
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
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryDelete("o"),
                    new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "o")
                },
                new EXEASTNodeComposite
                (
                    "not_empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("o")
                    }
                )
            ));

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
                { "o", "Observer"}
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
        public void EXEScopeLoopWhile_Normal_07()
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

            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"}
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Subject", 10},
                { "Observer", 45}
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

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_08()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
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
                        new EXEASTNodeLeaf(EXEExecutionGlobals.LoopIterationCap.ToString())
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", EXEExecutionGlobals.LoopIterationCap.ToString()}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_09()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXEScopeCondition
                    (
                        OALProgram.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("0"))
                        },
                        new EXEASTNodeComposite
                        (
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("0")
                            }
                        ),
                        new EXEScope
                        (
                            OALProgram.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandAssignment("y", new EXEASTNodeLeaf("\"0\""))
                            }
                        )
                    ),
                    new EXECommandAssignment
                    (
                        "x",
                        new EXEASTNodeComposite
                        (
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
                        new EXEASTNodeLeaf("2")
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "2"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Bad_01()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
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
                    "+",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "0"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Bad_02()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
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
                        new EXEASTNodeLeaf("y"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "0"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Bad_03()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment(
                        "x",
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
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "0"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Bad_04()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
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
                new EXEASTNodeLeaf(EXETypes.BooleanTrue)
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", EXEExecutionGlobals.LoopIterationCap.ToString()}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();
            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState, ActualPrimitiveVarState["x"]);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Bad_05()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                },
                new EXEASTNodeComposite
                (
                    "<",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("3000")
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "0"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Stress_01()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));

            int cap = 10;
            EXEScope Temp;
            EXEScope Current = OALProgram.SuperScope;
            Char x = 'a';
            for (int i = 0; i < cap; i++)
            {
                Current.AddCommand(new EXECommandAssignment(x.ToString(), new EXEASTNodeLeaf("0")));
                if (i != 0)
                {
                    Current.AddCommand
                    (
                        new EXECommandAssignment
                        (
                            ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                            new EXEASTNodeComposite
                            (
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                                    new EXEASTNodeLeaf("1")
                                }
                            )
                        )
                    );
                }


                Temp = new EXEScopeLoopWhile
                (
                    null,
                    new EXECommand[]
                    {
                    },
                    new EXEASTNodeComposite
                    (
                        "<",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(x.ToString()),
                            new EXEASTNodeLeaf("2")
                        }
                    )
                );
                x = (Char)(Convert.ToUInt16(x) + 1);

                Current.AddCommand(Temp);
                Current = Temp;
            }
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    "x",
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf("x"),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "1024"},
                {"a", "2"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Stress_02()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));

            int cap = 13;
            EXEScope Temp;
            EXEScope Current = OALProgram.SuperScope;
            Char x = 'a';
            for (int i = 0; i < cap; i++)
            {
                Current.AddCommand(new EXECommandAssignment(x.ToString(), new EXEASTNodeLeaf("0")));
                if (i != 0)
                {
                    Current.AddCommand
                    (
                        new EXECommandAssignment
                        (
                            ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                            new EXEASTNodeComposite
                            (
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                                    new EXEASTNodeLeaf("1")
                                }
                            )
                        )
                    );
                }


                Temp = new EXEScopeLoopWhile
                (
                    null,
                    new EXECommand[]
                    {
                    },
                    new EXEASTNodeComposite
                    (
                        "<",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(x.ToString()),
                            new EXEASTNodeLeaf("2")
                        }
                    )
                );
                x = (Char)(Convert.ToUInt16(x) + 1);

                Current.AddCommand(Temp);
                Current = Temp;
            }
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    "x",
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf("x"),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "8192"},
                {"a", "2"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Stress_03()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));

            int cap = 14;
            EXEScope Temp;
            EXEScope Current = OALProgram.SuperScope;
            Char x = 'a';
            for (int i = 0; i < cap; i++)
            {
                Current.AddCommand(new EXECommandAssignment(x.ToString(), new EXEASTNodeLeaf("0")));
                if (i != 0)
                {
                    Current.AddCommand
                    (
                        new EXECommandAssignment
                        (
                            ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                            new EXEASTNodeComposite
                            (
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                                    new EXEASTNodeLeaf("1")
                                }
                            )
                        )
                    );
                }


                Temp = new EXEScopeLoopWhile
                (
                    null,
                    new EXECommand[]
                    {
                    },
                    new EXEASTNodeComposite
                    (
                        "<",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(x.ToString()),
                            new EXEASTNodeLeaf("2")
                        }
                    )
                );
                x = (Char)(Convert.ToUInt16(x) + 1);

                Current.AddCommand(Temp);
                Current = Temp;
            }
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    "x",
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf("x"),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "16384"},
                {"a", "2"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Stress_04()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));

            int cap = 15;
            EXEScope Temp;
            EXEScope Current = OALProgram.SuperScope;
            Char x = 'a';
            for (int i = 0; i < cap; i++)
            {
                Current.AddCommand(new EXECommandAssignment(x.ToString(), new EXEASTNodeLeaf("0")));
                if (i != 0)
                {
                    Current.AddCommand
                    (
                        new EXECommandAssignment
                        (
                            ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                            new EXEASTNodeComposite
                            (
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                                    new EXEASTNodeLeaf("1")
                                }
                            )
                        )
                    );
                }


                Temp = new EXEScopeLoopWhile
                (
                    null,
                    new EXECommand[]
                    {
                    },
                    new EXEASTNodeComposite
                    (
                        "<",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(x.ToString()),
                            new EXEASTNodeLeaf("2")
                        }
                    )
                );
                x = (Char)(Convert.ToUInt16(x) + 1);

                Current.AddCommand(Temp);
                Current = Temp;
            }
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    "x",
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf("x"),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "32768"},
                {"a", "2"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Stress_05()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));

            int cap = 5;
            EXEScope Temp;
            EXEScope Current = OALProgram.SuperScope;
            Char x = 'a';
            for (int i = 0; i < cap; i++)
            {
                Current.AddCommand(new EXECommandAssignment(x.ToString(), new EXEASTNodeLeaf("0")));
                if (i != 0)
                {
                    Current.AddCommand
                    (
                        new EXECommandAssignment
                        (
                            ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                            new EXEASTNodeComposite
                            (
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                                    new EXEASTNodeLeaf("1")
                                }
                            )
                        )
                    );
                }


                Temp = new EXEScopeLoopWhile
                (
                    null,
                    new EXECommand[]
                    {
                    },
                    new EXEASTNodeComposite
                    (
                        "<",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(x.ToString()),
                            new EXEASTNodeLeaf("4")
                        }
                    )
                );
                x = (Char)(Convert.ToUInt16(x) + 1);

                Current.AddCommand(Temp);
                Current = Temp;
            }
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    "x",
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf("x"),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "1024"},
                {"a", "4"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Stress_06()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));

            int cap = 6;
            EXEScope Temp;
            EXEScope Current = OALProgram.SuperScope;
            Char x = 'a';
            for (int i = 0; i < cap; i++)
            {
                Current.AddCommand(new EXECommandAssignment(x.ToString(), new EXEASTNodeLeaf("0")));
                if (i != 0)
                {
                    Current.AddCommand
                    (
                        new EXECommandAssignment
                        (
                            ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                            new EXEASTNodeComposite
                            (
                                "+",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                                    new EXEASTNodeLeaf("1")
                                }
                            )
                        )
                    );
                }


                Temp = new EXEScopeLoopWhile
                (
                    null,
                    new EXECommand[]
                    {
                    },
                    new EXEASTNodeComposite
                    (
                        "<",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(x.ToString()),
                            new EXEASTNodeLeaf("7")
                        }
                    )
                );
                x = (Char)(Convert.ToUInt16(x) + 1);

                Current.AddCommand(Temp);
                Current = Temp;
            }
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    "x",
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf("x"),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );
            Current.AddCommand
            (
                new EXECommandAssignment
                (
                    ((Char)(Convert.ToUInt16(x) - 1)).ToString(),
                    new EXEASTNodeComposite
                    (
                        "+",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf(((Char)(Convert.ToUInt16(x) - 1)).ToString()),
                            new EXEASTNodeLeaf("1")
                        }
                    )
                )
            );

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "117649"},
                {"a", "7"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
    }
}
