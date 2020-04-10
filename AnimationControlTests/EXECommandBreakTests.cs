using Microsoft.VisualStudio.TestTools.UnitTesting;
using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace OALProgramControl.Tests
{
    [TestClass]
    public class EXECommandBreakTests
    {
        [TestMethod]
        public void Execute_Normal_While_01()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
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
                    ),
                    new EXECommandBreak(),
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
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "1"},
                {"y", "0"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_While_02()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(
            new EXEScopeLoopWhile
           (
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
                    ),
                    new EXEScopeCondition
                    (
                        OALProgram.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandBreak()
                        },
                        new EXEASTNodeComposite
                        (
                            ">",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("10")
                            }
                        )
                    ),
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
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("20")
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "11"},
                {"y", "10"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_While_03()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("counter", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        OALProgram.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment
                            (
                                "y",
                                new EXEASTNodeComposite
                                (
                                    "+",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("y"),
                                        new EXEASTNodeLeaf("1")
                                    }
                                )
                            ),
                            new EXEScopeCondition
                            (
                                OALProgram.SuperScope,
                                new EXECommand[]
                                {
                                    new EXECommandBreak()
                                },
                                new EXEASTNodeComposite
                                (
                                    ">=",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("x"),
                                        new EXEASTNodeLeaf("10"),
                                    }
                                )
                            ),
                            new EXECommandAssignment
                            (
                                "counter",
                                new EXEASTNodeComposite
                                (
                                    "+",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("counter"),
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
                                new EXEASTNodeLeaf("20")
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
                        new EXEASTNodeLeaf("20")
                    }
                )
            ));

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "20"},
                {"counter", "200"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_While_04()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand
            (
                new EXEScopeLoopWhile
                (
                    OALProgram.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandBreak()
                    },
                    new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                )
            );


            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_Foreach_01()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Visitor");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Visitor"),
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
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Visitor", "visitors"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand
            (
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
                        ),
                        new EXECommandBreak(),
                        new EXECommandAssignment
                        (
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
                    "visitor",
                    "visitors"
                )
            );

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "1"},
                {"y", "0"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_Foreach_02()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Visitor");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Visitor"),
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
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Visitor", "visitors"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand
            (
                new EXEScopeForEach
                (
                    OALProgram.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandBreak()
                    },
                    "visitor",
                    "visitors"
                )
            );

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "0"},
                {"y", "0"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_Foreach_03()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Visitor");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Visitor"),
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
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Visitor", "visitors"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand
            (
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
                        ),
                        new EXEScopeCondition
                        (
                            OALProgram.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandBreak()
                            },
                            new EXEASTNodeComposite
                            (
                                ">",
                                new EXEASTNode[]
                                {
                                    new EXEASTNodeLeaf("x"),
                                    new EXEASTNodeLeaf("5")
                                }
                            )
                        ),
                        new EXECommandAssignment
                        (
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
                    "visitor",
                    "visitors"
                )
            );

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "6"},
                {"y", "5"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_Foreach_04()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.ExecutionSpace.SpawnClass("Visitor");

            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXEScopeLoopWhile(
                OALProgram.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Visitor"),
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
            OALProgram.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Visitor", "visitors"));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            OALProgram.SuperScope.AddCommand
            (
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
                        ),
                        new EXEScopeCondition
                        (
                            OALProgram.SuperScope,
                            new EXECommand[]
                            {
                                new EXEScopeCondition
                                (
                                    OALProgram.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXEScopeCondition
                                        (
                                            OALProgram.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXEScopeCondition
                                                (
                                                    OALProgram.SuperScope,
                                                    new EXECommand[]
                                                    {
                                                        new EXEScopeCondition
                                                        (
                                                            OALProgram.SuperScope,
                                                            new EXECommand[]
                                                            {
                                                                new EXEScopeCondition
                                                                (
                                                                    OALProgram.SuperScope,
                                                                    new EXECommand[]
                                                                    {
                                                                        new EXEScopeCondition
                                                                        (
                                                                            OALProgram.SuperScope,
                                                                            new EXECommand[]
                                                                            {
                                                                                new EXEScopeCondition
                                                                                (
                                                                                    OALProgram.SuperScope,
                                                                                    new EXECommand[]
                                                                                    {
                                                                                        new EXECommandBreak()
                                                                                    },
                                                                                    new EXEASTNodeComposite
                                                                                    (
                                                                                        ">",
                                                                                        new EXEASTNode[]
                                                                                        {
                                                                                            new EXEASTNodeLeaf("x"),
                                                                                            new EXEASTNodeLeaf("4")
                                                                                        }
                                                                                    )
                                                                                )
                                                                            },
                                                                            new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                                                                        )
                                                                    },
                                                                    new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                                                                )
                                                            },
                                                            new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                                                        )
                                                    },
                                                    new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                                                )
                                            },
                                            new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                                        )
                                    },
                                    new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                                )
                            },
                            new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                        ),
                        new EXECommandAssignment
                        (
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
                    "visitor",
                    "visitors"
                )
            );

            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "5"},
                {"y", "4"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandBreak());


            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Bad_02()
        {
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand
            (
                new EXEScopeCondition
                (
                    OALProgram.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandBreak()
                    },
                    new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                )
            );


            Boolean Success = OALProgram.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };

            Dictionary<String, String> ActualPrimitiveVarState = OALProgram.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
    }
}