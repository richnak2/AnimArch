using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEValueBool : EXEValuePrimitive
    {
        protected bool Value;
        public override string TypeName => EXETypes.BooleanTypeName;

        public EXEValueBool(EXEValueBool original)
        {
            CopyValues(original, this);
        }
        public EXEValueBool(string value)
        {
            this.Value = EXETypes.DetermineBoolValue(value);
        }
        public override EXEValueBase DeepClone()
        {
            return new EXEValueBool(this);
        }
        public override EXEExecutionResult AssignValueFrom(EXEValueBase assignmentSource)
        {
            return assignmentSource.AssignValueTo(this);
        }
        public override EXEExecutionResult AssignValueTo(EXEValueBool assignmentTarget)
        {
            CopyValues(this, assignmentTarget);
            return EXEExecutionResult.Success();
        }
        private void CopyValues(EXEValueBool source, EXEValueBool target)
        {
            target.Value = source.Value;
        }
    }
}