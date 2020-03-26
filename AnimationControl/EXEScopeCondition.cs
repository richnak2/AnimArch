using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEScopeCondition : EXEScope
    {
        public EXEASTNode Condition { get; set; }
        private List<EXEScopeCondition> ElifScopes;
        public EXEScope ElseScope { get; set; }

        public EXEScopeCondition() : base()
        {
            this.Condition = null;
            this.ElifScopes = null;
            this.ElseScope = null;
        }

        public void AddElifScope(EXEScopeCondition ElifScope)
        {
            if (this.ElifScopes == null)
            {
                this.ElifScopes = new List<EXEScopeCondition>();
            }

            this.ElifScopes.Add(ElifScope);
        }

        // should evaluate to true only if base "if" is true
        public Boolean EvaluateCondition(EXEScope Scope, CDClassPool ExecutionSpace)
        {
            String Result = this.Condition.Evaluate(Scope, ExecutionSpace);

            return EXETypes.BooleanTrue.Equals(Result) ? true : false;
        }
        public override Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Boolean Success = this.Execute(Animation, Scope);
            return Success;
        }
        public override Boolean Execute(Animation Animation, EXEScope Scope)
        {
            this.Animation = Animation;
            Boolean Result = true;
            Boolean AScopeWasExecuted = false;

            
            Animation.AccessInstanceDatabase();
            Boolean IfConditionResult = this.EvaluateCondition(Scope, Animation.ExecutionSpace);
            Animation.LeaveInstanceDatabase();
            if (IfConditionResult)
            {
                Console.WriteLine("If - Condition is true");
                int i = 0;
                foreach (EXECommand Command in this.Commands)
                {
                    Console.WriteLine("If - Bout to do " + ++i + "th command");
                    Result = Command.SynchronizedExecute(Animation, this);
                    Console.WriteLine("If - Done " + i + "th command");
                    if (!Result)
                    {
                        break;
                    }
                }
                AScopeWasExecuted = true;
            }

            if (AScopeWasExecuted)
            {
                return Result;
            }

            if (this.ElifScopes != null)
            {
                foreach (EXEScopeCondition CurrentElif in this.ElifScopes)
                {
                    Animation.AccessInstanceDatabase();
                    Boolean ElifConditionResult = CurrentElif.EvaluateCondition(Scope, Animation.ExecutionSpace);
                    Animation.LeaveInstanceDatabase();
                    if (ElifConditionResult)
                    {
                        Result = CurrentElif.SynchronizedExecute(Animation, CurrentElif);
                        AScopeWasExecuted = true;
                        break;
                    }
                }
            }

            if (AScopeWasExecuted)
            {
                return Result;
            }

            if (this.ElseScope != null)
            {
                Result = this.ElseScope.SynchronizedExecute(Animation, ElseScope);
            }

            return Result;
        }
    }
}
