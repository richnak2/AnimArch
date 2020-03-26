using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimationControl
{
    public class EXEReferencingSetVariable : EXEReferenceHandle
    {
        private List<EXEReferencingVariable> ReferencingVariables;
        public EXEReferencingSetVariable(String Name, String ClassName) : base(Name, ClassName)
        {
            this.ReferencingVariables = new List<EXEReferencingVariable>(); ;
        }

        public void AddReferencingVariable(EXEReferencingVariable NewReferencingVariable)
        {
            this.ReferencingVariables.Add(NewReferencingVariable);
        }

        public List<EXEReferencingVariable> GetReferencingVariables()
        {
            return this.ReferencingVariables;
        }

        public override List<long> GetReferencedIds()
        {
            return this.ReferencingVariables.Select(x => { return x.ReferencedInstanceId; }).ToList().FindAll(x => x >= 0).ToList();
        }
    }
}
