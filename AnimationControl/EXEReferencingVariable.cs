using System;
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

        // SetUloh1
        public CDClassInstance RetrieveReferencedClassInstance(CDClassPool ExecutionSpace)
        {
            foreach(CDClass Class in ExecutionSpace.ClassPool)
            {
                if (String.Equals(Class.Name, this.ClassName)) {
                    foreach (CDClassInstance ClassInstance in Class.Instances)
                    {
                        if (ClassInstance.UniqueID == this.ReferencedInstanceId)  return ClassInstance;
                    }
                }
            }

            return null;
        }
    }
}
