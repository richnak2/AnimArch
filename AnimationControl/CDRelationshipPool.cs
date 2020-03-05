using System;
using System.Collections.Generic;

namespace AnimationControl
{
    public class CDRelationshipPool
    {
        private long RelationshipIdSeed;
        private List<CDRelationship> RelationshipPool { get; }

        public CDRelationshipPool()
        {
            this.RelationshipIdSeed = 1;
            this.RelationshipPool = new List<CDRelationship>();
        }

        public CDRelationship SpawnRelationship(String FromClass, String ToClass)
        {
            CDRelationship NewRelationship = new CDRelationship(FromClass, ToClass, "R" + this.RelationshipIdSeed);
            this.RelationshipPool.Add(NewRelationship);
            this.RelationshipIdSeed++;

            return NewRelationship;
        }
        //SetUloh3
        //Relationships are bi-directional -> do not mind the order of classes
        public Boolean RelationshipExists(String Class1Name, String Class2Name)
        {
            throw new NotImplementedException();
        }
        //SetUloh3
        // use previous method to check if that relationship exists at all
        // if it does not, return null
        // create unit tests for this one (keep the naming convention as it is in other tests)
        public CDRelationship GetRelationshipByClasses(String Class1, String Class2)
        {
            throw new NotImplementedException();
        }
    }
}
