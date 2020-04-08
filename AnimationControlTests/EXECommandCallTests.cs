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
            CollectionAssert.AreEquivalent(ExpectedCallHistory, ActualCallHistory);
            CollectionAssert.AreEqual(ExpectedPrimitiveVars, ActualPrimitiveVars);
        }/*
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
            EXECommandCall[] Calls
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
            CollectionAssert.AreEquivalent(ExpectedCallHistory, ActualCallHistory);
            CollectionAssert.AreEqual(ExpectedPrimitiveVars, ActualPrimitiveVars);
        }*/
    }
}