using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXEEvaluatorHandleOperators
    {
        private static List<String> ValidOperators = new List<String>(new String[] { "empty", "not_empty", "cardinality"});

        public bool IsHandleOperator(String Operator)
        {
            return ValidOperators.Contains(Operator);
        }

        public String Evaluate(String Operator, List<String> Operands, EXEScope Scope, CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace)
        {
            String Result = null;

            if (Operator == null || Operands == null)
            {
                return Result;
            }

            if (!IsHandleOperator(Operator))
            {
                return Result;
            }

            if (Operator.Length == 1)
            {
                switch (Operator) {
                    case "empty":
                        Result = EvaluateEmpty(Operands.First(), Scope, ExecutionSpace, RelationshipSpace);
                        break;
                    case "not_empty":
                        Result = EvaluateNotEmpty(Operands.First(), Scope, ExecutionSpace, RelationshipSpace);
                        break;
                    case "cardinality":
                        Result = EvaluateCardinality(Operands.First(), Scope, ExecutionSpace, RelationshipSpace);
                        break;
                }
                    
            }

            return Result;
        }

        public String EvaluateEmpty(String Operand, EXEScope Scope, CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace)
        {
            String Result = null;

            if (EXETypes.UnitializedName.Equals(Operand))
            {
                Result = EXETypes.BooleanTrue;
                return Result;
            }
           
            EXEReferencingVariable SingleInstanceVariable = Scope.FindReferencingVariableByName(Operand);
            if (SingleInstanceVariable != null)
            {
                CDClassInstance SingleInstance = SingleInstanceVariable.RetrieveReferencedClassInstance(ExecutionSpace);
                if (SingleInstance.Initialized)
                {
                    Result = EXETypes.BooleanFalse;
                }
                else
                {
                    Result = EXETypes.BooleanTrue;
                }

                return Result;
            }

            EXEReferencingSetVariable MultiInstanceVariable = Scope.FindSetReferencingVariableByName(Operand);
            if (MultiInstanceVariable != null)
            {
                if (MultiInstanceVariable.GetReferencingVariables().Any())
                {
                    Result = EXETypes.BooleanFalse;
                }
                else
                {
                    Result = EXETypes.BooleanTrue;
                }

                return Result;
            }

            return Result;
        }

        public String EvaluateNotEmpty(String Operand, EXEScope Scope, CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace)
        {
            String Result = null;
            String TempResult = EvaluateEmpty(Operand, Scope, ExecutionSpace, RelationshipSpace);

            if (EXETypes.BooleanTrue.Equals(TempResult))
            {
                Result = EXETypes.BooleanFalse;
            }
            else if (EXETypes.BooleanFalse.Equals(TempResult))
            {
                Result = EXETypes.BooleanTrue;
            }

            return Result;
        }
        public String EvaluateCardinality(String Operand, EXEScope Scope, CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace)
        {
            String Result = null;

            if (EXETypes.UnitializedName.Equals(Operand))
            {
                Result = "0";
                return Result;
            }

            EXEReferencingVariable SingleInstanceVariable = Scope.FindReferencingVariableByName(Operand);
            if (SingleInstanceVariable != null)
            {
                CDClassInstance SingleInstance = SingleInstanceVariable.RetrieveReferencedClassInstance(ExecutionSpace);
                if (SingleInstance.Initialized)
                {
                    Result = "1";
                }
                else
                {
                    Result = "0";
                }

                return Result;
            }

            EXEReferencingSetVariable MultiInstanceVariable = Scope.FindSetReferencingVariableByName(Operand);
            if (MultiInstanceVariable != null)
            {
                if (MultiInstanceVariable.GetReferencingVariables().Any())
                {
                    Result = MultiInstanceVariable.GetReferencingVariables().Count.ToString();
                }
                else
                {
                    Result = "0";
                }

                return Result;
            }

            return Result;
        }
    }
}
