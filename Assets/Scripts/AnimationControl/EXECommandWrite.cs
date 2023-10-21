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
        private List<EXEASTNodeBase> Arguments { get; }

        public EXECommandWrite(List<EXEASTNodeBase> Arguments)
        {
            this.Arguments = Arguments;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            

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
            return new EXECommandWrite(Arguments.Select(argument => argument.Clone()).ToList());
        }
    }
}
