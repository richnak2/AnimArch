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

        public EXEReferencingVariable()
        {
            this.Name = "";
            this.ClassName = "";
            this.ReferencedInstanceId = -1;
        }

        public CDClassInstance RetrieveReferencedClassInstance(CDClassPool ExecutionSpace)
        {
            throw new NotImplementedException();
        }
    }
}
