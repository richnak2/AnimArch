using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using AnimationControl.OAL;

namespace AnimationControl
{
    public class OALParser2
    {
        static void Main(string[] args)
        {

            try
            {
                string oalexample = "create object instance observer1 of Observer;\n" +
                                    "create object instance observer2 of Observer;\n";

                ICharStream target = new AntlrInputStream(oalexample);
                ITokenSource lexer = new OALLexer(target);
                ITokenStream tokens = new CommonTokenStream(lexer);
                OALParser parser = new OALParser(tokens);
                parser.BuildParseTree = true;

                //ExprParser.LiteralContext result = parser.literal();
                OALParser.LinesContext result = parser.lines();
                Console.Write(result.ToStringTree());
                Console.WriteLine();

                OALVisitor2 test = new OALVisitor2();

                test.VisitLines(result);

                EXEScope e = test.e;

                Console.WriteLine("Overenie parsovania:");
                Console.WriteLine(((EXECommandQueryCreate)e.Commands[0]).ClassName);
                Console.WriteLine(((EXECommandQueryCreate)e.Commands[0]).ReferencingVariableName);

                Console.WriteLine(((EXECommandQueryCreate)e.Commands[1]).ClassName);
                Console.WriteLine(((EXECommandQueryCreate)e.Commands[1]).ReferencingVariableName);

                /*Console.WriteLine(((EXECommandQueryCreate)e.Commands[2]).ClassName);
                Console.WriteLine(((EXECommandQueryCreate)e.Commands[2]).ReferencingVariableName);*/

                //List<CreateQuery> list = new List<CreateQuery>();
                //list = (List<CreateQuery>)test.VisitLines(result);

                /*foreach (CreateQuery item in list)
                {
                    Console.WriteLine("-> " + item.ClassName + " " + item.InstanceName);

                }
                list.ForEach(Console.WriteLine);*/

                //  var htmlChat = new Test();
                // antlr4.tree.ParseTreeWalker.DEFAULT.walk(htmlChat, result);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
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
              
                if (Char.IsWhiteSpace(c))
                {
                    if (!"".Equals(CurrentCommand))
                    {
                        Tokens.Add(CurrentCommand);
                        CurrentCommand = "";
                    }
                    continue;
                }

                if (!CurrentCommand.Any())
                {
                    CurrentCommand += c;
                    continue;
                }

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

                throw new Exception("Uknown token");
            }

            if (!"".Equals(CurrentCommand))
            {
                Tokens.Add(CurrentCommand);
            }

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
    }
}
