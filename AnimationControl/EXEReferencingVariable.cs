using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEReferencingVariable : EXEReferenceHandle
    {
        public long ReferencedInstanceId { get; set; }

        public EXEReferencingVariable(String Name, String ClassName, long ReferencedInstanceId) : base(Name, ClassName)
        {
            this.ReferencedInstanceId = ReferencedInstanceId;
        }

        public bool IsInitialized()
        {
            return this.ReferencedInstanceId >= 0;
        }

        // SetUloh1
        public CDClassInstance RetrieveReferencedClassInstance(CDClassPool ExecutionSpace)
        {
            CDClass Class = ExecutionSpace.getClassByName(this.ClassName);
            if (Class == null)
            {
                return null;
            }
            CDClassInstance Instance = Class.GetInstanceByID(this.ReferencedInstanceId);

            return Instance;
        }
        new public List<long> GetReferencedIds()
        {
            return new List<long>(new long[] { ReferencedInstanceId }).FindAll(x => x >= 0).ToList();
        }
    }
}
