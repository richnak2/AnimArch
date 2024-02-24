using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodStringReplace : BuiltInMethodString
    {
        public override EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters)
        {
            EXEValueString oldStringValue = parameters[0] as EXEValueString;
            EXEValueString newStringValue = parameters[1] as EXEValueString;

            string oldString = oldStringValue.Value;
            string newString = newStringValue.Value;
            string fullString = owningObject.Value;

            string newFullString = fullString.Replace(oldString, newString);

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueString(string.Format("\"{0}\"", newFullString));
            return result;
        }
    }
}