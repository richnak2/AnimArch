using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OALProgramControl.Tests
{
    [TestClass]
    public class CDRelationshipPoolTests
    {
        [TestMethod]
        public void RelationshipExists_Normal_01()
        {
            OALProgram OALProgram = new OALProgram();
            CDRelationship Rel = OALProgram.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = OALProgram.RelationshipSpace.RelationshipExistsByClasses("Fortress", "Soldier");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_02()
        {
            OALProgram OALProgram = new OALProgram();
            CDRelationship Rel = OALProgram.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = OALProgram.RelationshipSpace.RelationshipExistsByClasses("Soldier", "Fortress");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_03()
        {
            OALProgram OALProgram = new OALProgram();
            CDRelationship Rel = OALProgram.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = OALProgram.RelationshipSpace.RelationshipExistsByClasses("Fortress", "Soldier");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_04()
        {
            OALProgram OALProgram = new OALProgram();
            CDRelationship Rel = OALProgram.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = OALProgram.RelationshipSpace.RelationshipExistsByClasses("Soldier", "Fortress");
            Assert.IsTrue(ActualResult);
        }
        public void RelationshipExists_Normal_05()
        {
            OALProgram OALProgram = new OALProgram();
            CDRelationship Rel1 = OALProgram.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            CDRelationship Rel2 = OALProgram.RelationshipSpace.SpawnRelationship("Shield", "Soldier");
            CDRelationship Rel3 = OALProgram.RelationshipSpace.SpawnRelationship("Sword", "Soldier");

            Boolean ActualResult = OALProgram.RelationshipSpace.RelationshipExistsByClasses("Sword", "Soldier");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_06()
        {
            OALProgram OALProgram = new OALProgram();
            CDRelationship Rel1 = OALProgram.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            CDRelationship Rel2 = OALProgram.RelationshipSpace.SpawnRelationship("Shield", "Soldier");
            CDRelationship Rel3 = OALProgram.RelationshipSpace.SpawnRelationship("Sword", "Soldier");

            Boolean ActualResult = OALProgram.RelationshipSpace.RelationshipExistsByClasses("Soldier", "Fortress");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Bad_01()
        {
            OALProgram OALProgram = new OALProgram();

            Boolean ActualResult = OALProgram.RelationshipSpace.RelationshipExistsByClasses("Fortress", "Soldier");
            Assert.IsFalse(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Bad_02()
        {
            OALProgram OALProgram = new OALProgram();
            OALProgram.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");

            Boolean ActualResult = OALProgram.RelationshipSpace.RelationshipExistsByClasses("Fortress", "Sword");
            Assert.IsFalse(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Bad_03()
        {
            OALProgram OALProgram = new OALProgram();
            CDRelationship Rel1 = OALProgram.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            CDRelationship Rel2 = OALProgram.RelationshipSpace.SpawnRelationship("Shield", "Soldier");
            CDRelationship Rel3 = OALProgram.RelationshipSpace.SpawnRelationship("Sword", "Soldier");

            Boolean ActualResult = OALProgram.RelationshipSpace.RelationshipExistsByClasses("Fortress", "Sword");
            Assert.IsFalse(ActualResult);
        }
    }
}