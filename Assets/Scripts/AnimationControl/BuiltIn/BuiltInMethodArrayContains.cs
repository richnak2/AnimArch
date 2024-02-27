using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;
using AnimArch.Extensions;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodArrayContains : BuiltInMethodArray
    {
        protected override EXEExecutionResult Evaluate(EXEValueArray owningObject, List<EXEValueBase> parameters)
        {
            EXEValueBase searchedElement = parameters[0];

            bool contains
                = owningObject.Elements
                    .Any(element => (searchedElement.IsEqualTo(element).ReturnedOutput as EXEValueBool).Value);

            EXEExecutionResult result = EXEExecutionResult.Success();

            result.ReturnedOutput = new EXEValueBool(contains);
            return result;
        }
    }
}