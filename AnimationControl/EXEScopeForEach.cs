using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXEScopeForEach : EXEScope
    {
        public String IteratorName { get; set; }
        public String IterableName { get; set; }

        public EXEScopeForEach(String Iterator, String Iterable)
        {
            this.IteratorName = Iterator;
            this.IterableName = Iterable;
        }
        new public Boolean Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            EXEReferencingVariable IteratorVariable = Scope.FindReferencingVariableByName(this.IteratorName);
            EXEReferencingSetVariable IterableVariable = Scope.FindSetReferencingVariableByName(this.IterableName);

            // We cannot iterate over not existing reference set
            if (IterableVariable == null)
            {
                return false;
            }

            // If iterator already exists and its class does not match the iterable class, we cannot do this
            if (IteratorVariable != null && !IteratorVariable.ClassName.Equals(IterableVariable.ClassName))
            {
                return false;
            }

            // If iterator name is already taken for another variable, we quit again. Otherwise we create the iterator variable
            if (IteratorVariable == null)
            {
                Boolean IteratorCreationSuccess = Scope.AddVariable(new EXEReferencingVariable(this.IteratorName, IterableVariable.ClassName, -1));
                if (!IteratorCreationSuccess)
                {
                    return false;
                }
            }


            Boolean Success = true;
            foreach (EXEReferencingVariable CurrentItem in IterableVariable.GetReferencingVariables())
            {
                IteratorVariable.ReferencedInstanceId = CurrentItem.ReferencedInstanceId;

                Success = base.Execute(ExecutionSpace, RelationshipSpace, this);
                if (!Success)
                {
                    break;
                }
            }

            return Success;
        }
    }
}
