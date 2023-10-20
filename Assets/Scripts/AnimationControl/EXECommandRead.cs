using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Visualization.UI;

namespace OALProgramControl
{
    public class EXECommandRead : EXECommand
    {
        private String VariableName { get; }
        private String AttributeName { get; }
        private String ReadType { get; }
        private EXEASTNodeBase Prompt { get; }  // Must be String type

        public EXECommandRead(String VariableName, String AttributeName, String ReadType, EXEASTNodeBase Prompt)
        {
            this.VariableName = VariableName;
            this.AttributeName = AttributeName;
            this.ReadType = ReadType;
            this.Prompt = Prompt;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            String Result = "";

            if (this.Prompt != null)
            {
                Result = this.Prompt.Evaluate(SuperScope, OALProgram.ExecutionSpace);
                if (Result == null)
                {
                    return Error("XEC1123", ErrorMessage.FailedExpressionEvaluation(Prompt, this.SuperScope));
                }

                String ResultType = EXETypes.DetermineVariableType("", Result);

                // We need String otherwise this fails
                if (EXETypes.StringTypeName.Equals(ResultType))
                {
                    // Remove double quotes
                    Result = Result.Replace("\"", "");
                }
                else if (!EXETypes.UnitializedName.Equals(ResultType))
                {
                    return Error("XEC1124", ErrorMessage.InvalidValueForType(ResultType, EXETypes.StringTypeName));
                }
            }

            ConsolePanel.Instance.YieldOutput(Result);

            return Success();
        }

        public EXEExecutionResult AssignReadValue(String Value, OALProgram OALProgram)
        {
            String ValueType;

            if (this.ReadType.Contains("int"))
            {
                ValueType = EXETypes.IntegerTypeName;

                if (!int.TryParse(Value, out _))
                {
                    return Error("XEC1125", ErrorMessage.InvalidValueForType(Value, EXETypes.IntegerTypeName));
                }
            }
            else if (this.ReadType.Contains("real"))
            {
                ValueType = EXETypes.RealTypeName;

                try
                {
                    decimal.Parse(Value, CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    return Error("XEC1126", ErrorMessage.InvalidValueForType(Value, EXETypes.RealTypeName));
                }
            }
            else if (this.ReadType.Contains("bool"))
            {
                ValueType = EXETypes.BooleanTypeName;

                if (!EXETypes.BooleanTrue.Equals(Value) && !EXETypes.BooleanFalse.Equals(Value))
                {
                    return Error("XEC1127", ErrorMessage.InvalidValueForType(Value, EXETypes.BooleanTypeName));
                }
            }
            // It must be String
            else
            {
                ValueType = EXETypes.StringTypeName;
                Value = '\"' + Value + '\"';
            }

            if (this.AttributeName == null)
            {
                EXEPrimitiveVariable PrimitiveVariable = this.SuperScope.FindPrimitiveVariableByName(this.VariableName);

                if (PrimitiveVariable != null)
                {
                    // If PrimitiveVariable exists and its type is UNDEFINED
                    if (EXETypes.UnitializedName.Equals(PrimitiveVariable.Type))
                    {
                        return Error("XEC1128", ErrorMessage.ExistingUndefinedVariable(PrimitiveVariable.Name));
                    }

                    // We need to compare primitive types
                    if (!Object.Equals(PrimitiveVariable.Type, ValueType))
                    {
                        return Error("XEC1129", ErrorMessage.InvalidAssignment(Value, ValueType, PrimitiveVariable.Name, PrimitiveVariable.Type));
                    }

                    // If the types don't match, this fails and returns false
                    Value = EXETypes.AdjustAssignedValue(PrimitiveVariable.Type, Value);

                    EXEExecutionResult assignmentResult =  PrimitiveVariable.AssignValue("", Value);
                    assignmentResult.OwningCommand = this;
                    return assignmentResult;
                }
                // We must create new Variable, it depends on the type of ValueType
                else
                {
                    // If the types don't match, this fails and returns false
                    Value = EXETypes.AdjustAssignedValue(ValueType, Value);
                    
                    EXEExecutionResult addVariableResult = SuperScope.AddVariable(new EXEPrimitiveVariable(this.VariableName, Value, ValueType));
                    addVariableResult.OwningCommand = this;
                    return addVariableResult;
                }        
            }
            else
            {
                EXEReferenceEvaluator RefEvaluator = new EXEReferenceEvaluator();
                
                EXEExecutionResult setAttributeResult = RefEvaluator.SetAttributeValue(this.VariableName, this.AttributeName, SuperScope, OALProgram, Value, ValueType);
                setAttributeResult.OwningCommand = this;
                return setAttributeResult;
            }
        }

        public override String ToCodeSimple()
        {
            return (this.AttributeName == null ? this.VariableName : (this.VariableName + "." + this.AttributeName))
                + " = " + this.ReadType + (this.Prompt != null ? this.Prompt.ToCode() : "") + (this.ReadType.Equals("read(") ? ")" : "))");
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandRead(VariableName, AttributeName, ReadType, Prompt);
        }
    }
}
