using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AnimationControl.Tests
{
    [TestClass()]
    public class EXECommandQueryUnrelateTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            //Set up the execution space
            CDClassPool ClassPool = new CDClassPool();
            CDRelationshipPool RelPool = new CDRelationshipPool();

            CDClass Class1 = ClassPool.SpawnClass("Observer");
            CDClass Class2 = ClassPool.SpawnClass("Subject");
            CDRelationship Rel = RelPool.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst = Class2.CreateClassInstance();
            Rel.CreateRelationship(Class1Inst.UniqueID, Class2Inst.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s", "Subject", Class2Inst.UniqueID));

            //prepare the tested object
            EXECommandQueryUnrelate Command = new EXECommandQueryUnrelate("o", "s", Rel.RelationshipName);
            Boolean ActionSuccess = Command.Execute(ClassPool, RelPool, Scope);

            Boolean Success = RelPool.GetRelationshipByClasses("Observer", "Subject").InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst.UniqueID);

            Assert.IsFalse(Success);
            Assert.IsTrue(ActionSuccess);
        }
        [TestMethod]
        public void Execute_Normal_02()
        {
            //Set up the execution space
            CDClassPool ClassPool = new CDClassPool();
            CDRelationshipPool RelPool = new CDRelationshipPool();

            CDClass Class1 = ClassPool.SpawnClass("Observer");
            CDClass Class2 = ClassPool.SpawnClass("Subject");
            CDRelationship Rel = RelPool.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst = Class2.CreateClassInstance();
            Rel.CreateRelationship(Class1Inst.UniqueID, Class2Inst.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s", "Subject", Class2Inst.UniqueID));

            //prepare the tested object
            EXECommandQueryUnrelate Command = new EXECommandQueryUnrelate("o", "s", Rel.RelationshipName);
            Boolean ActionSuccess = Command.Execute(ClassPool, RelPool, Scope);

            Boolean Success = RelPool.GetRelationshipByClasses("Observer", "Subject").InstanceRelationshipExists(Class2Inst.UniqueID, Class1Inst.UniqueID);

            Assert.IsFalse(Success);
            Assert.IsTrue(ActionSuccess);
        }
        [TestMethod]
        public void Execute_Normal_03()
        {
            //Set up the execution space
            CDClassPool ClassPool = new CDClassPool();
            CDRelationshipPool RelPool = new CDRelationshipPool();

            CDClass Class1 = ClassPool.SpawnClass("Observer");
            CDClass Class2 = ClassPool.SpawnClass("Subject");
            CDRelationship Rel = RelPool.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            CDClassInstance Class2Inst2 = Class2.CreateClassInstance();
            CDClassInstance Class2Inst3 = Class2.CreateClassInstance();
            Rel.CreateRelationship(Class1Inst.UniqueID, Class2Inst2.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s1", "Subject", Class2Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s2", "Subject", Class2Inst2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s3", "Subject", Class2Inst3.UniqueID));

            //prepare the tested object
            EXECommandQueryUnrelate Command = new EXECommandQueryUnrelate("o", "s2", Rel.RelationshipName);
            Boolean ActionSuccess = Command.Execute(ClassPool, RelPool, Scope);

            Boolean Success = RelPool.GetRelationshipByName(Rel.RelationshipName).InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst2.UniqueID);

            Assert.IsFalse(Success);
            Assert.IsTrue(ActionSuccess);
        }

        [TestMethod]
        public void Execute_Normal_04()
        {
            //Set up the execution space
            CDClassPool ClassPool = new CDClassPool();
            CDRelationshipPool RelPool = new CDRelationshipPool();

            CDClass Class1 = ClassPool.SpawnClass("Observer");
            CDClass Class2 = ClassPool.SpawnClass("Subject");
            CDRelationship Rel = RelPool.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            CDClassInstance Class2Inst2 = Class2.CreateClassInstance();
            CDClassInstance Class2Inst3 = Class2.CreateClassInstance();
            Rel.CreateRelationship(Class1Inst.UniqueID, Class2Inst2.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s1", "Subject", Class2Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s2", "Subject", Class2Inst2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s3", "Subject", Class2Inst3.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("s4", "Observer", Class2Inst2.UniqueID));

            //prepare the tested object
            EXECommandQueryUnrelate Command = new EXECommandQueryUnrelate("o", "s4", Rel.RelationshipName);
            Boolean ActionSuccess = Command.Execute(ClassPool, RelPool, Scope);

            Boolean Success = RelPool.GetRelationshipByName(Rel.RelationshipName).InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst2.UniqueID);

            Assert.IsFalse(Success);
            Assert.IsTrue(ActionSuccess);
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            //Set up the execution space
            CDClassPool ClassPool = new CDClassPool();
            CDRelationshipPool RelPool = new CDRelationshipPool();

            CDClass Class1 = ClassPool.SpawnClass("Observer");
            CDClass Class2 = ClassPool.SpawnClass("Subject");
            CDRelationship Rel = RelPool.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst = Class2.CreateClassInstance();
            Rel.CreateRelationship(Class1Inst.UniqueID, Class2Inst.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s", "Subject", Class2Inst.UniqueID));

            //prepare the tested object
            EXECommandQueryUnrelate Command = new EXECommandQueryUnrelate("o", "x", Rel.RelationshipName);
            Boolean ActionSuccess = Command.Execute(ClassPool, RelPool, Scope);

            Boolean Success = RelPool.GetRelationshipByClasses("Observer", "Subject").InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst.UniqueID);

            Assert.IsFalse(Success);
            Assert.IsFalse(ActionSuccess);
        }
        [TestMethod]
        public void Execute_Bad_02()
        {
            //Set up the execution space
            CDClassPool ClassPool = new CDClassPool();
            CDRelationshipPool RelPool = new CDRelationshipPool();

            CDClass Class1 = ClassPool.SpawnClass("Observer");
            CDClass Class2 = ClassPool.SpawnClass("Subject");
            CDRelationship Rel = RelPool.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst = Class2.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s", "Subject", Class2Inst.UniqueID));

            //prepare the tested object
            EXECommandQueryUnrelate Command = new EXECommandQueryUnrelate("o", "s", Rel.RelationshipName);
            Boolean Success = Command.Execute(ClassPool, RelPool, Scope);

            Assert.IsFalse(Success);
        }
        [TestMethod]
        public void Execute_Bad_03()
        {
            //Set up the execution space
            CDClassPool ClassPool = new CDClassPool();
            CDRelationshipPool RelPool = new CDRelationshipPool();

            CDClass Class1 = ClassPool.SpawnClass("Observer");
            CDClass Class2 = ClassPool.SpawnClass("Subject");
            CDRelationship Rel = RelPool.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst = Class2.CreateClassInstance();
            Rel.CreateRelationship(Class1Inst.UniqueID, Class2Inst.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s", "Subject", Class2Inst.UniqueID));

            //prepare the tested object
            EXECommandQueryUnrelate Command = new EXECommandQueryUnrelate("o", "s", "R58");
            Boolean ActionSuccess = Command.Execute(ClassPool, RelPool, Scope);

            Boolean Success = RelPool.GetRelationshipByClasses("Observer", "Subject").InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst.UniqueID);

            Assert.IsFalse(Success);
            Assert.IsFalse(ActionSuccess);
        }
    }
}