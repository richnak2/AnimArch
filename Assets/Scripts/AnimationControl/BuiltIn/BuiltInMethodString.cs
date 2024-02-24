using OALProgramControl;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public abstract class BuiltInMethodString
    {
        public abstract EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters);
    }
}