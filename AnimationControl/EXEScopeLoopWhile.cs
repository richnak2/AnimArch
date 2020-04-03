using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEScopeLoopWhile : EXEScope
    {
        public EXEASTNode Condition;

        public EXEScopeLoopWhile(EXEASTNode Condition) : base()
        {
            this.Condition = Condition;
        }
        public EXEScopeLoopWhile(EXEScope SuperScope, EXECommand[] Commands, EXEASTNode Condition) : base(SuperScope, Commands)
        {
            this.Condition = Condition;
        }

        public override Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Boolean Success = this.Execute(Animation, Scope);
            return Success;
        }
        public override Boolean Execute(Animation Animation , EXEScope Scope)
        {
            Boolean Success = true;
            this.Animation = Animation;

            bool ConditionTrue = true;
            String ConditionResult;
            while (ConditionTrue)
            {
                Animation.AccessInstanceDatabase();
                ConditionResult = this.Condition.Evaluate(Scope, Animation.ExecutionSpace);
                Animation.LeaveInstanceDatabase();
                if (ConditionResult == null)
                {
                    return false;
                }
                ConditionTrue = EXETypes.BooleanTrue.Equals(ConditionResult);
                if (!ConditionTrue)
                {
                    break;
                }

                foreach (EXECommand Command in this.Commands)
                {
                    Success = Command.SynchronizedExecute(Animation, this);
                    if (!Success)
                    {
                        break;
                    }
                }
                if (!Success)
                {
                    break;
                }
            }
            return Success;
        }
    }
}
