using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl.OAL;
using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using AnimationControl.OAL;

namespace AnimationControl.OAL.Tests
{
    [TestClass()]
    public class OALVisitor2Tests
    {
        [TestMethod()]
        public void VisitExeCommandQueryCreate_normal_01()
        {

            String oalexample = "create object instance of Visitor;";

            ICharStream target = new AntlrInputStream(oalexample);
            ITokenSource lexer = new OALLexer(target);
            ITokenStream tokens = new CommonTokenStream(lexer);
            OALParser parser = new OALParser(tokens);
            parser.BuildParseTree = true;

            //ExprParser.LiteralContext result = parser.literal();
            OALParser.LinesContext result = parser.lines();
            //Console.Write(result.ToStringTree());
            //Console.WriteLine();

            OALVisitor2 test = new OALVisitor2();
            test.VisitLines(result);

            EXEScope e = test.globalExeScope;

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Visitor;\n";

            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}