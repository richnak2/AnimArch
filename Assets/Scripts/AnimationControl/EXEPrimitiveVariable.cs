using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{
    public class EXEPrimitiveVariable
    {
        public String Name { get; }
        public String Type { get; }
        public String Value { get; set; }

        public EXEPrimitiveVariable(String Name, String Value)
        {
            this.Name = Name;
            this.Value = Value;
            this.Type = EXETypes.DetermineVariableType(Name, Value);
        }

        public EXEPrimitiveVariable(String Name, String Value, String Type)
        {
            this.Name = Name;
            this.Value = Value;
            this.Type = Type;

            if (!EXETypes.IsValidValue(Value, Type))
            {
                throw new Exception(String.Format("Value: \"{0}\"\nType: \"{1}\"\nName: \"{2}\"", Value, Type, Name));
            }
        }

        public EXEExecutionResult AssignValue(String name, String NewValue)
        {

            if (EXETypes.UniqueIDTypeName.Equals(name))
            {
                return EXEExecutionResult.Error(ErrorMessage.AssignmentToReadonlyProperty(name));
            }

            if (EXETypes.UnitializedName.Equals(this.Type))
            {
                return EXEExecutionResult.Error(ErrorMessage.ExistingUndefinedVariable(this.Name));
            }

            String NewValueType = EXETypes.DetermineVariableType(name, NewValue);
            if (NewValueType == this.Type || EXETypes.UnitializedName.Equals(NewValue))
            {
                this.Value = NewValue;
                return EXEExecutionResult.Success();
            }

            if (NewValueType == EXETypes.IntegerTypeName && this.Type == EXETypes.RealTypeName && EXEExecutionGlobals.AllowPromotionOfIntegerToReal)
            {
                int NewValueInt = int.Parse(NewValue);
                double newValueDouble = NewValueInt;
                this.Value = newValueDouble.ToString();
                return EXEExecutionResult.Success();
            }

            if (NewValueType == EXETypes.RealTypeName && this.Type == EXETypes.IntegerTypeName && EXEExecutionGlobals.AllowLossyAssignmentOfRealToInteger)
            {
                decimal newValueDouble = decimal.Parse(NewValue, CultureInfo.InvariantCulture);
                int newValueInt = (int) newValueDouble;
                this.Value = newValueInt.ToString();
                return EXEExecutionResult.Success();
            }

            return EXEExecutionResult.Error(ErrorMessage.InvalidAssignment(NewValue, NewValueType, this.Name, this.Type));
        }
    }
}
