using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXERelationshipSelectionTests
    {
        [TestMethod]
        public void EvaluateTest()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship RelClass12 = Animation.RelationshipSpace.SpawnRelationship(Class1.Name, Class2.Name);

            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            RelClass12.CreateRelationship(Class1Inst1.UniqueID, Class2Inst1.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("subject", "Subject", Class2Inst1.UniqueID));

            EXERelationshipSelection RelSel = new EXERelationshipSelection("subject");
            RelSel.AddRelationshipLink(new EXERelationshipLink(RelClass12.RelationshipName, "Observer"));

            List<long> ActualIds = RelSel.Evaluate(Animation.RelationshipSpace, Scope);
            List<long> ExpectedIds = new List<long>(new long[] { Class1Inst1.UniqueID });

            CollectionAssert.AreEquivalent(ExpectedIds, ActualIds);
        }
    }
}