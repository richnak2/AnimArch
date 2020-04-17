using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using AnimationControl.OAL;

namespace OALProgramControl
{
    public class OALParser2
    {
        static void Main(string[] args)
        {

            try
            {
                string oalexample = "relate subject to observer across R15;\n" +
                                    "relate dog to owner across R7;\n" +
                                    "select any dog from instances of Dog;\n" +
                                    "select many all_dogs from instances of Dog;\n" +
                                    "select any young_dog from instances of Dog where selected.age < 5 OR 6;\n" +
                                    "select any young_dog from instances of Dog where cardinality observers and not x > 5 or not_empty g;" +
                                    "select any dog from instances of Dog where x == (1+2)*3;" +
                                    "select any dog from instances of Dog where (1 /(obj1.x * 0.22 + object1.y * 0.55 - object1.z /2) == \"ahoj\" + \"te deti\" or (x+3 != y%4+2)) and (x<=1 or x+1>=y or obj1.x<2 or obj1.y>1.2 * cardinality observers) and (empty k or not_empty g) and not x>5;" +
                                    "create object instance of Visitor;" +
                                    "create object instance myUserAccount of UserAccount;" +
                                    "unrelate subject from observer across R15;" +
                                    "unrelate dog from owner across R7;" +
                                    "delete object instance current_user;" +
                                    "delete object instance observer3;" +
                                    "x = 15.0 * y;";

                OALParser parser = null;
                try
                {
                    ICharStream target = new AntlrInputStream(oalexample);
                    ITokenSource lexer = new OALLexer(target);
                    ITokenStream tokens = new CommonTokenStream(lexer);
                    parser = new OALParser(tokens);
                    parser.BuildParseTree = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                }

                //ExprParser.LiteralContext result = parser.literal();
                OALParser.LinesContext result = parser.lines();
                Console.Write(result.ToStringTree());
                Console.WriteLine();

                OALVisitor2 test = new OALVisitor2();

                try
                {
                    test.VisitLines(result);
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                    
                }

                EXEScope e = test.globalExeScope;

                Console.WriteLine(e.ToCode());

                Console.WriteLine("Overenie parsovania:");
                Console.WriteLine("Prvy command:");
                Console.WriteLine(((EXECommandQueryRelate)e.Commands[0]).Variable1Name);
                Console.WriteLine(((EXECommandQueryRelate)e.Commands[0]).Variable2Name);
                Console.WriteLine(((EXECommandQueryRelate)e.Commands[0]).RelationshipName);
                Console.WriteLine("\nDruhy command:");
                Console.WriteLine(((EXECommandQueryRelate)e.Commands[1]).Variable1Name);
                Console.WriteLine(((EXECommandQueryRelate)e.Commands[1]).Variable2Name);
                Console.WriteLine(((EXECommandQueryRelate)e.Commands[1]).RelationshipName);
                Console.WriteLine("\nTreti command:");
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[2]).Cardinality);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[2]).VariableName);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[2]).ClassName);
                Console.WriteLine("\nStvrty command:");
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[3]).Cardinality);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[3]).VariableName);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[3]).ClassName);
                Console.WriteLine("\nPiaty command:");
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[4]).Cardinality);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[4]).VariableName);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[4]).ClassName);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[4]).WhereCondition.ToCode());
                Console.WriteLine("\nSiesty command:");
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[5]).Cardinality);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[5]).VariableName);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[5]).ClassName);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[5]).WhereCondition.ToCode());
                Console.WriteLine("\nSiedmy command:");
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[6]).Cardinality);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[6]).VariableName);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[6]).ClassName);
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[6]).WhereCondition.ToCode());
                Console.WriteLine("\nOsmy command:");
                Console.WriteLine(((EXECommandQuerySelect)e.Commands[7]).ToCode());
                //Console.WriteLine(((EXECommandQuerySelect)e.Commands[7]).VariableName);
                //Console.WriteLine(((EXECommandQuerySelect)e.Commands[7]).ClassName);
                //Console.WriteLine(((EXECommandQuerySelect)e.Commands[7]).WhereCondition.ToCode());
                Console.WriteLine("\nDeviaty command:");
                Console.WriteLine(((EXECommandQueryCreate)e.Commands[8]).ClassName);
                Console.WriteLine("\nDesiaty command:");
                Console.WriteLine(((EXECommandQueryCreate)e.Commands[9]).ClassName);
                Console.WriteLine(((EXECommandQueryCreate)e.Commands[9]).ReferencingVariableName);
                Console.WriteLine("\nJedenasty command:");
                Console.WriteLine(((EXECommandQueryUnrelate)e.Commands[10]).Variable1Name);
                Console.WriteLine(((EXECommandQueryUnrelate)e.Commands[10]).Variable2Name);
                Console.WriteLine(((EXECommandQueryUnrelate)e.Commands[10]).RelationshipName);
                Console.WriteLine("\nDvanasty command:");
                Console.WriteLine(((EXECommandQueryUnrelate)e.Commands[11]).Variable1Name);
                Console.WriteLine(((EXECommandQueryUnrelate)e.Commands[11]).Variable2Name);
                Console.WriteLine(((EXECommandQueryUnrelate)e.Commands[11]).RelationshipName);
                Console.WriteLine("\nTrinasty command:");
                Console.WriteLine(((EXECommandQueryDelete)e.Commands[12]).VariableName);
                Console.WriteLine("\nStrnasty command:");
                Console.WriteLine(((EXECommandQueryDelete)e.Commands[13]).VariableName);
                Console.WriteLine("\nPatnasty command:");
                Console.WriteLine(((EXECommandAssignment)e.Commands[14]).VariableName);
                Console.WriteLine(((EXECommandAssignment)e.Commands[14]).AssignedExpression.ToCode());

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


            EXEASTNodeComposite C = new EXEASTNodeComposite(
                "+",
                new EXEASTNode[]
                {
                    new EXEASTNodeLeaf("6"),
                    new EXEASTNodeComposite
                    (
                        "-",
                        new EXEASTNode[]
                        {
                            new EXEASTNodeLeaf("1"),
                            new EXEASTNodeLeaf("2")
                        }
                    )
                }
            );
            Console.WriteLine(C.ToCode());
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
            new List<String> (new String[] { "==", "!="}),
            new List<String> (new String[] { "not"}),
            new List<String> (new String[] { "empty", "not_empty"}),
            new List<String> (new String[] { "<=", ">=", "<", ">" }),
            new List<String> (new String[] { "+", "-"}),
            new List<String> (new String[] { "*", "/", "%"}),
            new List<String> (new String[] { "cardinality"}),
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
        public static int GetOperatorLevel(String Operator)
        {
            for (int i = 0; i < LeveledOperators.Count; i++)
            {
                if (LeveledOperators[i].Contains(Operator))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
