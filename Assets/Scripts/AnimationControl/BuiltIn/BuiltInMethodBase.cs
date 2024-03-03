using OALProgramControl;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public abstract class BuiltInMethodBase
    {
        public abstract EXEExecutionResult Evaluate(EXEValueBase owningObject, List<EXEValueBase> parameters);
        public BuiltInMethodBase Clone()
        {
            return (BuiltInMethodBase) Activator.CreateInstance(this.GetType());
        }
    }
}