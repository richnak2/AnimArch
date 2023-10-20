using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEValueUnitialized : EXEValueBase
    {
        protected EXEValueBase ValueAfterInitialization;
        public override string TypeName => EXETypes.UnitializedName;
        public EXEValueUnitialized(EXEValueUnitialized original)
        {
            CopyValues(original, this);
        }
        public EXEValueUnitialized()
        {
            this.ValueAfterInitialization = null;
        }
        public override EXEValueBase DeepClone()
        {
            return new EXEValueUnitialized(this);
        }
        public override EXEExecutionResult AssignValueFrom(EXEValueBase assignmentSource)
        {
            // Value has not yet been initialized, so anything can be assigned to this
            if (ValueAfterInitialization == null)
            {
                this.ValueAfterInitialization = assignmentSource.DeepClone();
                return EXEExecutionResult.Success();
            }

            // Value has already been initialized, so we're typed
            return ValueAfterInitialization.AssignValueFrom(assignmentSource);
        }
        private void CopyValues(EXEValueUnitialized source, EXEValueUnitialized target)
        {
            target.ValueAfterInitialization = source.ValueAfterInitialization.DeepClone();
        }
    }
}