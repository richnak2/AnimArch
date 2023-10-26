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
using OALProgramControl;
using UnityEngine;

namespace AnimationControl.OAL
{
    public class OALVisitorConcrete : OALBaseVisitor<object>
    {
        public OALVisitorConcrete() { }

        private void HandleError(string errorMessage, ParserRuleContext context)
        {
            throw new Exception(string.Format("Error while processing '{0}'.\n-------------------\n{1}", errorMessage, context.GetText()));
        }

        // AccessChain
        public override object VisitAccessChain([NotNull] OALParser.AccessChainContext context)
        {
            if (context.methodCall() == null && context.NAME() == null)
            {
                HandleError("Visiting AccessChain - both 'methodCall' and 'NAME' are null.", context);
            }


            if (context.methodCall() != null && context.NAME() != null)
            {
                HandleError("Visiting AccessChain - both 'methodCall' and 'NAME' are not null.", context);
            }

            EXEASTNodeAccessChain accessChain = new EXEASTNodeAccessChain();

            VisitAccessChain(context, accessChain);

            if (!accessChain.GetElements().Any())
            {
                HandleError("Visiting AccessChain - the created access chain is empty.", context);
            }

            return accessChain;
        }
        private void VisitAccessChain([NotNull] OALParser.AccessChainContext context, EXEASTNodeAccessChain existingChain)
        {
            if (context.methodCall() != null)
            {
                existingChain.AddElement(VisitMethodCall(context.methodCall()) as EXEASTNodeMethodCall);
            }
            else if (context.NAME() != null)
            {
                existingChain.AddElement(VisitTerminal(context.NAME()) as EXEASTNodeLeaf);
            }

            if (context.accessChain() != null)
            {
                VisitAccessChain(context, existingChain);
            }
        }

        // Attribute - attribute name
        public override object VisitAttribute([NotNull] OALParser.AttributeContext context)
        {
            return VisitTerminal(context.NAME());
        }

        // Bracketed expression
        public override object VisitBracketedExpr([NotNull] OALParser.BracketedExprContext context)
        {
            if
            (
                !(
                    context.ChildCount == 3
                    && "(".Equals(context.GetChild(0).GetText())
                    && context.expr() != null
                    && ")".Equals(context.GetChild(2).GetText())
                )
            )
            {
                HandleError("Malformed bracketed expression.", context);
            }

            object expression = VisitExpr(context.expr());

            if (expression is not EXEASTNodeBase)
            {
                HandleError(string.Format("Bracketed expression contains '{0}' instead of EXEASTNodeBase.", expression?.GetType().Name ?? "NULL"), context);
            }

            return expression;
        }

        // Assignment command
        public override object VisitExeCommandAssignment([NotNull] OALParser.ExeCommandAssignmentContext context)
        {
            if (context.accessChain() == null || context.expr() == null)
            {
                HandleError("Malformed assignment command.", context);
            }
            
            EXEASTNodeAccessChain assignmentTarget = VisitAccessChain(context.accessChain()) as EXEASTNodeAccessChain;
            EXEASTNodeBase assignedExpression = VisitExpr(context.expr()) as EXEASTNodeBase;

            return new EXECommandAssignment(assignmentTarget, assignedExpression);
        }

        // Expression
        public override object VisitExpr([NotNull] OALParser.ExprContext context)
        {
            if (context.NUM() != null)
            {
                return VisitTerminal(context.NUM());
            }
            else if (context.BOOL() != null)
            {
                return VisitTerminal(context.BOOL());
            }
            else if (context.STRING() != null)
            {
                return VisitTerminal(context.STRING());
            }
            else if (context.accessChain() != null)
            {
                return VisitAccessChain(context.accessChain());
            }
            else if (context.bracketedExpr() != null)
            {
                return VisitBracketedExpr(context.bracketedExpr());
            }
            // Unary operator
            else if (context.ChildCount == 2)
            {
                string _operator = context.GetChild(0).GetText();

                if (!EXETypes.IsUnaryOperator(_operator))
                {
                    HandleError("Malformed expression - unknown unary operator.", context);
                }

                object operand = Visit(context.GetChild(1));

                if (operand is not EXEASTNodeBase)
                {
                    HandleError(string.Format("Malformed expression - operand of unary operator is not EXEASTNodeBase, instead it is '{0}'.", operand?.GetType().Name ?? "NULL"), context);
                }

                return new EXEASTNodeComposite(_operator, new EXEASTNodeBase[] { operand as EXEASTNodeBase });
            }
            // Binary operator
            else if (context.ChildCount == 3)
            {
                string _operator = context.GetChild(1).GetText();

                if (!EXETypes.IsBinaryOperator(_operator))
                {
                    HandleError("Malformed expression - unknown binary operator.", context);
                }

                object operand1 = Visit(context.GetChild(0));

                if (operand1 is not EXEASTNodeBase)
                {
                    HandleError(string.Format("Malformed expression - operand of binary operator is not EXEASTNodeBase, instead it is '{0}'.", operand1?.GetType().Name ?? "NULL"), context);
                }

                object operand2 = Visit(context.GetChild(0));

                if (operand2 is not EXEASTNodeBase)
                {
                    HandleError(string.Format("Malformed expression - operand of binary operator is not EXEASTNodeBase, instead it is '{0}'.", operand2?.GetType().Name ?? "NULL"), context);
                }

                return new EXEASTNodeComposite(_operator, new EXEASTNodeBase[] { operand1 as EXEASTNodeBase, operand2 as EXEASTNodeBase });
            }
            else
            {
                HandleError("Malformed expression - generic error.", context);
                return null;
            }
        }

        // Single command
        public override object VisitLine([NotNull] OALParser.LineContext context)
        {
            if (context.ChildCount != 1)
            {
                HandleError("Line contains invalid count of children.", context);
            }

            return Visit(context.GetChild(0));
        }

        // A series of commands
        public override object VisitLines([NotNull] OALParser.LinesContext context)
        {
            EXEScopeMethod methodScope = new EXEScopeMethod();

            object parsedChild = null;

            for (int i = 0; i < context.children.Count - 1; i++)
            {
                parsedChild = Visit(context.children[i]);

                if (parsedChild is EXEComment)
                {
                    continue;
                }

                if (parsedChild is EXECommand)
                {
                    methodScope.AddCommand(parsedChild as EXECommand);
                    continue;
                }

                HandleError(string.Format("Lines contain something that is not a command - '{0}'.", context.children[i].GetText()), context);
            }

            if (!"EOF".Equals(context.children.Last()))
            {
                HandleError(string.Format("Last element in lines is something other than EOF - '{0}'.", context.children.Last().GetText()), context);
            }

            return methodScope;
        }

        // Visit terminal node - most likely a primitive literal or variable/attribute name
        public override object VisitTerminal(ITerminalNode node)
        {
            return new EXEASTNodeLeaf(node.GetText());
        }
    }
}