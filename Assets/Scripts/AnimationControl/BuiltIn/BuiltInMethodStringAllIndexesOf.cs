using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;
using AnimArch.Extensions;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodStringAllIndexesOf : BuiltInMethodString
    {
        public override EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters)
        {
            EXEValueString delimiterValue = parameters[0] as EXEValueString;

            string delimiter = delimiterValue.Value;
            string stringToSplit = owningObject.Value;

            List<int> indices = stringToSplit.AllIndexesOf(delimiter);

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput
                = new EXEValueArray(EXETypes.IntegerTypeName + "[]")
                {
                    Elements = indices
                        .Select(index => new EXEValueInt(index) as EXEValueBase)
                        .ToList()
                };
            return result;
        }
    }
}