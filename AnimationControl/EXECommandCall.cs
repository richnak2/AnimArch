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
        new public Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Boolean Success = this.Execute(Animation, Scope);
            return Success;
        }

        new public Boolean Execute(Animation Animation, EXEScope Scope)
        {
            //Animation.Visualizer.UnHighlightClass(this.CallerClass);
            Animation.Visualizer.HighlightClass(this.CallerClass);

            Thread.Sleep( (int) (Animation.HighlightDuration/3.0d * 1000));

            Animation.Visualizer.HighlightRelationship(this.RelationshipName);
            Animation.Visualizer.UnHighlightClass(this.CallerClass);

            Thread.Sleep((int)(Animation.HighlightDuration / 3.0d * 1000));

            Animation.Visualizer.HighlightClass(this.CalledClass);
            Animation.Visualizer.UnHighlightRelationship(this.RelationshipName);

            Thread.Sleep((int)(Animation.HighlightDuration / 3.0d * 1000));

            Animation.Visualizer.UnHighlightClass(this.CalledClass);

            return true;
        }
    }
}
