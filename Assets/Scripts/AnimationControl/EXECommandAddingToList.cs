using System;
using System.Collections;
using System.Collections.Generic;

namespace OALProgramControl
{
    public class EXECommandAddingToList : EXECommand
    {
        public String VariableName { get; private set; }
        public String AttributeName { get; private set; }
        public EXEASTNode Item { get; private set; }

        public EXECommandAddingToList(String VariableName, String AttributeName, EXEASTNode Item)
        {
            this.VariableName = VariableName;
            this.AttributeName = AttributeName;
            this.Item = Item;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            String SetVariableClassName;
            EXEReferencingSetVariable SetVariable = null; // This is important if we do not have AttributeName
            CDClassInstance ClassInstance = null; // This is important if we have AttributeName

            if (this.AttributeName == null)
            {
                // If we do not have AttributeName, VariableName must be set variable reference
                SetVariable = SuperScope.FindSetReferencingVariableByName(this.VariableName);
                if (SetVariable == null)
                {
                    return Error(ErrorMessage.VariableNotFound(this.VariableName, this.SuperScope));
                }

                SetVariableClassName = SetVariable.ClassName;
            }
            else
            {
                // If we have AttributeName, VariableName must be reference (not set variable)
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

                // We need to check if it is list
                if (Attribute.Type.Length < 2 || !"[]".Equals(Attribute.Type.Substring(Attribute.Type.Length - 2, 2)))
                {
                    return Error(ErrorMessage.AddingToNotList(this.VariableName + "." + this.AttributeName, Attribute.Type));
                }

                SetVariableClassName = Attribute.Type.Substring(0, Attribute.Type.Length - 2);

                ClassInstance = VariableClass.GetInstanceByID(Variable.ReferencedInstanceId);
                if (ClassInstance == null)
                {
                    return Error(ErrorMessage.InstanceNotFound(Variable.ReferencedInstanceId, VariableClass));
                }
            }

            // We need to compare class types
            string listType = this.SuperScope.DetermineVariableType(this.Item.AccessChain(), OALProgram.ExecutionSpace);

            if (!this.Item.IsReference() || !Object.Equals(SetVariableClassName, listType))
            {
                return Error(ErrorMessage.AddingToInvalidTypeList(SetVariableClassName, listType));
            }
            
            String IDValue = this.Item.Evaluate(SuperScope, OALProgram.ExecutionSpace);

            if (!EXETypes.IsValidReferenceValue(IDValue, SetVariableClassName))
            {
                return Error
                    (
                        ErrorMessage.IsNotReference
                        (
                            string.Join(".", this.Item.AccessChain()),
                            EXETypes.DetermineVariableType
                            (
                                string.Join(".", this.Item.AccessChain()),
                                this.Item.Evaluate(this.SuperScope, OALProgram.ExecutionSpace)
                            )
                        )
                    );
            }

            long ItemInstanceID = long.Parse(IDValue);

            CDClass SetVariableClass = OALProgram.ExecutionSpace.getClassByName(SetVariableClassName);
            if (SetVariableClass == null)
            {
                return Error(ErrorMessage.ClassNotFound(SetVariableClassName, OALProgram));
            }

            CDClassInstance Instance = SetVariableClass.GetInstanceByIDRecursiveDownward(ItemInstanceID);
            if (Instance == null)
            {
                return Error(ErrorMessage.InstanceNotFound(ItemInstanceID, SetVariableClass));
            }


            if (this.AttributeName == null)
            {
                SetVariable.AddReferencingVariable(new EXEReferencingVariable("", SetVariableClassName, ItemInstanceID));
            }
            else
            {
                String Values = ClassInstance.GetAttributeValue(this.AttributeName);

                if (Values.Length > 0 && !EXETypes.UnitializedName.Equals(Values))
                {
                    Values += "," + ItemInstanceID.ToString();
                    ClassInstance.SetAttribute(this.AttributeName, Values);
                }
                else
                {
                    ClassInstance.SetAttribute(this.AttributeName, ItemInstanceID.ToString());
                }
            }

            return Success();
        }

        public override string ToCodeSimple()
        {
            return "add " + this.Item.ToCode()
                + " to " + (this.AttributeName == null ? this.VariableName : (this.VariableName + "." + this.AttributeName));
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandAddingToList(VariableName, AttributeName, Item);
        }
    }
}
