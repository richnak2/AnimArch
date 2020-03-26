using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandAssignmentTests
    {
        [TestMethod]
        public void Execute_Normal_Int_01()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("x", EXETypes.UnitializedName));

            EXEASTNode AssignedExpression = new EXEASTNodeLeaf("15");
            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(Animation, Scope);
            Dictionary<String, String> ExprectedOutput = new Dictionary<string, string> {
                {"x","15"}
            };
            Dictionary<String, String> ActualOutput = Scope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExprectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Execute_Normal_Int_02()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("x", EXETypes.UnitializedName));
            Scope.AddVariable(new EXEPrimitiveVariable("y", "17"));

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("+");
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("y"));
            AssignedExpression.AddOperand( new EXEASTNodeLeaf("13"));

            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(Animation, Scope);
            Dictionary<String, String> ExprectedOutput = new Dictionary<string, string> {
                {"x","30"},
                {"y","17"}
            };
            Dictionary<String, String> ActualOutput = Scope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExprectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Execute_Normal_Int_03()
        {

            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("x", "15"));
            Scope.AddVariable(new EXEPrimitiveVariable("y", "10"));

            EXEScope SuperScope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("z", "30"));

            Scope.SuperScope = SuperScope;

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("+");

            EXEASTNodeComposite MultExpr = new EXEASTNodeComposite("*");
            MultExpr.AddOperand(new EXEASTNodeLeaf("y"));
            MultExpr.AddOperand(new EXEASTNodeLeaf("z"));

            AssignedExpression.AddOperand(MultExpr);
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("50"));

            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(Animation, Scope);
            Dictionary<String, String> ExprectedOutput = new Dictionary<string, string> {
                {"x","350"},
                {"y","10"},
                {"z","30"}
            };
            Dictionary<String, String> ActualOutput = Scope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExprectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Execute_Normal_Int_04()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("Human");
            Class1.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));
            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            Class1Inst1.SetAttribute("age", "50");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("x", "45"));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("human", "Human", Class1Inst1.UniqueID));

            Scope.SuperScope = SuperScope;

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("+");

            EXEASTNodeComposite MultExpr = new EXEASTNodeComposite(".");
            MultExpr.AddOperand(new EXEASTNodeLeaf("human"));
            MultExpr.AddOperand(new EXEASTNodeLeaf("age"));

            AssignedExpression.AddOperand(MultExpr);
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("50"));

            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(Animation, Scope);
            Dictionary<String, String> ExprectedOutput = new Dictionary<string, string> {
                {"x","100"}
            };
            Dictionary<String, String> ActualOutput = Scope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExprectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Execute_Normal_Int_05()
        {
            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("-");
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("25"));
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("50"));
            
            EXECommandAssignment AssignCommand1 = new EXECommandAssignment("x", AssignedExpression);
            EXECommandAssignment AssignCommand2 = new EXECommandAssignment("y", new EXEASTNodeLeaf("0"));
            EXECommandAssignment AssignCommand3 = new EXECommandAssignment("z", new EXEASTNodeLeaf("1"));

            Boolean Success1 = AssignCommand1.Execute(Animation, Scope);
            Boolean Success2 = AssignCommand2.Execute(Animation, Scope);
            Boolean Success3 = AssignCommand3.Execute(Animation, Scope);
            Dictionary<String, String> ExprectedOutput = new Dictionary<string, string> {
                {"x","-25"},
                {"y","0"},
                {"z","1"}
            };
            Dictionary<String, String> ActualOutput = Scope.GetStateDictRecursive();

            Assert.IsTrue(Success1);
            Assert.IsTrue(Success2);
            Assert.IsTrue(Success3);
            CollectionAssert.AreEquivalent(ExprectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Execute_Normal_Bool_01()
        {

            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("x", EXETypes.UnitializedName));
            Scope.AddVariable(new EXEPrimitiveVariable("y", EXETypes.BooleanFalse));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEPrimitiveVariable("z", EXETypes.BooleanTrue));

            Scope.SuperScope = SuperScope;

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("and");

            EXEASTNodeComposite MultExpr = new EXEASTNodeComposite("not");
            MultExpr.AddOperand(new EXEASTNodeLeaf("y"));

            AssignedExpression.AddOperand(MultExpr);
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("z"));
            AssignedExpression.AddOperand(new EXEASTNodeLeaf(EXETypes.BooleanTrue));

            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(Animation, Scope);
            Dictionary<String, String> ExprectedOutput = new Dictionary<string, string> {
                {"x", EXETypes.BooleanTrue},
                {"y", EXETypes.BooleanFalse},
                {"z", EXETypes.BooleanTrue}
            };
            Dictionary<String, String> ActualOutput = Scope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExprectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Execute_Bad_Bool_01()
        {

            Animation Animation = new Animation();
            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEPrimitiveVariable("x", "45"));
            Scope.AddVariable(new EXEPrimitiveVariable("y", EXETypes.BooleanFalse));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEPrimitiveVariable("z", EXETypes.BooleanTrue));

            Scope.SuperScope = SuperScope;

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("and");

            EXEASTNodeComposite MultExpr = new EXEASTNodeComposite("not");
            MultExpr.AddOperand(new EXEASTNodeLeaf("y"));

            AssignedExpression.AddOperand(MultExpr);
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("z"));
            AssignedExpression.AddOperand(new EXEASTNodeLeaf(EXETypes.BooleanTrue));

            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(Animation, Scope);
            Dictionary<String, String> ExprectedOutput = new Dictionary<string, string> {
                {"x", "45"},
                {"y", EXETypes.BooleanFalse},
                {"z", EXETypes.BooleanTrue}
            };
            Dictionary<String, String> ActualOutput = Scope.GetStateDictRecursive();

            Assert.IsFalse(Success);
            CollectionAssert.AreEquivalent(ExprectedOutput, ActualOutput);
        }
    }
}