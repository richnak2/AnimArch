using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControlTests
{
    [TestClass]
    public class EXECommandAssignmentTests
    {
        [TestMethod]
        public void Execute_Normal_Int_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDRelationshipPool RelationshipSpace = new CDRelationshipPool();
            EXEScope Scope = new EXEScope();
            EXEPrimitiveVariable Var1 = new EXEPrimitiveVariable("x", EXETypes.IntegerTypeName);
            Scope.AddVariable(Var1);

            EXEASTNode AssignedExpression = new EXEASTNodeLeaf("15");
            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(ExecutionSpace, RelationshipSpace, Scope);
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
            CDClassPool ExecutionSpace = new CDClassPool();
            CDRelationshipPool RelationshipSpace = new CDRelationshipPool();
            EXEScope Scope = new EXEScope();
            EXEPrimitiveVariable Var1 = new EXEPrimitiveVariable("x", EXETypes.IntegerTypeName);
            Scope.AddVariable(Var1);
            EXEPrimitiveVariable Var2 = new EXEPrimitiveVariable("y", EXETypes.IntegerTypeName);
            Var2.Value = "17";
            Scope.AddVariable(Var2);

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("+");
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("y"));
            AssignedExpression.AddOperand( new EXEASTNodeLeaf("13"));

            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(ExecutionSpace, RelationshipSpace, Scope);
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
            CDClassPool ExecutionSpace = new CDClassPool();
            CDRelationshipPool RelationshipSpace = new CDRelationshipPool();
            EXEScope Scope = new EXEScope();
            EXEPrimitiveVariable Var1 = new EXEPrimitiveVariable("x", EXETypes.IntegerTypeName);
            Var1.Value = "45";
            Scope.AddVariable(Var1);
            EXEPrimitiveVariable Var2 = new EXEPrimitiveVariable("y", EXETypes.IntegerTypeName);
            Var2.Value = "10";
            Scope.AddVariable(Var2);

            EXEScope SuperScope = new EXEScope();
            EXEPrimitiveVariable Var3 = new EXEPrimitiveVariable("z", EXETypes.IntegerTypeName);
            Var3.Value = "30";
            Scope.AddVariable(Var3);

            Scope.SuperScope = SuperScope;

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("+");

            EXEASTNodeComposite MultExpr = new EXEASTNodeComposite("*");
            MultExpr.AddOperand(new EXEASTNodeLeaf("y"));
            MultExpr.AddOperand(new EXEASTNodeLeaf("z"));

            AssignedExpression.AddOperand(MultExpr);
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("50"));

            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(ExecutionSpace, RelationshipSpace, Scope);
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
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass Class1 = ExecutionSpace.SpawnClass("Human");
            Class1.AddAttribute(new CDAttribute("age", EXETypes.IntegerTypeName));
            CDClassInstance Class1Inst1 = Class1.CreateClassInstance();
            Class1Inst1.SetAttribute("age", "50");

            CDRelationshipPool RelationshipSpace = new CDRelationshipPool();
            EXEScope Scope = new EXEScope();
            EXEPrimitiveVariable Var1 = new EXEPrimitiveVariable("x", EXETypes.IntegerTypeName);
            Var1.Value = "45";
            Scope.AddVariable(Var1);

            EXEScope SuperScope = new EXEScope();
            EXEReferencingVariable Var3 = new EXEReferencingVariable("human", "Human", Class1Inst1.UniqueID);
            Scope.AddVariable(Var3);

            Scope.SuperScope = SuperScope;

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("+");

            EXEASTNodeComposite MultExpr = new EXEASTNodeComposite(".");
            MultExpr.AddOperand(new EXEASTNodeLeaf("human"));
            MultExpr.AddOperand(new EXEASTNodeLeaf("age"));

            AssignedExpression.AddOperand(MultExpr);
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("50"));

            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(ExecutionSpace, RelationshipSpace, Scope);
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
            CDClassPool ExecutionSpace = new CDClassPool();

            CDRelationshipPool RelationshipSpace = new CDRelationshipPool();
            EXEScope Scope = new EXEScope();

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("-");
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("25"));
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("50"));
            
            EXECommandAssignment AssignCommand1 = new EXECommandAssignment("x", AssignedExpression);
            EXECommandAssignment AssignCommand2 = new EXECommandAssignment("y", new EXEASTNodeLeaf("0"));
            EXECommandAssignment AssignCommand3 = new EXECommandAssignment("z", new EXEASTNodeLeaf("1"));

            Boolean Success1 = AssignCommand1.Execute(ExecutionSpace, RelationshipSpace, Scope);
            Boolean Success2 = AssignCommand2.Execute(ExecutionSpace, RelationshipSpace, Scope);
            Boolean Success3 = AssignCommand3.Execute(ExecutionSpace, RelationshipSpace, Scope);
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
            CDClassPool ExecutionSpace = new CDClassPool();
            CDRelationshipPool RelationshipSpace = new CDRelationshipPool();
            EXEScope Scope = new EXEScope();
            EXEPrimitiveVariable Var1 = new EXEPrimitiveVariable("x", EXETypes.IntegerTypeName);
            Var1.Value = "45";
            Scope.AddVariable(Var1);
            EXEPrimitiveVariable Var2 = new EXEPrimitiveVariable("y", EXETypes.IntegerTypeName);
            Var2.Value = EXETypes.BooleanFalse;
            Scope.AddVariable(Var2);

            EXEScope SuperScope = new EXEScope();
            EXEPrimitiveVariable Var3 = new EXEPrimitiveVariable("z", EXETypes.IntegerTypeName);
            Var3.Value = EXETypes.BooleanTrue;
            Scope.AddVariable(Var3);

            Scope.SuperScope = SuperScope;

            EXEASTNodeComposite AssignedExpression = new EXEASTNodeComposite("and");

            EXEASTNodeComposite MultExpr = new EXEASTNodeComposite("not");
            MultExpr.AddOperand(new EXEASTNodeLeaf("y"));

            AssignedExpression.AddOperand(MultExpr);
            AssignedExpression.AddOperand(new EXEASTNodeLeaf("z"));
            AssignedExpression.AddOperand(new EXEASTNodeLeaf(EXETypes.BooleanTrue));

            EXECommandAssignment AssignCommand = new EXECommandAssignment("x", AssignedExpression);

            Boolean Success = AssignCommand.Execute(ExecutionSpace, RelationshipSpace, Scope);
            Dictionary<String, String> ExprectedOutput = new Dictionary<string, string> {
                {"x", EXETypes.BooleanTrue},
                {"y", EXETypes.BooleanFalse},
                {"z", EXETypes.BooleanTrue}
            };
            Dictionary<String, String> ActualOutput = Scope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExprectedOutput, ActualOutput);
        }
    }
}