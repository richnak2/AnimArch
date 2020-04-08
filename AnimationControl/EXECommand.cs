using System;

namespace AnimationControl
{
    public abstract class EXECommand
    {
        public virtual Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Animation.AccessInstanceDatabase();
            Console.WriteLine(this.ToCode());
            Boolean Success = this.Execute(Animation, Scope);
            Console.WriteLine("Done");
            Animation.LeaveInstanceDatabase();
            return Success;
        }
        public virtual Boolean IsComposite()
        {
            return false;
        }
        public abstract Boolean Execute(Animation Animation, EXEScope Scope);

        public virtual String ToCode(String Indent = "")
        {
            return Indent + ToCodeSimple() + "\n";
        }
        public virtual String ToCodeSimple()
        {
            return "Command";
        }
    }
}
