using System;
using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEValueReal : EXEValuePrimitive
    {
        protected string WholePart;
        protected string DecimalPart;
        protected bool IsNegative;
        public override string TypeName => EXETypes.RealTypeName;

        public EXEValueReal(EXEValueReal original)
        {
            CopyValues(original, this);
        }
        public EXEValueReal(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Real value cannot be determined from null nor from an empty string.");
            }

            if (value[0] == '-')
            {
                this.IsNegative = true;
                value = value.Substring(1, value.Length - 1);
            }

            if (!EXETypes.IsValidRealValue(value))
            {
                throw new ArgumentException(string.Format("\"{0}\" is not a valid real value.", value));
            }

            string[] parts = value.Split(".");
            this.WholePart = parts[0];
            this.DecimalPart = parts[0];
        }
        public override EXEValueBase DeepClone()
        {
            return new EXEValueReal(this);
        }
        public override EXEExecutionResult AssignValueFrom(EXEValueBase assignmentSource)
        {
            return assignmentSource.AssignValueTo(this);
        }
        public override EXEExecutionResult AssignValueTo(EXEValueReal assignmentTarget)
        {
            CopyValues(this, assignmentTarget);

            return EXEExecutionResult.Success();
        }
        private void CopyValues(EXEValueReal source, EXEValueReal target)
        {
            target.WholePart = source.WholePart;
            target.DecimalPart = source.DecimalPart;
            target.IsNegative = source.IsNegative;
        }
    }
}