//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from OAL.g4 by ANTLR 4.9.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="OALParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public interface IOALVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.lines"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLines([NotNull] OALParser.LinesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine([NotNull] OALParser.LineContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.commands"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCommands([NotNull] OALParser.CommandsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.parCommand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParCommand([NotNull] OALParser.ParCommandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.threadCommand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitThreadCommand([NotNull] OALParser.ThreadCommandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.ifCommand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfCommand([NotNull] OALParser.IfCommandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.elif"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElif([NotNull] OALParser.ElifContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.elseBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseBlock([NotNull] OALParser.ElseBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondition([NotNull] OALParser.ConditionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.whileCommand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileCommand([NotNull] OALParser.WhileCommandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.foreachCommand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForeachCommand([NotNull] OALParser.ForeachCommandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.continueCommand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitContinueCommand([NotNull] OALParser.ContinueCommandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.breakCommand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBreakCommand([NotNull] OALParser.BreakCommandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.commentCommand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCommentCommand([NotNull] OALParser.CommentCommandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.exeCommandQueryCreate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExeCommandQueryCreate([NotNull] OALParser.ExeCommandQueryCreateContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.exeCommandQueryDelete"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExeCommandQueryDelete([NotNull] OALParser.ExeCommandQueryDeleteContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.exeCommandAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExeCommandAssignment([NotNull] OALParser.ExeCommandAssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.exeCommandCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExeCommandCall([NotNull] OALParser.ExeCommandCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.exeCommandCreateList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExeCommandCreateList([NotNull] OALParser.ExeCommandCreateListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.listLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListLiteral([NotNull] OALParser.ListLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.exeCommandAddingToList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExeCommandAddingToList([NotNull] OALParser.ExeCommandAddingToListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.exeCommandRemovingFromList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExeCommandRemovingFromList([NotNull] OALParser.ExeCommandRemovingFromListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.exeCommandWrite"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExeCommandWrite([NotNull] OALParser.ExeCommandWriteContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.exeCommandRead"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExeCommandRead([NotNull] OALParser.ExeCommandReadContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.exeCommandWait"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExeCommandWait([NotNull] OALParser.ExeCommandWaitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.returnCommand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnCommand([NotNull] OALParser.ReturnCommandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr([NotNull] OALParser.ExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.accessChain"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAccessChain([NotNull] OALParser.AccessChainContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.accessChainPrefix"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAccessChainPrefix([NotNull] OALParser.AccessChainPrefixContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.accessChainElement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAccessChainElement([NotNull] OALParser.AccessChainElementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.methodCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodCall([NotNull] OALParser.MethodCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.parameterList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParameterList([NotNull] OALParser.ParameterListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.parameterListSuffix"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParameterListSuffix([NotNull] OALParser.ParameterListSuffixContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.bracketedExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBracketedExpr([NotNull] OALParser.BracketedExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.typeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeName([NotNull] OALParser.TypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.className"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassName([NotNull] OALParser.ClassNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.arrayType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayType([NotNull] OALParser.ArrayTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.variableName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableName([NotNull] OALParser.VariableNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.methodName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodName([NotNull] OALParser.MethodNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="OALParser.attribute"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAttribute([NotNull] OALParser.AttributeContext context);
}
