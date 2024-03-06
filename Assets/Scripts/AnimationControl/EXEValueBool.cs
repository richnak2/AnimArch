﻿using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEValueBool : EXEValuePrimitive
    {
        public bool Value;
        public override string TypeName => EXETypes.BooleanTypeName;

        public EXEValueBool(EXEValueBool original)
        {
            CopyValues(original, this);
        }
        public EXEValueBool(string value)
        {
            this.Value = EXETypes.DetermineBoolValue(value);
        }
        public EXEValueBool(bool value)
        {
            this.Value = value;
        }
        public override EXEValueBase DeepClone()
        {
            return new EXEValueBool(this);
        }

        public override void Accept(Visitor v)
        {
            v.VisitExeValueBool(this);
        }
        protected override EXEExecutionResult AssignValueFromConcrete(EXEValueBase assignmentSource)
        {
            return assignmentSource.AssignValueTo(this);
        }
        public override EXEExecutionResult AssignValueTo(EXEValueBool assignmentTarget)
        {
            if (!this.WasInitialized)
            {
                return UninitializedError();
            }

            CopyValues(this, assignmentTarget);
            this.WasInitialized = true;

            return EXEExecutionResult.Success();
        }
        private void CopyValues(EXEValueBool source, EXEValueBool target)
        {
            target.Value = source.Value;
        }
        public override EXEExecutionResult ApplyOperator(string operation)
        {
            if (!this.WasInitialized)
            {
                return UninitializedError();
            }

            EXEExecutionResult result;

            if ("not".Equals(operation))
            {
                this.Value = !this.Value;
                result = EXEExecutionResult.Success();
                result.ReturnedOutput = this;
                return result;
            }
            else if("type_name".Equals(operation))
            {
                result = EXEExecutionResult.Success();
                result.ReturnedOutput = new EXEValueString(string.Format(@"""{0}""", this.TypeName));
                return result;
            }

            return base.ApplyOperator(operation);
        }
        public override EXEExecutionResult ApplyOperator(string operation, EXEValueBase operand)
        {
            if (!this.WasInitialized || !operand.WasInitialized)
            {
                return base.ApplyOperator(operation, operand);
            }

            EXEExecutionResult result = base.ApplyOperator(operation, operand);

            if ("or".Equals(operation.ToLower()))
            {
                if (operand is not EXEValueBool)
                {
                    return base.ApplyOperator(operation, operand);
                }

                this.Value = this.Value || (operand as EXEValueBool).Value;
                result = EXEExecutionResult.Success();
                result.ReturnedOutput = this;
                return result;
            }
            else if ("and".Equals(operation.ToLower()))
            {
                if (operand is not EXEValueBool)
                {
                    return base.ApplyOperator(operation, operand);
                }

                this.Value = this.Value && (operand as EXEValueBool).Value;
                result = EXEExecutionResult.Success();
                result.ReturnedOutput = this;
                return result;
            }
            else if ("==".Equals(operation))
            {
                if (operand is not EXEValueBool)
                {
                    return base.ApplyOperator(operation, operand);
                }

                this.Value = this.Value == (operand as EXEValueBool).Value;
                result = EXEExecutionResult.Success();
                result.ReturnedOutput = this;
                return result;
            }
            else if ("!=".Equals(operation))
            {
                if (operand is not EXEValueBool)
                {
                    return base.ApplyOperator(operation, operand);
                }

                this.Value = this.Value != (operand as EXEValueBool).Value;
                result = EXEExecutionResult.Success();
                result.ReturnedOutput = this;
                return result;
            }
          
            return base.ApplyOperator(operation, operand);
        }

        public override string ToObjectDiagramText()
        {
            return this.Value ? EXETypes.BooleanTrue : EXETypes.BooleanFalse;
        }
    }
}