using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQueryDeleteTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(Animation, Scope);

            int ExpectedOutput = 0;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 0;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Normal_02()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();
            CDClassInstance Inst2 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst2.UniqueID));

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(Animation, Scope);

            int ExpectedOutput = 1;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 1;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Normal_03()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst1.UniqueID));

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(Animation, Scope);

            int ExpectedOutput = 0;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 0;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Normal_04()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();
            CDClassInstance Inst2 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst2.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("swamp_creature_3", "Creature", Inst1.UniqueID));
            Scope.SuperScope = SuperScope;

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(Animation, Scope);

            int ExpectedOutput = 1;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 1;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Normal_05()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst1.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("swamp_creature_3", "Creature", Inst1.UniqueID));
            Scope.SuperScope = SuperScope;

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(Animation, Scope);

            int ExpectedOutput = 0;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 0;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Normal_06()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();
            Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst1.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("swamp_creature_3", "Creature", Inst1.UniqueID));
            Scope.SuperScope = SuperScope;

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature");
            Query.Execute(Animation, Scope);

            int ExpectedOutput = 1;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 0;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Creature");
            CDClassInstance Inst1 = Class1.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature", "Creature", Inst1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("swamp_creature_2", "Creature", Inst1.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("swamp_creature_3", "Creature", Inst1.UniqueID));
            Scope.SuperScope = SuperScope;

            EXECommandQueryDelete Query = new EXECommandQueryDelete("swamp_creature_4");
            Query.Execute(Animation, Scope);

            int ExpectedOutput = 1;
            int ActualOutput = Class1.InstanceCount();

            int ExpectedOutput2 = 3;
            int ActualOutput2 = Scope.ValidVariableReferencingCountRecursive();

            Assert.AreEqual((ExpectedOutput, ExpectedOutput2), (ActualOutput, ActualOutput2));
        }
    }
}