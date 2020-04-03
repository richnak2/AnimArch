using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEScopeConditionTests
    {
        [TestMethod]
        public void EXEScopeCondition_Normal_01()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("1")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("11"))
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
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "20"},
                {"y", "11"},
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_02()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("1")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("11"))
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
                {"x", "20"},
                {"y", "1"},
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_03()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("1")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("10"))
                },
                new EXEASTNodeComposite
                (
                    "<",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                ),
                new EXEScope
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandAssignment("y", new EXEASTNodeLeaf("5"))
                    }
                )
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "20"},
                {"y", "5"},
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_04()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("1")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("10"))
                },
                new EXEASTNodeComposite
                (
                    ">",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                ),
                new EXEScope
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandAssignment("y", new EXEASTNodeLeaf("5"))
                    }
                )
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "20"},
                {"y", "10"},
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_05()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("1")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("\"\"")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("\"Zero\""))
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
                new EXEScopeCondition[]
                {
                    new EXEScopeCondition
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("\"One\""))
                        },
                        new EXEASTNodeComposite(
                            "==",
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
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("\"Two\""))
                        },
                        new EXEASTNodeComposite(
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("2")
                            }
                        )
                    )
                },
                new EXEScope
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandAssignment("y", new EXEASTNodeLeaf("\"None\""))
                    }
                )
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "1"},
                {"y", "\"One\""},
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_06()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("\"\"")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("\"Zero\""))
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
                new EXEScopeCondition[]
                {
                    new EXEScopeCondition
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("\"One\""))
                        },
                        new EXEASTNodeComposite(
                            "==",
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
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("\"Two\""))
                        },
                        new EXEASTNodeComposite(
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("2")
                            }
                        )
                    )
                },
                new EXEScope
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandAssignment("y", new EXEASTNodeLeaf("\"None\""))
                    }
                )
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "0"},
                {"y", "\"Zero\""},
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_07()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("2")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("\"\"")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("\"Zero\""))
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
                new EXEScopeCondition[]
                {
                    new EXEScopeCondition
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("\"One\""))
                        },
                        new EXEASTNodeComposite(
                            "==",
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
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("\"Two\""))
                        },
                        new EXEASTNodeComposite(
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("2")
                            }
                        )
                    )
                },
                new EXEScope
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandAssignment("y", new EXEASTNodeLeaf("\"None\""))
                    }
                )
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "2"},
                {"y", "\"Two\""},
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_08()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("\"\"")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("\"Zero\""))
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
                new EXEScopeCondition[]
                {
                    new EXEScopeCondition
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("\"One\""))
                        },
                        new EXEASTNodeComposite(
                            "==",
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
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("\"Two\""))
                        },
                        new EXEASTNodeComposite(
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("2")
                            }
                        )
                    )
                },
                new EXEScope
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandAssignment("y", new EXEASTNodeLeaf("\"None\""))
                    }
                )
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "3"},
                {"y", "\"None\""},
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_09()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("3")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("\"\"")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("\"Zero\""))
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
                new EXEScopeCondition[]
                {
                    new EXEScopeCondition
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("\"One\""))
                        },
                        new EXEASTNodeComposite(
                            "==",
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
                            new EXECommandAssignment("y", new EXEASTNodeLeaf("\"Two\""))
                        },
                        new EXEASTNodeComposite(
                            "==",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("2")
                            }
                        )
                    )
                }
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "3"},
                {"y", "\"\""},
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_10()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("active", EXETypes.BooleanTypeName));
            ClassObserver.AddAttribute(new CDAttribute("status", EXETypes.StringTypeName));


            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer", "active", new EXEASTNodeLeaf(EXETypes.BooleanTrue)));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "selected_o"));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXEScopeCondition(
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("observer", "status", new EXEASTNodeLeaf("\"Active\""))
                        },
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected_o"),
                                new EXEASTNodeLeaf("active")
                            }
                        ),
                        new EXEScopeCondition[]
                        {
                            new EXEScopeCondition(
                                Animation.SuperScope,
                                new EXECommand[]
                                {
                                    new EXECommandAssignment("selected_o", "status", new EXEASTNodeLeaf("\"Inactive\""))
                                },
                                new EXEASTNodeComposite
                                (
                                    "not",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected_o"),
                                                new EXEASTNodeLeaf("active")
                                            }
                                        )
                                    }
                                )
                            )
                        }
                    )
                },
                new EXEASTNodeComposite
                (
                    "not_empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("selected_o")
                    }
                )
            ));

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
                { "selected_o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarsState = new Dictionary<String, String>()
            {
                { "observer.active", EXETypes.BooleanTrue },
                { "observer.status", "\"Active\"" },
                { "selected_o.active", EXETypes.BooleanTrue },
                { "selected_o.status", "\"Active\"" }
            };
            int ExpectedValidRefVarCount = 2;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarsState = Animation.SuperScope.GetAllHandleStateAttrsDictRecursive(Animation.ExecutionSpace);
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarsState, ActualCreatedVarsState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_11()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("active", EXETypes.BooleanTypeName));
            ClassObserver.AddAttribute(new CDAttribute("status", EXETypes.StringTypeName));


            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer", "active", new EXEASTNodeLeaf(EXETypes.BooleanFalse)));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "selected_o"));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXEScopeCondition(
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("observer", "status", new EXEASTNodeLeaf("\"Active\""))
                        },
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected_o"),
                                new EXEASTNodeLeaf("active")
                            }
                        ),
                        new EXEScopeCondition[]
                        {
                            new EXEScopeCondition(
                                Animation.SuperScope,
                                new EXECommand[]
                                {
                                    new EXECommandAssignment("selected_o", "status", new EXEASTNodeLeaf("\"Inactive\""))
                                },
                                new EXEASTNodeComposite
                                (
                                    "not",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected_o"),
                                                new EXEASTNodeLeaf("active")
                                            }
                                        )
                                    }
                                )
                            )
                        }
                    )
                },
                new EXEASTNodeComposite
                (
                    "not_empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("selected_o")
                    }
                )
            ));

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
                { "observer", "Observer"},
                { "selected_o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarsState = new Dictionary<String, String>()
            {
                { "observer.active", EXETypes.BooleanFalse},
                { "observer.status", "\"Inactive\"" },
                { "selected_o.active", EXETypes.BooleanFalse },
                { "selected_o.status", "\"Inactive\"" }
            };
            int ExpectedValidRefVarCount = 2;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarsState = Animation.SuperScope.GetAllHandleStateAttrsDictRecursive(Animation.ExecutionSpace);
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarsState, ActualCreatedVarsState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_12()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("active", EXETypes.BooleanTypeName));
            ClassObserver.AddAttribute(new CDAttribute("status", EXETypes.StringTypeName));


            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "observer"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("observer", "active", new EXEASTNodeLeaf(EXETypes.BooleanFalse)));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "selected_o"));
            Animation.SuperScope.AddCommand(new EXECommandQueryDelete("observer"));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXEScopeCondition(
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("observer", "status", new EXEASTNodeLeaf("\"Active\""))
                        },
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected_o"),
                                new EXEASTNodeLeaf("active")
                            }
                        ),
                        new EXEScopeCondition[]
                        {
                            new EXEScopeCondition(
                                Animation.SuperScope,
                                new EXECommand[]
                                {
                                    new EXECommandAssignment("selected_o", "status", new EXEASTNodeLeaf("\"Inactive\""))
                                },
                                new EXEASTNodeComposite
                                (
                                    "not",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected_o"),
                                                new EXEASTNodeLeaf("active")
                                            }
                                        )
                                    }
                                )
                            )
                        }
                    )
                },
                new EXEASTNodeComposite
                (
                    "not_empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("selected_o")
                    }
                )
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "selected_o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarsState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarsState = Animation.SuperScope.GetAllHandleStateAttrsDictRecursive(Animation.ExecutionSpace);
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarsState, ActualCreatedVarsState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_13()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("active", EXETypes.BooleanTypeName));
            ClassObserver.AddAttribute(new CDAttribute("status", EXETypes.StringTypeName));


            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "selected_o"));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXEScopeCondition(
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("observer", "status", new EXEASTNodeLeaf("\"Active\""))
                        },
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected_o"),
                                new EXEASTNodeLeaf("active")
                            }
                        ),
                        new EXEScopeCondition[]
                        {
                            new EXEScopeCondition(
                                Animation.SuperScope,
                                new EXECommand[]
                                {
                                    new EXECommandAssignment("selected_o", "status", new EXEASTNodeLeaf("\"Inactive\""))
                                },
                                new EXEASTNodeComposite
                                (
                                    "not",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected_o"),
                                                new EXEASTNodeLeaf("active")
                                            }
                                        )
                                    }
                                )
                            )
                        }
                    )
                },
                new EXEASTNodeComposite
                (
                    "not_empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("selected_o")
                    }
                )
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "selected_o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarsState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarsState = Animation.SuperScope.GetAllHandleStateAttrsDictRecursive(Animation.ExecutionSpace);
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarsState, ActualCreatedVarsState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_14()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddAttribute(new CDAttribute("active", EXETypes.BooleanTypeName));
            ClassObserver.AddAttribute(new CDAttribute("status", EXETypes.StringTypeName));


            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "selected_o"));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Observer"),
                    new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityAny, "Observer", "selected_o"),
                    new EXECommandAssignment("selected_o", "active", new EXEASTNodeLeaf(EXETypes.BooleanTrue)),
                },
                new EXEASTNodeComposite
                (
                    "empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("selected_o")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXEScopeCondition(
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("selected_o", "status", new EXEASTNodeLeaf("\"Active\""))
                        },
                        new EXEASTNodeComposite
                        (
                            ".",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("selected_o"),
                                new EXEASTNodeLeaf("active")
                            }
                        ),
                        new EXEScopeCondition[]
                        {
                            new EXEScopeCondition(
                                Animation.SuperScope,
                                new EXECommand[]
                                {
                                    new EXECommandAssignment("selected_o", "status", new EXEASTNodeLeaf("\"Inactive\""))
                                },
                                new EXEASTNodeComposite
                                (
                                    "not",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeComposite
                                        (
                                            ".",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("selected_o"),
                                                new EXEASTNodeLeaf("active")
                                            }
                                        )
                                    }
                                )
                            )
                        }
                    )
                },
                new EXEASTNodeComposite
                (
                    "not_empty",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("selected_o")
                    }
                )
            ));

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
                { "selected_o", "Observer"}
            };
            Dictionary<String, String> ExpectedCreatedVarsState = new Dictionary<String, String>()
            {
                { "selected_o.active", EXETypes.BooleanTrue },
                { "selected_o.status", "\"Active\"" }
            };
            int ExpectedValidRefVarCount = 1;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarsState = Animation.SuperScope.GetAllHandleStateAttrsDictRecursive(Animation.ExecutionSpace);
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarsState, ActualCreatedVarsState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
        }
        [TestMethod]
        public void EXEScopeCondition_Bad_01()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("11"))
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
            ));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("y")));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "20"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Bad_02()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("11"))
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
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Bad_03()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("x", new EXEASTNodeLeaf("30"))
                },
                new EXEASTNodeComposite
                (
                    ">",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("30")
                    }
                ),
                new EXEScopeCondition[]
                {
                    new EXEScopeCondition
                    (
                        Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("x", new EXEASTNodeLeaf("25"))
                },
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("y"),
                        new EXEASTNodeLeaf("5")
                    }
                )
                    )
                }
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                { "x", "20" }
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Bad_04()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("x", new EXEASTNodeLeaf("30"))
                },
                null,
                new EXEScopeCondition[]
                {
                    new EXEScopeCondition
                    (
                        Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("x", new EXEASTNodeLeaf("25"))
                },
                new EXEASTNodeComposite
                (
                    "==",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("y"),
                        new EXEASTNodeLeaf("5")
                    }
                )
                    )
                }
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                { "x", "20" }
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeCondition_Bad_05()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXEScopeCondition(
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandAssignment("x", new EXEASTNodeLeaf("30"))
                },
                new EXEASTNodeComposite
                (
                    ">",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("30")
                    }
                ),
                new EXEScopeCondition[]
                {
                    new EXEScopeCondition
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandAssignment("x", new EXEASTNodeLeaf("25"))
                        },
                        null
                    )
                }
            ));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                { "x", "20" }
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
    }
}