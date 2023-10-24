using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OALProgramControl
{
    public class MethodInvocationInfo
    {
        public CDMethod CallerMethod { get; }
        public CDMethod CalledMethod { get; }
        public CDRelationship Relation { get; }
        public CDClassInstance CallerObject { get; }
        public CDClassInstance CalledObject { get; }

        public MethodInvocationInfo(CDMethod callerMethod, CDMethod calledMethod, CDRelationship relation, CDClassInstance callerObject, CDClassInstance calledObject)
        {
            this.CalledMethod = callerMethod;
            this.CalledMethod = calledMethod;
            this.Relation = relation;
            this.CallerObject = callerObject;
            this.CalledObject = calledObject;
        }

        public override string ToString()
        {
            return string.Join
            (
                " ",
                new string[]
                {
                    this.CallerMethod.OwningClass.Name,
                    this.CallerMethod.Name,
                    this.Relation.RelationshipName,
                    this.CalledMethod.OwningClass.Name,
                    this.CalledMethod.Name
                }
            );
        }
    }
}
