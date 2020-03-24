using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AnimationControl.Tests
{
    [TestClass]
    public class CDRelationshipPoolTests
    {
        [TestMethod]
        public void RelationshipExists_Normal_01()
        {
            Animation Animation = new Animation();
            CDRelationship Rel = Animation.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = Animation.RelationshipSpace.RelationshipExistsByClasses("Fortress", "Soldier");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_02()
        {
            Animation Animation = new Animation();
            CDRelationship Rel = Animation.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = Animation.RelationshipSpace.RelationshipExistsByClasses("Soldier", "Fortress");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_03()
        {
            Animation Animation = new Animation();
            CDRelationship Rel = Animation.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = Animation.RelationshipSpace.RelationshipExistsByClasses("Fortress", "Soldier");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_04()
        {
            Animation Animation = new Animation();
            CDRelationship Rel = Animation.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = Animation.RelationshipSpace.RelationshipExistsByClasses("Soldier", "Fortress");
            Assert.IsTrue(ActualResult);
        }
        public void RelationshipExists_Normal_05()
        {
            Animation Animation = new Animation();
            CDRelationship Rel1 = Animation.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            CDRelationship Rel2 = Animation.RelationshipSpace.SpawnRelationship("Shield", "Soldier");
            CDRelationship Rel3 = Animation.RelationshipSpace.SpawnRelationship("Sword", "Soldier");

            Boolean ActualResult = Animation.RelationshipSpace.RelationshipExistsByClasses("Sword", "Soldier");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_06()
        {
            Animation Animation = new Animation();
            CDRelationship Rel1 = Animation.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            CDRelationship Rel2 = Animation.RelationshipSpace.SpawnRelationship("Shield", "Soldier");
            CDRelationship Rel3 = Animation.RelationshipSpace.SpawnRelationship("Sword", "Soldier");

            Boolean ActualResult = Animation.RelationshipSpace.RelationshipExistsByClasses("Soldier", "Fortress");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Bad_01()
        {
            Animation Animation = new Animation();

            Boolean ActualResult = Animation.RelationshipSpace.RelationshipExistsByClasses("Fortress", "Soldier");
            Assert.IsFalse(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Bad_02()
        {
            Animation Animation = new Animation();
            Animation.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");

            Boolean ActualResult = Animation.RelationshipSpace.RelationshipExistsByClasses("Fortress", "Sword");
            Assert.IsFalse(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Bad_03()
        {
            Animation Animation = new Animation();
            CDRelationship Rel1 = Animation.RelationshipSpace.SpawnRelationship("Fortress", "Soldier");
            CDRelationship Rel2 = Animation.RelationshipSpace.SpawnRelationship("Shield", "Soldier");
            CDRelationship Rel3 = Animation.RelationshipSpace.SpawnRelationship("Sword", "Soldier");

            Boolean ActualResult = Animation.RelationshipSpace.RelationshipExistsByClasses("Fortress", "Sword");
            Assert.IsFalse(ActualResult);
        }
    }
}