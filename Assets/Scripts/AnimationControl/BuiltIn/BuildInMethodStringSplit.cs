using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodStringSplit : BuiltInMethodString
    {
        public override EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters)
        {
            EXEValueString delimiterValue = parameters[0] as EXEValueString;

            string delimiter = delimiterValue.Value;
            string stringToSplit = owningObject.Value;

            string[] splitTokens = stringToSplit.Split(new[] { delimiter }, StringSplitOptions.None);

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput
                = new EXEValueArray(EXETypes.StringTypeName + "[]")
                {
                    Elements = splitTokens
                        .Select(token => new EXEValueString(string.Format("\"{0}\"", token)) as EXEValueBase)
                        .ToList()
                };
            return result;
        }
    }
}