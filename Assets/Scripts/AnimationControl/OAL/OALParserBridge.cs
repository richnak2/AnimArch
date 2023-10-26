using AnimationControl.OAL;
using Antlr4.Runtime;
using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AnimationControl.OAL
{
    public class OALParserBridge
    {
        public static EXEScopeMethod Parse(String Code)
        {
            ICharStream target = new AntlrInputStream(Code);
            ITokenSource lexer = new OALLexer(target);
            ITokenStream tokens = new CommonTokenStream(lexer);
            OALParser parser = new OALParser(tokens)
            {
                BuildParseTree = true
            };

            //ExprParser.LiteralContext result = parser.literal();
            OALParser.LinesContext parsedLines = parser.lines();

            OALVisitorConcrete visitor = new OALVisitorConcrete();

            EXEScopeMethod result = visitor.VisitLines(parsedLines) as EXEScopeMethod;

            return result;
        }

        public static String PythonParse(String Code, List<String> Attributes)
        {
            OALParser parser = null;
            try
            {
                ICharStream target = new AntlrInputStream(Code);
                ITokenSource lexer = new OALLexer(target);
                ITokenStream tokens = new CommonTokenStream(lexer);
                parser = new OALParser(tokens)
                {
                    BuildParseTree = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            OALParser.LinesContext result = parser.lines();
            OALToPythonVisitor visitor = new OALToPythonVisitor(Attributes);
            String pythonCode = "";

            try
            {
                pythonCode = visitor.VisitLines(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return pythonCode;
        }
    }
}
