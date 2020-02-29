using System;
using System.Collections.Generic;

namespace AnimationControl
{
    public class CDRelationship
    {
        public String FromClass { get;}
        public String ToClass { get;}
        public String RelationshipName { get; }

        private List<(long, long)> InstanceRelationships { get;}

        public CDRelationship(String FromClass, String ToClass, String Name)
        {
            this.FromClass = FromClass;
            this.ToClass = ToClass;
            this.RelationshipName = Name;
            this.InstanceRelationships = new List<(long, long)>();
        }

        public Boolean InstanceRelationshipExists(long Instance1Id, long Instance2Id)
        {
            Boolean Result = false;
            foreach ((long, long) Tupple in this.InstanceRelationships)
            {
                if (Tupple == (Instance1Id, Instance2Id) || Tupple == (Instance2Id, Instance1Id))
                {
                    Result = true;
                    break;
                }
            }
            return Result;
        }
        //SetUloh3
        //return list of all ids related to given id
        //do not forget that relationships are bi-directional
        //if no instances are related, return empty list
        //create unit tests for this one (keep the naming convention as it is in other tests). Do not forget about edge cases
        public List<long> GetRelatedInstaceIds(long Id)
        {
            throw new NotImplementedException();
        }
        public Boolean CreateRelationship(long Instance1Id, long Instance2Id)
        {
            Boolean Result = false;

            if (InstanceRelationshipExists(Instance1Id, Instance2Id))
            {
                Result = false;
            }
            else
            {
                this.InstanceRelationships.Add((Instance1Id, Instance2Id));
                Result = true;
            }

            return Result;
        }

        public void ClearRelationships()
        {
            this.InstanceRelationships.Clear();
        }
    }
}
