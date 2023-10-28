﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace OALProgramControl
{
    public class EXEScopeParallel : EXEScope
    {
        private List<EXEScope> Threads { get; }
        public EXEScopeParallel(IEnumerable<EXEScope> threads) : base()
        {
            this.Threads = threads.ToList();
            foreach (EXEScope Thread in Threads)
            {
                this.AddThread(Thread);
            }
        }

        public void AddThread(EXEScope Thread)
        {
            Thread.SetSuperScope(this);
            this.Threads.Add(Thread);
        }
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            this.CommandStack.Fork(this.Threads);

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
            String Result = HighlightCodeIf(Highlight, Indent + "par\n");
            if (this.Threads != null)
            {
                foreach (EXEScope Thread in this.Threads)
                {
                    Result += HighlightCodeIf(Highlight, Indent + "\tthread\n");
                    foreach (EXECommand Command in Thread.Commands)
                    {
                        Result += Command.ToFormattedCode(Indent + "\t\t");
                    }
                    Result += HighlightCodeIf(Highlight, Indent + "\tend thread;\n");
                }
            }
            Result += HighlightCodeIf(Highlight, Indent + "end par;\n");
            return Result;
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeParallel(Threads.Select(x => (EXEScope)x.CreateClone()));
        }
    }
}