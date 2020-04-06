using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXECommandBreak : EXECommand
    {
        public override bool Execute(Animation Animation, EXEScope Scope)
        {
            return Scope.PropagateControlCommand(LoopControlStructure.Break);
        }
    }
}
