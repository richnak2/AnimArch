using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class OALCommandParser
    {
        public static List<String> ArithmeticOperatorList = new List<String>(new String[] { "+", "-", "*", "/", "%" });
        // Do not forget that each operator ("<=") needs to be sooner than its substrings ("<")
        public static List<String> LogicalOperatorList = new List<String>(new String[] { "==", "!=", "<=", ">=", "and", "or", "not", "<", ">" });
        public static String AccessOperator = ".";
        public static String AssignmentOperator = "=";
        public static List<String> AllOperators = new List<String>(new String[] { "-", "+", "*", "/", "%", "==", "!=", "<", ">", "<=", ">=", "and", "or", "not", ".", "=" });
        public static List<List<String>> LeveledOperators = new List<List<String>>(new List<String>[] {
            new List<String> (new String[] { "and", "or"}),
            new List<String> (new String[] { "not"}),
            new List<String> (new String[] { "==", "!="}),
            new List<String> (new String[] { "<=", ">=", "<", ">" }),
            new List<String> (new String[] { "+", "-"}),
            new List<String> (new String[] { "*", "/", "%"}),
            new List<String> (new String[] { AccessOperator})
        });
        public static List<String> UnaryOperators = new List<String>(new String[] { "not" });
        public static int LongestOperatorLength = 3;

        public static String VariableNameChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789_#";

        public static List<String> ControlKeywords = new List<String>(new String[] { "break", "continue", "return" });

        public EXEASTNode ConstructAST(String ExpressionCommand)
        {
            EXEASTNode AST = null;
            EXEQueryChecker QueryChecker = new EXEQueryChecker();

            String ClearedExpressionCommand = EXEParseUtil.SqueezeWhiteSpace(ExpressionCommand);
            // Bollock - think about (5 + 6) * (5 + 7)
            /*while ("(".Equals(ClearedExpressionCommand[0]) && ")".Equals(ClearedExpressionCommand[ExpressionCommand.Length - 1]))
            {
                ClearedExpressionCommand = ClearedExpressionCommand.Substring(1, ClearedExpressionCommand.Length - 2);
            }*/

            if ("\"".Equals(ClearedExpressionCommand[0]) && "\"".Equals(ClearedExpressionCommand[ExpressionCommand.Length - 1]))
            {
                AST = new EXEASTNodeLeaf(ExpressionCommand, false, false, false);
            }
            //First go for query
            else if (QueryChecker.IsQuery(ClearedExpressionCommand))
            {
                AST = QueryChecker.ConstructQueryAST(ClearedExpressionCommand);
            }
            //Then go for top level commands -> control (return, break, continue)
            else if (IsControlCommand(ClearedExpressionCommand))
            {
                AST = ConstructControlCommandAST(ClearedExpressionCommand);
            }
            //Then go for top level commands -> assign
            else if (IsAssignment(ClearedExpressionCommand))
            {
                AST = ConstructAssignmentCommandAST(ClearedExpressionCommand);
            }
            //Then if it has operator, it is an expression and needs to be treated as such
            else if (ContainsOperator(ExpressionCommand))
            {
                AST = ConstructExpressionAST(ExpressionCommand);
            }
            //If we got here, we have leaf node -> variable/attribute/method name or literal value
            else
            {
                AST = new EXEASTNodeLeaf(EXEParseUtil.SqueezeWhiteSpace(ExpressionCommand));
            }

            return AST;
        }

        private EXEASTNode ConstructControlCommandAST(String Command)
        {
            EXEASTNode AST = null;
            if (!IsControlCommand(Command))
            {
                return AST;
            }

            String SanitizedCommand = EXEParseUtil.SqueezeWhiteSpace(Command);
            String[] CommandTokens = SanitizedCommand.Split(' ');

            if (CommandTokens[0] == "continue" || CommandTokens[0] == "break")
            {
                AST = new EXEASTNodeLeaf(CommandTokens[0], false, false, false);
            }
            else if (CommandTokens[0] == "return")
            {
                EXEASTNodeComposite TempAST = new EXEASTNodeComposite("return");
                TempAST.AddOperand(ConstructAST(CommandTokens[1]));
            }

            return AST;
        }

        private EXEASTNodeComposite ConstructAssignmentCommandAST(String Command)
        {
            EXEASTNodeComposite AST = null;
            if (!IsAssignment(Command))
            {
                return AST;
            }

            String SanitizedCommand = EXEParseUtil.SqueezeWhiteSpace(Command);
            Console.WriteLine("ConstrAssign:" + SanitizedCommand + "EOL");
            String[] CommandTokens = SanitizedCommand.Split(' ');

            AST = new EXEASTNodeComposite("=");
            AST.AddOperand(ConstructAST(CommandTokens[0]));
            AST.AddOperand(ConstructAST(string.Join("", CommandTokens.Skip(2).ToArray())));

            return AST;
        }

        //We cannot get String here -> only if it is operand of something else
        private EXEASTNode ConstructExpressionAST(String Command)
        {
            Console.WriteLine("ConstrExpress:" + Command + "EOL");
            EXEASTNode AST = null;
            if (Command.Length == 0)
            {
                return AST;
            }

            if (!ContainsOperator(Command))
            {
                AST = new EXEASTNodeLeaf(EXEParseUtil.SqueezeWhiteSpace(Command));
                return AST;
            }

            String SanitizedCommand = EXEParseUtil.SqueezeWhiteSpace(Command);
            Console.WriteLine("|" + SanitizedCommand + "|");

            Stack<String> TopLevelBracketChunks = TokenizeTopLevelBracketChunks(SanitizedCommand);


            if (TopLevelBracketChunks.Count == 1)
            {
                Console.WriteLine("OnlyOneBracketChunk:" + TopLevelBracketChunks.Peek() + "EOL");
                Stack<(String, int)> TopLevelOperatorStack = new Stack<(String, int)>(IdentifyTopLevelOperators(SanitizedCommand));
                if (IsUnaryOperator(TopLevelOperatorStack.Peek().Item1))
                {
                    EXEASTNodeComposite ASTemp = new EXEASTNodeComposite(TopLevelOperatorStack.Peek().Item1);
                    ASTemp.AddOperand(ConstructExpressionAST(SanitizedCommand.Substring(
                        TopLevelOperatorStack.Peek().Item2 + TopLevelOperatorStack.Peek().Item1.Length
                        )));
                    AST = ASTemp;
                }
                else
                {
                    Console.WriteLine("OnlyOneBracketChunk,NotUnary:" + TopLevelBracketChunks.Peek() + "EOL");
                    int PreviousOperatorIndex = SanitizedCommand.Length;
                    EXEASTNode RightOperand = null;
                    EXEASTNode LeftOperand = null;
                    EXEASTNodeComposite ASTemp = null;
                    (String, int) CurrentOperator = (null, 0);
                    while (TopLevelOperatorStack.Count > 0)
                    {
                        CurrentOperator = TopLevelOperatorStack.Pop();

                        if (RightOperand == null)
                        {
                            RightOperand = ConstructExpressionAST(SanitizedCommand.Substring(
                                CurrentOperator.Item2 + CurrentOperator.Item1.Length,
                                PreviousOperatorIndex - (CurrentOperator.Item2 + CurrentOperator.Item1.Length))
                            );
                        }
                        else
                        {
                            LeftOperand = ConstructExpressionAST(SanitizedCommand.Substring(
                                CurrentOperator.Item2 + CurrentOperator.Item1.Length,
                                PreviousOperatorIndex - (CurrentOperator.Item2 + CurrentOperator.Item1.Length))
                            );
                            ASTemp = new EXEASTNodeComposite(CurrentOperator.Item1);
                            ASTemp.AddOperand(LeftOperand);
                            ASTemp.AddOperand(RightOperand);
                            RightOperand = ASTemp;
                        }

                        PreviousOperatorIndex = CurrentOperator.Item2;
                    }

                    LeftOperand = ConstructExpressionAST(SanitizedCommand.Substring(0, PreviousOperatorIndex));
                    ASTemp = new EXEASTNodeComposite(CurrentOperator.Item1);
                    ASTemp.AddOperand(LeftOperand);
                    ASTemp.AddOperand(RightOperand);
                    AST = ASTemp;
                }
            }
            else if (IsUnaryOperator(EXEParseUtil.SqueezeWhiteSpace(TopLevelBracketChunks.Pop())))
            {
                AST = ConstructExpressionAST(TopLevelBracketChunks.Pop());
            }
            else
            {
                while (TopLevelBracketChunks.Any())
                {
                    Console.WriteLine("TLBC-PeekOuter:" + TopLevelBracketChunks.Peek() + "EOL");
                    if (AST == null)
                    {
                        Console.WriteLine("TLBC");
                        Console.WriteLine("TLBC-Peek1:" + TopLevelBracketChunks.Peek() + "EOL");
                        EXEASTNode RightOperand = ConstructExpressionAST(TopLevelBracketChunks.Pop());
                        Console.WriteLine("TLBC2");
                        Console.WriteLine("TLBC-Peek2:" + TopLevelBracketChunks.Peek() + "EOL");
                        String Operator = EXEParseUtil.SqueezeWhiteSpace(TopLevelBracketChunks.Pop());
                        EXEASTNode LeftOperand = ConstructExpressionAST(TopLevelBracketChunks.Pop());

                        EXEASTNodeComposite ASTemp = new EXEASTNodeComposite(Operator);
                        ASTemp.AddOperand(LeftOperand);
                        ASTemp.AddOperand(RightOperand);
                        AST = ASTemp;
                    }
                    else
                    {
                        String Operator = EXEParseUtil.SqueezeWhiteSpace(TopLevelBracketChunks.Pop());
                        EXEASTNode LeftOperand = ConstructExpressionAST(TopLevelBracketChunks.Pop());

                        EXEASTNodeComposite ASTemp = new EXEASTNodeComposite(Operator);
                        ASTemp.AddOperand(LeftOperand);
                        ASTemp.AddOperand(AST);
                        AST = ASTemp;
                    }
                }
            }

            return AST;
        }

        public Stack<String> TokenizeTopLevelBracketChunks(String Command)
        {
            Console.WriteLine("TokenizeBrackets:" + Command + "EOL");

            Stack<String> Result = new Stack<String>();

            int CurrentBracketLevel = 0;
            StringBuilder CurrentChunkBuilder = new StringBuilder();
            String SanitizedCommand = EXEParseUtil.SqueezeWhiteSpace(Command);
            Boolean InString = false;

            foreach (char c in SanitizedCommand)
            {
                if (c == '"')
                {
                    InString = !InString;
                    CurrentChunkBuilder.Append(c);
                    continue;
                }

                if (InString)
                {
                    CurrentChunkBuilder.Append(c);
                    continue;
                }

                if (c == '(')
                {
                    CurrentBracketLevel++;
                    if (CurrentBracketLevel > 1)
                    {
                        CurrentChunkBuilder.Append(c);
                    }
                    else if(CurrentChunkBuilder.Length != 0)
                    {
                        Result.Push(CurrentChunkBuilder.ToString());
                        CurrentChunkBuilder.Clear();
                    }
                    continue;
                }

                if (c == ')')
                {
                    CurrentBracketLevel--;
                    if (CurrentBracketLevel > 0)
                    {
                        CurrentChunkBuilder.Append(c);
                    }
                    else
                    {
                        Result.Push(CurrentChunkBuilder.ToString());
                        CurrentChunkBuilder.Clear();
                    }
                    continue;
                }

                CurrentChunkBuilder.Append(c);
            }
            if (CurrentChunkBuilder.Length != 0)
            {
                Result.Push(CurrentChunkBuilder.ToString());
            }

            return Result;
        }
        private List<(String, int)> IdentifyTopLevelOperators(String Command)
        {
            String[] DoubleQuoteTokens = Command.Split('"');
             int MinOperatorLevel = int.MaxValue;
             int CummulativeLength = 0;
             // Operator, index to DoubleQuoteTokens[current]
             List<(String, int)> TopLevelOperators = new List<(String, int)>();
             for (int i = 0; i < DoubleQuoteTokens.Length; i++)
             {
                 if (i % 2 == 0)
                 {
                     int CurrentOperatorLevel = 0;
                     foreach (List<String> CurrentLevelOperators in LeveledOperators)
                     {
                         ++CurrentOperatorLevel;
                         if (CurrentOperatorLevel > MinOperatorLevel)
                         {
                             break;
                         }
                         foreach (String Operator in CurrentLevelOperators)
                         {
                             List<int> Indexes = AllIndexesOf(DoubleQuoteTokens[i], Operator);
                             if (Indexes.Count <= 0)
                             {
                                 continue;
                             }

                             if (CurrentOperatorLevel < MinOperatorLevel)
                             {
                                 TopLevelOperators.Clear();
                                 MinOperatorLevel = CurrentOperatorLevel;
                             }

                             foreach (int Index in Indexes)
                             {
                                 TopLevelOperators.Add((Operator, CummulativeLength + Index));
                             }
                         }
                     }
                 }

                 CummulativeLength += DoubleQuoteTokens[i].Length;
             }

            return TopLevelOperators;
        }
        public static Boolean IsValidName(String Name)
        {
            Boolean Result = true;
            if (Name == null)
            {
                return false;
            }
            foreach (char c in Name)
            {
                if (!VariableNameChars.Contains(c))
                {
                    Result = false;
                }
            }
            if (Name.Length == 0)
            {
                Result = false;
            }
            return Result;
        }

        public static Boolean IsValidRelationshipName(String Name)
        {
            if (Name == null || Name.Length == 0)
            {
                return false;
            }

            if (!'R'.Equals(Name[0]))
            {
                return false;
            }

            if ('0'.Equals(Name[1]))
            {
                return false;
            }

            if (!int.TryParse(Name.Substring(1), out _))
            {
                return false;
            }

            return false;
        }
        public Boolean CanBeOperator(String Operator)
        {
            Boolean Result = IsOperator(Operator);
            if (Result)
            {
                return Result;
            }

            foreach (String ExistingOperator in AllOperators)
            {
                if (ExistingOperator.Length >= Operator.Length)
                {
                    if (ExistingOperator.Substring(0, Operator.Length).Equals(Operator))
                    {
                        Result = true;
                        break;
                    }
                }
            }

            return Result;
        }
        public Boolean IsOperator(String String)
        {
            Boolean Result = false;
            foreach (String Operator in AllOperators)
            {
                if (String.Contains(Operator))
                {
                    Result = true;
                    break;
                }
            }
            return Result;
        }

        private Boolean IsControlCommand (String Command)
        {
            String SanitizedCommand = EXEParseUtil.SqueezeWhiteSpace(Command);
            String[] CommandTokens = SanitizedCommand.Split(' ');
            Boolean result = false;
            if (ControlKeywords.Contains(CommandTokens[0]))
            {
                result = true;
            }
            return result;
        }
        private Boolean IsAssignment(String Command)
        {
            Boolean Result = false;
            //This means we have assignment
            if (!Command.Contains("==") && Command.Contains("="))
            {
                Result = true;
            }
            return Result;
        }
        // Beware of strings
        private Boolean ContainsOperator(String ExpressionCommand)
        {
            Boolean Result = false;
            Boolean InString = false;

            foreach (char c in ExpressionCommand)
            {
                if (c == '"')
                {
                    InString = !InString;
                    continue;
                }

                if (InString)
                {
                    continue;
                }

                if (IsOperator(ExpressionCommand))
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }

        private Boolean ContainsBrackets(String ExpressionCommand)
        {
            Boolean Result = false;
            Boolean InString = false;

            foreach (char c in ExpressionCommand)
            {
                if (c == '"')
                {
                    InString = !InString;
                    continue;
                }

                if (InString)
                {
                    continue;
                }

                if (c == '(' || c == ')')
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }

        private Boolean ContainsString(String Command)
        {
            Boolean Result = false;
            if (Command.Contains("\""))
            {
                Result = true;
            }
            return Result;
        }

        // https://stackoverflow.com/questions/2641326/finding-all-positions-of-substring-in-a-larger-string-in-c-sharp
        private List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return null;
            }
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        private Boolean IsUnaryOperator(String Operator)
        {
            String SanitizedCommand = EXEParseUtil.SqueezeWhiteSpace(Operator);
            Boolean Result = UnaryOperators.Contains(SanitizedCommand);
            return Result;
        }
    }
}
