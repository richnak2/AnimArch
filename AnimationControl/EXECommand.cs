using System;

namespace AnimationControl
{
    public abstract class EXECommand
    {
        public virtual Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Animation.AccessInstanceDatabase();
            Boolean Success = this.Execute(Animation, Scope);
            Animation.LeaveInstanceDatabase();
            return Success;
        }
        public abstract Boolean Execute(Animation Animation, EXEScope Scope);
    }
}
