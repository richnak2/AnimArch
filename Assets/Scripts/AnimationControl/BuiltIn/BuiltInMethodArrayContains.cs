using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;
using AnimArch.Extensions;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodArrayContains : BuiltInMethodArray, IReturnCollector
    {
        private EXEValueBase ReturnedValue; // Only used if .Equals method is applied
        private int CurrentlyEvaluatedIndex;

        public BuiltInMethodArrayContains()
        {
            this.ReturnedValue = null;
            this.CurrentlyEvaluatedIndex = -1;
        }

        public void CollectReturn(EXEValueBase returnedValue)
        {
            this.ReturnedValue = returnedValue;
        }

        public MethodInvocatorInfo GetOriginatorData()
        {
            return null;
        }

        protected override EXEExecutionResult Evaluate(EXEValueArray owningObject, List<EXEValueBase> parameters)
        {
            // '.Equals' has been invoked, let us collect the output
            if (this.ReturnedValue != null)
            {
                if (this.ReturnedValue is not EXEValueBool)
                {
                    return EXEExecutionResult.Error(string.Format("Equals method of '{0}' returned '{1}' (it needs to return a bool)", owningObject.TypeName, this.ReturnedValue.TypeName), "XEC2052");
                }

                if ((this.ReturnedValue as EXEValueBool).Value)
                {
                    EXEExecutionResult resultB = EXEExecutionResult.Success();
                    resultB.ReturnedOutput = this.ReturnedValue;
                    return resultB;
                }
            }

            CurrentlyEvaluatedIndex++;

            if (CurrentlyEvaluatedIndex >= owningObject.Elements.Count)
            {
                EXEExecutionResult resultB = EXEExecutionResult.Success();
                resultB.ReturnedOutput = new EXEValueBool(false);
                return resultB;
            }

            EXEValueBase searchedElement = parameters[0];
            EXEExecutionResult result = EXEExecutionResult.Success();

            // Invoke '.Equals' method.
            if
            (
                searchedElement is EXEValueReference
                && (searchedElement as EXEValueReference).TypeClass.MethodExists("Equals", true)
            )
            {
                EXEScopeMethod methodCode = (searchedElement as EXEValueReference).TypeClass.GetMethodByName("Equals", true).ExecutableCode;

                EXEVariable selfVar = new EXEVariable("self", owningObject.Elements[CurrentlyEvaluatedIndex]);
                EXEExecutionResult selfVarAddSuccess = methodCode.AddVariable(selfVar);
                if (!selfVarAddSuccess.IsSuccess)
                {
                    return selfVarAddSuccess;
                }

                EXEVariable param = new EXEVariable("obj", searchedElement);
                EXEExecutionResult paramVarAddSuccess = methodCode.AddParameterVariable(param);
                if (!paramVarAddSuccess.IsSuccess)
                {
                    return paramVarAddSuccess;
                }

                methodCode.MethodCallOrigin = this;
                result.PendingCommand = methodCode;
            }
            // Apply '==' operator.
            else
            {
                bool contains
                    = owningObject.Elements
                        .Any(element => (searchedElement.IsEqualTo(element).ReturnedOutput as EXEValueBool).Value);

                result.ReturnedOutput = new EXEValueBool(contains);
            }

            return result;
        }
    }
}