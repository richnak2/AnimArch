using System;
using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEValueInt : EXEValuePrimitive
    {
        protected string Value;
        protected bool IsNegative;
        public override string TypeName => EXETypes.IntegerTypeName;

        public EXEValueInt(EXEValueInt original)
        {
            CopyValues(original, this);
        }
        public EXEValueInt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Int value cannot be determined from null nor from an empty string.");
            }

            if (value[0] == '-')
            {
                this.IsNegative = true;
                value = value.Substring(1, value.Length - 1);
            }

            if (!EXETypes.IsValidIntValue(value))
            {
                throw new ArgumentException(string.Format("\"{0}\" is not a valid int value.", value));
            }

            this.Value = value;
        }
        public override EXEValueBase DeepClone()
        {
            return new EXEValueInt(this);
        }
        public override EXEExecutionResult AssignValueFrom(EXEValueBase assignmentSource)
        {
            return assignmentSource.AssignValueTo(this);
        }
        public override EXEExecutionResult AssignValueTo(EXEValueInt assignmentTarget)
        {
            CopyValues(this, assignmentTarget);

            return EXEExecutionResult.Success();
        }
        private void CopyValues(EXEValueInt source, EXEValueInt target)
        {
            target.Value = source.Value;
            target.IsNegative = source.IsNegative;
        }
    }
}