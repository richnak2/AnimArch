using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandContinueTests
    {
        [TestMethod]
        public void Execute_Normal_While_01()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile(
                Animation.SuperScope,
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
                    new EXECommandContinue(),
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

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"},
                {"y", "0"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_While_02()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile(
                Animation.SuperScope,
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
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandContinue()
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

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "20"},
                {"y", "10"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_While_03()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("counter", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        Animation.SuperScope,
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
                                Animation.SuperScope,
                                new EXECommand[]
                                {
                                    new EXECommandContinue()
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

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "20"},
                {"counter", "200"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_Foreach_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Visitor");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile(
                Animation.SuperScope,
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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Visitor", "visitors"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand
            (
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
                        ),
                        new EXECommandContinue(),
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

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"},
                {"y", "0"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_Foreach_02()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Visitor");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile(
                Animation.SuperScope,
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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Visitor", "visitors"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand
            (
                new EXEScopeForEach
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandContinue()
                    },
                    "visitor",
                    "visitors"
                )
            );

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "0"},
                {"y", "0"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_Foreach_03()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Visitor");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile(
                Animation.SuperScope,
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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Visitor", "visitors"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand
            (
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
                        ),
                        new EXEScopeCondition
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandContinue()
                            },
                            new EXEASTNodeComposite
                            (
                                "<",
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

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"},
                {"y", "6"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_Foreach_04()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Visitor");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile(
                Animation.SuperScope,
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
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Visitor", "visitors"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand
            (
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
                        ),
                        new EXEScopeCondition
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXEScopeCondition
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXEScopeCondition
                                        (
                                            Animation.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXEScopeCondition
                                                (
                                                    Animation.SuperScope,
                                                    new EXECommand[]
                                                    {
                                                        new EXEScopeCondition
                                                        (
                                                            Animation.SuperScope,
                                                            new EXECommand[]
                                                            {
                                                                new EXEScopeCondition
                                                                (
                                                                    Animation.SuperScope,
                                                                    new EXECommand[]
                                                                    {
                                                                        new EXEScopeCondition
                                                                        (
                                                                            Animation.SuperScope,
                                                                            new EXECommand[]
                                                                            {
                                                                                new EXEScopeCondition
                                                                                (
                                                                                    Animation.SuperScope,
                                                                                    new EXECommand[]
                                                                                    {
                                                                                        new EXECommandContinue()
                                                                                    },
                                                                                    new EXEASTNodeComposite
                                                                                    (
                                                                                        "<",
                                                                                        new EXEASTNode[]
                                                                                        {
                                                                                            new EXEASTNodeLeaf("x"),
                                                                                            new EXEASTNodeLeaf("5")
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

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"},
                {"y", "6"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandContinue());


            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Bad_02()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand
            (
                new EXEScopeCondition
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandContinue()
                    },
                    new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                )
            );


            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Bad_03()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand
            (
                new EXEScopeLoopWhile
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandContinue()
                    },
                    new EXEASTNodeLeaf(EXETypes.BooleanTrue)
                )
            );


            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
    }
}