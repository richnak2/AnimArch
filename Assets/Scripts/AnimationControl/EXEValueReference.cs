using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEValueReference : EXEValueBase
    {
        public override string TypeName => ClassInstance.OwningClass.Name;
        public override bool CanHaveAttributes => true;
        public override bool CanHaveMethods => true;
        protected CDClassInstance ClassInstance;
        public EXEValueReference(EXEValueReference original)
        {
            CopyValues(original, this);
        }
        public override EXEValueBase DeepClone()
        {
            return new EXEValueReference(this);
        }
        public override bool AttributeExists(string attributeName)
        {
            return this.ClassInstance.OwningClass.GetAttributeByName(attributeName) != null;
        }
        public override bool MethodExists(string methodName)
        {
            return FindMethod(methodName) != null;
        }
        public override EXEExecutionResult RetrieveAttributeValue(string attributeName)
        {
            EXEValueBase attributeValue = this.ClassInstance.GetAttributeValue(attributeName);

            if (attributeValue == null)
            {
                return EXEExecutionResult.Error(ErrorMessage.AttributeNotFoundOnClass(attributeName, ClassInstance.OwningClass), "XEC2005");
            }

            EXEExecutionResult executionResult = EXEExecutionResult.Success();
            executionResult.ReturnedOutput = attributeValue;
            return executionResult;
        }
        public override CDMethod FindMethod(string methodName)
        {
            return this.ClassInstance.OwningClass.GetMethodByName(methodName);
        }
        public override EXEExecutionResult AssignValueFrom(EXEValueBase assignmentSource)
        {
            return assignmentSource.AssignValueTo(this);
        }
        public override EXEExecutionResult AssignValueTo(EXEValueReference assignmentTarget)
        {
            if (!this.ClassInstance.OwningClass.CanBeAssignedTo(assignmentTarget.ClassInstance.OwningClass))
            {
                return base.AssignValueTo(assignmentTarget);
            }

            CopyValues(this, assignmentTarget);

            return EXEExecutionResult.Success();
        }
        private void CopyValues(EXEValueReference source, EXEValueReference target)
        {
            target.ClassInstance = source.ClassInstance;
        }
    }
}