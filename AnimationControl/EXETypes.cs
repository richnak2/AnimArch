using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXETypes
    {
        public static String IntegerTypeName = "integer";
        public const String RealTypeName = "real";
        public const String BooleanTypeName = "boolean";
        public const String StringTypeName = "string";
        public const String DateTypeName = "date";
        public const String UniqueIDTypeName = "unique_ID";
        public const String ReferenceTypeName = "reference";

        public const String BooleanTrue = "TRUE";
        public const String BooleanFalse = "FALSE";

        public static String UnitializedName = "UNDEFINED";

        public static String DetermineVariableType(String name, String value)
        {
            if (name == UniqueIDTypeName)
            {
                return UniqueIDTypeName;
            }

            if (long.TryParse(value, out _))
            {
                return IntegerTypeName;
            }

            try
            {
                double.Parse(value, CultureInfo.InvariantCulture);
                return RealTypeName;
            }
            catch (Exception e)
            {
            }

            if (value == EXETypes.BooleanTrue || value == EXETypes.BooleanFalse)
            {
                return BooleanTypeName;
            }

            if (value[0] == '"' && value[value.Length-1] == '"')
            {
                return StringTypeName;
            }


            return ReferenceTypeName;
        }
        public static Boolean IsValidValue(String Value, String Type)
        {
            Boolean Result = false;
            switch (Type)
            {
                case "integer":
                    Result = int.TryParse(Value, out _);
                    break;
                case "real":
                    try
                    {
                        double.Parse(Value, CultureInfo.InvariantCulture);
                        Result = true;
                    }
                    catch (Exception e)
                    {
                        Result = false;
                    }
                    break;
                case "boolean":
                    Result = Boolean.TryParse(Value, out _);
                    break;
                case "unique_ID":
                    //Result = Name == UniqueIDTypeName;
                    Result = int.TryParse(Value, out _);
                    break;
                case "date":
                    //TODO (ak vobec)
                    break;
                default:
                    Result = false;
                    break;
            }

            return Result;
        }
    }
}
