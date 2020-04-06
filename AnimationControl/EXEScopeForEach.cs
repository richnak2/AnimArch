using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEScopeForEach : EXEScope
    {
        public String IteratorName { get; set; }
        public String IterableName { get; set; }

        public EXEScopeForEach(String Iterator, String Iterable)  : base()
        {
            this.IteratorName = Iterator;
            this.IterableName = Iterable;
        }
        public EXEScopeForEach(EXEScope SuperScope, EXECommand[] Commands, String Iterator, String Iterable) : base(SuperScope, Commands)
        {
            this.IteratorName = Iterator;
            this.IterableName = Iterable;
        }
        public override Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Boolean Success = this.Execute(Animation, this);
            return Success;
        }
        public override Boolean Execute(Animation Animation, EXEScope Scope)
        {
            this.Animation = Animation;

            Animation.AccessInstanceDatabase();
            EXEReferencingVariable IteratorVariable = this.FindReferencingVariableByName(this.IteratorName);
            EXEReferencingSetVariable IterableVariable = this.FindSetReferencingVariableByName(this.IterableName);
            Animation.LeaveInstanceDatabase();

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
                IteratorVariable = new EXEReferencingVariable(this.IteratorName, IterableVariable.ClassName, -1);
                Success = this.GetSuperScope().AddVariable(IteratorVariable);
            }

            if (Success)
            {
                foreach (EXEReferencingVariable CurrentItem in IterableVariable.GetReferencingVariables())
                {
                    //!!NON-RECURSIVE!!
                    this.ClearVariables();

                    IteratorVariable.ReferencedInstanceId = CurrentItem.ReferencedInstanceId;

                    Console.WriteLine("ForEach: " + CurrentItem.ReferencedInstanceId);

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
            

            return Success;
        }
    }
}
