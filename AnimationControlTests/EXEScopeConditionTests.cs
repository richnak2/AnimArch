using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControlTests
{
    [TestClass]
    public class EXEScopeConditionTests
    {
        [TestMethod]
        public void EXEScopeCondition_Normal_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDRelationshipPool RelationshipSpace = new CDRelationshipPool();
            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEPrimitiveVariable("x", "0"));
            SuperScope.AddVariable(new EXEPrimitiveVariable("y", "0"));

            EXEScopeCondition ConditionScope = new EXEScopeCondition();
            ConditionScope.SuperScope = SuperScope;

            EXECommandAssignment AssignC1 = new EXECommandAssignment("y", new EXEASTNodeLeaf("50"));
            ConditionScope.AddCommand(AssignC1);

            EXEASTNodeLeaf Condition = new EXEASTNodeLeaf(EXETypes.BooleanTrue);
            ConditionScope.Condition = Condition;

            Boolean Success = ConditionScope.Execute(ExecutionSpace, RelationshipSpace, null);
            Dictionary<String, String> ExprectedOutput = new Dictionary<string, string> {
                {"x", "0"},
                {"y", "50"},
            };
            Dictionary<String, String> ActualOutput = ConditionScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExprectedOutput, ActualOutput);
        }
        [TestMethod]
        public void EXEScopeCondition_Normal_02()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDRelationshipPool RelationshipSpace = new CDRelationshipPool();
            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEPrimitiveVariable("x", "0"));
            SuperScope.AddVariable(new EXEPrimitiveVariable("y", "0"));

            EXEScopeCondition ConditionScope = new EXEScopeCondition();
            ConditionScope.SuperScope = SuperScope;

            EXECommandAssignment AssignC1 = new EXECommandAssignment("y", new EXEASTNodeLeaf("50"));
            ConditionScope.AddCommand(AssignC1);

            EXEASTNodeLeaf Condition = new EXEASTNodeLeaf(EXETypes.BooleanFalse);
            ConditionScope.Condition = Condition;

            Boolean Success = ConditionScope.Execute(ExecutionSpace, RelationshipSpace, null);
            Dictionary<String, String> ExprectedOutput = new Dictionary<string, string> {
                {"x", "0"},
                {"y", "0"},
            };
            Dictionary<String, String> ActualOutput = ConditionScope.GetStateDictRecursive();

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExprectedOutput, ActualOutput);
        }
    }
}