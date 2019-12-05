using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public interface EXECommandInterface
    {
        String PrintSelf(Boolean IsTopLevel);

        void PrintAST();
        void Parse(EXEScope SuperScope);
        String GetCode();
        Boolean Execute(OALAnimationRepresentation ExecutionSpace, EXEScope Scope);
    }
}
