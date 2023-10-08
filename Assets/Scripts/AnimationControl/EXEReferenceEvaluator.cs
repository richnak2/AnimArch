using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{
    public class EXEReferenceEvaluator
    {
        public String EvaluateAttributeValue(String ReferencingVariableName, String AttributeName, EXEScope Scope, CDClassPool ExecutionSpace)
        {
            EXEReferencingVariable ReferencingVariable = Scope.FindReferencingVariableByName(ReferencingVariableName);
            if (ReferencingVariable == null)
            {
                return null;
            }
            CDClassInstance ClassInstance = ExecutionSpace.GetClassInstanceById(ReferencingVariable.ClassName, ReferencingVariable.ReferencedInstanceId);
            if (ClassInstance == null)
            {
                return null;
            }
            return ClassInstance.GetAttributeValue(AttributeName);
        }

        public EXEExecutionResult SetAttributeValue(String ReferencingVariableName, String AttributeName, EXEScope Scope, OALProgram OALProgram, String NewValue, String NewValueType)
        {
            EXEReferencingVariable ReferencingVariable = Scope.FindReferencingVariableByName(ReferencingVariableName);
            if (ReferencingVariable == null)
            {
                return EXEExecutionResult.Error(ErrorMessage.VariableNotFound(ReferencingVariableName, Scope));
            }

            CDClass Class = OALProgram.ExecutionSpace.getClassByName(ReferencingVariable.ClassName);
            if (Class == null)
            {
                return EXEExecutionResult.Error(ErrorMessage.ClassNotFound(ReferencingVariable.ClassName, OALProgram));
            }

            CDClassInstance ClassInstance = Class.GetInstanceByID(ReferencingVariable.ReferencedInstanceId);
            if (ClassInstance == null)
            {
                return EXEExecutionResult.Error(ErrorMessage.InstanceNotFound(ReferencingVariable.ReferencedInstanceId, Class));
            }

            //TODO: Typ attributu nemoze byt ReferenceTypeName alebo UnitializedTypeName ci ?
            CDAttribute Attribute = Class.GetAttributeByName(AttributeName);
            if (Attribute == null)
            {
                return EXEExecutionResult.Error(ErrorMessage.AttributeNotFoundOnClass(AttributeName, Class));
            }

            if (!EXETypes.CanBeAssignedToAttribute(AttributeName, Attribute.Type, NewValueType))
            {
                return EXEExecutionResult.Error(ErrorMessage.InvalidAssignment(NewValue, NewValueType, ReferencingVariableName + "." + AttributeName, Attribute.Type));
            }

            if (EXETypes.IsPrimitive(Attribute.Type))
            {
                if (!EXETypes.IsValidValue(NewValue, Attribute.Type))
                {
                    return EXEExecutionResult.Error(ErrorMessage.InvalidValueForType(NewValue, Attribute.Type));
                }

                return ClassInstance.SetAttribute(AttributeName, EXETypes.AdjustAssignedValue(Attribute.Type, NewValue));
            }
            else if ("[]".Equals(Attribute.Type.Substring(Attribute.Type.Length - 2, 2)))
            {
                string className = Attribute.Type.Substring(0, Attribute.Type.Length - 2);
                CDClass AttributeClass = OALProgram.ExecutionSpace.getClassByName(className);
                if (AttributeClass == null)
                {
                    return EXEExecutionResult.Error(ErrorMessage.ClassNotFound(className, OALProgram));
                }

                if (!EXETypes.IsValidReferenceValue(NewValue, Attribute.Type))
                {
                    return EXEExecutionResult.Error(ErrorMessage.InvalidReference(ReferencingVariableName + "." + AttributeName, NewValue ));
                }

                long[] IDs = String.Empty.Equals(NewValue) ? new long[] { } : NewValue.Split(',').Select(id => long.Parse(id)).ToArray();

                CDClassInstance Instance;
                foreach (long ID in IDs)
                {
                    Instance = AttributeClass.GetInstanceByID(ID);
                    if (Instance == null)
                    {
                        return EXEExecutionResult.Error(ErrorMessage.InstanceNotFound(ID, AttributeClass));
                    }
                }

                return ClassInstance.SetAttribute(AttributeName, NewValue);
            }
            else if (!String.IsNullOrEmpty(Attribute.Type))
            {
                CDClass AttributeClass = OALProgram.ExecutionSpace.getClassByName(Attribute.Type);
                if (AttributeClass == null)
                {
                    return EXEExecutionResult.Error(ErrorMessage.ClassNotFound(AttributeClass.Name, OALProgram));
                }

                CDClass NewValueClass = OALProgram.ExecutionSpace.getClassByName(NewValueType);
                if (NewValueClass == null)
                {
                    return EXEExecutionResult.Error(ErrorMessage.ClassNotFound(NewValueClass.Name, OALProgram));
                }

                if (!EXETypes.IsValidReferenceValue(NewValue, AttributeClass.Name))
                {
                    return EXEExecutionResult.Error(ErrorMessage.InvalidReference(ReferencingVariableName + "." + AttributeName, NewValue));
                }

                long IDValue = long.Parse(NewValue);

                CDClassInstance Instance = NewValueClass.GetInstanceByID(IDValue);
                if (Instance == null)
                {
                    return EXEExecutionResult.Error(ErrorMessage.InstanceNotFound(IDValue, AttributeClass));
                }

                return ClassInstance.SetAttribute(AttributeName, NewValue);
            }

            return EXEExecutionResult.Error(ErrorMessage.AttributeNotFoundOnClass(AttributeName, Class));
        }     
    }
}
