using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{
    public class EXEScopeLoopWhile : EXEScopeLoop
    {
        public EXEASTNodeBase Condition;
        private int IterationCounter;

        public EXEScopeLoopWhile(EXEASTNodeBase Condition) : base()
        {
            this.Condition = Condition;
            this.IterationCounter = 0;
        }
        public EXEScopeLoopWhile(EXEScope SuperScope, EXECommand[] Commands, EXEASTNodeBase Condition) : base(SuperScope, Commands)
        {
            this.Condition = Condition;
            this.IterationCounter = 0;
        }
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            String ConditionResult = this.Condition.Evaluate(SuperScope, OALProgram.ExecutionSpace);

            //!!NON-RECURSIVE!!
            this.ClearVariables();

            if (ConditionResult == null)
            {
                return Error("XEC1158", ErrorMessage.FailedExpressionEvaluation(this.Condition, this.SuperScope));
            }
            if (!EXETypes.BooleanTypeName.Equals(EXETypes.DetermineVariableType("", ConditionResult)))
            {
                return Error("XEC1159", ErrorMessage.InvalidValueForType(ConditionResult, EXETypes.BooleanTypeName));
            }

            bool ConditionTrue = EXETypes.BooleanTrue.Equals(ConditionResult);
            if (ConditionTrue)
            {
                if (IterationCounter >= EXEExecutionGlobals.LoopIterationCap)
                {
                    return Error("XEC1160", ErrorMessage.IterationLoopThresholdCrossed(EXEExecutionGlobals.LoopIterationCap));
                }
                else
                {
                    IterationCounter++;
                    OALProgram.CommandStack.Enqueue(this);
                    AddCommandsToStack(this.Commands);
                    this.ClearVariables();
                }
            }

            return Success();
        }

        public override String ToCode(String Indent = "")
        {
            return FormatCode(Indent, false);
        }
        public override string ToFormattedCode(string Indent = "")
        {
            return FormatCode(Indent, IsActive);
        }
        private string FormatCode(String Indent, bool Highlight)
        {
            String Result = HighlightCodeIf(Highlight, Indent + "while (" + this.Condition.ToCode() + ")\n");
            foreach (EXECommand Command in this.Commands)
            {
                Result += Command.ToFormattedCode(Indent + "\t");
            }
            Result += HighlightCodeIf(Highlight, Indent + "end while;\n");
            return Result;
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeLoopWhile(Condition);
        }
    }
}
