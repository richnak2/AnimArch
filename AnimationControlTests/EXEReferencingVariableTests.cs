using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEReferencingVariableTests
    {
        [TestMethod]
        public void RetrieveReferencedClassInstance_Normal_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass UserClass = ExecutionSpace.SpawnClass("User");
            UserClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Age", EXETypes.IntegerTypeName));

            CDClassInstance Inst1 = UserClass.CreateClassInstance();
            Inst1.SetAttribute("Nick", "Alex");
            CDClassInstance Inst2 = UserClass.CreateClassInstance();
            Inst2.SetAttribute("Nick", "Miso");
            CDClassInstance Inst3 = UserClass.CreateClassInstance();
            Inst3.SetAttribute("Nick", "Mato");
            CDClassInstance Inst4 = UserClass.CreateClassInstance();
            Inst4.SetAttribute("Nick", "Samo");

            EXEScope Scope = new EXEScope();

            EXEReferencingVariable ReferencingVariable = new EXEReferencingVariable("user1", "User", Inst2.UniqueID);
            CDClassInstance ReferencedInstance = ReferencingVariable.RetrieveReferencedClassInstance(ExecutionSpace);

            Dictionary<string, string> ExpectedOutput = Inst2.State;
            Dictionary<string, string> ActualOutput = ReferencedInstance.State;

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void RetrieveReferencedClassInstance_Bad_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass UserClass = ExecutionSpace.SpawnClass("User");
            UserClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Age", EXETypes.IntegerTypeName));

            CDClassInstance Inst1 = UserClass.CreateClassInstance();
            Inst1.SetAttribute("Nick", "Alex");
            CDClassInstance Inst2 = UserClass.CreateClassInstance();
            Inst2.SetAttribute("Nick", "Miso");
            CDClassInstance Inst3 = UserClass.CreateClassInstance();
            Inst3.SetAttribute("Nick", "Mato");
            CDClassInstance Inst4 = UserClass.CreateClassInstance();
            Inst4.SetAttribute("Nick", "Samo");

            EXEScope Scope = new EXEScope();

            EXEReferencingVariable ReferencingVariable = new EXEReferencingVariable("user1", "User", Inst4.UniqueID + 1);
            CDClassInstance ReferencedInstance = ReferencingVariable.RetrieveReferencedClassInstance(ExecutionSpace);

            Assert.IsNull(ReferencedInstance);
        }
        [TestMethod]
        public void RetrieveReferencedClassInstance_Bad_02()
        {
            CDClassPool ExecutionSpace = new CDClassPool();
            CDClass UserClass = ExecutionSpace.SpawnClass("User");
            UserClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));
            UserClass.AddAttribute(new CDAttribute("Age", EXETypes.IntegerTypeName));

            CDClassInstance Inst1 = UserClass.CreateClassInstance();
            Inst1.SetAttribute("Nick", "Alex");
            CDClassInstance Inst2 = UserClass.CreateClassInstance();
            Inst2.SetAttribute("Nick", "Miso");
            CDClassInstance Inst3 = UserClass.CreateClassInstance();
            Inst3.SetAttribute("Nick", "Mato");
            CDClassInstance Inst4 = UserClass.CreateClassInstance();
            Inst4.SetAttribute("Nick", "Samo");

            EXEScope Scope = new EXEScope();

            EXEReferencingVariable ReferencingVariable = new EXEReferencingVariable("user1", "SomeOtherClass", Inst4.UniqueID );
            CDClassInstance ReferencedInstance = ReferencingVariable.RetrieveReferencedClassInstance(ExecutionSpace);

            Assert.IsNull(ReferencedInstance);
        }

        [TestMethod]
        public void FindReferencingVariableByNameTest_Normal_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();

            CDClass UserAccountClass1 = ExecutionSpace.SpawnClass("UserAccount1");
            UserAccountClass1.AddAttribute(new CDAttribute("Nick1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("FirstName1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("LastName1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("Email1", EXETypes.StringTypeName));

            CDClass UserAccountClass2 = ExecutionSpace.SpawnClass("UserAccount2");
            UserAccountClass2.AddAttribute(new CDAttribute("Nick2", EXETypes.StringTypeName));
            UserAccountClass2.AddAttribute(new CDAttribute("FirstName2", EXETypes.StringTypeName));
            UserAccountClass2.AddAttribute(new CDAttribute("LastName2", EXETypes.StringTypeName));
            UserAccountClass2.AddAttribute(new CDAttribute("Email2", EXETypes.StringTypeName));

            CDClass UserAccountClass3 = ExecutionSpace.SpawnClass("UserAccount3");
            UserAccountClass3.AddAttribute(new CDAttribute("Nick3", EXETypes.StringTypeName));
            UserAccountClass3.AddAttribute(new CDAttribute("FirstName3", EXETypes.StringTypeName));
            UserAccountClass3.AddAttribute(new CDAttribute("LastName3", EXETypes.StringTypeName));
            UserAccountClass3.AddAttribute(new CDAttribute("Email3", EXETypes.StringTypeName));

            CDClassInstance Inst1 = UserAccountClass1.CreateClassInstance("x");
            CDClassInstance Inst2 = UserAccountClass2.CreateClassInstance("y");
            CDClassInstance Inst3 = UserAccountClass3.CreateClassInstance("z");

            EXEScope Scope = new EXEScope();
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Nick1", UserAccountClass1.Name, Inst1.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Nick2", UserAccountClass2.Name, Inst2.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Nick3", UserAccountClass3.Name, Inst3.UniqueID));

            EXEReferencingVariable SeekedVariable = Scope.FindReferencingVariableByName("Nick1");
            (String, String, long) ActualOutput = (SeekedVariable.Name, SeekedVariable.ClassName, SeekedVariable.ReferencedInstanceId);

            (String, String, long) ExpectedOutput = ("Nick1", UserAccountClass1.Name, Inst1.UniqueID);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void FindReferencingVariableByNameTest_Normal_02()
        {
            CDClassPool ExecutionSpace = new CDClassPool();

            CDClass UserAccountClass1 = ExecutionSpace.SpawnClass("UserAccount1");
            UserAccountClass1.AddAttribute(new CDAttribute("Nick1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("FirstName1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("LastName1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("Email1", EXETypes.StringTypeName));

            UserAccountClass1.AddAttribute(new CDAttribute("Nick2", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("FirstName2", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("LastName2", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("Email2", EXETypes.StringTypeName));

            UserAccountClass1.AddAttribute(new CDAttribute("Nick3", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("FirstName3", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("LastName3", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("Email3", EXETypes.StringTypeName));

            CDClassInstance Inst1 = UserAccountClass1.CreateClassInstance("x");


            EXEScope Scope = new EXEScope();
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Nick1", UserAccountClass1.Name, Inst1.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Nick2", UserAccountClass1.Name, Inst1.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Nick3", UserAccountClass1.Name, Inst1.UniqueID));

            EXEReferencingVariable SeekedVariable = Scope.FindReferencingVariableByName("Nick2");
            (String, String, long) ActualOutput = (SeekedVariable.Name, SeekedVariable.ClassName, SeekedVariable.ReferencedInstanceId);

            (String, String, long) ExpectedOutput = ("Nick2", UserAccountClass1.Name, Inst1.UniqueID);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void FindReferencingVariableByNameTest_Normal_03()
        {
            CDClassPool ExecutionSpace = new CDClassPool();

            CDClass UserAccountClass1 = ExecutionSpace.SpawnClass("UserAccount1");
            UserAccountClass1.AddAttribute(new CDAttribute("Nick1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("FirstName1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("LastName1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("Email1", EXETypes.StringTypeName));

            CDClass UserAccountClass2 = ExecutionSpace.SpawnClass("UserAccount2");
            UserAccountClass2.AddAttribute(new CDAttribute("Nick2", EXETypes.StringTypeName));
            UserAccountClass2.AddAttribute(new CDAttribute("FirstName2", EXETypes.StringTypeName));
            UserAccountClass2.AddAttribute(new CDAttribute("LastName2", EXETypes.StringTypeName));
            UserAccountClass2.AddAttribute(new CDAttribute("Email2", EXETypes.StringTypeName));

            CDClass UserAccountClass3 = ExecutionSpace.SpawnClass("UserAccount3");
            UserAccountClass3.AddAttribute(new CDAttribute("Nick3", EXETypes.StringTypeName));
            UserAccountClass3.AddAttribute(new CDAttribute("FirstName3", EXETypes.StringTypeName));
            UserAccountClass3.AddAttribute(new CDAttribute("LastName3", EXETypes.StringTypeName));
            UserAccountClass3.AddAttribute(new CDAttribute("Email3", EXETypes.StringTypeName));

            CDClassInstance Inst1 = UserAccountClass1.CreateClassInstance("x");
            CDClassInstance Inst2 = UserAccountClass2.CreateClassInstance("y");
            CDClassInstance Inst3 = UserAccountClass3.CreateClassInstance("z");

            EXEScope Scope = new EXEScope();
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Nick1", UserAccountClass1.Name, Inst1.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Nick2", UserAccountClass2.Name, Inst2.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Email3", UserAccountClass3.Name, Inst3.UniqueID));

            EXEReferencingVariable SeekedVariable = Scope.FindReferencingVariableByName("Email3");
            (String, String, long) ActualOutput = (SeekedVariable.Name, SeekedVariable.ClassName, SeekedVariable.ReferencedInstanceId);

            (String, String, long) ExpectedOutput = ("Email3", UserAccountClass3.Name, Inst3.UniqueID);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void FindReferencingVariableByNameTest_Bad_01()
        {
            CDClassPool ExecutionSpace = new CDClassPool();

            CDClass UserAccountClass1 = ExecutionSpace.SpawnClass("UserAccount1");
            UserAccountClass1.AddAttribute(new CDAttribute("Nick1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("FirstName1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("LastName1", EXETypes.StringTypeName));
            UserAccountClass1.AddAttribute(new CDAttribute("Email1", EXETypes.StringTypeName));

            CDClass UserAccountClass2 = ExecutionSpace.SpawnClass("UserAccount2");
            UserAccountClass2.AddAttribute(new CDAttribute("Nick2", EXETypes.StringTypeName));
            UserAccountClass2.AddAttribute(new CDAttribute("FirstName2", EXETypes.StringTypeName));
            UserAccountClass2.AddAttribute(new CDAttribute("LastName2", EXETypes.StringTypeName));
            UserAccountClass2.AddAttribute(new CDAttribute("Email2", EXETypes.StringTypeName));

            CDClass UserAccountClass3 = ExecutionSpace.SpawnClass("UserAccount3");
            UserAccountClass3.AddAttribute(new CDAttribute("Nick3", EXETypes.StringTypeName));
            UserAccountClass3.AddAttribute(new CDAttribute("FirstName3", EXETypes.StringTypeName));
            UserAccountClass3.AddAttribute(new CDAttribute("LastName3", EXETypes.StringTypeName));
            UserAccountClass3.AddAttribute(new CDAttribute("Email3", EXETypes.StringTypeName));

            CDClassInstance Inst1 = UserAccountClass1.CreateClassInstance("x");
            CDClassInstance Inst2 = UserAccountClass2.CreateClassInstance("y");
            CDClassInstance Inst3 = UserAccountClass3.CreateClassInstance("z");

            EXEScope Scope = new EXEScope();
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Nick1", UserAccountClass1.Name, Inst1.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Nick2", UserAccountClass2.Name, Inst2.UniqueID));
            Scope.ReferencingVariables.Add(new EXEReferencingVariable("Email3", UserAccountClass3.Name, Inst3.UniqueID));

            EXEReferencingVariable SeekedVariable = Scope.FindReferencingVariableByName("notExisting");


            Assert.IsNull(SeekedVariable);
        }



        [TestMethod]
        public void FindSetReferencingVariableByNameTest_Normal_01()
        {
           // List<EXEReferencingVariable> ReferencingVariables = new List<EXEReferencingVariable>;
            //ReferencingVariables.Add()
            EXEScope Scope = new EXEScope();
     

            EXEReferencingVariable ReferencingVariable1 = (new EXEReferencingVariable("Name1", "ClassName1", 10000001));
            EXEReferencingVariable ReferencingVariable2 = (new EXEReferencingVariable("Name2", "ClassName2", 10000002));
            EXEReferencingVariable ReferencingVariable3 = (new EXEReferencingVariable("Name3", "ClassName3", 10000003));


            EXEReferencingVariable ReferencingVariable11 = (new EXEReferencingVariable("Name11", "ClassName11", 100000011));
            EXEReferencingVariable ReferencingVariable22 = (new EXEReferencingVariable("Name22", "ClassName23", 100000022));
            EXEReferencingVariable ReferencingVariable33 = (new EXEReferencingVariable("Name33", "ClassName33", 100000033));

            EXEReferencingVariable ReferencingVariable111 = (new EXEReferencingVariable("Name111", "ClassName111", 1000000111));
            EXEReferencingVariable ReferencingVariable222 = (new EXEReferencingVariable("Name222", "ClassName222", 1000000222));
            EXEReferencingVariable ReferencingVariable333 = (new EXEReferencingVariable("Name333", "ClassName333", 1000000333));

            EXEReferencingSetVariable SetVariable1 = new EXEReferencingSetVariable();
            SetVariable1.AddReferencingVariable(ReferencingVariable1);
            SetVariable1.AddReferencingVariable(ReferencingVariable2);
            SetVariable1.AddReferencingVariable(ReferencingVariable3);


            EXEReferencingSetVariable SetVariable2 = new EXEReferencingSetVariable();
            SetVariable2.AddReferencingVariable(ReferencingVariable11);
            SetVariable2.AddReferencingVariable(ReferencingVariable22);
            SetVariable2.AddReferencingVariable(ReferencingVariable33);

            EXEReferencingSetVariable SetVariable3 = new EXEReferencingSetVariable();
            SetVariable3.AddReferencingVariable(ReferencingVariable111);
            SetVariable3.AddReferencingVariable(ReferencingVariable222);
            SetVariable3.AddReferencingVariable(ReferencingVariable333);

            Scope.SetReferencingVariables.Add(SetVariable1);
            Scope.SetReferencingVariables.Add(SetVariable2);
            Scope.SetReferencingVariables.Add(SetVariable3);

            EXEReferencingSetVariable SeekedVariable = Scope.FindSetReferencingVariableByName("Name3");
            

            Assert.AreEqual(SetVariable1, SeekedVariable);
        }

        [TestMethod]
        public void FindSetReferencingVariableByNameTest_Normal_02()
        {
     
            EXEScope Scope = new EXEScope();

            EXEReferencingVariable ReferencingVariable1 = (new EXEReferencingVariable("Name1", "ClassName1", 10000001));
            EXEReferencingVariable ReferencingVariable2 = (new EXEReferencingVariable("Name2", "ClassName2", 10000002));
            EXEReferencingVariable ReferencingVariable3 = (new EXEReferencingVariable("Name3", "ClassName3", 10000003));


            EXEReferencingVariable ReferencingVariable11 = (new EXEReferencingVariable("Name11", "ClassName11", 100000011));
            EXEReferencingVariable ReferencingVariable22 = (new EXEReferencingVariable("Name22", "ClassName23", 100000022));
            EXEReferencingVariable ReferencingVariable33 = (new EXEReferencingVariable("Name33", "ClassName33", 100000033));

            EXEReferencingVariable ReferencingVariable111 = (new EXEReferencingVariable("Name111", "ClassName111", 1000000111));
            EXEReferencingVariable ReferencingVariable222 = (new EXEReferencingVariable("Name222", "ClassName222", 1000000222));
            EXEReferencingVariable ReferencingVariable333 = (new EXEReferencingVariable("Name333", "ClassName333", 1000000333));

            EXEReferencingSetVariable SetVariable1 = new EXEReferencingSetVariable();
            SetVariable1.AddReferencingVariable(ReferencingVariable1);
            SetVariable1.AddReferencingVariable(ReferencingVariable2);
            SetVariable1.AddReferencingVariable(ReferencingVariable3);


            EXEReferencingSetVariable SetVariable2 = new EXEReferencingSetVariable();
            SetVariable2.AddReferencingVariable(ReferencingVariable11);
            SetVariable2.AddReferencingVariable(ReferencingVariable22);
            SetVariable2.AddReferencingVariable(ReferencingVariable33);

            EXEReferencingSetVariable SetVariable3 = new EXEReferencingSetVariable();
            SetVariable3.AddReferencingVariable(ReferencingVariable111);
            SetVariable3.AddReferencingVariable(ReferencingVariable222);
            SetVariable3.AddReferencingVariable(ReferencingVariable333);

            Scope.SetReferencingVariables.Add(SetVariable1);
            Scope.SetReferencingVariables.Add(SetVariable2);
            Scope.SetReferencingVariables.Add(SetVariable3);


            EXEReferencingSetVariable SeekedVariable = Scope.FindSetReferencingVariableByName("Name22");


            Assert.AreEqual(SetVariable2, SeekedVariable);
        }

        [TestMethod]
        public void FindSetReferencingVariableByNameTest_Normal_03()
        {
            // List<EXEReferencingVariable> ReferencingVariables = new List<EXEReferencingVariable>;
            //ReferencingVariables.Add()
            EXEScope Scope = new EXEScope();


            EXEReferencingVariable ReferencingVariable1 = (new EXEReferencingVariable("Name1", "ClassName1", 10000001));
            EXEReferencingVariable ReferencingVariable2 = (new EXEReferencingVariable("Name2", "ClassName2", 10000002));
            EXEReferencingVariable ReferencingVariable3 = (new EXEReferencingVariable("Name3", "ClassName3", 10000003));


            EXEReferencingVariable ReferencingVariable11 = (new EXEReferencingVariable("Name11", "ClassName11", 100000011));
            EXEReferencingVariable ReferencingVariable22 = (new EXEReferencingVariable("Name22", "ClassName23", 100000022));
            EXEReferencingVariable ReferencingVariable33 = (new EXEReferencingVariable("Name33", "ClassName33", 100000033));

            EXEReferencingVariable ReferencingVariable111 = (new EXEReferencingVariable("Name111", "ClassName111", 1000000111));
            EXEReferencingVariable ReferencingVariable222 = (new EXEReferencingVariable("Name222", "ClassName222", 1000000222));
            EXEReferencingVariable ReferencingVariable333 = (new EXEReferencingVariable("Name333", "ClassName333", 1000000333));

            EXEReferencingSetVariable SetVariable1 = new EXEReferencingSetVariable();
            SetVariable1.AddReferencingVariable(ReferencingVariable1);
            SetVariable1.AddReferencingVariable(ReferencingVariable2);
            SetVariable1.AddReferencingVariable(ReferencingVariable3);


            EXEReferencingSetVariable SetVariable2 = new EXEReferencingSetVariable();
            SetVariable2.AddReferencingVariable(ReferencingVariable11);
            SetVariable2.AddReferencingVariable(ReferencingVariable22);
            SetVariable2.AddReferencingVariable(ReferencingVariable33);

            EXEReferencingSetVariable SetVariable3 = new EXEReferencingSetVariable();
            SetVariable3.AddReferencingVariable(ReferencingVariable111);
            SetVariable3.AddReferencingVariable(ReferencingVariable222);
            SetVariable3.AddReferencingVariable(ReferencingVariable333);

            Scope.SetReferencingVariables.Add(SetVariable1);
            Scope.SetReferencingVariables.Add(SetVariable2);
            Scope.SetReferencingVariables.Add(SetVariable3);


            EXEReferencingSetVariable SeekedVariable = Scope.FindSetReferencingVariableByName("Name111");


            Assert.AreEqual(SetVariable3, SeekedVariable);
        }

        [TestMethod]
        public void FindSetReferencingVariableByNameTest_Bad_01()
        {
            // List<EXEReferencingVariable> ReferencingVariables = new List<EXEReferencingVariable>;
            //ReferencingVariables.Add()
            EXEScope Scope = new EXEScope();


            EXEReferencingVariable ReferencingVariable1 = (new EXEReferencingVariable("Name1", "ClassName1", 10000001));
            EXEReferencingVariable ReferencingVariable2 = (new EXEReferencingVariable("Name2", "ClassName2", 10000002));
            EXEReferencingVariable ReferencingVariable3 = (new EXEReferencingVariable("Name3", "ClassName3", 10000003));


            EXEReferencingVariable ReferencingVariable11 = (new EXEReferencingVariable("Name11", "ClassName11", 100000011));
            EXEReferencingVariable ReferencingVariable22 = (new EXEReferencingVariable("Name22", "ClassName23", 100000022));
            EXEReferencingVariable ReferencingVariable33 = (new EXEReferencingVariable("Name33", "ClassName33", 100000033));

            EXEReferencingVariable ReferencingVariable111 = (new EXEReferencingVariable("Name111", "ClassName111", 1000000111));
            EXEReferencingVariable ReferencingVariable222 = (new EXEReferencingVariable("Name222", "ClassName222", 1000000222));
            EXEReferencingVariable ReferencingVariable333 = (new EXEReferencingVariable("Name333", "ClassName333", 1000000333));

            EXEReferencingSetVariable SetVariable1 = new EXEReferencingSetVariable();
            SetVariable1.AddReferencingVariable(ReferencingVariable1);
            SetVariable1.AddReferencingVariable(ReferencingVariable2);
            SetVariable1.AddReferencingVariable(ReferencingVariable3);


            EXEReferencingSetVariable SetVariable2 = new EXEReferencingSetVariable();
            SetVariable2.AddReferencingVariable(ReferencingVariable11);
            SetVariable2.AddReferencingVariable(ReferencingVariable22);
            SetVariable2.AddReferencingVariable(ReferencingVariable33);

            EXEReferencingSetVariable SetVariable3 = new EXEReferencingSetVariable();
            SetVariable3.AddReferencingVariable(ReferencingVariable111);
            SetVariable3.AddReferencingVariable(ReferencingVariable222);
            SetVariable3.AddReferencingVariable(ReferencingVariable333);

            Scope.SetReferencingVariables.Add(SetVariable1);
            Scope.SetReferencingVariables.Add(SetVariable2);
            Scope.SetReferencingVariables.Add(SetVariable3);


            EXEReferencingSetVariable SeekedVariable = Scope.FindSetReferencingVariableByName("NotExisting");


            Assert.IsNull(SeekedVariable);
        }

    }
}