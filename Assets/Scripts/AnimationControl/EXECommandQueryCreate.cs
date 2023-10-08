using System;

namespace OALProgramControl
{
    public class EXECommandQueryCreate : EXECommand
    {
        public String ReferencingVariableName { get; }
        private String ReferencingAttributeName { get; }
        public String ClassName { get; }

        public EXECommandQueryCreate(String ClassName, String ReferencingVariableName, String ReferencingAttributeName)
        {
            this.ReferencingVariableName = ReferencingVariableName;
            this.ReferencingAttributeName = ReferencingAttributeName;
            this.ClassName = ClassName;
        }

        public EXECommandQueryCreate(String ClassName)
        {
            ReferencingVariableName = "";
            ReferencingAttributeName = null;
            this.ClassName = ClassName;
        }

        // SetUloh2
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            //Create an instance of given class -> will affect ExecutionSpace.
            //If ReferencingVariableName is provided (is not ""), create a referencing variable pointing to this instance -> will affect scope
            CDClass Class = OALProgram.ExecutionSpace.getClassByName(ClassName);
            if (Class == null)
            {
                return Error("XEC1054", ErrorMessage.ClassNotFound(ClassName, OALProgram));
            }

            EXEReferencingVariable Variable = SuperScope.FindReferencingVariableByName(ReferencingVariableName);

            if (ReferencingAttributeName == null)
            {
                if (Variable != null && !String.Equals(ClassName, Variable.ClassName))
                {
                    return Error("XEC1055", ErrorMessage.InvalidObjectCreation(ClassName, ReferencingVariableName, Variable.ClassName));
                }

                CDClassInstance NewInstance = Class.CreateClassInstance();
                if (NewInstance == null)
                {
                    return Error("XEC1056", ErrorMessage.FailedObjectCreation(Class.Name));
                }

                if (!"".Equals(ReferencingVariableName))
                {
                    if (Variable != null)
                    {
                        Variable.ReferencedInstanceId = NewInstance.UniqueID;
                    }
                    else
                    {
                        Variable = new EXEReferencingVariable(ReferencingVariableName, Class.Name,
                            NewInstance.UniqueID);
                        return SuperScope.AddVariable(Variable);
                    }
                }
            }
            else
            {
                if (Variable == null)
                {
                    return Error("XEC1057", ErrorMessage.VariableNotFound(ReferencingAttributeName, this.SuperScope));
                }

                CDClass VariableClass = OALProgram.ExecutionSpace.getClassByName(Variable.ClassName);
                if (VariableClass == null)
                {
                    return Error("XEC1058", ErrorMessage.ClassNotFound(Variable.ClassName, OALProgram));
                }

                CDAttribute Attribute = VariableClass.GetAttributeByName(ReferencingAttributeName);
                if (Attribute == null)
                {
                    return Error("XEC1059", ErrorMessage.AttributeNotFoundOnClass(ReferencingAttributeName, VariableClass));
                }

                if (!String.Equals(ClassName, Attribute.Type))
                {
                    return Error("XEC1060", ErrorMessage.InvalidObjectCreation(ClassName, ReferencingVariableName + "." + ReferencingAttributeName, Attribute.Type));
                }

                CDClassInstance ClassInstance = VariableClass.GetInstanceByID(Variable.ReferencedInstanceId);
                if (ClassInstance == null)
                {
                    return Error("XEC1061", ErrorMessage.InstanceNotFound(Variable.ReferencedInstanceId, VariableClass));
                }

                CDClassInstance NewInstance = Class.CreateClassInstance();
                if (NewInstance == null)
                {
                    return Error("XEC1062", ErrorMessage.FailedObjectCreation(Class.Name));
                }

                return ClassInstance.SetAttribute(ReferencingAttributeName, NewInstance.UniqueID.ToString());
            }

            return Success();
        }

        public override string ToCodeSimple()
        {
            return "create object instance "
                   + ("".Equals(ReferencingVariableName) ? "" :
                       ReferencingAttributeName == null ? (ReferencingVariableName + " ") :
                       (ReferencingVariableName + "." + ReferencingAttributeName + " "))
                   + "of " + ClassName;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandQueryCreate(ClassName, ReferencingVariableName, ReferencingAttributeName);
        }
    }
}