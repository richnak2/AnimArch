using OALProgramControl;
using System.Collections.Generic;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public abstract class BuiltInMethodBase
    {
        public abstract EXEExecutionResult Evaluate(EXEValueBase owningObject, List<EXEValueBase> parameters);
    }
}