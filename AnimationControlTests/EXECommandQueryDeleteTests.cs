using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQueryDeleteTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            CDClassPool CP = new CDClassPool();
            CDRelationshipPool RP = new CDRelationshipPool();
            CDClass Class1 = CP.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(CP, RP, Scope);

            int ExpectedOutput = 0;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 0;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Normal_02()
        {
            CDClassPool CP = new CDClassPool();
            CDRelationshipPool RP = new CDRelationshipPool();
            CDClass Class1 = CP.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();
            CDClassInstance Inst2 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst2.UniqueID));

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(CP, RP, Scope);

            int ExpectedOutput = 1;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 1;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Normal_03()
        {
            CDClassPool CP = new CDClassPool();
            CDRelationshipPool RP = new CDRelationshipPool();
            CDClass Class1 = CP.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst1.UniqueID));

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(CP, RP, Scope);

            int ExpectedOutput = 0;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 0;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Normal_04()
        {
            CDClassPool CP = new CDClassPool();
            CDRelationshipPool RP = new CDRelationshipPool();
            CDClass Class1 = CP.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();
            CDClassInstance Inst2 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst2.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("swamp_creature_3", "Creature", Inst1.UniqueID));
            Scope.SuperScope = SuperScope;

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(CP, RP, Scope);

            int ExpectedOutput = 1;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 1;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Normal_05()
        {
            CDClassPool CP = new CDClassPool();
            CDRelationshipPool RP = new CDRelationshipPool();
            CDClass Class1 = CP.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst1.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("swamp_creature_3", "Creature", Inst1.UniqueID));
            Scope.SuperScope = SuperScope;

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(CP, RP, Scope);

            int ExpectedOutput = 0;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 0;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Normal_06()
        {
            CDClassPool CP = new CDClassPool();
            CDRelationshipPool RP = new CDRelationshipPool();
            CDClass Class1 = CP.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();
            Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst1.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("swamp_creature_3", "Creature", Inst1.UniqueID));
            Scope.SuperScope = SuperScope;

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(CP, RP, Scope);

            int ExpectedOutput = 1;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 0;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            CDClassPool CP = new CDClassPool();
            CDRelationshipPool RP = new CDRelationshipPool();
            CDClass Class1 = CP.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();
            Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst1.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("swamp_creature_3", "Creature", Inst1.UniqueID));
            Scope.SuperScope = SuperScope;

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature_4");
            Query.Execute(CP, RP, Scope);

            int ExpectedOutput = 1;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 3;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
    }
}