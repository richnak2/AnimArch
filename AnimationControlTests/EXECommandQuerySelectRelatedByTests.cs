using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQuerySelectRelatedByTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship RelClass12 = Animation.RelationshipSpace.SpawnRelationship(Class1.Name, Class2.Name);

            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            RelClass12.CreateRelationship(Class1Inst1.UniqueID, Class2Inst1.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("observer", "Observer", Class1Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("subject", "Subject", Class2Inst1.UniqueID));

            EXERelationshipSelection RelSelect = new EXERelationshipSelection("subject");
            RelSelect.AddRelationshipLink(new EXERelationshipLink(RelClass12.RelationshipName, Class1.Name));
            EXECommandQuerySelectRelatedBy Query = new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityAny, "observer_of_subject", null, RelSelect);
            Boolean Success = Query.Execute(Animation, Scope);

            long ExpectedResult = Class1Inst1.UniqueID;
            long ActualResult = Scope.FindReferencingVariableByName("observer_of_subject").ReferencedInstanceId;

            Assert.IsTrue(Success);
            Assert.AreEqual(ExpectedResult, ActualResult);
        }
        [TestMethod]
        public void Execute_Normal_02()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship RelClass12 = Animation.RelationshipSpace.SpawnRelationship(Class1.Name, Class2.Name);

            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            CDClassInstance Class1Inst2 = Class1.CreateClassInstance();
            CDClassInstance Class1Inst3 = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            RelClass12.CreateRelationship(Class1Inst1.UniqueID, Class2Inst1.UniqueID);
            RelClass12.CreateRelationship(Class1Inst2.UniqueID, Class2Inst1.UniqueID);
            RelClass12.CreateRelationship(Class1Inst3.UniqueID, Class2Inst1.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("observer1", "Observer", Class1Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("observer2", "Observer", Class1Inst2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("observer3", "Observer", Class1Inst3.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("subject", "Subject", Class2Inst1.UniqueID));

            EXERelationshipSelection RelSelect = new EXERelationshipSelection("subject");
            RelSelect.AddRelationshipLink(new EXERelationshipLink(RelClass12.RelationshipName, Class1.Name));
            EXECommandQuerySelectRelatedBy Query = new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityMany, "observers", null, RelSelect);
            Boolean Success = Query.Execute(Animation, Scope);

            List<long> ExpectedResult = new List<long> (new long[] { Class1Inst1.UniqueID, Class1Inst2.UniqueID, Class1Inst3.UniqueID });
            List<long> ActualResult = Scope.FindSetReferencingVariableByName("observers").GetReferencedIds();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedResult, ActualResult);
        }
        [TestMethod]
        public void Execute_Normal_03()
        {
            
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            Class1.AddAttribute(new CDAttribute("Value", EXETypes.IntegerTypeName));
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship RelClass12 = Animation.RelationshipSpace.SpawnRelationship(Class1.Name, Class2.Name);

            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            CDClassInstance Class1Inst2 = Class1.CreateClassInstance();
            CDClassInstance Class1Inst3 = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            Class1Inst1.SetAttribute("Value", "10");
            Class1Inst2.SetAttribute("Value", "20");
            Class1Inst3.SetAttribute("Value", "30");
            RelClass12.CreateRelationship(Class1Inst1.UniqueID, Class2Inst1.UniqueID);
            RelClass12.CreateRelationship(Class1Inst2.UniqueID, Class2Inst1.UniqueID);
            RelClass12.CreateRelationship(Class1Inst3.UniqueID, Class2Inst1.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("observer1", "Observer", Class1Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("observer2", "Observer", Class1Inst2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("observer3", "Observer", Class1Inst3.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("subject", "Subject", Class2Inst1.UniqueID));

            EXERelationshipSelection RelSelect = new EXERelationshipSelection("subject");
            RelSelect.AddRelationshipLink(new EXERelationshipLink(RelClass12.RelationshipName, Class1.Name));
            EXEASTNodeComposite WhereCondition = new EXEASTNodeComposite("<");
            EXEASTNodeComposite AccessAST = new EXEASTNodeComposite(".");
            AccessAST.AddOperand(new EXEASTNodeLeaf("selected"));
            AccessAST.AddOperand(new EXEASTNodeLeaf("Value"));
            WhereCondition.AddOperand(AccessAST);
            WhereCondition.AddOperand(new EXEASTNodeLeaf("15"));

            EXECommandQuerySelectRelatedBy Query = new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityAny, "observer", WhereCondition, RelSelect);
            Boolean Success = Query.Execute(Animation, Scope);

            long ExpectedResult = Class1Inst1.UniqueID;
            long ActualResult = Scope.FindReferencingVariableByName("observer").ReferencedInstanceId;

            Assert.IsTrue(Success);
            Assert.AreEqual(ExpectedResult, ActualResult);
        }
        [TestMethod]
        public void Execute_Normal_04()
        {
            
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            Class1.AddAttribute(new CDAttribute("Value", EXETypes.IntegerTypeName));
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship RelClass12 = Animation.RelationshipSpace.SpawnRelationship(Class1.Name, Class2.Name);

            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            CDClassInstance Class1Inst2 = Class1.CreateClassInstance();
            CDClassInstance Class1Inst3 = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            Class1Inst1.SetAttribute("Value", "10");
            Class1Inst2.SetAttribute("Value", "20");
            Class1Inst3.SetAttribute("Value", "30");
            RelClass12.CreateRelationship(Class1Inst1.UniqueID, Class2Inst1.UniqueID);
            RelClass12.CreateRelationship(Class1Inst2.UniqueID, Class2Inst1.UniqueID);
            RelClass12.CreateRelationship(Class1Inst3.UniqueID, Class2Inst1.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("observer1", "Observer", Class1Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("observer2", "Observer", Class1Inst2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("observer3", "Observer", Class1Inst3.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("subject", "Subject", Class2Inst1.UniqueID));

            EXERelationshipSelection RelSelect = new EXERelationshipSelection("subject");
            RelSelect.AddRelationshipLink(new EXERelationshipLink(RelClass12.RelationshipName, Class1.Name));
            EXEASTNodeComposite WhereCondition = new EXEASTNodeComposite(">");
            EXEASTNodeComposite AccessAST = new EXEASTNodeComposite(".");
            AccessAST.AddOperand(new EXEASTNodeLeaf("selected"));
            AccessAST.AddOperand(new EXEASTNodeLeaf("Value"));
            WhereCondition.AddOperand(AccessAST);
            WhereCondition.AddOperand(new EXEASTNodeLeaf("15"));

            EXECommandQuerySelectRelatedBy Query = new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityMany, "observer", WhereCondition, RelSelect);
            Boolean Success = Query.Execute(Animation, Scope);

            List<long> ExpectedResult = new List<long>(new long[] { Class1Inst2.UniqueID, Class1Inst3.UniqueID });
            List<long> ActualResult = Scope.FindSetReferencingVariableByName("observers").GetReferencedIds();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedResult, ActualResult);
        }
        [TestMethod]
        public void Execute_Normal_05()
        {
            
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            Class1.AddAttribute(new CDAttribute("Value", EXETypes.IntegerTypeName));
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship RelClass12 = Animation.RelationshipSpace.SpawnRelationship(Class1.Name, Class2.Name);

            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            CDClassInstance Class1Inst2 = Class1.CreateClassInstance();
            CDClassInstance Class1Inst3 = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            Class1Inst1.SetAttribute("Value", "10");
            Class1Inst2.SetAttribute("Value", "20");
            Class1Inst3.SetAttribute("Value", "30");
            RelClass12.CreateRelationship(Class1Inst1.UniqueID, Class2Inst1.UniqueID);
            RelClass12.CreateRelationship(Class1Inst2.UniqueID, Class2Inst1.UniqueID);
            RelClass12.CreateRelationship(Class1Inst3.UniqueID, Class2Inst1.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("observer1", "Observer", Class1Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("observer2", "Observer", Class1Inst2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("observer3", "Observer", Class1Inst3.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("subject", "Subject", Class2Inst1.UniqueID));

            EXERelationshipSelection RelSelect = new EXERelationshipSelection("subject");
            RelSelect.AddRelationshipLink(new EXERelationshipLink(RelClass12.RelationshipName, Class1.Name));
            EXEASTNodeComposite WhereCondition = new EXEASTNodeComposite(">");
            EXEASTNodeComposite AccessAST = new EXEASTNodeComposite(".");
            AccessAST.AddOperand(new EXEASTNodeLeaf("selected"));
            AccessAST.AddOperand(new EXEASTNodeLeaf("Value"));
            WhereCondition.AddOperand(AccessAST);
            WhereCondition.AddOperand(new EXEASTNodeLeaf("45"));

            EXECommandQuerySelectRelatedBy Query = new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityMany, "observer", WhereCondition, RelSelect);
            Boolean Success = Query.Execute(Animation, Scope);

            List<long> ExpectedResult = new List<long>();
            List<long> ActualResult = Scope.FindSetReferencingVariableByName("observers").GetReferencedIds();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedResult, ActualResult);
        }
        [TestMethod]
        public void Execute_Normal_06()
        {
            
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            Class1.AddAttribute(new CDAttribute("Value", EXETypes.IntegerTypeName));
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship RelClass12 = Animation.RelationshipSpace.SpawnRelationship(Class1.Name, Class2.Name);

            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            CDClassInstance Class1Inst2 = Class1.CreateClassInstance();
            CDClassInstance Class1Inst3 = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            Class1Inst1.SetAttribute("Value", "10");
            Class1Inst2.SetAttribute("Value", "20");
            Class1Inst3.SetAttribute("Value", "30");
            RelClass12.CreateRelationship(Class1Inst1.UniqueID, Class2Inst1.UniqueID);
            RelClass12.CreateRelationship(Class1Inst2.UniqueID, Class2Inst1.UniqueID);
            RelClass12.CreateRelationship(Class1Inst3.UniqueID, Class2Inst1.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("observer1", "Observer", Class1Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("observer2", "Observer", Class1Inst2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("observer3", "Observer", Class1Inst3.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("subject", "Subject", Class2Inst1.UniqueID));

            EXERelationshipSelection RelSelect = new EXERelationshipSelection("subject");
            RelSelect.AddRelationshipLink(new EXERelationshipLink(RelClass12.RelationshipName, Class1.Name));
            EXEASTNodeComposite WhereCondition = new EXEASTNodeComposite(">");
            EXEASTNodeComposite AccessAST = new EXEASTNodeComposite(".");
            AccessAST.AddOperand(new EXEASTNodeLeaf("selected"));
            AccessAST.AddOperand(new EXEASTNodeLeaf("Value"));
            WhereCondition.AddOperand(AccessAST);
            WhereCondition.AddOperand(new EXEASTNodeLeaf("45"));

            EXECommandQuerySelectRelatedBy Query = new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityAny, "observer", WhereCondition, RelSelect);
            Boolean Success = Query.Execute(Animation, Scope);

            long ExpectedResult = -1;
            long ActualResult = Scope.FindReferencingVariableByName("observers").ReferencedInstanceId;

            Assert.IsTrue(Success);
            Assert.AreEqual(ExpectedResult, ActualResult);
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship RelClass12 = Animation.RelationshipSpace.SpawnRelationship(Class1.Name, Class2.Name);

            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            RelClass12.CreateRelationship(Class1Inst1.UniqueID, Class2Inst1.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("observer", "Observer", Class1Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("subject", "Subject", Class2Inst1.UniqueID));

            EXERelationshipSelection RelSelect = new EXERelationshipSelection("subject2");
            RelSelect.AddRelationshipLink(new EXERelationshipLink(RelClass12.RelationshipName, Class1.Name));
            EXECommandQuerySelectRelatedBy Query = new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityAny, "observer_of_subject", null, RelSelect);
            Boolean Success = Query.Execute(Animation, Scope);

            long ActualResult = Scope.FindReferencingVariableByName("observer_of_subject").ReferencedInstanceId;

            Assert.IsFalse(Success);
            Assert.IsNull(ActualResult);
        }
        [TestMethod]
        public void Execute_Bad_02()
        {
            
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship RelClass12 = Animation.RelationshipSpace.SpawnRelationship(Class1.Name, Class2.Name);

            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            RelClass12.CreateRelationship(Class1Inst1.UniqueID, Class2Inst1.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("observer", "Observer", Class1Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("subject", "Subject", Class2Inst1.UniqueID));

            EXERelationshipSelection RelSelect = new EXERelationshipSelection("subject");
            RelSelect.AddRelationshipLink(new EXERelationshipLink(RelClass12.RelationshipName, Class2.Name));
            EXECommandQuerySelectRelatedBy Query = new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityAny, "observer_of_subject", null, RelSelect);
            Boolean Success = Query.Execute(Animation, Scope);

            long ActualResult = Scope.FindReferencingVariableByName("observer_of_subject").ReferencedInstanceId;

            Assert.IsFalse(Success);
            Assert.IsNull(ActualResult);
        }
        [TestMethod]
        public void Execute_Bad_03()
        {
            
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Observer");
            CDClass Class2 = Animation.ExecutionSpace.SpawnClass("Subject");
            CDRelationship RelClass12 = Animation.RelationshipSpace.SpawnRelationship(Class1.Name, Class2.Name);

            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            CDClassInstance Class2Inst1 = Class2.CreateClassInstance();
            RelClass12.CreateRelationship(Class1Inst1.UniqueID, Class2Inst1.UniqueID);

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("observer", "Observer", Class1Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("subject", "Subject", Class2Inst1.UniqueID));

            EXERelationshipSelection RelSelect = new EXERelationshipSelection("subject");
            RelSelect.AddRelationshipLink(new EXERelationshipLink("R4", Class1.Name));
            EXECommandQuerySelectRelatedBy Query = new EXECommandQuerySelectRelatedBy(EXECommandQuerySelect.CardinalityAny, "observer_of_subject", null, RelSelect);
            Boolean Success = Query.Execute(Animation, Scope);

            long ActualResult = Scope.FindReferencingVariableByName("observer_of_subject").ReferencedInstanceId;

            Assert.IsFalse(Success);
            Assert.IsNull(ActualResult);
        }
    }
}