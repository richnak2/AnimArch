using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodStringFirstIndexOf : BuiltInMethodString
    {
        protected override EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters)
        {
            EXEValueString delimiterValue = parameters[0] as EXEValueString;

            string substring = delimiterValue.Value;
            string superString = owningObject.Value;

            int indexOf = superString.IndexOf(substring);

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueInt(indexOf);
            return result;
        }
    }
}