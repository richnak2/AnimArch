using System;

namespace AnimationControl
{
    public class EXEReferenceEvaluator
    {
        //SetUloh1
        // We have variable name, attribute name and scope, in which to look for variable
        // We need to get the value of given attribute of given variable
        // If this does not exist, return null
        // You will use EXEScope.FindReferencingVariableByName() method, but you need to implement it first
        // user.name

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
            return ClassInstance.GetAttribute(AttributeName);
        }

        //SetUloh1
        // Similar as task above, but this time we set the attribute value to "NewValue" parameter
        // But it's not that easy, you need to check if attribute type and NewValue type are the same (e.g. both are integer)
        // To do that, you need to find the referencing variable's class (via Scope) and then the attribute's type (vie ExecutionSpace)
        // When you know the type of attribute, use EXETypes.IsValidValue to see if you can or cannot assign that value to that attribute
        // You assign it in Scope
        // Return if you could assign it or not
        // EXETypes.determineVariableType()
        public Boolean SetAttributeValue(String ReferencingVariableName, String AttributeName, EXEScope Scope, CDClassPool ExecutionSpace, String NewValue)
        {
            EXEReferencingVariable ReferencingVariable = Scope.FindReferencingVariableByName(ReferencingVariableName);
            if (ReferencingVariable == null) return false;

            CDClassInstance ClassInstance = ExecutionSpace.GetClassInstanceById(ReferencingVariable.ClassName, ReferencingVariable.ReferencedInstanceId);
            if (ClassInstance == null) return false;

            // Posuvas zly argument metode DetermineVariableType
            //check if attribute exist
            String Attribute = ClassInstance.GetAttribute(AttributeName);
            if (Attribute == null) return false;

            String AttributeType = EXETypes.DetermineVariableType(null, Attribute);
            String NewValueType = EXETypes.DetermineVariableType(null, NewValue);
            if (!String.Equals(AttributeType, NewValueType)) return false;


            return ClassInstance.SetAttribute(AttributeName, NewValue);

        }
    }
}
