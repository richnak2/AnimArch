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
            CDRelationshipPool RelPool = new CDRelationshipPool();
            CDRelationship Rel = RelPool.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = RelPool.RelationshipExists("Fortress", "Soldier");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_02()
        {
            CDRelationshipPool RelPool = new CDRelationshipPool();
            CDRelationship Rel = RelPool.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = RelPool.RelationshipExists("Soldier", "Fortress");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_03()
        {
            CDRelationshipPool RelPool = new CDRelationshipPool();
            CDRelationship Rel = RelPool.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = RelPool.RelationshipExists("Fortress", "Soldier");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_04()
        {
            CDRelationshipPool RelPool = new CDRelationshipPool();
            CDRelationship Rel = RelPool.SpawnRelationship("Fortress", "Soldier");
            Rel.CreateRelationship(331, 225);

            Boolean ActualResult = RelPool.RelationshipExists("Soldier", "Fortress");
            Assert.IsTrue(ActualResult);
        }
        public void RelationshipExists_Normal_05()
        {
            CDRelationshipPool RelPool = new CDRelationshipPool();
            CDRelationship Rel1 = RelPool.SpawnRelationship("Fortress", "Soldier");
            CDRelationship Rel2 = RelPool.SpawnRelationship("Shield", "Soldier");
            CDRelationship Rel3 = RelPool.SpawnRelationship("Sword", "Soldier");

            Boolean ActualResult = RelPool.RelationshipExists("Sword", "Soldier");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Normal_06()
        {
            CDRelationshipPool RelPool = new CDRelationshipPool();
            CDRelationship Rel1 = RelPool.SpawnRelationship("Fortress", "Soldier");
            CDRelationship Rel2 = RelPool.SpawnRelationship("Shield", "Soldier");
            CDRelationship Rel3 = RelPool.SpawnRelationship("Sword", "Soldier");

            Boolean ActualResult = RelPool.RelationshipExists("Soldier", "Fortress");
            Assert.IsTrue(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Bad_01()
        {
            CDRelationshipPool RelPool = new CDRelationshipPool();

            Boolean ActualResult = RelPool.RelationshipExists("Fortress", "Soldier");
            Assert.IsFalse(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Bad_02()
        {
            CDRelationshipPool RelPool = new CDRelationshipPool();
            RelPool.SpawnRelationship("Fortress", "Soldier");

            Boolean ActualResult = RelPool.RelationshipExists("Fortress", "Sword");
            Assert.IsFalse(ActualResult);
        }
        [TestMethod]
        public void RelationshipExists_Bad_03()
        {
            CDRelationshipPool RelPool = new CDRelationshipPool();
            CDRelationship Rel1 = RelPool.SpawnRelationship("Fortress", "Soldier");
            CDRelationship Rel2 = RelPool.SpawnRelationship("Shield", "Soldier");
            CDRelationship Rel3 = RelPool.SpawnRelationship("Sword", "Soldier");

            Boolean ActualResult = RelPool.RelationshipExists("Fortress", "Sword");
            Assert.IsFalse(ActualResult);
        }
    }
}