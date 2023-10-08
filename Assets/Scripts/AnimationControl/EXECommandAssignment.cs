using System;
using System.Linq;

namespace OALProgramControl
{
    public class EXECommandAssignment : EXECommand
    {
        public String VariableName { get; }
        public String AttributeName { get; }
        public EXEASTNode AssignedExpression { get; }

        public EXECommandAssignment(String VariableName, EXEASTNode AssignedExpression)
        {
            this.VariableName = VariableName;
            this.AttributeName = null;
            this.AssignedExpression = AssignedExpression;
        }
        public EXECommandAssignment(String VariableName, String AttributeName, EXEASTNode AssignedExpression)
        {
            this.VariableName = VariableName;
            this.AttributeName = AttributeName;
            this.AssignedExpression = AssignedExpression;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            String AssignedValue = this.AssignedExpression.Evaluate(SuperScope, OALProgram.ExecutionSpace);
            if (AssignedValue == null)
            {
                return Error("XEC1010", ErrorMessage.FailedExpressionEvaluation(AssignedExpression, this.SuperScope));
            }

            // We find the type of AssignedExpression
            String AssignedType;
            if (this.AssignedExpression.IsReference())
            {
                AssignedType = SuperScope.DetermineVariableType(this.AssignedExpression.AccessChain(), OALProgram.ExecutionSpace);
                if (AssignedType == null)
                {
                    return Error("XEC1011", ErrorMessage.FailedExpressionTypeDetermination(AssignedExpression));
                }
            }
            // It must be primitive, not reference
            else
            {
                AssignedType = EXETypes.DetermineVariableType("", AssignedValue);
                if (AssignedType == null)
                {
                    return Error("XEC1012", ErrorMessage.FailedExpressionTypeDetermination(AssignedValue));
                }
            }

            // If we are assigning to a variable
            if (this.AttributeName == null)
            {
                EXEPrimitiveVariable PrimitiveVariable = SuperScope.FindPrimitiveVariableByName(this.VariableName);
                EXEReferencingVariable ReferencingVariable = SuperScope.FindReferencingVariableByName(this.VariableName);
                EXEReferencingSetVariable SetVariable = SuperScope.FindSetReferencingVariableByName(this.VariableName);

                if (PrimitiveVariable != null)
                {
                    // If PrimitiveVariable exists and its type is UNDEFINED
                    if (EXETypes.UnitializedName.Equals(PrimitiveVariable.Type))
                    {
                        return Error("XEC1013", ErrorMessage.ExistingUndefinedVariable(PrimitiveVariable.Name));
                    }

                    // If we are assigning value of type A to variable of type B
                    if (!Object.Equals(PrimitiveVariable.Type, AssignedType))
                    {
                        return Error("XEC1014", ErrorMessage.InvalidAssignment(AssignedValue, AssignedType, PrimitiveVariable.Name, PrimitiveVariable.Type));
                    }

                    // If the types don't match, this fails and returns false
                    AssignedValue = EXETypes.AdjustAssignedValue(PrimitiveVariable.Type, AssignedValue);
                    EXEExecutionResult Result = PrimitiveVariable.AssignValue("", AssignedValue);
                    Result.OwningCommand = this;
                }
                else if (ReferencingVariable != null)
                {
                    CDClass Class = OALProgram.ExecutionSpace.getClassByName(ReferencingVariable.ClassName);
                    if (Class == null)
                    {
                        return Error("XEC1015", ErrorMessage.ClassNotFound(ReferencingVariable.ClassName, OALProgram));
                    }


                    if (!this.AssignedExpression.IsReference() || !string.Equals(ReferencingVariable.ClassName, AssignedType))
                    {
                        return Error("XEC1016", ErrorMessage.InvalidAssignment(AssignedExpression.ToCode(), AssignedType, ReferencingVariable.Name, ReferencingVariable.ClassName));
                    }


                    if (!EXETypes.IsValidReferenceValue(AssignedValue, Class.Name))
                    {
                        return Error("XEC1017", ErrorMessage.InvalidReference(AssignedExpression.ToCode(), AssignedValue));
                    }

                    long IDValue = long.Parse(AssignedValue);

                    CDClassInstance ClassInstance = Class.GetInstanceByID(IDValue);
                    if (ClassInstance == null)
                    {
                        return Error("XEC1018", ErrorMessage.InstanceNotFound(IDValue, Class));
                    }

                    ReferencingVariable.ReferencedInstanceId = IDValue;
                    return Success();
                }
                else if (SetVariable != null)
                {
                    CDClass Class = OALProgram.ExecutionSpace.getClassByName(SetVariable.ClassName);
                    if (Class == null)
                    {
                        return Error("XEC1019", ErrorMessage.ClassNotFound(SetVariable.ClassName, OALProgram));
                    }

                    if (!this.AssignedExpression.IsReference() || !string.Equals(SetVariable.ClassName, AssignedType))
                    {
                        return Error("XEC1020", ErrorMessage.InvalidAssignment(AssignedExpression.ToCode(), AssignedType, SetVariable.Name, SetVariable.ClassName));
                    }


                    if (!EXETypes.IsValidReferenceValue(AssignedValue, Class.Name))
                    {
                        return Error("XEC1021", ErrorMessage.InvalidReference(AssignedExpression.ToCode(), AssignedValue));
                    }

                    long[] IDs = String.Empty.Equals(AssignedValue) ? new long[] { } : AssignedValue.Split(',').Select(id => long.Parse(id)).ToArray();

                    CDClassInstance ClassInstance;
                    foreach (long ID in IDs)
                    {
                        ClassInstance = Class.GetInstanceByID(ID);
                        if (ClassInstance == null)
                        {
                            return Error("XEC1022", ErrorMessage.InstanceNotFound(ID, Class));
                        }
                    }

                    SetVariable.ClearVariables();

                    foreach (long ID in IDs)
                    {
                        SetVariable.AddReferencingVariable(new EXEReferencingVariable("", Class.Name, ID));
                    }
                    return Success();
                }
                // We must create new Variable, it depends on the type of AssignedExpression
                else
                {
                    // Its type is UNDEFINED
                    if (EXETypes.UnitializedName.Equals(AssignedType))
                    {
                        return Error("XEC1023", ErrorMessage.CreatingUndefinedVariable(VariableName));
                    }
                    else if (EXETypes.IsPrimitive(AssignedType))
                    {
                        // If the types don't match, this fails and returns false
                        AssignedValue = EXETypes.AdjustAssignedValue(AssignedType, AssignedValue);

                        EXEExecutionResult Result = SuperScope.AddVariable(new EXEPrimitiveVariable(this.VariableName, AssignedValue, AssignedType));
                        Result.OwningCommand = this;
                        return Result;
                    }
                    else if ("[]".Equals(AssignedType.Substring(AssignedType.Length - 2, 2)))
                    {
                        string className = AssignedType.Substring(0, AssignedType.Length - 2);
                        CDClass Class = OALProgram.ExecutionSpace.getClassByName(className);
                        if (Class == null)
                        {
                            return Error("XEC1024", ErrorMessage.ClassNotFound(className, OALProgram));
                        }

                        if (!EXETypes.IsValidReferenceValue(AssignedValue, AssignedType))
                        {
                            return Error("XEC1025", ErrorMessage.InvalidReference(this.VariableName, AssignedValue));
                        }

                        long[] IDs = String.Empty.Equals(AssignedValue) ? new long[] { } : AssignedValue.Split(',').Select(id => long.Parse(id)).ToArray();

                        CDClassInstance ClassInstance;
                        foreach (long ID in IDs)
                        {
                            ClassInstance = Class.GetInstanceByID(ID);
                            if (ClassInstance == null)
                            {
                                return Error("XEC1026", ErrorMessage.InstanceNotFound(ID, Class));
                            }
                        }

                        EXEReferencingSetVariable CreatedSetVariable = new EXEReferencingSetVariable(this.VariableName, Class.Name);

                        foreach (long ID in IDs)
                        {
                            CreatedSetVariable.AddReferencingVariable(new EXEReferencingVariable("", Class.Name, ID));
                        }

                        EXEExecutionResult Result = SuperScope.AddVariable(CreatedSetVariable);
                        Result.OwningCommand = this;
                        return Result;
                    }
                    else if (!String.IsNullOrEmpty(AssignedType))
                    {
                        CDClass Class = OALProgram.ExecutionSpace.getClassByName(AssignedType);
                        if (Class == null)
                        {
                            return Error("XEC1027", ErrorMessage.ClassNotFound(AssignedType, OALProgram));
                        }

                        if (!EXETypes.IsValidReferenceValue(AssignedValue, AssignedType))
                        {
                            return Error("XEC1028", ErrorMessage.InvalidReference(this.VariableName, AssignedValue));
                        }

                        long ID = long.Parse(AssignedValue);

                        CDClassInstance ClassInstance = Class.GetInstanceByID(ID);
                        if (ClassInstance == null)
                        {
                            return Error("XEC1029", ErrorMessage.InstanceNotFound(ID, Class));
                        }

                        EXEExecutionResult Result = SuperScope.AddVariable(new EXEReferencingVariable(this.VariableName, Class.Name, ID));
                        Result.OwningCommand = this;
                        return Result;
                    }
                }
            }
            // We are assigning to an attribute of a variable
            else
            {
                EXEReferenceEvaluator RefEvaluator = new EXEReferenceEvaluator();

                EXEExecutionResult Result = RefEvaluator.SetAttributeValue(this.VariableName, this.AttributeName, SuperScope, OALProgram, AssignedValue, AssignedType);
                Result.OwningCommand = this;
                return Result;
            }

            return Success();
        }

        public override String ToCodeSimple()
        {
            String Result = this.VariableName;
            if (this.AttributeName != null)
            {
                Result += "." + this.AttributeName;
            }
            Result += " = " + this.AssignedExpression.ToCode();
            return Result;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandAssignment(VariableName, AttributeName, AssignedExpression);
        }
    }
}
