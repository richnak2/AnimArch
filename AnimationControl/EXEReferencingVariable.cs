﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEReferencingVariable
    {
        public String Name { get; }
        public String ClassName { get; }
        public long ReferencedInstanceId { get; set; }

        public EXEReferencingVariable(String Name, String ClassName, long ReferencedInstanceId)
        {
            this.Name = Name;
            this.ClassName = ClassName;
            this.ReferencedInstanceId = ReferencedInstanceId;
        }

        // SetUloh2
        public CDClassInstance RetrieveReferencedClassInstance(CDClassPool ExecutionSpace)
        {
            throw new NotImplementedException();
        }
    }
}
