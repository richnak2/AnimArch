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

        public EXEScopeCondition(EXEScope SuperScope, EXECommand[] Commands, EXEASTNode Condition) : base(SuperScope, Commands)
        {
            this.Condition = Condition;
            this.ElifScopes = null;
            this.ElseScope = null;
        }
        public EXEScopeCondition(EXEScope SuperScope, EXECommand[] Commands, EXEASTNode Condition, EXEScope ElseScope) : base(SuperScope, Commands)
        {
            this.Condition = Condition;
            this.ElifScopes = null;
            this.ElseScope = ElseScope;
        }
        public EXEScopeCondition(EXEScope SuperScope, EXECommand[] Commands, EXEASTNode Condition, EXEScopeCondition[] ElifScopes) : base(SuperScope, Commands)
        {
            this.Condition = Condition;
            this.ElifScopes = ElifScopes.ToList();
            this.ElseScope = null;
        }
        public EXEScopeCondition(EXEScope SuperScope, EXECommand[] Commands, EXEASTNode Condition, EXEScopeCondition[] ElifScopes, EXEScope ElseScope) : base(SuperScope, Commands)
        {
            this.Condition = Condition;
            this.ElifScopes = ElifScopes.ToList();
            this.ElseScope = ElseScope;
        }

        public void AddElifScope(EXEScopeCondition ElifScope)
        {
            if (this.ElifScopes == null)
            {
                this.ElifScopes = new List<EXEScopeCondition>();
            }

            this.ElifScopes.Add(ElifScope);
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

            Console.WriteLine("About to evaluate condition");

            if (this.Condition == null)
            {
                return false;
            }

            Animation.AccessInstanceDatabase();
            String ConditionResult = this.Condition.Evaluate(Scope, Animation.ExecutionSpace);
            Animation.LeaveInstanceDatabase();
            if (ConditionResult == null)
            {
                return false;
            }
            Boolean IfConditionResult = EXETypes.BooleanTrue.Equals(ConditionResult) ? true : false;
            Console.WriteLine("Evaluated condition: " + IfConditionResult);

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
                    if (CurrentElif.Condition == null)
                    {
                        return false;
                    }
                    Animation.AccessInstanceDatabase();
                    ConditionResult = CurrentElif.Condition.Evaluate(Scope, Animation.ExecutionSpace);
                    Animation.LeaveInstanceDatabase();

                    if (ConditionResult == null)
                    {
                        return false;
                    }
                    IfConditionResult = EXETypes.BooleanTrue.Equals(ConditionResult) ? true : false;
                    
                    if (IfConditionResult)
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
                Console.WriteLine("Executing elif");
                Result = this.ElseScope.SynchronizedExecute(Animation, ElseScope);
                Console.WriteLine("Executed elif: " + Result);
            }

            return Result;
        }
    }
}
