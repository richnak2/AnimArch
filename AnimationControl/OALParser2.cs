using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class OALParser2
    {
        static void Main(string[] args)
        {
            OALParser2 parser = new OALParser2();
            String Code = "relate o to s across Observer->Subject[R46];\n";
            List<String> tokens = parser.Tokenize(Code);

            int i = 1;
            foreach (String token in tokens)
            {
                Console.WriteLine(i++ + ": " + token);
            }

            Console.ReadLine();
        }

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
        public static List<String> UnaryOperators = new List<String>(new String[] { "not", "empty", "not_empty", "cardinality" });
        public static int LongestOperatorLength = 3;
        private static List<String> Keywords = new List<String>(new String[]
            {
             "create", "object", "instance", "of",
            "relate", "to", "across", 
            "select", "any", "many", "from", "instances", "where", "related", "by",
            "if", "elif", "else", "end",
            "par", "thread",
            "while",
            "for", "each", "in",
            "break", "continue"
            });
        private static List<String> SpecialChars = new List<String>(new String[] { "[", "]", "->", ";", "(", ")" });
        public List<String> Tokenize(String Code)
        {
            List<String> Tokens = new List<String>();
            String CurrentCommand = "";
            Boolean InString = false;
            foreach (char c in Code)
            {
                /*Console.WriteLine(c);
                Console.WriteLine("CurrentCommand: " + CurrentCommand);*/
                if (InString)
                {
                    Console.WriteLine(c + " : " + CurrentCommand + " : " + CurrentCommand.Last());
                    if (c == '"' && CurrentCommand.Last() != '\\')
                    {
                        InString = false;
                        CurrentCommand += c;
                        Tokens.Add(CurrentCommand);
                        CurrentCommand = "";
                    }
                    else
                    {
                        CurrentCommand += c;
                    }
                    continue;
                }

                //Console.WriteLine("Passed First Condition");

                if (c == '"')
                {
                    if (CurrentCommand.Any())
                    {
                        Tokens.Add(CurrentCommand);
                        CurrentCommand = "";
                    }

                    CurrentCommand += c;
                    InString = true;
                    continue;
                }


                if(IsBracket(c))
                {
                    if (CurrentCommand.Any())
                    {
                        Tokens.Add(CurrentCommand);
                        CurrentCommand = "";
                    }

                    Tokens.Add(c.ToString());

                    continue;
                }
                //Console.WriteLine("Passed Second Condition");

                if (Char.IsWhiteSpace(c))
                {
                    if (!"".Equals(CurrentCommand))
                    {
                        Tokens.Add(CurrentCommand);
                        CurrentCommand = "";
                    }
                    continue;
                }

                Console.WriteLine("Passed 3rd Condition");

                if (!CurrentCommand.Any())
                {
                    CurrentCommand += c;
                    continue;
                }


                /*Console.WriteLine("Passed 4th Condition");
                Console.WriteLine("Perhaps I'll append " + c + " after " + CurrentCommand.Last());
                Console.WriteLine(IsNameChar(c) + ", " + IsNameChar(CurrentCommand.Last()));*/
                if (IsNameChar(c) != IsNameChar(CurrentCommand.Last()))
                {
                    Tokens.Add(CurrentCommand);
                    CurrentCommand = c.ToString();
                    continue;
                }
                else if (IsNameChar(c) == IsNameChar(CurrentCommand.Last()))
                {
                    CurrentCommand += c;
                    continue;
                }

                //Console.WriteLine("Passed 5th Condition");
                throw new Exception("Uknown token");
            }

            if (!"".Equals(CurrentCommand))
            {
                Tokens.Add(CurrentCommand);
                //Console.WriteLine("Appended last token");
            }
            //Console.WriteLine("Not last token");

            /*int i = 1;
            foreach (String token in Tokens)
            {
                Console.WriteLine(i++ + ": " + token);
            }

            Console.ReadLine();*/

            return Tokens;
        }
        private Boolean IsBracket(char c)
        {
            return c == '(' || c == ')' || c == '[' || c == ']';
        }
        private Boolean IsNameChar(char c)
        {
            return Char.IsLetterOrDigit(c) || c == '_';
        }
        private List<OALCodeLine> ToLinesOfCode(String Code)
        {
            String[] Lines = Code.Split('\n');
            List<OALCodeLine> Result = new List<OALCodeLine>();
            int order = 1;
            foreach (String Line in Lines)
            {
                Result.Append(new OALCodeLine(order, Line));
                order++;
            }

            return Result;
        }
    }
}
