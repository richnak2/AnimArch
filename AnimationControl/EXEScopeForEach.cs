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
            Boolean Success = false;

            //this.ReferencingVariables.Add(this.Iterator);

            throw new NotImplementedException();
            /*foreach (EXEReferencingVariable CurrentItem in this.Iterable.GetReferencingVariables())
            {
                this.Iterator.ReferencedInstanceId = CurrentItem.ReferencedInstanceId;

                Success = base.Execute(ExecutionSpace, RelationshipSpace, this);
                if (!Success)
                {
                    break;
                }
            }

            return Success;*/
        }
    }
}
