using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodStringSubstringFrom : BuiltInMethodString
    {
        public override EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters)
        {
            EXEValueInt startingIndexValue = parameters[0] as EXEValueInt;

            int startingIndex = (int)startingIndexValue.Value;
            string stringValue = owningObject.Value;

            string substring = string.Format("\"{0}\"", stringValue.Substring(startingIndex));

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueString(substring);
            return result;
        }
    }
}