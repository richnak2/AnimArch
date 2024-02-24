using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodStringLength : BuiltInMethodString
    {
        public override EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters)
        {
            string stringValue = owningObject.Value;

            int length = stringValue.Length;

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueInt(length);
            return result;
        }
    }
}