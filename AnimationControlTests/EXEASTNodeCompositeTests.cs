using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEASTNodeCompositeTests
    {
        [TestMethod]
        public void Evaluate_Normal_Bool_01()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("Over", EXETypes.BooleanFalse));
            Scope.AddVariable(new EXEPrimitiveVariable("i", "0"));
            Scope.AddVariable(new EXEPrimitiveVariable("count", "15"));

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("and");

            EXEASTNodeComposite AST2 = new EXEASTNodeComposite("<");
            AST2.AddOperand(new EXEASTNodeLeaf("i"));
            AST2.AddOperand(new EXEASTNodeLeaf("count"));

            AST1.AddOperand(AST2);

            EXEASTNodeComposite AST3 = new EXEASTNodeComposite("not");
            AST3.AddOperand(new EXEASTNodeLeaf("Over"));

            AST1.AddOperand(AST3);

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = EXETypes.BooleanTrue;

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Bool_02()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("Over", EXETypes.BooleanTrue));
            Scope.AddVariable(new EXEPrimitiveVariable("i", "0"));
            Scope.AddVariable(new EXEPrimitiveVariable("count", "15"));

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("and");

            EXEASTNodeComposite AST2 = new EXEASTNodeComposite("<");
            AST2.AddOperand(new EXEASTNodeLeaf("i"));
            AST2.AddOperand(new EXEASTNodeLeaf("count"));

            AST1.AddOperand(AST2);

            EXEASTNodeComposite AST3 = new EXEASTNodeComposite("not");
            AST3.AddOperand(new EXEASTNodeLeaf("Over"));

            AST1.AddOperand(AST3);

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = EXETypes.BooleanFalse;

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Bool_03()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("Over", EXETypes.BooleanFalse));
            Scope.AddVariable(new EXEPrimitiveVariable("i", "15"));
            Scope.AddVariable(new EXEPrimitiveVariable("count", "15"));

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("and");

            EXEASTNodeComposite AST2 = new EXEASTNodeComposite("<");
            AST2.AddOperand(new EXEASTNodeLeaf("i"));
            AST2.AddOperand(new EXEASTNodeLeaf("count"));

            AST1.AddOperand(AST2);

            EXEASTNodeComposite AST3 = new EXEASTNodeComposite("not");
            AST3.AddOperand(new EXEASTNodeLeaf("Over"));

            AST1.AddOperand(AST3);

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = EXETypes.BooleanFalse;

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_String_01()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("+");
            AST1.AddOperand(new EXEASTNodeLeaf("\"ahoj \""));
            AST1.AddOperand(new EXEASTNodeLeaf("\"Marisa\""));

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "\"ahoj Marisa\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_String_02()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("+");
            AST1.AddOperand(new EXEASTNodeLeaf("\"ahoj \""));
            AST1.AddOperand(new EXEASTNodeLeaf("\"Marisa \""));
            AST1.AddOperand(new EXEASTNodeLeaf("\"ako sa mas?\""));

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "\"ahoj Marisa ako sa mas?\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_String_03()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("<");
            AST1.AddOperand(new EXEASTNodeLeaf("\"ahoj\""));
            AST1.AddOperand(new EXEASTNodeLeaf("\"Marisa\""));

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = EXETypes.BooleanTrue;

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_String_04()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("<");

            EXEASTNodeComposite AST2 = new EXEASTNodeComposite("+");
            AST2.AddOperand(new EXEASTNodeLeaf("\"ahoj\""));
            AST2.AddOperand(new EXEASTNodeLeaf("\"2\""));

            EXEASTNodeComposite AST3 = new EXEASTNodeComposite("+");
            AST3.AddOperand(new EXEASTNodeLeaf("\"ahoj\""));
            AST3.AddOperand(new EXEASTNodeLeaf("\"1\""));

            AST1.AddOperand(AST2);
            AST1.AddOperand(AST3);

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = EXETypes.BooleanFalse;

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Int_01()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("+");
            AST1.AddOperand(new EXEASTNodeLeaf("5"));
            AST1.AddOperand(new EXEASTNodeLeaf("7"));

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "12";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Int_02()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("*");
            AST1.AddOperand(new EXEASTNodeLeaf("5"));

            EXEASTNodeComposite AST2 = new EXEASTNodeComposite("+");
            AST2.AddOperand(new EXEASTNodeLeaf("2"));
            AST2.AddOperand(new EXEASTNodeLeaf("4"));

            AST1.AddOperand(AST2);

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "30";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Int_03()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("x", "10"));
            Scope.AddVariable(new EXEPrimitiveVariable("y", "4"));

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("*");
            AST1.AddOperand(new EXEASTNodeLeaf("x"));

            EXEASTNodeComposite AST2 = new EXEASTNodeComposite("+");
            AST2.AddOperand(new EXEASTNodeLeaf("y"));
            AST2.AddOperand(new EXEASTNodeLeaf("4"));

            AST1.AddOperand(AST2);

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "80";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Int_04()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("x", "10"));
            Scope.AddVariable(new EXEPrimitiveVariable("y", "4"));
            Scope.AddVariable(new EXEPrimitiveVariable("z", "3"));

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("-");
            AST1.AddOperand(new EXEASTNodeLeaf("x"));

            EXEASTNodeComposite AST2 = new EXEASTNodeComposite("/");
            AST2.AddOperand(new EXEASTNodeLeaf("40"));

            EXEASTNodeComposite AST3 = new EXEASTNodeComposite("+");
            AST3.AddOperand(new EXEASTNodeLeaf("z"));
            AST3.AddOperand(new EXEASTNodeLeaf("7"));

            AST2.AddOperand(AST3);
            AST1.AddOperand(AST2);

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "6";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Real_01()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("x", "2.0"));
            Scope.AddVariable(new EXEPrimitiveVariable("y", "2.2"));

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("*");
            AST1.AddOperand(new EXEASTNodeLeaf("x"));

            EXEASTNodeComposite AST2 = new EXEASTNodeComposite("+");
            AST2.AddOperand(new EXEASTNodeLeaf("y"));
            AST2.AddOperand(new EXEASTNodeLeaf("4.4"));

            AST1.AddOperand(AST2);

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "13.2";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Real_02()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite("*");
            AST1.AddOperand(new EXEASTNodeLeaf("1.2"));
            AST1.AddOperand(new EXEASTNodeLeaf("1.2"));

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "1.44";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_BuildReal_01()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite(".");
            AST1.AddOperand(new EXEASTNodeLeaf("1"));
            AST1.AddOperand(new EXEASTNodeLeaf("1"));

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "1.1";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_BuildReal_02()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite(".");
            AST1.AddOperand(new EXEASTNodeLeaf("16"));
            AST1.AddOperand(new EXEASTNodeLeaf("0"));

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "16.0";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_BuildReal_03()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite(".");
            AST1.AddOperand(new EXEASTNodeLeaf("0"));
            AST1.AddOperand(new EXEASTNodeLeaf("2345"));

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "0.2345";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Access_01()
        {
            Animation Animation = new Animation();
            CDClass C1 = Animation.ExecutionSpace.SpawnClass("Dog");
            C1.AddAttribute(new CDAttribute("name", EXETypes.StringTypeName));
            CDClassInstance C1I1 = C1.CreateClassInstance();
            C1I1.SetAttribute("name", "Dunco");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("dog1", "Dog", C1I1.UniqueID));

            EXEASTNodeComposite AST1 = new EXEASTNodeComposite(".");
            AST1.AddOperand(new EXEASTNodeLeaf("dog1"));
            AST1.AddOperand(new EXEASTNodeLeaf("name"));

            String ActualOutput = AST1.Evaluate(Scope, Animation.ExecutionSpace);
            String ExpectedOutput = "Dunco";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
    }
}