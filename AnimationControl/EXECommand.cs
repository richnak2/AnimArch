using System;

namespace AnimationControl
{
    public class EXECommand
    {
        public String GetCode()
        {
            //return this.OALCode;
            throw new NotImplementedException();
        }
        public String PrintSelf(Boolean IsTopLevel)
        {
            //return this.OALCode;
            throw new NotImplementedException();
        }
        public void PrintAST()
        {
            //this.AST.PrintPretty("", false);
            throw new NotImplementedException();
        }

        public Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Animation.AccessInstanceDatabase();
            Boolean Success = this.Execute(Animation, Scope);
            Animation.LeaveInstanceDatabase();
            return Success;
        }

        public Boolean Execute(Animation Animation, EXEScope Scope)
        {
            throw new NotImplementedException();
        }
    }
}
