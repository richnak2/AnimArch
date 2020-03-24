using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEReferenceEvaluatorTests
    {
        //SetUloh1
        // The test scenarios are a bit complicated here, because we first must prepare classes, their instances and referencing variables,
        // before we call methods to set a and get attribute values
        // Add some more tests. You are the one who implements these methods, so you know which situations need to be checked

        [TestMethod]
        public void EvaluateAttributeValue_Normal_01()
        {
            Animation Animation = new Animation();

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));

            CDClassInstance ClassInstance = UserAccountClass.CreateClassInstance();
            ClassInstance.SetAttribute("Nick", "\"Jano245\"");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user", "UserAccount", ClassInstance.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            String ActualOutput = Evaluator.EvaluateAttributeValue("new_user", "Nick", Scope, Animation.ExecutionSpace);

            String ExpectedOutput = "\"Jano245\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void EvaluateAttributeValue_Normal_02()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            String ActualOutput = Evaluator.EvaluateAttributeValue("new_user2", "LastName", Scope, Animation.ExecutionSpace);

            String ExpectedOutput = "\"Cirova\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void EvaluateAttributeValue_Normal_03()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            CDClassInstance ClassInstance4 = UserAccountClass.CreateClassInstance();
            ClassInstance4.SetAttribute("Nick", "\"PPP\"");
            ClassInstance4.SetAttribute("FirstName", "\"Pepe\"");
            ClassInstance4.SetAttribute("LastName", "\"Peen\"");
            ClassInstance4.SetAttribute("Email", "\"pp@gmail.com\"");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("new_user4", "UserAccount", ClassInstance4.UniqueID));
            Scope.SuperScope = SuperScope;

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            String ActualOutput = Evaluator.EvaluateAttributeValue("new_user4", "Email", Scope, Animation.ExecutionSpace);

            String ExpectedOutput = "\"pp@gmail.com\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void EvaluateAttributeValue_Bad_01()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            String ActualOutput = Evaluator.EvaluateAttributeValue("new_user4", "LastName", Scope, Animation.ExecutionSpace);

            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void EvaluateAttributeValue_Bad_02()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            String ActualOutput = Evaluator.EvaluateAttributeValue("new_user3", "Age", Scope, Animation.ExecutionSpace);

            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void EvaluateAttributeValue_Bad_03()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            CDClassInstance ClassInstance4 = UserAccountClass.CreateClassInstance();
            ClassInstance4.SetAttribute("Nick", "\"PPP\"");
            ClassInstance4.SetAttribute("FirstName", "\"Pepe\"");
            ClassInstance4.SetAttribute("LastName", "\"Peen\"");
            ClassInstance4.SetAttribute("Email", "\"pp@gmail.com\"");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("new_user4", "UserAccount", ClassInstance4.UniqueID));
            Scope.SuperScope = SuperScope;

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            String ActualOutput = Evaluator.EvaluateAttributeValue("new_user5", "Email", Scope, Animation.ExecutionSpace);

            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void EvaluateAttributeValue_Bad_04()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            CDClassInstance ClassInstance4 = UserAccountClass.CreateClassInstance();
            ClassInstance4.SetAttribute("Nick", "\"PPP\"");
            ClassInstance4.SetAttribute("FirstName", "\"Pepe\"");
            ClassInstance4.SetAttribute("LastName", "\"Peen\"");
            ClassInstance4.SetAttribute("Email", "\"pp@gmail.com\"");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEScope SuperScope = new EXEScope();
            SuperScope.AddVariable(new EXEReferencingVariable("new_user4", "UserAccount", ClassInstance4.UniqueID));
            Scope.SuperScope = SuperScope;

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            String ActualOutput = Evaluator.EvaluateAttributeValue("new_user4", "Age", Scope, Animation.ExecutionSpace);

            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void SetAttributeValue_Normal_01()
        {
            Animation Animation = new Animation();

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));

            CDClassInstance ClassInstance = UserAccountClass.CreateClassInstance();
            ClassInstance.SetAttribute("Nick", "\"Jano245\"");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user", "UserAccount", ClassInstance.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            Evaluator.SetAttributeValue("new_user", "Nick", Scope, Animation.ExecutionSpace, "\"Jano69\"");

            String ActualOutput = ClassInstance.GetAttributeValue("Nick");

            String ExpectedOutput = "\"Jano69\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void SetAttributeValue_Normal_02()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            Evaluator.SetAttributeValue("new_user1", "FirstName", Scope, Animation.ExecutionSpace, "\"Ivan\"");

            String ActualOutput = ClassInstance1.GetAttributeValue("FirstName");

            String ExpectedOutput = "\"Ivan\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void SetAttributeValue_Normal_03()
        {
            Animation Animation = new Animation();

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.BooleanTypeName));

            CDClassInstance ClassInstance = UserAccountClass.CreateClassInstance();

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user", "UserAccount", ClassInstance.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            Evaluator.SetAttributeValue("new_user", "Nick", Scope, Animation.ExecutionSpace, EXETypes.BooleanFalse);

            String ActualOutput = ClassInstance.GetAttributeValue("Nick");

            String ExpectedOutput = EXETypes.BooleanFalse;

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void SetAttributeValue_Bad_01()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            List<(String, String, String, String)> ExpectedState =
                new List<(string, string, string, string)>(new (string, string, string, string)[] {
                    (ClassInstance1.GetAttributeValue("Nick"), ClassInstance1.GetAttributeValue("FirstName"),ClassInstance1.GetAttributeValue("LastName"),ClassInstance1.GetAttributeValue("Email")),
                    (ClassInstance2.GetAttributeValue("Nick"), ClassInstance2.GetAttributeValue("FirstName"),ClassInstance2.GetAttributeValue("LastName"),ClassInstance2.GetAttributeValue("Email")),
                    (ClassInstance3.GetAttributeValue("Nick"), ClassInstance3.GetAttributeValue("FirstName"),ClassInstance3.GetAttributeValue("LastName"),ClassInstance3.GetAttributeValue("Email"))
            });

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            bool Success = Evaluator.SetAttributeValue("new_user4", "FirstName", Scope, Animation.ExecutionSpace, "\"Ivan\"");

            List<(String, String, String, String)> ActualState =
                new List<(string, string, string, string)>(new (string, string, string, string)[] {
                    (ClassInstance1.GetAttributeValue("Nick"), ClassInstance1.GetAttributeValue("FirstName"),ClassInstance1.GetAttributeValue("LastName"),ClassInstance1.GetAttributeValue("Email")),
                    (ClassInstance2.GetAttributeValue("Nick"), ClassInstance2.GetAttributeValue("FirstName"),ClassInstance2.GetAttributeValue("LastName"),ClassInstance2.GetAttributeValue("Email")),
                    (ClassInstance3.GetAttributeValue("Nick"), ClassInstance3.GetAttributeValue("FirstName"),ClassInstance3.GetAttributeValue("LastName"),ClassInstance3.GetAttributeValue("Email"))
            });

            Assert.IsFalse(Success);
            CollectionAssert.AreEqual(ExpectedState, ActualState);
        }
        [TestMethod]
        public void SetAttributeValue_Bad_02()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            List<(String, String, String, String)> ExpectedState =
                new List<(string, string, string, string)>(new (string, string, string, string)[] {
                    (ClassInstance1.GetAttributeValue("Nick"), ClassInstance1.GetAttributeValue("FirstName"),ClassInstance1.GetAttributeValue("LastName"),ClassInstance1.GetAttributeValue("Email")),
                    (ClassInstance2.GetAttributeValue("Nick"), ClassInstance2.GetAttributeValue("FirstName"),ClassInstance2.GetAttributeValue("LastName"),ClassInstance2.GetAttributeValue("Email")),
                    (ClassInstance3.GetAttributeValue("Nick"), ClassInstance3.GetAttributeValue("FirstName"),ClassInstance3.GetAttributeValue("LastName"),ClassInstance3.GetAttributeValue("Email"))
            });

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            bool Success = Evaluator.SetAttributeValue("new_user3", "Age", Scope, Animation.ExecutionSpace, "\"Ivan\"");

            List<(String, String, String, String)> ActualState =
                new List<(string, string, string, string)>(new (string, string, string, string)[] {
                    (ClassInstance1.GetAttributeValue("Nick"), ClassInstance1.GetAttributeValue("FirstName"),ClassInstance1.GetAttributeValue("LastName"),ClassInstance1.GetAttributeValue("Email")),
                    (ClassInstance2.GetAttributeValue("Nick"), ClassInstance2.GetAttributeValue("FirstName"),ClassInstance2.GetAttributeValue("LastName"),ClassInstance2.GetAttributeValue("Email")),
                    (ClassInstance3.GetAttributeValue("Nick"), ClassInstance3.GetAttributeValue("FirstName"),ClassInstance3.GetAttributeValue("LastName"),ClassInstance3.GetAttributeValue("Email"))
            });

            Assert.IsFalse(Success);
            CollectionAssert.AreEqual(ExpectedState, ActualState);
        }
        [TestMethod]
        public void SetAttributeValue_Bad_03()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");

            List<(String, String, String, String)> ExpectedState =
                new List<(string, string, string, string)>(new (string, string, string, string)[] {
                    (ClassInstance1.GetAttributeValue("Nick"), ClassInstance1.GetAttributeValue("FirstName"),ClassInstance1.GetAttributeValue("LastName"),ClassInstance1.GetAttributeValue("Email")),
                    (ClassInstance2.GetAttributeValue("Nick"), ClassInstance2.GetAttributeValue("FirstName"),ClassInstance2.GetAttributeValue("LastName"),ClassInstance2.GetAttributeValue("Email")),
                    (ClassInstance3.GetAttributeValue("Nick"), ClassInstance3.GetAttributeValue("FirstName"),ClassInstance3.GetAttributeValue("LastName"),ClassInstance3.GetAttributeValue("Email"))
            });

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            bool Success = Evaluator.SetAttributeValue("new_user3", "Nick", Scope, Animation.ExecutionSpace, "52");

            List<(String, String, String, String)> ActualState =
                new List<(string, string, string, string)>(new (string, string, string, string)[] {
                    (ClassInstance1.GetAttributeValue("Nick"), ClassInstance1.GetAttributeValue("FirstName"),ClassInstance1.GetAttributeValue("LastName"),ClassInstance1.GetAttributeValue("Email")),
                    (ClassInstance2.GetAttributeValue("Nick"), ClassInstance2.GetAttributeValue("FirstName"),ClassInstance2.GetAttributeValue("LastName"),ClassInstance2.GetAttributeValue("Email")),
                    (ClassInstance3.GetAttributeValue("Nick"), ClassInstance3.GetAttributeValue("FirstName"),ClassInstance3.GetAttributeValue("LastName"),ClassInstance3.GetAttributeValue("Email"))
            });

            Assert.IsFalse(Success);
            CollectionAssert.AreEqual(ExpectedState, ActualState);
        }
        [TestMethod]
        public void SetAttributeValue_Bad_04()
        {
            Animation Animation = new Animation();

            CDClass GameClass = Animation.ExecutionSpace.SpawnClass("Game");
            GameClass.AddAttribute(new CDAttribute("Score", EXETypes.IntegerTypeName));
            GameClass.AddAttribute(new CDAttribute("Name", EXETypes.StringTypeName));

            CDClass UserAccountClass = Animation.ExecutionSpace.SpawnClass("UserAccount");
            UserAccountClass.AddAttribute(new CDAttribute("Nick", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("FirstName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("LastName", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Email", EXETypes.StringTypeName));
            UserAccountClass.AddAttribute(new CDAttribute("Age", EXETypes.IntegerTypeName));

            CDClassInstance ClassInstance1 = UserAccountClass.CreateClassInstance();
            ClassInstance1.SetAttribute("Nick", "\"Jano245\"");
            ClassInstance1.SetAttribute("FirstName", "\"Jano\"");
            ClassInstance1.SetAttribute("LastName", "\"Podnozka\"");
            ClassInstance1.SetAttribute("Email", "\"jano.podnozka17@gmail.com\"");
            ClassInstance1.SetAttribute("Age", "22");

            CDClassInstance ClassInstance2 = UserAccountClass.CreateClassInstance();
            ClassInstance2.SetAttribute("Nick", "\"sexica2521\"");
            ClassInstance2.SetAttribute("FirstName", "\"Maria\"");
            ClassInstance2.SetAttribute("LastName", "\"Cirova\"");
            ClassInstance2.SetAttribute("Email", "\"majka.cajka@azet.sk\"");
            ClassInstance1.SetAttribute("Age", "17");

            CDClassInstance ClassInstance3 = UserAccountClass.CreateClassInstance();
            ClassInstance3.SetAttribute("Nick", "\"MedievalCollectibles\"");
            ClassInstance3.SetAttribute("FirstName", "\"Anne\"");
            ClassInstance3.SetAttribute("LastName", "\"Vazziereth\"");
            ClassInstance3.SetAttribute("Email", "\"medieval.collectibles@gmail.com\"");
            ClassInstance1.SetAttribute("Age", "34");

            List<(String, String, String, String, String)> ExpectedState =
                new List<(string, string, string, string, string)>(new (string, string, string, string, string)[] {
                    (ClassInstance1.GetAttributeValue("Nick"), ClassInstance1.GetAttributeValue("FirstName"),ClassInstance1.GetAttributeValue("LastName"),ClassInstance1.GetAttributeValue("Email"),ClassInstance1.GetAttributeValue("Age")),
                    (ClassInstance2.GetAttributeValue("Nick"), ClassInstance2.GetAttributeValue("FirstName"),ClassInstance2.GetAttributeValue("LastName"),ClassInstance2.GetAttributeValue("Email"),ClassInstance2.GetAttributeValue("Age")),
                    (ClassInstance3.GetAttributeValue("Nick"), ClassInstance3.GetAttributeValue("FirstName"),ClassInstance3.GetAttributeValue("LastName"),ClassInstance3.GetAttributeValue("Email"),ClassInstance3.GetAttributeValue("Age"))
            });

            EXEScope Scope = new EXEScope();
            Scope.AddVariable(new EXEReferencingVariable("new_user1", "UserAccount", ClassInstance1.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user2", "UserAccount", ClassInstance2.UniqueID));
            Scope.AddVariable(new EXEReferencingVariable("new_user3", "UserAccount", ClassInstance3.UniqueID));

            EXEReferenceEvaluator Evaluator = new EXEReferenceEvaluator();
            bool Success = Evaluator.SetAttributeValue("new_user3", "Age", Scope, Animation.ExecutionSpace, EXETypes.BooleanFalse);

            List<(String, String, String, String, String)> ActualState =
                new List<(string, string, string, string, string)>(new (string, string, string, string, string)[] {
                    (ClassInstance1.GetAttributeValue("Nick"), ClassInstance1.GetAttributeValue("FirstName"),ClassInstance1.GetAttributeValue("LastName"),ClassInstance1.GetAttributeValue("Email"),ClassInstance1.GetAttributeValue("Age")),
                    (ClassInstance2.GetAttributeValue("Nick"), ClassInstance2.GetAttributeValue("FirstName"),ClassInstance2.GetAttributeValue("LastName"),ClassInstance2.GetAttributeValue("Email"),ClassInstance2.GetAttributeValue("Age")),
                    (ClassInstance3.GetAttributeValue("Nick"), ClassInstance3.GetAttributeValue("FirstName"),ClassInstance3.GetAttributeValue("LastName"),ClassInstance3.GetAttributeValue("Email"),ClassInstance3.GetAttributeValue("Age"))
            });

            Assert.IsFalse(Success);
            CollectionAssert.AreEqual(ExpectedState, ActualState);
        }
    }
}