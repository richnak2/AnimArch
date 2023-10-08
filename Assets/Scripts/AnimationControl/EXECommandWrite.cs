using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Visualization.UI;

namespace OALProgramControl
{
    public class EXECommandWrite : EXECommand
    {
        private List<EXEASTNode> Arguments { get; }

        public EXECommandWrite(List<EXEASTNode> Arguments)
        {
            this.Arguments = Arguments;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            String Result = "";
            String ArgumentValue;
            String ArgumentType;

            for (int i = 0; i < this.Arguments.Count; i++)
            {
                ArgumentValue = this.Arguments[i].Evaluate(SuperScope, OALProgram.ExecutionSpace);
                if (ArgumentValue == null)
                {
                    return Error("XEC1140", ErrorMessage.FailedExpressionEvaluation(this.Arguments[i], this.SuperScope));
                }

                if (this.Arguments[i].IsReference())
                {
                    ArgumentType = SuperScope.DetermineVariableType(this.Arguments[i].AccessChain(), OALProgram.ExecutionSpace);
                    if (ArgumentType == null)
                    {
                        return Error("XEC1141", ErrorMessage.FailedExpressionTypeDetermination(string.Join(".", this.Arguments[i].AccessChain())));
                    }
                }
                // It must be primitive, not reference
                else
                {
                    ArgumentType = EXETypes.DetermineVariableType("", ArgumentValue);
                    if (ArgumentType == null)
                    {
                        return Error("XEC1142", ErrorMessage.FailedExpressionTypeDetermination(ArgumentValue));
                    }
                }

                //TODO: zatial to nemoze byt instancia, a ani pole instancii
                if (EXETypes.IsPrimitive(ArgumentType) || EXETypes.UnitializedName.Equals(ArgumentType))
                {
                    if (EXETypes.StringTypeName.Equals(ArgumentType))
                    {
                        // Remove double quotes
                        ArgumentValue = ArgumentValue.Replace("\"", "");
                    }

                    Result += ArgumentValue;
                }
                else
                {
                    return Error("XEC1143", ErrorMessage.PrintValueMustBePrimitive());
                }
            }

            ConsolePanel.Instance.YieldOutput(Result);

            return Success();
        }

        public override string ToCodeSimple()
        {
            String Result = "write(";

            if (this.Arguments.Any())
            {
                Result += this.Arguments[0].ToCode();

                for (int i = 1; i < this.Arguments.Count; i++)
                {
                    Result += ", " + this.Arguments[i].ToCode();
                }
            }
            Result += ")";

            return Result;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandWrite(Arguments);
        }
    }
}
