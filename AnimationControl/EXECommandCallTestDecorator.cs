using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXECommandCallTestDecorator : EXECommand
    {
        private EXECommandCall DecoratedCommand { get; }
        private StringBuffer CallTextBuffer { get; }
        public EXECommandCallTestDecorator(EXECommandCall DecoratedCommand, StringBuffer CallTextBuffer)
        {
            this.DecoratedCommand = DecoratedCommand;
            this.CallTextBuffer = CallTextBuffer;
        }

        public override bool Execute(Animation Animation, EXEScope Scope)
        {
            bool Result = DecoratedCommand.Execute(Animation, Scope);
            if (!Result)
            {
                return Result;
            }
            CallTextBuffer.Append(DecoratedCommand.ToCode());
            return Result;
        }
    }
}
