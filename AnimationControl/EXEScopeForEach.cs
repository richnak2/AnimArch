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
        public override Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Boolean Success = this.Execute(Animation, Scope);
            return Success;
        }
        public override Boolean Execute(Animation Animation, EXEScope Scope)
        {
            this.Animation = Animation;
            Animation.AccessInstanceDatabase();

            EXEReferencingVariable IteratorVariable = Scope.FindReferencingVariableByName(this.IteratorName);
            EXEReferencingSetVariable IterableVariable = Scope.FindSetReferencingVariableByName(this.IterableName);

            Boolean Success = true;

            // We cannot iterate over not existing reference set
            if (Success & IterableVariable == null)
            {
                Success = false;
            }

            // If iterator already exists and its class does not match the iterable class, we cannot do this
            if (Success & IteratorVariable != null && !IteratorVariable.ClassName.Equals(IterableVariable.ClassName))
            {
                Success = false;
            }

            // If iterator name is already taken for another variable, we quit again. Otherwise we create the iterator variable
            if (Success & IteratorVariable == null)
            {
                Boolean IteratorCreationSuccess = Scope.AddVariable(new EXEReferencingVariable(this.IteratorName, IterableVariable.ClassName, -1));
                if (!IteratorCreationSuccess)
                {
                    Success = false;
                }
            }

            if (Success)
            {
                foreach (EXEReferencingVariable CurrentItem in IterableVariable.GetReferencingVariables())
                {
                    IteratorVariable.ReferencedInstanceId = CurrentItem.ReferencedInstanceId;

                    foreach (EXECommand Command in this.Commands)
                    {
                        Success = Command.SynchronizedExecute(Animation, this);
                        if (!Success)
                        {
                            break;
                        }
                    }
                }
            }
            Animation.LeaveInstanceDatabase();

            return Success;
        }
    }
}
