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
            CDClassPool ClassPool = new CDClassPool();
            CDRelationshipPool RelPool = new CDRelationshipPool();
            
            CDClass Class1 = ClassPool.SpawnClass("Observer");
            CDClass Class2 = ClassPool.SpawnClass("Subject");
            CDRelationship Rel = RelPool.SpawnRelationship("Observer", "Subject");

            CDClassInstance Class1Inst = Class1.CreateClassInstance("");
            CDClassInstance Class2Inst = Class2.CreateClassInstance("");
            Rel.CreateRelationship(Class1Inst.UniqueID, Class2Inst.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("o", "Observer", Class1Inst.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("s", "Subject", Class2Inst.UniqueID));

            //prepare the tested object
            EXECommandQueryRelate Command = new EXECommandQueryRelate("o", "s", Rel.RelationshipName, "Observer", "Subject");
            Command.Execute(ClassPool, RelPool, Scope);

            Boolean Success = RelPool.GetRelationshipByClasses("Observer", "Subject").InstanceRelationshipExists(Class1Inst.UniqueID, Class2Inst.UniqueID);

            Assert.IsTrue(Success);
        }
    }
}