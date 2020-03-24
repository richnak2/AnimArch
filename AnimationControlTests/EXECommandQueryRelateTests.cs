using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQueryRelateTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship Rel = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst = Class2.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s", "Subject", Class2Inst.UniqueID));

            //prepare the tested object
            EXECommandQueryRelate Command = new EXECommandQueryRelate("o", "s", Rel.RelationshipName);
            Boolean ActionSuccess = Command.Execute(Animation, Scope);

            Boolean Success = Animation.RelationshipSpace.GetRelationshipByClasses("Observer", "Subject").InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst.UniqueID);

            Assert.IsTrue(Success);
            Assert.IsTrue(ActionSuccess);
        }

        [TestMethod]
        public void Execute_Normal_02()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship Rel = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            CDClassInstance Class2Inst2 = Class2.CreateClassInstance();
            CDClassInstance Class2Inst3 = Class2.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s1", "Subject", Class2Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s2", "Subject", Class2Inst2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s3", "Subject", Class2Inst3.UniqueID));

            //prepare the tested object
            EXECommandQueryRelate Command = new EXECommandQueryRelate("o", "s2", Rel.RelationshipName);
            Boolean ActionSuccess = Command.Execute(Animation, Scope);

            Boolean Success = Animation.RelationshipSpace.GetRelationshipByName(Rel.RelationshipName).InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst2.UniqueID);

            Assert.IsTrue(Success);
            Assert.IsTrue(ActionSuccess);
        }

        [TestMethod]
        public void Execute_Normal_03()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship Rel = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            CDClassInstance Class2Inst2 = Class2.CreateClassInstance();
            CDClassInstance Class2Inst3 = Class2.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s1", "Subject", Class2Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s2", "Subject", Class2Inst2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s3", "Subject", Class2Inst3.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("s4", "Observer", Class2Inst2.UniqueID));

            //prepare the tested object
            EXECommandQueryRelate Command = new EXECommandQueryRelate("o", "s4", Rel.RelationshipName);
            Boolean ActionSuccess = Command.Execute(Animation, Scope);

            Boolean Success = Animation.RelationshipSpace.GetRelationshipByName(Rel.RelationshipName).InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst2.UniqueID);

            Assert.IsTrue(Success);
            Assert.IsTrue(ActionSuccess);
        }

        [TestMethod]
        public void Execute_Bad_01()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship Rel = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst = Class2.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s", "Subject", Class2Inst.UniqueID));

            //prepare the tested object
            EXECommandQueryRelate Command = new EXECommandQueryRelate("o", "x", Rel.RelationshipName);
            Boolean ActionSuccess = Command.Execute(Animation, Scope);

            Boolean Success = Animation.RelationshipSpace.GetRelationshipByClasses("Observer", "Subject").InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst.UniqueID);

            Assert.IsFalse(Success);
            Assert.IsFalse(ActionSuccess);
        }
        [TestMethod]
        public void Execute_Bad_02()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDRelationship Rel = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
           
            //prepare the tested object
            EXECommandQueryRelate Command = new EXECommandQueryRelate("o", "s", Rel.RelationshipName);
            Boolean ActionSuccess = Command.Execute(Animation, Scope);

            Boolean Success = Animation.RelationshipSpace.GetRelationshipByClasses("Observer", "Subject").InstanceRelationshipExists(Class1Inst.UniqueID, Class1Inst.UniqueID);

            Assert.IsFalse(Success);
            Assert.IsFalse(ActionSuccess);
        }
        [TestMethod]
        public void Execute_Bad_03()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship Rel = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance();
            CDClassInstance Class2Inst = Class2.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("s", "Subject", Class2Inst.UniqueID));

            //prepare the tested object
            EXECommandQueryRelate Command = new EXECommandQueryRelate("o", "s", "R58");
            Boolean ActionSuccess = Command.Execute(Animation, Scope);

            Boolean Success = Animation.RelationshipSpace.GetRelationshipByClasses("Observer", "Subject").InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst.UniqueID);

            Assert.IsFalse(Success);
            Assert.IsFalse(ActionSuccess);
        }
    }
}