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
            if (callerMethod == null)
            {
                throw new ArgumentNullException("callerMethod");
            }

            if (calledMethod == null)
            {
                throw new ArgumentNullException("calledMethod");
            }

            if (callerObject == null)
            {
                throw new ArgumentNullException("callerObject");
            }

            if (calledObject == null)
            {
                throw new ArgumentNullException("calledObject");
            }

            this.CallerMethod = callerMethod;
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
                    this.CallerMethod?.OwningClass?.Name ?? "NULL",
                    this.CallerMethod?.Name ?? "NULL",
                    this.Relation?.RelationshipName ?? "NULL",
                    this.CalledMethod?.OwningClass?.Name ?? "NULL",
                    this.CalledMethod?.Name ?? "NULL"
                }
            );
        }
    }
}
