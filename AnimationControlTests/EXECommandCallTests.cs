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

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
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
    }
}