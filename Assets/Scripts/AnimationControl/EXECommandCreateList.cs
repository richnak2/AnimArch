using System;
using System.Collections.Generic;
using System.Linq;

namespace OALProgramControl
{
    public class EXECommandCreateList : EXECommand
    {
        private String VariableName { get; }
        private String AttributeName { get; }
        private String ClassName { get; }
        private List<EXEASTNode> Items { get; }

        public EXECommandCreateList(String VariableName, String AttributeName, String ClassName, List<EXEASTNode> Items)
        {
            this.VariableName = VariableName;
            this.AttributeName = AttributeName;
            this.ClassName = ClassName;
            this.Items = Items;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            CDClass Class = OALProgram.ExecutionSpace.getClassByName(this.ClassName);
            if (Class == null)
            {
                return Error(ErrorMessage.ClassNotFound(this.ClassName, OALProgram));
            }

            EXEReferencingSetVariable SetVariable = null; // Important if we do not have AttributeName
            CDClassInstance ClassInstance = null; // Important if we have AttributeName

            if (this.AttributeName == null)
            {
                SetVariable = SuperScope.FindSetReferencingVariableByName(this.VariableName);

                if (SetVariable != null)
                {
                    if (!String.Equals(this.ClassName, SetVariable.ClassName))
                    {
                        return Error(ErrorMessage.AssignNewListToVariableHoldingListOfAnotherType(SetVariable.Name, SetVariable.Type, this.ClassName));
                    }

                    SetVariable.ClearVariables();
                }
                else
                {
                    SetVariable = new EXEReferencingSetVariable(this.VariableName, Class.Name);

                    EXEExecutionResult result = SuperScope.AddVariable(SetVariable);
                    result.OwningCommand = this;
                    return result;
                }
            }
            else
            {
                EXEReferencingVariable Variable = SuperScope.FindReferencingVariableByName(this.VariableName);
                if (Variable == null)
                {
                    return Error(ErrorMessage.VariableNotFound(this.VariableName, this.SuperScope));
                }

                CDClass VariableClass = OALProgram.ExecutionSpace.getClassByName(Variable.ClassName);
                if (VariableClass == null)
                {
                    return Error(ErrorMessage.ClassNotFound(Variable.ClassName, OALProgram));
                }

                CDAttribute Attribute = VariableClass.GetAttributeByName(this.AttributeName);
                if (Attribute == null)
                {
                    return Error(ErrorMessage.AttributeNotFoundOnClass(this.AttributeName, VariableClass));
                }
                
                String AttributeClassName = Attribute.Type.Substring(0, Attribute.Type.Length - 2);
                if (!String.Equals(this.ClassName, AttributeClassName))
                {
                    return Error(ErrorMessage.AssignNewListToVariableHoldingListOfAnotherType(VariableName + "." + AttributeName, AttributeClassName, this.ClassName));
                }

                ClassInstance = VariableClass.GetInstanceByID(Variable.ReferencedInstanceId);
                if (ClassInstance == null)
                {
                    return Error(ErrorMessage.InstanceNotFound(Variable.ReferencedInstanceId, VariableClass));
                }

                EXEExecutionResult result = ClassInstance.SetAttribute(this.AttributeName, "");
                result.OwningCommand = this;
                return result;
            }

            if (this.Items.Any())
            {
                String Result = "";

                foreach (EXEASTNode item in this.Items)
                {
                    string addedItemClassName = this.SuperScope.DetermineVariableType(item.AccessChain(), OALProgram.ExecutionSpace);

                    if
                    (
                        
                        !(
                            item.IsReference()
                            &&
                            Object.Equals(Class.Name, addedItemClassName)    
                        )
                    )
                    {
                        return Error(ErrorMessage.AddInvalidValueToList(VariableName + (AttributeName == null ? string.Empty : ("." + AttributeName)), Class.Name, item.ToCode(), addedItemClassName));
                    }

                    String IDValue = item.Evaluate(SuperScope, OALProgram.ExecutionSpace);

                    if (!EXETypes.IsValidReferenceValue(IDValue, Class.Name))
                    {
                        return Error(ErrorMessage.InvalidValueForType(Class.Name, IDValue));
                    }

                    long ID = long.Parse(IDValue);

                    CDClassInstance Instance = Class.GetInstanceByID(ID);
                    if (Instance == null)
                    {
                        return Error(ErrorMessage.InstanceNotFound(ID, Class));
                    }

                    if (this.AttributeName == null)
                    {
                        SetVariable.AddReferencingVariable(new EXEReferencingVariable("", Class.Name, ID));
                    }
                    else
                    {
                        Result += IDValue + ",";
                    }
                }

                if (this.AttributeName != null)
                {
                    Result = Result.Remove(Result.Length - 1, 1); // Remove last ","
                    ClassInstance.SetAttribute(this.AttributeName, Result);
                }
            }
            
            return Success();
        }

        public override string ToCodeSimple()
        {
            String Result = "create list " + (this.AttributeName == null ? this.VariableName : (this.VariableName + "." + this.AttributeName))
                + " of " + this.ClassName;

            if (this.Items.Any())
            {
                Result += " { " + this.Items[0].ToCode();

                for (int i = 1; i < this.Items.Count; i++)
                {
                    Result += ", " + this.Items[i].ToCode();
                }
                Result += " }";
            }
            
            return Result;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandCreateList(VariableName, AttributeName, ClassName, Items);
        }
    }
}
