using System;

namespace OALProgramControl
{
    public class EXECommandQueryDelete : EXECommand
    {
        private String VariableName { get; }
        private String AttributeName { get; }

        public EXECommandQueryDelete(String VariableName, String AttributeName)
        {
            this.VariableName = VariableName;
            this.AttributeName = AttributeName;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEReferencingVariable Variable = SuperScope.FindReferencingVariableByName(this.VariableName);

            if (Variable != null)
            {
                String VariableClassName = Variable.ClassName;
                long VariableInstanceId = Variable.ReferencedInstanceId;

                if (this.AttributeName != null)
                {
                    CDClass VariableClass = OALProgram.ExecutionSpace.getClassByName(Variable.ClassName);
                    if (VariableClass == null)
                    {
                        return Error("XEC1063", ErrorMessage.ClassNotFound(Variable.ClassName, OALProgram));
                    }

                    CDAttribute Attribute = VariableClass.GetAttributeByName(this.AttributeName);
                    if (Attribute == null)
                    {
                        return Error("XEC1064", ErrorMessage.AttributeNotFoundOnClass(this.AttributeName, VariableClass));
                    }

                    if ("[]".Equals(Attribute.Type.Substring(Attribute.Type.Length - 2, 2)))
                    {
                        return Error("XEC1065", ErrorMessage.IsNotArray(Variable.Name + "." + Attribute.Name));
                    }

                    CDClassInstance ClassInstance = VariableClass.GetInstanceByID(Variable.ReferencedInstanceId);
                    if (ClassInstance == null)
                    {
                        return Error("XEC1066", ErrorMessage.InstanceNotFound(Variable.ReferencedInstanceId, VariableClass));
                    }

                    if (!long.TryParse(ClassInstance.GetAttributeValue(this.AttributeName), out VariableInstanceId))
                    {
                        return Error("XEC1067", ErrorMessage.InvalidReference(this.VariableName + "." + this.AttributeName, ClassInstance.GetAttributeValue(this.AttributeName)));
                    }

                    // We need to check if Attribute.Type is type of some class, not primitive type
                    CDClass AttributeClass = OALProgram.ExecutionSpace.getClassByName(Attribute.Type);
                    if (AttributeClass == null)
                    {
                        return Error("XEC1068", ErrorMessage.ClassNotFound(Attribute.Type, OALProgram));
                    }

                    VariableClassName = Attribute.Type;
                }

                bool DestructionSuccess = OALProgram.ExecutionSpace.DestroyInstance(VariableClassName, VariableInstanceId);
                if (DestructionSuccess && SuperScope.UnsetReferencingVariables(VariableClassName, VariableInstanceId))
                {
                    return Success();
                }
                else
                {
                    return Error("XEC1069", ErrorMessage.FailedObjectDestruction(VariableName + (AttributeName == null ? string.Empty : ("." + AttributeName))));
                }
            }

            return Error("XEC1070", ErrorMessage.VariableNotFound(this.VariableName, this.SuperScope));
        }
        public override string ToCodeSimple()
        {
            return "delete object instance " + (this.AttributeName == null ? this.VariableName : (this.VariableName + "." + this.AttributeName));
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandQueryDelete(VariableName, AttributeName);
        }
    }
}
