using OALProgramControl;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public abstract class BuiltInMethodString : BuiltInMethodBase
    {
        public override EXEExecutionResult Evaluate(EXEValueBase owningObject, List<EXEValueBase> parameters)
        {
            return Evaluate(owningObject as EXEValueString, parameters);
        }

        protected abstract EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters);
    }
}