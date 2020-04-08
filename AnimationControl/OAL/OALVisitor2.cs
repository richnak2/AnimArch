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
                //Console.WriteLine(context.ChildCount);
                if (child.GetType().ToString().Contains("KeyLetter"))
                {
                    ClassName = ParseUtil.StripWhiteSpace((child.GetText()));
                    Console.WriteLine("Class name---" + ClassName + "---");
                }

                if (child.GetType().ToString().Contains("InstanceHandle"))
                {
                    InstanceName = ParseUtil.StripWhiteSpace((child.GetText()));
                    Console.WriteLine("Instance name---" + InstanceName + "---");
                }
                
            }
            
            if (InstanceName != null)
            {
                stackEXEScope.Peek().AddCommand(new EXECommandQueryCreate(ClassName, InstanceName));
            }
            else
            {
                stackEXEScope.Peek().AddCommand(new EXECommandQueryCreate(ClassName));
            }
            
            return null;
        }


        public override object VisitExeCommandQueryRelate([NotNull] OALParser.ExeCommandQueryRelateContext context)
        {
            String VariableName1 = context.GetChild(1).GetText();
            String VariableName2 = context.GetChild(3).GetText();
            String RelationshipName = context.GetChild(5).GetText();

            //Console.WriteLine(VariableName1);
            //Console.WriteLine(VariableName2);
            //Console.WriteLine(RelationshipName);

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
                //Console.WriteLine("where");
                Visit(context.GetChild(5));
                //Console.WriteLine("pocet v zasobniku: " + stackEXEASTNode.Count);

                WhereExpression = stackEXEASTNode.Peek();
                stackEXEScope.Peek().AddCommand(new EXECommandQuerySelect(Cardinality, ClassName, VariableName, WhereExpression));
            }
            else
            {
                stackEXEScope.Peek().AddCommand(new EXECommandQuerySelect(Cardinality, ClassName, VariableName));
            }

            //Console.WriteLine("ending execommandqueryselect");
            stackEXEASTNode.Clear();

            return null;
            //return base.VisitExeCommandQuerySelect(context);
        }


        public override object VisitExeCommandQueryUnrelate([NotNull] OALParser.ExeCommandQueryUnrelateContext context)
        {
            String VariableName1 = context.GetChild(1).GetText();
            String VariableName2 = context.GetChild(3).GetText();
            String RelationshipName = context.GetChild(5).GetText();

            stackEXEScope.Peek().AddCommand(new EXECommandQueryUnrelate(VariableName1, VariableName2, RelationshipName));

            return null;
            //return base.VisitExeCommandQueryUnrelate(context);
        }


        public override object VisitExeCommandQueryDelete([NotNull] OALParser.ExeCommandQueryDeleteContext context)
        {
            String VariableName = context.GetChild(1).GetText();

            stackEXEScope.Peek().AddCommand(new EXECommandQueryDelete(VariableName));

            return null;
            //return base.VisitExeCommandQueryDelete(context);
        }


        public override object VisitExeCommandAssignment([NotNull] OALParser.ExeCommandAssignmentContext context)
        {//TODO priradenie atributu
            String VariableName = context.GetChild(0).GetText().Equals("assign ") ? context.GetChild(1).GetText() : context.GetChild(0).GetText();

            _ = context.GetChild(0).GetText().Equals("assign ") ? Visit(context.GetChild(3)) : Visit(context.GetChild(2));

            EXEASTNode expression = stackEXEASTNode.Peek();
            stackEXEScope.Peek().AddCommand(new EXECommandAssignment(VariableName, expression));

            stackEXEASTNode.Clear();

            return null;
            //return base.VisitExeCommandAssignment(context);
        }


        public override object VisitExpr([NotNull] OALParser.ExprContext context)
        {

            //Console.WriteLine("Expr: " + context.ChildCount);
            //Console.WriteLine(context.GetChild(0).GetType().Name);
            
            if (context.ChildCount == 1)
            {
                stackEXEASTNode.Peek().AddOperand(new EXEASTNodeLeaf(ParseUtil.StripWhiteSpace(context.GetChild(0).GetText())));
            }
            else if(context.ChildCount == 2)
            {               
                EXEASTNodeComposite ast = new EXEASTNodeComposite(ParseUtil.StripWhiteSpace(context.GetChild(0).GetText()));
                stackEXEASTNode.Push(ast);
                
                base.VisitExpr(context);

                if (context.GetChild(0).GetType().Name.Contains("TerminalNode") && !context.GetChild(0).GetText().Equals("not "))
                {
                    stackEXEASTNode.Peek().AddOperand(new EXEASTNodeLeaf(ParseUtil.StripWhiteSpace(context.GetChild(1).GetText())));
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
                    EXEASTNodeComposite ast = new EXEASTNodeComposite(ParseUtil.StripWhiteSpace(context.GetChild(1).GetText()));
                    stackEXEASTNode.Push(ast);
                }

                base.VisitExpr(context);
                
                if (context.GetChild(0).GetType().Name.Contains("TerminalNode") && !context.GetChild(0).GetText().Equals("("))
                {
                    stackEXEASTNode.Peek().AddOperand(new EXEASTNodeLeaf(ParseUtil.StripWhiteSpace(context.GetChild(0).GetText())));
                    stackEXEASTNode.Peek().AddOperand(new EXEASTNodeLeaf(ParseUtil.StripWhiteSpace(context.GetChild(2).GetText())));
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


        public override object VisitExeCommandQuerySelectRelatedBy([NotNull] OALParser.ExeCommandQuerySelectRelatedByContext context)
        {
            String Cardinality = context.GetChild(0).GetText().Contains("many") ? "many" : "any";
            Console.WriteLine("Cardinality = " + Cardinality);

            String VariableName = context.GetChild(1).GetText();
            Console.WriteLine("VariableName = " + VariableName);

            String StartingVariable = context.GetChild(3).GetText();
            Console.WriteLine("StartingVariable = " + StartingVariable);

            String ClassName = context.GetChild(5).GetText();
            Console.WriteLine("ClassName = " + ClassName);

            String RelationshipName = context.GetChild(6).GetText().Replace('[',' ').Replace(']',' ').Trim();
            Console.WriteLine("RelationshipName = " + RelationshipName);

            List<EXERelationshipLink> list = new List<EXERelationshipLink>();
            EXERelationshipLink eXERelationshipLink = new EXERelationshipLink(RelationshipName, ClassName);
            EXERelationshipSelection eXERelationshipSelection = new EXERelationshipSelection(StartingVariable);
            eXERelationshipSelection.AddRelationshipLink(eXERelationshipLink);
            EXEASTNode WhereExpression = null;

            int i = 7;
            while (context.GetChild(i).GetText().Equals("->"))
            {
                String ClassName2 = context.GetChild(i + 1).GetText();
                Console.WriteLine("ClassName = " + ClassName2);

                String RelationshipName2 = context.GetChild(i + 2).GetText().Replace('[', ' ').Replace(']', ' ').Trim();
                Console.WriteLine("RelationshipName = " + RelationshipName2);

                EXERelationshipLink eXERelationshipLink2 = new EXERelationshipLink(RelationshipName2, ClassName2);
                eXERelationshipSelection.AddRelationshipLink(eXERelationshipLink2);
                i += 3;
            }

            Console.WriteLine("Where 3= " + context.GetChild(context.ChildCount - 3).GetText());
            if (context.GetChild(context.ChildCount - 3).GetText().Contains("where"))
            {
                Visit(context.GetChild(context.ChildCount - 2));
                WhereExpression = stackEXEASTNode.Peek();
            }
            stackEXEScope.Peek().AddCommand(new EXECommandQuerySelectRelatedBy(Cardinality, VariableName, WhereExpression, eXERelationshipSelection));

            stackEXEASTNode.Clear();

            return null;
            //return base.VisitExeCommandQuerySelectRelatedBy(context);
        }
    }
}
