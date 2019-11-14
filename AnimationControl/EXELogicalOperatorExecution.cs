using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXELogicalOperatorExecution
    {
        public Boolean Equals(EXEPrimitiveVariable FirstVar, EXEPrimitiveVariable SecondVar)
        {
            Boolean TypesAreComparable = FirstVar.Type == SecondVar.Type;
            if (!TypesAreComparable)
            {
                TypesAreComparable = (FirstVar.Type == EXETypes.IntegerTypeName && SecondVar.Type == EXETypes.RealTypeName)
                    || (FirstVar.Type == EXETypes.RealTypeName && SecondVar.Type == EXETypes.IntegerTypeName);
            }

            if (!TypesAreComparable)
            {
                return false;
            }

            if (FirstVar.Type == SecondVar.Type)
            {
                return FirstVar == SecondVar;
            }

            if (FirstVar.Type == EXETypes.IntegerTypeName && SecondVar.Type == EXETypes.RealTypeName)
            {
                int FirstVarInt = int.Parse(FirstVar.Value);
                double SecondVarReal = double.Parse(SecondVar.Value);

                return FirstVarInt == SecondVarReal;
            }

            if(FirstVar.Type == EXETypes.RealTypeName && SecondVar.Type == EXETypes.IntegerTypeName)
            {
                double FirstVarReal = double.Parse(FirstVar.Value);
                int SecondVarInt = int.Parse(SecondVar.Value);

                return FirstVarReal == SecondVarInt;
            }

            return false;
        }
    }
}
