using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimArch.Extensions;
using UnityEngine;
using Object = System.Object;

namespace OALProgramControl
{
    public abstract class EXECommandCallBase : EXECommand
    {
        public readonly EXEASTNodeMethodCall MethodCall;
        public EXEValueBase ReturnedValue;

        public EXECommandCallBase(EXEASTNodeMethodCall methodCall)
        {
            this.MethodCall = methodCall;
            this.ReturnedValue = null;
        }
    }
}
