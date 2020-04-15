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
    public class EXECommandCallOALVisitor2Tests
    {
        [TestMethod()]
        public void EXECommandCall_01()
        {
            string oalexample = "call from Observer::init() to Subject::register() across R4;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "call from Observer::init() to Subject::register() across R4;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandCall_02()
        {
            string oalexample = "call from Subject::register() to Subject::addObserver();";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "call from Subject::register() to Subject::addObserver();\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandCall_03()
        {
            string oalexample = "call from Subject::register() to Subject::addObserver() across R7;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "call from Subject::register() to Subject::addObserver() across R7;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandCall_04()
        {
            string oalexample = "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Observer::init() to Subject::register() across R4;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Observer::init() to Subject::register() across R4;\n";

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
