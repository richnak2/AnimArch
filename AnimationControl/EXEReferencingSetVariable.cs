using System;
using System.Collections.Generic;

namespace AnimationControl
{
    public class EXEReferencingSetVariable
    {
        public String Name { get; }
        public String ClassName { get; }
        private List<EXEReferencingVariable> ReferencingVariables;

        public EXEReferencingSetVariable(String Name, String Class)
        {
            this.Name = Name;
            this.ClassName = Class;
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
    }
}
