using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodStringSubstring : BuiltInMethodString
    {
        protected override EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters)
        {
            EXEValueInt startingIndexValue = parameters[0] as EXEValueInt;
            EXEValueInt lengthValue = parameters[1] as EXEValueInt;

            int startingIndex = (int)startingIndexValue.Value;
            int length = (int)lengthValue.Value;
            string stringValue = owningObject.Value;

            string substring = string.Format("\"{0}\"", stringValue.Substring(startingIndex, length));

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueString(substring);
            return result;
        }
    }
}