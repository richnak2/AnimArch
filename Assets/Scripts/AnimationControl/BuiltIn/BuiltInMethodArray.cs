using OALProgramControl;
using System.Collections.Generic;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public abstract class BuiltInMethodArray : BuiltInMethodBase
    {
        public override EXEExecutionResult Evaluate(EXEValueBase owningObject, List<EXEValueBase> parameters)
        {
            return Evaluate(owningObject as EXEValueArray, parameters);
        }

        protected abstract EXEExecutionResult Evaluate(EXEValueArray owningObject, List<EXEValueBase> parameters);
    }
}