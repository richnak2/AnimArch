using System;
using System.Collections.Generic;
using System.Linq;

namespace OALProgramControl
{
    public class EXECommandRemovingFromList : EXECommand
    {
        private String VariableName { get; }
        private String AttributeName { get; }
        private EXEASTNode Item { get; }

        public EXECommandRemovingFromList(String VariableName, String AttributeName, EXEASTNode Item)
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
                if (!"[]".Equals(Attribute.Type.Substring(Attribute.Type.Length - 2, 2)))
                {
                    return Error(ErrorMessage.RemovingFromNotList(this.VariableName + "." + this.AttributeName, Attribute.Type));
                }

                SetVariableClassName = Attribute.Type.Substring(0, Attribute.Type.Length - 2);

                ClassInstance = VariableClass.GetInstanceByID(Variable.ReferencedInstanceId);
                if (ClassInstance == null)
                {
                    return Error(ErrorMessage.InstanceNotFound(Variable.ReferencedInstanceId, VariableClass));
                }
            }

            // We need to compare class types
            string itemType = this.SuperScope.DetermineVariableType(this.Item.AccessChain(), OALProgram.ExecutionSpace);
            if
            (
                !(
                    this.Item.IsReference()
                    &&
                    Object.Equals(SetVariableClassName, itemType)
                )
            )
            {
                return Error(ErrorMessage.RemovingFromInvalidTypeList(itemType, SetVariableClassName));
            }

            String IDValue = this.Item.Evaluate(SuperScope, OALProgram.ExecutionSpace);

            if (!EXETypes.IsValidReferenceValue(IDValue, SetVariableClassName))
            {
                return Error(ErrorMessage.InvalidReference(Item.ToCode(), IDValue));
            }

            long ItemInstanceID = long.Parse(IDValue);

            CDClass SetVariableClass = OALProgram.ExecutionSpace.getClassByName(SetVariableClassName);
            if (SetVariableClass == null)
            {
                return Error(ErrorMessage.ClassNotFound(SetVariableClassName, OALProgram));
            }

            CDClassInstance Instance = SetVariableClass.GetInstanceByID(ItemInstanceID);
            if (Instance == null)
            {
                return Error(ErrorMessage.InstanceNotFound(ItemInstanceID, SetVariableClass));
            }

            if (this.AttributeName == null)
            {
                SetVariable.UnsetVariables(ItemInstanceID);
            }
            else
            {
                String Values = ClassInstance.GetAttributeValue(this.AttributeName);

                if (Values.Length > 0 && !EXETypes.UnitializedName.Equals(Values))
                {
                    List<String> IDs = Values.Split(',').ToList();
                    IDs.RemoveAll(id => id == ItemInstanceID.ToString());
                    Values = String.Join(",", IDs);

                    ClassInstance.SetAttribute(this.AttributeName, Values);
                }
            }

            return Success();
        }

        public override string ToCodeSimple()
        {
            return "remove " + this.Item.ToCode()
                + " from " + (this.AttributeName == null ? this.VariableName : (this.VariableName + "." + this.AttributeName));
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandRemovingFromList(VariableName, AttributeName, Item);
        }
    }
}
