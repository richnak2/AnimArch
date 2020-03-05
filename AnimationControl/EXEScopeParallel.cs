using System;
using System.Collections.Generic;

namespace AnimationControl
{
    class EXEScopeParallel : EXEScope
    {
        private List<EXEScope> Threads = null;

        public EXEScopeParallel()
        {
            this.Threads = new List<EXEScope>();
        }

        public void AddThread(EXEScope Thread)
        {
            this.Threads.Add(Thread);
        }

        new public Boolean Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            throw new NotImplementedException();
        }
    }
}