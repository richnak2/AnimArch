using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;
using System.Collections;

namespace AnimationControl.OAL
{
    public class OALVisitor2:OALBaseVisitor<object>
    {
        public EXEScope globalExeScope;
        private Stack stack;

        public OALVisitor2()
        {
            this.globalExeScope = new EXEScope();
            this.stack = new Stack();
            this.stack.Push(this.globalExeScope);         

        }


        public override object VisitExeCommandQueryCreate([NotNull] OALParser.ExeCommandQueryCreateContext context)
        {
            String ClassName = null;
            String InstanceName = null;

            foreach (IParseTree child in context.children)
            {
                Console.WriteLine(context.ChildCount);
                if (child.GetType().ToString().Contains("KeyLetter"))
                {
                    ClassName = child.GetText();
                    Console.WriteLine(ClassName);
                }

                if (child.GetType().ToString().Contains("InstanceHandle"))
                {
                    InstanceName = child.GetText();
                    Console.WriteLine(InstanceName);
                }
                
            }
            
            if (stack.Count == 0)
            {
                if (InstanceName != null)
                {
                    globalExeScope.AddCommand(new EXECommandQueryCreate(ClassName, InstanceName));
                }
                else
                {
                    globalExeScope.AddCommand(new EXECommandQueryCreate(ClassName));
                }
            }
            else
            {
                //TODO
            }

            return null;
        }


        public override object VisitExeCommandQueryRelate([NotNull] OALParser.ExeCommandQueryRelateContext context)
        {
            String VariableName1 = null;
            String VariableName2 = null;
            String RelationshipName = null;

            VariableName1 = context.GetChild(1).GetText();
            VariableName2 = context.GetChild(3).GetText();
            RelationshipName = context.GetChild(5).GetText();

            Console.WriteLine(VariableName1);
            Console.WriteLine(VariableName2);
            Console.WriteLine(RelationshipName);



            if (stack.Count == 0)
            {
                globalExeScope.AddCommand(new EXECommandQueryRelate(VariableName1, VariableName2, RelationshipName));
            }


            return null;
            //return base.VisitExeCommandQueryRelate(context);
        }


        public override object VisitExeCommandQuerySelect([NotNull] OALParser.ExeCommandQuerySelectContext context)
        {


            return null;
            //return base.VisitExeCommandQuerySelect(context);
        }

    }
}
