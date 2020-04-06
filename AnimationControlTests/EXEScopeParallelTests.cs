using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.ParallelTests
{
    [TestClass]
    public class EXEScopeParallelTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand
            (
                    new EXEScopeParallel
                    (
                        new EXEScope[]
                        {
                            new EXEScope
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
                                }
                            ),
                            new EXEScope
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
                                                new EXEASTNodeLeaf("2")
                                            }
                                        )
                                    )
                                }
                            ),
                            new EXEScope
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
                                                new EXEASTNodeLeaf("3")
                                            }
                                        )
                                    )
                                }
                            )
                        }
                    )
            );

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "6"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_02()
        {
            Animation Animation = new Animation();

            EXEScope[] Threads = new EXEScope[20];
            for (int i = 0; i < Threads.Length; i++)
            {
                Threads[i] = new EXEScope
                            (
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
                                        new EXEASTNodeComposite
                                        (
                                            "<",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("y"),
                                                new EXEASTNodeLeaf("10")
                                            }
                                        )
                                    )
                                }
                            );
            }

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand
            (
                    new EXEScopeParallel
                    (
                        Threads
                    )
            );

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "200"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_03()
        {
            Animation Animation = new Animation();

            EXEScope[] Threads = new EXEScope[20];
            for (int i = 0; i < Threads.Length; i++)
            {
                Threads[i] = new EXEScope
                            (
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
                                        new EXEASTNodeComposite
                                        (
                                            "<",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("y"),
                                                new EXEASTNodeLeaf("100")
                                            }
                                        )
                                    )
                                }
                            );
            }

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand
            (
                    new EXEScopeParallel
                    (
                        Threads
                    )
            );

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "2000"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_04()
        {
            Animation Animation = new Animation();

            EXEScope[] Threads = new EXEScope[100];
            for (int i = 0; i < Threads.Length; i++)
            {
                Threads[i] = new EXEScope
                            (
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
                                        new EXEASTNodeComposite
                                        (
                                            "<",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("y"),
                                                new EXEASTNodeLeaf("100")
                                            }
                                        )
                                    )
                                }
                            );
            }

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand
            (
                    new EXEScopeParallel
                    (
                        Threads
                    )
            );

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10000"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Normal_05()
        {
            Animation Animation = new Animation();

            EXEScope[] Threads = new EXEScope[1000];
            for (int i = 0; i < Threads.Length; i++)
            {
                Threads[i] = new EXEScope
                            (
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
                                        new EXEASTNodeComposite
                                        (
                                            "<",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("y"),
                                                new EXEASTNodeLeaf("1")
                                            }
                                        )
                                    )
                                }
                            );
            }

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand
            (
                    new EXEScopeParallel
                    (
                        Threads
                    )
            );

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "1000"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            Animation Animation = new Animation();

            EXEScope[] Threads = new EXEScope[20];
            for (int i = 0; i < Threads.Length-1; i++)
            {
                Threads[i] = new EXEScope
                            (
                                Animation.SuperScope,
                                new EXECommand[]
                                {
                                    new EXECommandAssignment("x", new EXEASTNodeLeaf("2"))
                                }
                            );
            }
            Threads[Threads.Length - 1] = new EXEScope
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandQueryDelete("BussinessMan")
                    }
                );

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("2")));
            Animation.SuperScope.AddCommand
            (
                    new EXEScopeParallel
                    (
                        Threads
                    )
            );

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                { "x", "2" }
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
    }
}