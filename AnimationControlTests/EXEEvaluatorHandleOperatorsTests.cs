using Microsoft.VisualStudio.TestTools.UnitTesting;
using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace OALProgramControl.Tests
{
    [TestClass()]
    public class EXEEvaluatorHandleOperatorsTests
    {
        [TestMethod]
        public void IsHandleOperator_Normal_01()
        {
            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();
            bool ActualOutput = EHO.IsHandleOperator("empty");
            Assert.IsTrue(ActualOutput);
        }
        [TestMethod]
        public void IsHandleOperator_Normal_02()
        {
            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();
            bool ActualOutput = EHO.IsHandleOperator("not_empty");
            Assert.IsTrue(ActualOutput);
        }
        [TestMethod]
        public void IsHandleOperator_Normal_03()
        {
            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();
            bool ActualOutput = EHO.IsHandleOperator("cardinality");
            Assert.IsTrue(ActualOutput);
        }
        [TestMethod]
        public void IsHandleOperator_Bad_01()
        {
            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();
            bool ActualOutput = EHO.IsHandleOperator("");
            Assert.IsFalse(ActualOutput);
        }
        [TestMethod]
        public void IsHandleOperator_Bad_02()
        {
            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();
            bool ActualOutput = EHO.IsHandleOperator(null);
            Assert.IsFalse(ActualOutput);
        }
        [TestMethod]
        public void IsHandleOperator_Bad_03()
        {
            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();
            bool ActualOutput = EHO.IsHandleOperator("+");
            Assert.IsFalse(ActualOutput);
        }
        [TestMethod]
        public void IsHandleOperator_Bad_04()
        {
            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();
            bool ActualOutput = EHO.IsHandleOperator("not");
            Assert.IsFalse(ActualOutput);
        }
        [TestMethod]
        public void IsHandleOperator_Bad_05()
        {
            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();
            bool ActualOutput = EHO.IsHandleOperator("assign");
            Assert.IsFalse(ActualOutput);
        }

        [TestMethod]
        public void EvaluateEmpty_Normal_True_01()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("D1", "Dog", -1));

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = EXETypes.BooleanTrue;
            String ActualOutput = EHO.EvaluateEmpty("D1", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EvaluateEmpty_Normal_True_02()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            EXEReferencingSetVariable SetVar = new EXEReferencingSetVariable("dogs", "Dog");
            Scope.AddVariable(SetVar);

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = EXETypes.BooleanTrue;
            String ActualOutput = EHO.EvaluateEmpty("dogs", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EvaluateEmpty_Normal_False_01()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("D1", "Dog", 55));

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = EXETypes.BooleanFalse;
            String ActualOutput = EHO.EvaluateEmpty("D1", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EvaluateEmpty_Normal_False_02()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            EXEReferencingSetVariable SetVar = new EXEReferencingSetVariable("dogs", "Dog");
            SetVar.AddReferencingVariable(new EXEReferencingVariable("", "Dog", 5));
            Scope.AddVariable(SetVar);

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = EXETypes.BooleanFalse;
            String ActualOutput = EHO.EvaluateEmpty("dogs", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EvaluateEmpty_Bad_01()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            EXEReferencingSetVariable SetVar = new EXEReferencingSetVariable("dogs", "Dog");
            SetVar.AddReferencingVariable(new EXEReferencingVariable("", "Dog", 5));
            Scope.AddVariable(SetVar);

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ActualOutput = EHO.EvaluateEmpty("doggos", Scope);

            Assert.IsNull(ActualOutput);
        }
        [TestMethod]
        public void EvaluateEmpty_Bad_02()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ActualOutput = EHO.EvaluateEmpty("doggos", Scope);

            Assert.IsNull(ActualOutput);
        }
        [TestMethod]
        public void EvaluateEmpty_Bad_03()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("D1", "Dog", 55));

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ActualOutput = EHO.EvaluateEmpty("D2", Scope);

            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void EvaluateNotEmpty_Normal_False_01()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("D1", "Dog", -1));

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = EXETypes.BooleanFalse;
            String ActualOutput = EHO.EvaluateNotEmpty("D1", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EvaluateNotEmpty_Normal_False_02()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            EXEReferencingSetVariable SetVar = new EXEReferencingSetVariable("dogs", "Dog");
            Scope.AddVariable(SetVar);

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = EXETypes.BooleanFalse;
            String ActualOutput = EHO.EvaluateNotEmpty("dogs", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EvaluateNotEmpty_Normal_True_01()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("D1", "Dog", 55));

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = EXETypes.BooleanTrue;
            String ActualOutput = EHO.EvaluateNotEmpty("D1", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EvaluateNotEmpty_Normal_True_02()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            EXEReferencingSetVariable SetVar = new EXEReferencingSetVariable("dogs", "Dog");
            SetVar.AddReferencingVariable(new EXEReferencingVariable("", "Dog", 5));
            Scope.AddVariable(SetVar);
            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = EXETypes.BooleanTrue;
            String ActualOutput = EHO.EvaluateNotEmpty("dogs", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void EvaluateNotEmpty_Bad_01()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            EXEReferencingSetVariable SetVar = new EXEReferencingSetVariable("dogs", "Dog");
            SetVar.AddReferencingVariable(new EXEReferencingVariable("", "Dog", 5));
            Scope.AddVariable(SetVar);

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ActualOutput = EHO.EvaluateNotEmpty("doggos", Scope);

            Assert.IsNull(ActualOutput);
        }
        [TestMethod]
        public void EvaluateNotEmpty_Bad_02()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ActualOutput = EHO.EvaluateNotEmpty("doggos", Scope);

            Assert.IsNull(ActualOutput);
        }
        [TestMethod]
        public void EvaluateNotEmpty_Bad_03()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("D1", "Dog", 55));

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ActualOutput = EHO.EvaluateNotEmpty("D2", Scope);

            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void EvaluateCardinality_Normal_01()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("D1", "Dog", -1));

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = "0";
            String ActualOutput = EHO.EvaluateCardinality("D1", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EvaluateCardinality_Normal_02()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            EXEReferencingSetVariable SetVar = new EXEReferencingSetVariable("dogs", "Dog");
            Scope.AddVariable(SetVar);

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = "0";
            String ActualOutput = EHO.EvaluateCardinality("dogs", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EvaluateCardinality_Normal_03()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("D1", "Dog", 55));

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = "1";
            String ActualOutput = EHO.EvaluateCardinality("D1", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EvaluateCardinality_Normal_04()
        {
            OALProgram OALProgram = new OALProgram();
            CDClass DogClass = OALProgram.ExecutionSpace.SpawnClass("Dog");
            DogClass.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            DogClass.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));

            EXEScope Scope = new EXEScope();
            EXEReferencingSetVariable SetVar = new EXEReferencingSetVariable("dogs", "Dog");
            SetVar.AddReferencingVariable(new EXEReferencingVariable("", "Dog", 1));
            SetVar.AddReferencingVariable(new EXEReferencingVariable("", "Dog", 2));
            SetVar.AddReferencingVariable(new EXEReferencingVariable("", "Dog", 3));
            SetVar.AddReferencingVariable(new EXEReferencingVariable("", "Dog", 4));
            Scope.AddVariable(SetVar);

            EXEEvaluatorHandleOperators EHO = new EXEEvaluatorHandleOperators();

            String ExpectedOutput = "4";
            String ActualOutput = EHO.EvaluateCardinality("dogs", Scope);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
    }
}