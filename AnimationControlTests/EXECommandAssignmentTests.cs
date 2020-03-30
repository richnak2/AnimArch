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

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("15")));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"x","15"}
            };
            Dictionary<String, String> ExpectedReferencingVarState = new Dictionary<string, string>
            {
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<String, String> ActualReferencingVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace);

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedReferencingVarState, ActualReferencingVarState);
        }
        [TestMethod]
        public void Execute_Normal_Int_02()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("15")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("6")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("z", new EXEASTNodeLeaf("-56")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("a", new EXEASTNodeLeaf("-13")));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"x","15"},
                {"y","6"},
                {"z","-56"},
                {"a","-13"}
            };
            Dictionary<String, String> ExpectedReferencingVarState = new Dictionary<string, string>
            {
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<String, String> ActualReferencingVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace);

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedReferencingVarState, ActualReferencingVarState);
        }
        [TestMethod]
        public void Execute_Normal_Int_03()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("15")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("z", new EXEASTNodeLeaf("-56")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("a", new EXEASTNodeLeaf("-13")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "swamp_monster1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "health",new EXEASTNodeLeaf("1024")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "max_health", new EXEASTNodeLeaf("1024")));
            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"x","15"},
                {"y","0"},
                {"z","-56"},
                {"a","-13"}
            };
            Dictionary<String, String> ExpectedReferencingVarState = new Dictionary<string, string>
            {
                { "swamp_monster1.health", "1024"},
                { "swamp_monster1.max_health", "1024"},
                { "swamp_monster1.mana", EXETypes.UnitializedName},
                { "swamp_monster1.max_mana", EXETypes.UnitializedName}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<String, String> ActualReferencingVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace);

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedReferencingVarState, ActualReferencingVarState);
        }
        [TestMethod]
        public void Execute_Normal_Int_04()
        {
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("15")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("150")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("-15")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("-150")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("223344")));

            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"x","223344"}
            };
            Dictionary<String, String> ExpectedReferencingVarState = new Dictionary<string, string>
            {
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<String, String> ActualReferencingVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace);

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedReferencingVarState, ActualReferencingVarState);
        }
        [TestMethod]
        public void Execute_Normal_Int_05()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("321")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("123")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("z", new EXEASTNodeLeaf("750000")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "swamp_monster1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "health", new EXEASTNodeLeaf("1024")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "max_health", new EXEASTNodeLeaf("1024")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "mana", new EXEASTNodeLeaf("2")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "max_mana", new EXEASTNodeLeaf("2")));
            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"x","321"},
                {"y","123"},
                {"z","750000"}
            };
            Dictionary<String, String> ExpectedReferencingVarState = new Dictionary<string, string>
            {
                { "swamp_monster1.health", "1024"},
                { "swamp_monster1.max_health", "1024"},
                { "swamp_monster1.mana", "2"},
                { "swamp_monster1.max_mana", "2"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<String, String> ActualReferencingVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace);

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedReferencingVarState, ActualReferencingVarState);
        }
        [TestMethod]
        public void Execute_Normal_Int_06()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("321")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("123")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("z", new EXEASTNodeLeaf("750000")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "swamp_monster1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "health", new EXEASTNodeLeaf("1024")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "max_health", new EXEASTNodeLeaf("1024")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "mana", new EXEASTNodeLeaf("2")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "max_mana", new EXEASTNodeLeaf("2")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "health", new EXEASTNodeLeaf("1024")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "max_health", new EXEASTNodeLeaf("4048")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "mana", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "max_mana", new EXEASTNodeLeaf("500")));
            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"x","321"},
                {"y","123"},
                {"z","750000"}
            };
            Dictionary<String, String> ExpectedReferencingVarState = new Dictionary<string, string>
            {
                { "swamp_monster1.health", "1024"},
                { "swamp_monster1.max_health", "1024"},
                { "swamp_monster1.mana", "2"},
                { "swamp_monster1.max_mana", "2"},
                { "swamp_monster2.health", "1024"},
                { "swamp_monster2.max_health", "4048"},
                { "swamp_monster2.mana", "20"},
                { "swamp_monster2.max_mana", "500"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<String, String> ActualReferencingVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace);

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedReferencingVarState, ActualReferencingVarState);
        }
        [TestMethod]
        public void Execute_Normal_Int_07()
        {
            Animation Animation = new Animation();
            CDClass Class1 = Animation.ExecutionSpace.SpawnClass("SwampMonster");
            Class1.AddAttribute(new CDAttribute("health", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("max_health", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("mana", EXETypes.IntegerTypeName));
            Class1.AddAttribute(new CDAttribute("max_mana", EXETypes.IntegerTypeName));

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("321")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("y", new EXEASTNodeLeaf("123")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("z", new EXEASTNodeLeaf("750000")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "swamp_monster1"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "health", new EXEASTNodeLeaf("1024")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "max_health", new EXEASTNodeLeaf("1024")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "mana", new EXEASTNodeLeaf("2")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster1", "max_mana", new EXEASTNodeLeaf("2")));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("SwampMonster", "swamp_monster2"));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster2", "health", new EXEASTNodeLeaf("1024")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster2", "max_health", new EXEASTNodeLeaf("4048")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster2", "mana", new EXEASTNodeLeaf("20")));
            Animation.SuperScope.AddCommand(new EXECommandAssignment("swamp_monster2", "max_mana", new EXEASTNodeLeaf("500")));
            Boolean Success = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string>
            {
                {"x","321"},
                {"y","123"},
                {"z","750000"}
            };
            Dictionary<String, String> ExpectedReferencingVarState = new Dictionary<string, string>
            {
                { "swamp_monster1.health", "1024"},
                { "swamp_monster1.max_health", "4048"},
                { "swamp_monster1.mana", "20"},
                { "swamp_monster1.max_mana", "500"}
            };

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<String, String> ActualReferencingVarState = Animation.SuperScope.GetRefStateAttrsDictRecursive(Animation.ExecutionSpace);

            Assert.IsTrue(Success);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
            CollectionAssert.AreEquivalent(ExpectedReferencingVarState, ActualReferencingVarState);
        }
    }
}