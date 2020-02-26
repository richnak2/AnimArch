using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXEScopeLoopWhile : EXEScope
    {
        public EXEASTNode Condition;

        public EXEScopeLoopWhile(EXEASTNode Condition) : base()
        {
            this.Condition = Condition;
        }

        new public Boolean EvaluateCondition()
        {
            throw new NotImplementedException();
        }

        new public Boolean Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            Boolean Success = false;

            while (this.EvaluateCondition())
            {
                Success = base.Execute(ExecutionSpace, RelationshipSpace, this);
                if (!Success)
                {
                    break;
                }
            }

            return Success;
        }
    }
}
