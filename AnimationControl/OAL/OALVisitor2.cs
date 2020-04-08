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
        private Stack<EXEScope> stackEXEScope;
        private Stack<EXEASTNodeComposite> stackEXEASTNode;

        public OALVisitor2()
        {
            this.globalExeScope = new EXEScope();
            this.stackEXEASTNode = new Stack<EXEASTNodeComposite>();
            this.stackEXEScope = new Stack<EXEScope>();
            this.stackEXEScope.Push(this.globalExeScope);         

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
            
            if (stackEXEScope.Count == 0)
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
            String VariableName1 = context.GetChild(1).GetText();
            String VariableName2 = context.GetChild(3).GetText();
            String RelationshipName = context.GetChild(5).GetText();

            Console.WriteLine(VariableName1);
            Console.WriteLine(VariableName2);
            Console.WriteLine(RelationshipName);

            stackEXEScope.Peek().AddCommand(new EXECommandQueryRelate(VariableName1, VariableName2, RelationshipName));

            return null;
            //return base.VisitExeCommandQueryRelate(context);
        }


        public override object VisitExeCommandQuerySelect([NotNull] OALParser.ExeCommandQuerySelectContext context)
        {
            String Cardinality = context.GetChild(0).GetText().Contains("many") ? "many" : "any";
            String VariableName = context.GetChild(1).GetText();
            String ClassName = context.GetChild(3).GetText();
            EXEASTNode WhereExpression;

            if(context.GetChild(4).GetText().Contains("where"))
            {
                Console.WriteLine("where");
                Visit(context.GetChild(5));
                Console.WriteLine("pocet v zasobniku: " + stackEXEASTNode.Count);

                WhereExpression = stackEXEASTNode.Peek();
                stackEXEScope.Peek().AddCommand(new EXECommandQuerySelect(Cardinality, ClassName, VariableName, WhereExpression));
            }
            else
            {
                stackEXEScope.Peek().AddCommand(new EXECommandQuerySelect(Cardinality, ClassName, VariableName));
            }

            Console.WriteLine("ending execommandqueryselect");
            stackEXEASTNode.Clear();

            return null;
            //return base.VisitExeCommandQuerySelect(context);
        }


        public override object VisitExpr([NotNull] OALParser.ExprContext context)
        {

            Console.WriteLine("Expr: " + context.ChildCount);
            Console.WriteLine(context.GetChild(0).GetType().Name);
            
            if (context.ChildCount == 1)
            {
                stackEXEASTNode.Peek().AddOperand(new EXEASTNodeLeaf(context.GetChild(0).GetText()));
            }
            else if(context.ChildCount == 2)
            {
                EXEASTNodeComposite ast = new EXEASTNodeComposite(context.GetChild(0).GetText());
                stackEXEASTNode.Push(ast);
                
                base.VisitExpr(context);

                if (context.GetChild(0).GetType().Name.Contains("TerminalNode") && !context.GetChild(0).GetText().Equals("not "))
                {
                    stackEXEASTNode.Peek().AddOperand(new EXEASTNodeLeaf(context.GetChild(1).GetText()));
                    EXEASTNodeComposite temp = stackEXEASTNode.Pop();
                    stackEXEASTNode.Peek().AddOperand(temp);
                }
                else if(context.GetChild(0).GetText().Equals("not "))
                {
                    EXEASTNodeComposite temp = stackEXEASTNode.Pop();
                    stackEXEASTNode.Peek().AddOperand(temp);
                }
            }
            else if(context.ChildCount == 3)
            {
                if (!context.GetChild(0).GetText().Equals("("))
                {
                    EXEASTNodeComposite ast = new EXEASTNodeComposite(context.GetChild(1).GetText());
                    stackEXEASTNode.Push(ast);
                }

                base.VisitExpr(context);
                
                if (context.GetChild(0).GetType().Name.Contains("TerminalNode") && !context.GetChild(0).GetText().Equals("("))
                {
                    stackEXEASTNode.Peek().AddOperand(new EXEASTNodeLeaf(context.GetChild(0).GetText()));
                    stackEXEASTNode.Peek().AddOperand(new EXEASTNodeLeaf(context.GetChild(2).GetText()));
                    EXEASTNodeComposite temp = stackEXEASTNode.Pop();
                    stackEXEASTNode.Peek().AddOperand(temp);
                }
                else if (stackEXEASTNode.Count > 1 && !context.GetChild(0).GetText().Equals("("))
                {
                    EXEASTNodeComposite temp = stackEXEASTNode.Pop();
                    stackEXEASTNode.Peek().AddOperand(temp);
                }

            }

            return null;
            //return base.VisitExpr(context);
        }

    }
}
