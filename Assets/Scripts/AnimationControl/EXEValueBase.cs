using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public abstract class EXEValueBase
    {
        public abstract string TypeName { get; }
        public virtual bool CanHaveAttributes { get { return false; } }
        public virtual bool CanHaveMethods { get { return false; } }
        public bool WasInitialized = true;
        public abstract EXEValueBase DeepClone();
        public abstract string ToText();
        public virtual bool AttributeExists(string attributeName)
        {
            return false;
        }
        public virtual bool MethodExists(string methodName)
        {
            return false;
        }
        public virtual EXEExecutionResult RetrieveAttributeValue(string attributeName)
        {
            return EXEExecutionResult.Error(string.Format("Tried to find attribute \"{0}\" on something that does not have any attribute.", attributeName), "XEC2004");
        }
        public virtual CDMethod FindMethod(string methodName)
        {
            return null;
        }
        public virtual EXEExecutionResult AssignValueFrom(EXEValueBase assignmentSource)
        {
            return EXEExecutionResult.Error(AssignmentErrorMessage(assignmentSource.TypeName, this.TypeName), "XEC2006");
        }
        public virtual EXEExecutionResult AssignValueTo(EXEValueBool assignmentTarget)
        {
            return EXEExecutionResult.Error(AssignmentErrorMessage(this.TypeName, assignmentTarget.TypeName), "XEC2007");
        }
        public virtual EXEExecutionResult AssignValueTo(EXEValueInt assignmentTarget)
        {
            return EXEExecutionResult.Error(AssignmentErrorMessage(this.TypeName, assignmentTarget.TypeName), "XEC2008");
        }
        public virtual EXEExecutionResult AssignValueTo(EXEValueReal assignmentTarget)
        {
            return EXEExecutionResult.Error(AssignmentErrorMessage(this.TypeName, assignmentTarget.TypeName), "XEC2009");
        }
        public virtual EXEExecutionResult AssignValueTo(EXEValueString assignmentTarget)
        {
            return EXEExecutionResult.Error(AssignmentErrorMessage(this.TypeName, assignmentTarget.TypeName), "XEC2010");
        }
        public virtual EXEExecutionResult AssignValueTo(EXEValueReference assignmentTarget)
        {
            return EXEExecutionResult.Error(AssignmentErrorMessage(this.TypeName, assignmentTarget.TypeName), "XEC2011");
        }
        public virtual EXEExecutionResult AssignValueTo(EXEValueUnitialized assignmentTarget)
        {
            return EXEExecutionResult.Error(AssignmentErrorMessage(this.TypeName, assignmentTarget.TypeName), "XEC2012");
        }
        public virtual EXEExecutionResult ApplyOperator(string operation)
        {
            return EXEExecutionResult.Error(string.Format("Cannot apply unary operation \"{0}\" on operand \"{1}\".", operation, this.ToText()), "XEC2017");
        }
        public virtual EXEExecutionResult ApplyOperator(string operation, EXEValueBase operand)
        {
            return EXEExecutionResult.Error(string.Format("Cannot apply binary operation \"{0}\" on operands \"{1}\" and \"{2}\".", operation, this.ToText(), operand.ToText()), "XEC2018");
        }
        protected virtual EXEExecutionResult UninitializedError()
        {
            return EXEExecutionResult.Error("Tried to manipulate uninitialized value.", "XEC2013");
        }
        protected string AssignmentErrorMessage(string sourceType, string targetType)
        {
            return string.Format("Cannot assign value of type \"{1}\" to a variable of type \"{0}\".", sourceType, targetType);
        }
    }
}