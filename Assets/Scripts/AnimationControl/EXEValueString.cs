using System;
using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEValueString : EXEValuePrimitive
    {
        protected string Value;
        public override string TypeName => EXETypes.StringTypeName;

        public EXEValueString(EXEValueString original)
        {
            CopyValues(original, this);
        }
        public EXEValueString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("String value cannot be determined from null nor from an empty string.");
            }

            if (!EXETypes.IsValidStringValue(value))
            {
                throw new ArgumentException(string.Format("\"{0}\" is not a valid string value.", value));
            }

            this.Value = value.Substring(1, value.Length - 2);
        }
        public override EXEValueBase DeepClone()
        {
            return new EXEValueString(this);
        }
        public override EXEExecutionResult AssignValueFrom(EXEValueBase assignmentSource)
        {
            return assignmentSource.AssignValueTo(this);
        }
        public override EXEExecutionResult AssignValueTo(EXEValueString assignmentTarget)
        {
            CopyValues(this, assignmentTarget);

            return EXEExecutionResult.Success();
        }
        private void CopyValues(EXEValueString source, EXEValueString target)
        {
            target.Value = source.Value;
        }
    }
}