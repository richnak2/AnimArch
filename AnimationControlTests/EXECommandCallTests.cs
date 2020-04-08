using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandCallTests
    {
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Normal_01()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            StringBuffer StringBuffer = new StringBuffer();

            Animation.SuperScope.AddCommand(new EXECommandCallTestDecorator(
                new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                StringBuffer
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            String ExpectedCallHistory = "call from Observer::init() to Subject::register() across R1;\n";

            String ActualCallHistory = StringBuffer.GenerateString();

            Assert.IsTrue(ExecutionSuccess);
            Assert.AreEqual(ExpectedCallHistory, ActualCallHistory);
        }
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Normal_02()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            StringBuffer StringBuffer = new StringBuffer();

            Animation.SuperScope.AddCommand(new EXECommandCallTestDecorator(
                new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                StringBuffer
            ));
            Animation.SuperScope.AddCommand(new EXECommandCallTestDecorator(
                new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                StringBuffer
            ));

            Boolean ExecutionSuccess = Animation.Execute();

            String ExpectedCallHistory = 
                "call from Observer::init() to Subject::register() across R1;\n" +
                "call from Observer::init() to Subject::register() across R1;\n";

            String ActualCallHistory = StringBuffer.GenerateString();

            Assert.IsTrue(ExecutionSuccess);
            Assert.AreEqual(ExpectedCallHistory, ActualCallHistory);
        }
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Normal_03()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            StringBuffer StringBuffer = new StringBuffer();

            Animation.SuperScope.AddCommand
            (
                new EXECommandAssignment("x", new EXEASTNodeLeaf("0"))
            );
            Animation.SuperScope.AddCommand
            (
                new EXEScopeLoopWhile
                (
                    Animation.SuperScope,
                    new EXECommand[]
                    {
                        new EXECommandCallTestDecorator
                        (
                            new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                            StringBuffer
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
                            new EXEASTNodeLeaf("10")
                        }
                    )
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            String ExpectedCallHistory = "";
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory += "call from Observer::init() to Subject::register() across R1;\n";
            }

            String ActualCallHistory = StringBuffer.GenerateString();

            Assert.IsTrue(ExecutionSuccess);
            Assert.AreEqual(ExpectedCallHistory, ActualCallHistory);
        }
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Normal_04()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            StringBuffer StringBuffer = new StringBuffer();
            EXEScope[] Threads = new EXEScope[10];
            for (int i = 0; i < 10; i++)
            {
                Threads[i] = 
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
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
                            }
                        );
            }

            Animation.SuperScope.AddCommand
            (
                new EXECommandAssignment("x", new EXEASTNodeLeaf("0"))
            );
            Animation.SuperScope.AddCommand
            (
                new EXEScopeParallel
                (
                   Threads
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            List<String> ExpectedCallHistory = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory.Add("call from Observer::init() to Subject::register() across R1;\n");
            }
            Dictionary<String, String> ExpectedPrimitiveVars = new Dictionary<String, String>
            {
                { "x", "10"}
            };

            List<String> ActualCallHistory = StringBuffer.CloneStringList();
            Dictionary<String, String> ActualPrimitiveVars = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEqual(ExpectedCallHistory, ActualCallHistory);
            CollectionAssert.AreEqual(ExpectedPrimitiveVars, ActualPrimitiveVars);
        }
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Normal_05()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            StringBuffer StringBuffer = new StringBuffer();
            EXEScope[] Threads = new EXEScope[10];
            for (int i = 0; i < 10; i++)
            {
                Threads[i] =
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
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
                                ),
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R2", "Subject", "register"),
                                    StringBuffer
                                )
                            }
                        );
            }

            Animation.SuperScope.AddCommand
            (
                new EXECommandAssignment("x", new EXEASTNodeLeaf("0"))
            );
            Animation.SuperScope.AddCommand
            (
                new EXEScopeParallel
                (
                   Threads
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            List<String> ExpectedCallHistory = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory.Add("call from Observer::init() to Subject::register() across R1;\n");
            }
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory.Add("call from Observer::init() to Subject::register() across R2;\n");
            }
            Dictionary<String, String> ExpectedPrimitiveVars = new Dictionary<String, String>
            {
                { "x", "10"}
            };

            List<String> ActualCallHistory = StringBuffer.CloneStringList();
            Dictionary<String, String> ActualPrimitiveVars = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEqual(ExpectedCallHistory, ActualCallHistory);
            CollectionAssert.AreEqual(ExpectedPrimitiveVars, ActualPrimitiveVars);
        }
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Normal_06()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            ClassObserver.AddMethod(new CDMethod("update", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            ClassObserver.AddMethod(new CDMethod("update", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            StringBuffer StringBuffer = new StringBuffer();
            EXEScope[] Threads = new EXEScope[10];
            for (int i = 0; i < 10; i++)
            {
                Threads[i] =
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
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
                                ),
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                    StringBuffer
                                ),
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R2", "Subject", "register"),
                                    StringBuffer
                                )
                            }
                        );
            }

            Animation.SuperScope.AddCommand
            (
                new EXECommandAssignment("x", new EXEASTNodeLeaf("0"))
            );
            Animation.SuperScope.AddCommand
            (
                new EXEScopeParallel
                (
                   Threads
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            List<String> ExpectedCallHistory = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory.Add("call from Observer::init() to Subject::register() across R1;\n");
            }
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
            }
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory.Add("call from Observer::init() to Subject::register() across R2;\n");
            }
            Dictionary<String, String> ExpectedPrimitiveVars = new Dictionary<String, String>
            {
                { "x", "10"}
            };

            List<String> ActualCallHistory = StringBuffer.CloneStringList();
            Dictionary<String, String> ActualPrimitiveVars = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEqual(ExpectedCallHistory, ActualCallHistory);
            CollectionAssert.AreEqual(ExpectedPrimitiveVars, ActualPrimitiveVars);
        }
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Normal_07()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            ClassObserver.AddMethod(new CDMethod("update", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            ClassObserver.AddMethod(new CDMethod("update", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");
            CDClass ClassClient = Animation.ExecutionSpace.SpawnClass("Client");
            ClassClient.AddMethod(new CDMethod("setValue", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Client", "Subject");

            StringBuffer StringBuffer = new StringBuffer();
            EXEScope[] Threads = new EXEScope[10];
            for (int i = 0; i < 10; i++)
            {
                Threads[i] =
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
                                ),
                                new EXECommandAssignment("x", new EXEASTNodeLeaf("0")),
                                new EXEScopeLoopWhile
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandAssignment("x", new EXEASTNodeComposite(
                                            "+",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("x"),
                                                new EXEASTNodeLeaf("1"),
                                            }
                                        ))
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
                            }
                        );
            }

            Animation.SuperScope.AddCommand
            (
                new EXEScopeParallel
                (
                   Threads
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            List<String> ExpectedCallHistory = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory.Add("call from Observer::init() to Subject::register() across R1;\n");
            }
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");
                }
                for (int i = 0; i < 10; i++)
                {
                    ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
                }
            }
            Dictionary<String, String> ExpectedPrimitiveVars = new Dictionary<String, String>
            {
            };

            List<String> ActualCallHistory = StringBuffer.CloneStringList();
            Dictionary<String, String> ActualPrimitiveVars = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEqual(ExpectedCallHistory, ActualCallHistory);
            CollectionAssert.AreEqual(ExpectedPrimitiveVars, ActualPrimitiveVars);
        }
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Normal_08()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            ClassObserver.AddMethod(new CDMethod("update", "void"));
            ClassObserver.AddMethod(new CDMethod("destroy", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            ClassSubject.AddMethod(new CDMethod("update", "void"));
            ClassSubject.AddMethod(new CDMethod("unregister", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");
            CDClass ClassClient = Animation.ExecutionSpace.SpawnClass("Client");
            ClassClient.AddMethod(new CDMethod("setValue", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Client", "Subject");

            StringBuffer StringBuffer = new StringBuffer();
            EXEScope[] Threads = new EXEScope[10];
            for (int i = 0; i < 8; i++)
            {
                Threads[i] =
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
                                ),
                                new EXECommandAssignment("x", new EXEASTNodeLeaf("0")),
                                new EXEScopeLoopWhile
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandAssignment("x", new EXEASTNodeComposite(
                                            "+",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("x"),
                                                new EXEASTNodeLeaf("1"),
                                            }
                                        ))
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
                            }
                        );
            }
            for (int i = 8; i < 10; i++)
            {
                Threads[i] =
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
                                ),
                                new EXECommandAssignment("x", new EXEASTNodeLeaf("0")),
                                new EXEScopeLoopWhile
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandAssignment("x", new EXEASTNodeComposite(
                                            "+",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("x"),
                                                new EXEASTNodeLeaf("1"),
                                            }
                                        ))
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
                                new EXEScopeParallel
                                (
                                    new EXEScope[]
                                    {
                                        new EXEScope
                                        (
                                            Animation.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                                    StringBuffer
                                                ),
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                                    StringBuffer
                                                )
                                            }
                                        ),
                                        new EXEScope
                                        (
                                            Animation.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                                    StringBuffer
                                                ),
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                                    StringBuffer
                                                )
                                            }
                                        ),
                                        new EXEScope
                                        (
                                            Animation.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                                    StringBuffer
                                                ),
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                                    StringBuffer
                                                )
                                            }
                                        )
                                    }
                                )
                            }
                        );
            }

            Animation.SuperScope.AddCommand
            (
                new EXEScopeParallel
                (
                   Threads
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            List<String> ExpectedCallHistory = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory.Add("call from Observer::init() to Subject::register() across R1;\n");
            }
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");
                }
                for (int i = 0; i < 10; i++)
                {
                    ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
                }
            }
            
            
                //How many times they do shit, e.g. in while loop
            for (int j = 0; j < 1; j++)
            {
                //How many branches does each branched thread have
                // *
                //How many threads branch further
                for (int k = 0; k < 3 * 2; k++)
                {
                    ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");
                }
                for (int k = 0; k < 3 * 2; k++)
                {
                    ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
                }
            }
            Dictionary<String, String> ExpectedPrimitiveVars = new Dictionary<String, String>
            {
            };

            List<String> ActualCallHistory = StringBuffer.CloneStringList();
            Dictionary<String, String> ActualPrimitiveVars = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEqual(ExpectedCallHistory, ActualCallHistory);
            CollectionAssert.AreEqual(ExpectedPrimitiveVars, ActualPrimitiveVars);
        }
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Normal_09()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            ClassObserver.AddMethod(new CDMethod("update", "void"));
            ClassObserver.AddMethod(new CDMethod("destroy", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            ClassSubject.AddMethod(new CDMethod("update", "void"));
            ClassSubject.AddMethod(new CDMethod("unregister", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");
            CDClass ClassClient = Animation.ExecutionSpace.SpawnClass("Client");
            ClassClient.AddMethod(new CDMethod("setValue", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Client", "Subject");

            StringBuffer StringBuffer = new StringBuffer();
            EXEScope[] Threads = new EXEScope[10];
            for (int i = 0; i < 8; i++)
            {
                Threads[i] =
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
                                ),
                                new EXECommandAssignment("x", new EXEASTNodeLeaf("0")),
                                new EXEScopeLoopWhile
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandAssignment("x", new EXEASTNodeComposite(
                                            "+",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("x"),
                                                new EXEASTNodeLeaf("1"),
                                            }
                                        ))
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
                                new EXECommandAssignment("x", new EXEASTNodeLeaf("0")),
                                new EXEScopeLoopWhile
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandAssignment("x", new EXEASTNodeComposite(
                                            "+",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("x"),
                                                new EXEASTNodeLeaf("1"),
                                            }
                                        ))
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
                            }
                        );
            }
            for (int i = 8; i < 10; i++)
            {
                Threads[i] =
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
                                ),
                                new EXECommandAssignment("x", new EXEASTNodeLeaf("0")),
                                new EXEScopeLoopWhile
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandAssignment("x", new EXEASTNodeComposite(
                                            "+",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("x"),
                                                new EXEASTNodeLeaf("1"),
                                            }
                                        ))
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
                                new EXEScopeParallel
                                (
                                    new EXEScope[]
                                    {
                                        new EXEScope
                                        (
                                            Animation.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                                    StringBuffer
                                                ),
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                                    StringBuffer
                                                )
                                            }
                                        ),
                                        new EXEScope
                                        (
                                            Animation.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                                    StringBuffer
                                                ),
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                                    StringBuffer
                                                )
                                            }
                                        ),
                                        new EXEScope
                                        (
                                            Animation.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                                    StringBuffer
                                                ),
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                                    StringBuffer
                                                )
                                            }
                                        )
                                    }
                                )
                            }
                        );
            }

            Animation.SuperScope.AddCommand
            (
                new EXEScopeParallel
                (
                   Threads
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            List<String> ExpectedCallHistory = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory.Add("call from Observer::init() to Subject::register() across R1;\n");
            }
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");
                }
                for (int i = 0; i < 10; i++)
                {
                    ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
                }
            }


            //How many times they do shit, e.g. in while loop
            for (int j = 0; j < 1; j++)
            {
                //How many branches does each branched thread have
                // *
                //How many threads branch further
                for (int k = 0; k < 3 * 2 + 8; k++)
                {
                    ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");
                }
                for (int k = 0; k < 3 * 2 + 8; k++)
                {
                    ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
                }
            }
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 8 ; k++)
                {
                    ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");
                }
                for (int k = 0; k < 8; k++)
                {
                    ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
                }
            }
            Dictionary<String, String> ExpectedPrimitiveVars = new Dictionary<String, String>
            {
            };

            List<String> ActualCallHistory = StringBuffer.CloneStringList();
            Dictionary<String, String> ActualPrimitiveVars = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEqual(ExpectedCallHistory, ActualCallHistory);
            CollectionAssert.AreEqual(ExpectedPrimitiveVars, ActualPrimitiveVars);
        }
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Normal_10()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            ClassObserver.AddMethod(new CDMethod("update", "void"));
            ClassObserver.AddMethod(new CDMethod("destroy", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            ClassSubject.AddMethod(new CDMethod("update", "void"));
            ClassSubject.AddMethod(new CDMethod("unregister", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");
            CDClass ClassClient = Animation.ExecutionSpace.SpawnClass("Client");
            ClassClient.AddMethod(new CDMethod("setValue", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Client", "Subject");

            StringBuffer StringBuffer = new StringBuffer();
            EXEScope[] Threads = new EXEScope[10];
            for (int i = 0; i < 8; i++)
            {
                Threads[i] =
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
                                ),
                                new EXECommandAssignment("x", new EXEASTNodeLeaf("0")),
                                new EXEScopeLoopWhile
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandAssignment("x", new EXEASTNodeComposite(
                                            "+",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("x"),
                                                new EXEASTNodeLeaf("1"),
                                            }
                                        ))
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
                                new EXECommandAssignment("x", new EXEASTNodeLeaf("0")),
                                new EXEScopeLoopWhile
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandAssignment("x", new EXEASTNodeComposite(
                                            "+",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("x"),
                                                new EXEASTNodeLeaf("1"),
                                            }
                                        ))
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
                            }
                        );
            }
            for (int i = 8; i < 10; i++)
            {
                Threads[i] =
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
                                ),
                                new EXECommandAssignment("x", new EXEASTNodeLeaf("0")),
                                new EXEScopeLoopWhile
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandAssignment("x", new EXEASTNodeComposite(
                                            "+",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("x"),
                                                new EXEASTNodeLeaf("1"),
                                            }
                                        ))
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
                                new EXEScopeParallel
                                (
                                    new EXEScope[]
                                    {
                                        new EXEScope
                                        (
                                            Animation.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                                    StringBuffer
                                                ),
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                                    StringBuffer
                                                )
                                            }
                                        ),
                                        new EXEScope
                                        (
                                            Animation.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                                    StringBuffer
                                                ),
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                                    StringBuffer
                                                )
                                            }
                                        ),
                                        new EXEScope
                                        (
                                            Animation.SuperScope,
                                            new EXECommand[]
                                            {
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                                    StringBuffer
                                                ),
                                                new EXECommandCallTestDecorator
                                                (
                                                    new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                                    StringBuffer
                                                )
                                            }
                                        )
                                    }
                                )
                            }
                        );
            }

            Animation.SuperScope.AddCommand
            (
                new EXEScopeParallel
                (
                   Threads
                )
            );
            Animation.SuperScope.AddCommand(
                new EXECommandCallTestDecorator
                (
                    new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                    StringBuffer
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            List<String> ExpectedCallHistory = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                ExpectedCallHistory.Add("call from Observer::init() to Subject::register() across R1;\n");
            }
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");
                }
                for (int i = 0; i < 10; i++)
                {
                    ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
                }
            }


            //How many times they do shit, e.g. in while loop
            for (int j = 0; j < 1; j++)
            {
                //How many branches does each branched thread have
                // *
                //How many threads branch further
                for (int k = 0; k < 3 * 2 + 8; k++)
                {
                    ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");
                }
                for (int k = 0; k < 3 * 2 + 8; k++)
                {
                    ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
                }
            }
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");
                }
                for (int k = 0; k < 8; k++)
                {
                    ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
                }
            }
            ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");

            Dictionary<String, String> ExpectedPrimitiveVars = new Dictionary<String, String>
            {
            };

            List<String> ActualCallHistory = StringBuffer.CloneStringList();
            Dictionary<String, String> ActualPrimitiveVars = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEqual(ExpectedCallHistory, ActualCallHistory);
            CollectionAssert.AreEqual(ExpectedPrimitiveVars, ActualPrimitiveVars);
        }
        [Timeout(15000)]
        [TestMethod]
        public void Execute_Stress_01()
        {
            Animation Animation = new Animation();
            CDClass ClassObserver = Animation.ExecutionSpace.SpawnClass("Observer");
            ClassObserver.AddMethod(new CDMethod("init", "void"));
            ClassObserver.AddMethod(new CDMethod("update", "void"));
            CDClass ClassSubject = Animation.ExecutionSpace.SpawnClass("Subject");
            ClassSubject.AddMethod(new CDMethod("register", "bool"));
            ClassObserver.AddMethod(new CDMethod("update", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");
            CDClass ClassClient = Animation.ExecutionSpace.SpawnClass("Client");
            ClassClient.AddMethod(new CDMethod("setValue", "void"));
            Animation.RelationshipSpace.SpawnRelationship("Client", "Subject");

            StringBuffer StringBuffer = new StringBuffer();
            EXEScope[] Threads = new EXEScope[20];
            for (int i = 0; i < 20; i++)
            {
                Threads[i] =
                        new EXEScope
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandCallTestDecorator
                                (
                                    new EXECommandCall("Observer", "init", "R1", "Subject", "register"),
                                    StringBuffer
                                ),
                                new EXECommandAssignment("x", new EXEASTNodeLeaf("0")),
                                new EXEScopeLoopWhile
                                (
                                    Animation.SuperScope,
                                    new EXECommand[]
                                    {
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Client", "setValue", "R2", "Subject", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandCallTestDecorator
                                        (
                                            new EXECommandCall("Subject", "update", "R1", "Observer", "update"),
                                            StringBuffer
                                        ),
                                        new EXECommandAssignment("x", new EXEASTNodeComposite(
                                            "+",
                                            new EXEASTNode[]
                                            {
                                                new EXEASTNodeLeaf("x"),
                                                new EXEASTNodeLeaf("1"),
                                            }
                                        ))
                                    },
                                    new EXEASTNodeComposite
                                    (
                                        "<",
                                        new EXEASTNode[]
                                        {
                                            new EXEASTNodeLeaf("x"),
                                            new EXEASTNodeLeaf("25")
                                        }
                                    )
                                )
                            }
                        );
            }

            Animation.SuperScope.AddCommand
            (
                new EXEScopeParallel
                (
                   Threads
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            List<String> ExpectedCallHistory = new List<String>();
            for (int i = 0; i < 20; i++)
            {
                ExpectedCallHistory.Add("call from Observer::init() to Subject::register() across R1;\n");
            }
            for (int j = 0; j < 25; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    ExpectedCallHistory.Add("call from Client::setValue() to Subject::update() across R2;\n");
                }
                for (int i = 0; i < 20; i++)
                {
                    ExpectedCallHistory.Add("call from Subject::update() to Observer::update() across R1;\n");
                }
            }
            Dictionary<String, String> ExpectedPrimitiveVars = new Dictionary<String, String>
            {
            };

            List<String> ActualCallHistory = StringBuffer.CloneStringList();
            Dictionary<String, String> ActualPrimitiveVars = Animation.SuperScope.GetStateDictRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEqual(ExpectedCallHistory, ActualCallHistory);
            CollectionAssert.AreEqual(ExpectedPrimitiveVars, ActualPrimitiveVars);
        }
    }
}