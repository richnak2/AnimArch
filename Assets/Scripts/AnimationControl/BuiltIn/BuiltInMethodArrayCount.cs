using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;
using AnimArch.Extensions;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodArrayCount : BuiltInMethodArray
    {
        protected override EXEExecutionResult Evaluate(EXEValueArray owningObject, List<EXEValueBase> parameters)
        {
            int count = owningObject.Elements.Count;

            EXEExecutionResult result = EXEExecutionResult.Success();

            result.ReturnedOutput = new EXEValueInt(count);
            return result;
        }
    }
}