using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl.OAL;
using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using AnimationControl.OAL;
using AnimationControl;

namespace AnimationControl.OAL.Tests
{
    [TestClass()]
    public class EXECommandBreakContinueOALVisitor2Tests
    {
        [TestMethod()]
        public void EXECommandBreak_01()
        {
            string oalexample = "break;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "break;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandContinue_02()
        {
            string oalexample = "continue;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "continue;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        private EXEScope Init(String oalexample)
        {
            ICharStream target = new AntlrInputStream(oalexample);
            ITokenSource lexer = new OALLexer(target);
            ITokenStream tokens = new CommonTokenStream(lexer);
            OALParser parser = new OALParser(tokens);
            parser.BuildParseTree = true;

            OALParser.LinesContext result = parser.lines();
            Console.Write(result.ToStringTree());
            Console.WriteLine();

            OALVisitor2 test = new OALVisitor2();
            test.VisitLines(result);

            return test.globalExeScope;
        }
    }
}
