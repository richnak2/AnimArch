using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXECommandCall : EXECommand
    {
        public String CallerClass { get; }
        public String CalledClass { get; }
        public String CallerMethod { get; }
        public String CalledMethod { get; }
        public String RelationshipName { get; }

        public EXECommandCall(String CallerClass, String CallerMethod, String RelationshipName, String CalledClass, String CalledMethod)
        {
            this.CallerClass = CallerClass;
            this.CallerMethod = CallerMethod;
            this.RelationshipName = RelationshipName;
            this.CalledClass = CalledClass;
            this.CalledMethod = CalledMethod;
        }
        public override Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Boolean Success = this.Execute(Animation, Scope);
            return Success;
        }

        public override Boolean Execute(Animation Animation, EXEScope Scope)
        {
            Animation.RequestNextStep();

            return true;
        }
    }
}
