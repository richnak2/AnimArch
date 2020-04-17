using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl.OAL;
using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using OALProgramControl;

namespace AnimationControl.OAL.Tests
{
    [TestClass()]
    public class EXEScopeOALVisitor2Tests
    {
        [TestMethod()]
        public void EXEScopeLoopWhile_01()
        {
            string oalexample = "while(x != 7)\n" +
                "x = x + 1;\n" +
                "end while;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "while (x != 7)\n" +
                "\tx = x + 1;\n" +
                "end while;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopWhile_02()
        {
            string oalexample = "while(x != 7)\n" +
                "x = x + 1;\n" +
                "create object instance of Observer;\n" +
                "relate dog to owner across R7;\n" +
                "end while;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();

            String expectedResult = "while (x != 7)\n" +
                "\tx = x + 1;\n" +
                "\tcreate object instance of Observer;\n" +
                "\trelate dog to owner across R7;\n" +
                "end while;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopWhile_03()
        {
            string oalexample = "create object instance of Observer;\n" +
                "while(x != 7)\n" +
                "   x = x + 1;\n" +
                "   create object instance of Observer;\n" +
                "   relate dog to owner across R7;\n" +
                "end while;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Observer;\n" +
                "while (x != 7)\n" +
                "\tx = x + 1;\n" +
                "\tcreate object instance of Observer;\n" +
                "\trelate dog to owner across R7;\n" +
                "end while;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopWhile_04()
        {
            string oalexample = "while(x != 7)\n" +
                "   x = x + 1;\n" +
                "   create object instance of Observer;\n" +
                "   relate dog to owner across R7;\n" +
                "end while;\n" +
                "create object instance of Observer;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "while (x != 7)\n" +
                "\tx = x + 1;\n" +
                "\tcreate object instance of Observer;\n" +
                "\trelate dog to owner across R7;\n" +
                "end while;\n" +
                "create object instance of Observer;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopWhile_05()
        {
            string oalexample = "create object instance of Observer;\n" +
                "while(x != 7)\n" +
                "   x = x + 1;\n" +
                "   create object instance of Observer;\n" +
                "   relate dog to owner across R7;\n" +
                "end while;\n" +
                "create object instance of Observer;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Observer;\n" +
                "while (x != 7)\n" +
                "\tx = x + 1;\n" +
                "\tcreate object instance of Observer;\n" +
                "\trelate dog to owner across R7;\n" +
                "end while;\n" +
                "create object instance of Observer;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopWhile_06()
        {
            string oalexample = "while(x != 7)\n" +
                "while(y != 7)\n" +
                "y = y + 1;\n" +
                "end while;\n" +
                "x = x + 1;\n" +
                "end while;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "while (x != 7)\n" +
                "\twhile (y != 7)\n" +
                "\t\ty = y + 1;\n" +
                "\tend while;\n" +
                "\tx = x + 1;\n" +
                "end while;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopWhile_07()
        {
            string oalexample = "create object instance of Observer;\n" +
                "while(x != 7)\n" +
                "while(y != 7)\n" +
                "y = y + 1;\n" +
                "end while;\n" +
                "x = x + 1;\n" +
                "end while;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Observer;\n" +
                "while (x != 7)\n" +
                "\twhile (y != 7)\n" +
                "\t\ty = y + 1;\n" +
                "\tend while;\n" +
                "\tx = x + 1;\n" +
                "end while;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopWhile_08()
        {
            string oalexample = "create object instance of Observer;\n" +
                "while(x != 7)\n" +
                "while(y != 7)\n" +
                "y = y + 1;\n" +
                "end while;\n" +
                "x = x + 1;\n" +
                "end while;\n" +
                "create object instance of Observer;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Observer;\n" +
                "while (x != 7)\n" +
                "\twhile (y != 7)\n" +
                "\t\ty = y + 1;\n" +
                "\tend while;\n" +
                "\tx = x + 1;\n" +
                "end while;\n" +
                "create object instance of Observer;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopWhile_09()
        {
            string oalexample = "create object instance of Observer;\n" +
                "while(x != 7)\n" +
                "create object instance of Observer;\n" +
                "while(y != 7)\n" +
                "create object instance of Observer;\n" +
                "y = y + 1;\n" +
                "create object instance of Observer;\n" +
                "end while;\n" +
                "create object instance of Observer;\n" +
                "x = x + 1;\n" +
                "create object instance of Observer;\n" +
                "end while;\n" +
                "create object instance of Observer;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Observer;\n" +
                "while (x != 7)\n" +
                "\tcreate object instance of Observer;\n" +
                "\twhile (y != 7)\n" +
                "\t\tcreate object instance of Observer;\n" +
                "\t\ty = y + 1;\n" +
                "\t\tcreate object instance of Observer;\n" +
                "\tend while;\n" +
                "\tcreate object instance of Observer;\n" +
                "\tx = x + 1;\n" +
                "\tcreate object instance of Observer;\n" +
                "end while;\n" +
                "create object instance of Observer;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopWhile_10()
        {
            string oalexample = "while(x != 7)\n" +
                "while(y != 7)\n" +
                "while(z != 7)\n" +
                "z = z + 1;\n" +
                "end while;\n" +
                "y = y + 1;\n" +
                "end while;\n" +
                "x = x + 1;\n" +
                "end while;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "while (x != 7)\n" +
                "\twhile (y != 7)\n" +
                "\t\twhile (z != 7)\n" +
                "\t\t\tz = z + 1;\n" +
                "\t\tend while;\n" +
                "\t\ty = y + 1;\n" +
                "\tend while;\n" +
                "\tx = x + 1;\n" +
                "end while;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopWhile_11()
        {
            string oalexample = "while(true)\n" +
                "while(y != 7 and not car.state)\n" +
                "while(z != 7 or empty bottle)\n" +
                "z = z + 1 - 1 * 1 / 1 % 1;\n" +
                "end while;\n" +
                "y = y + 1 + 1;\n" +
                "end while;\n" +
                "x = (((x + 1)*(1 + x)) + 5) / 0;\n" +
                "end while;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "while (true)\n" +
                "\twhile (y != 7 and not car.state)\n" +
                "\t\twhile (z != 7 or empty bottle)\n" +
                "\t\t\tz = z + 1 - 1 * 1 / 1 % 1;\n" +
                "\t\tend while;\n" +
                "\t\ty = y + 1 + 1;\n" +
                "\tend while;\n" +
                "\tx = ((x + 1) * (1 + x) + 5) / 0;\n" +
                "end while;\n";

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

        [TestMethod()]
        public void EXEScopeLoopForEach_01()
        {
            string oalexample = "for each dog in my_dogs\n" +
            "\trelate dog to my_wife across R8;\n" +
            "end for;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "for each dog in my_dogs\n" +
            "\trelate dog to my_wife across R8;\n" +
            "end for;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeLoopForEach_02()
        {
            string oalexample = "for each dog in my_dogs\n" +
            "\tfor each dog in my_dogs\n" +
            "\t\trelate dog to my_wife across R8;\n" +
            "\tend for;\n" +
            "end for;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "for each dog in my_dogs\n" +
            "\tfor each dog in my_dogs\n" +
            "\t\trelate dog to my_wife across R8;\n" +
            "\tend for;\n" +
            "end for;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCombinations_01()
        {
            string oalexample = "for each dog in my_dogs\n" +
            "\twhile (z != 7)\n" +
            "\t\trelate dog to my_wife across R8;\n" +
            "\t\tz = z + 1;\n" +
            "\tend while;\n" +
            "end for;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "for each dog in my_dogs\n" +
            "\twhile (z != 7)\n" +
            "\t\trelate dog to my_wife across R8;\n" +
            "\t\tz = z + 1;\n" +
            "\tend while;\n" +
            "end for;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCombinations_02()
        {
            string oalexample = "for each dog in my_dogs\n" +
            "\twhile (z != 7)\n" +
            "\t\trelate dog to my_wife across R8;\n" +
            "\t\tif (not a)\n" +
            "\t\t\ta = true;\n" +
            "\t\telse\n" +
            "\t\t\twhile (a != false)\n" +
            "\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\ta = false;\n" +
            "\t\t\t\telse\n" +
            "\t\t\t\t\tb = 1;\n" +
            "\t\t\t\tend if;\n" +
            "\t\t\tend while;\n" +
            "\t\tend if;\n" +
            "\t\tz = z + 1;\n" +
            "\tend while;\n" +
            "end for;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "for each dog in my_dogs\n" +
            "\twhile (z != 7)\n" +
            "\t\trelate dog to my_wife across R8;\n" +
            "\t\tif (not a)\n" +
            "\t\t\ta = true;\n" +
            "\t\telse\n" +
            "\t\t\twhile (a != false)\n" +
            "\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\ta = false;\n" +
            "\t\t\t\telse\n" +
            "\t\t\t\t\tb = 1;\n" +
            "\t\t\t\tend if;\n" +
            "\t\t\tend while;\n" +
            "\t\tend if;\n" +
            "\t\tz = z + 1;\n" +
            "\tend while;\n" +
            "end for;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCombinations_03()
        {
            string oalexample = "par\n" +
                "\tthread\n" +
                "\t\tfor each dog in my_dogs\n" +
                "\t\t\twhile (z != 7)\n" +
                "\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\tif (not a)\n" +
                "\t\t\t\t\ta = true;\n" +
                "\t\t\t\telse\n" +
                "\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tend while;\n" +
                "\t\t\t\tend if;\n" +
                "\t\t\t\tz = z + 1;\n" +
                "\t\t\tend while;\n" +
                "\t\tend for;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tfor each dog in my_dogs\n" +
                "\t\t\twhile (z != 7)\n" +
                "\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\tif (not a)\n" +
                "\t\t\t\t\ta = true;\n" +
                "\t\t\t\telse\n" +
                "\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tend while;\n" +
                "\t\t\t\tend if;\n" +
                "\t\t\t\tz = z + 1;\n" +
                "\t\t\tend while;\n" +
                "\t\tend for;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tfor each dog in my_dogs\n" +
                "\t\t\twhile (z != 7)\n" +
                "\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\tif (not a)\n" +
                "\t\t\t\t\ta = true;\n" +
                "\t\t\t\telse\n" +
                "\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tend while;\n" +
                "\t\t\t\tend if;\n" +
                "\t\t\t\tz = z + 1;\n" +
                "\t\t\tend while;\n" +
                "\t\tend for;\n" +
                "\tend thread;\n" +
                "end par;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "par\n" +
                "\tthread\n" +
                "\t\tfor each dog in my_dogs\n" +
                "\t\t\twhile (z != 7)\n" +
                "\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\tif (not a)\n" +
                "\t\t\t\t\ta = true;\n" +
                "\t\t\t\telse\n" +
                "\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tend while;\n" +
                "\t\t\t\tend if;\n" +
                "\t\t\t\tz = z + 1;\n" +
                "\t\t\tend while;\n" +
                "\t\tend for;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tfor each dog in my_dogs\n" +
                "\t\t\twhile (z != 7)\n" +
                "\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\tif (not a)\n" +
                "\t\t\t\t\ta = true;\n" +
                "\t\t\t\telse\n" +
                "\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tend while;\n" +
                "\t\t\t\tend if;\n" +
                "\t\t\t\tz = z + 1;\n" +
                "\t\t\tend while;\n" +
                "\t\tend for;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tfor each dog in my_dogs\n" +
                "\t\t\twhile (z != 7)\n" +
                "\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\tif (not a)\n" +
                "\t\t\t\t\ta = true;\n" +
                "\t\t\t\telse\n" +
                "\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tend while;\n" +
                "\t\t\t\tend if;\n" +
                "\t\t\t\tz = z + 1;\n" +
                "\t\t\tend while;\n" +
                "\t\tend for;\n" +
                "\tend thread;\n" +
                "end par;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCombinations_04()
        {
            string oalexample = "while (true)\n" +
                "\tpar\n" +
                "\t\tthread\n" +
                "\t\t\tfor each dog in my_dogs\n" +
                "\t\t\t\twhile (z != 7)\n" +
                "\t\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\t\tif (not a)\n" +
                "\t\t\t\t\t\ta = true;\n" +
                "\t\t\t\t\telse\n" +
                "\t\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\t\tend while;\n" +
                "\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tz = z + 1;\n" +
                "\t\t\t\tend while;\n" +
                "\t\t\tend for;\n" +
                "\t\tend thread;\n" +
                "\t\tthread\n" +
                "\t\t\tfor each dog in my_dogs\n" +
                "\t\t\t\twhile (z != 7)\n" +
                "\t\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\t\tif (not a)\n" +
                "\t\t\t\t\t\ta = true;\n" +
                "\t\t\t\t\telse\n" +
                "\t\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\t\tend while;\n" +
                "\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tz = z + 1;\n" +
                "\t\t\t\tend while;\n" +
                "\t\t\tend for;\n" +
                "\t\tend thread;\n" +
                "\t\tthread\n" +
                "\t\t\tfor each dog in my_dogs\n" +
                "\t\t\t\twhile (z != 7)\n" +
                "\t\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\t\tif (not a)\n" +
                "\t\t\t\t\t\ta = true;\n" +
                "\t\t\t\t\telse\n" +
                "\t\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\t\tend while;\n" +
                "\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tz = z + 1;\n" +
                "\t\t\t\tend while;\n" +
                "\t\t\tend for;\n" +
                "\t\tend thread;\n" +
                "\tend par;\n" +
                "end while;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "while (true)\n" +
                "\tpar\n" +
                "\t\tthread\n" +
                "\t\t\tfor each dog in my_dogs\n" +
                "\t\t\t\twhile (z != 7)\n" +
                "\t\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\t\tif (not a)\n" +
                "\t\t\t\t\t\ta = true;\n" +
                "\t\t\t\t\telse\n" +
                "\t\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\t\tend while;\n" +
                "\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tz = z + 1;\n" +
                "\t\t\t\tend while;\n" +
                "\t\t\tend for;\n" +
                "\t\tend thread;\n" +
                "\t\tthread\n" +
                "\t\t\tfor each dog in my_dogs\n" +
                "\t\t\t\twhile (z != 7)\n" +
                "\t\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\t\tif (not a)\n" +
                "\t\t\t\t\t\ta = true;\n" +
                "\t\t\t\t\telse\n" +
                "\t\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\t\tend while;\n" +
                "\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tz = z + 1;\n" +
                "\t\t\t\tend while;\n" +
                "\t\t\tend for;\n" +
                "\t\tend thread;\n" +
                "\t\tthread\n" +
                "\t\t\tfor each dog in my_dogs\n" +
                "\t\t\t\twhile (z != 7)\n" +
                "\t\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\t\tif (not a)\n" +
                "\t\t\t\t\t\ta = true;\n" +
                "\t\t\t\t\telse\n" +
                "\t\t\t\t\t\twhile (a != false)\n" +
                "\t\t\t\t\t\t\tif (b == 1)\n" +
                "\t\t\t\t\t\t\t\ta = false;\n" +
                "\t\t\t\t\t\t\telse\n" +
                "\t\t\t\t\t\t\t\tb = 1;\n" +
                "\t\t\t\t\t\t\tend if;\n" +
                "\t\t\t\t\t\tend while;\n" +
                "\t\t\t\t\tend if;\n" +
                "\t\t\t\t\tz = z + 1;\n" +
                "\t\t\t\tend while;\n" +
                "\t\t\tend for;\n" +
                "\t\tend thread;\n" +
                "\tend par;\n" +
                "end while;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCombinations_05()
        {
            string oalexample = "for each dog in my_dogs\n" +
            "\twhile (z != 7)\n" +
            "\t\trelate dog to my_wife across R8;\n" +
            "\t\tif (not a)\n" +
            "\t\t\ta = true;\n" +
            "\t\telse\n" +
            "\t\t\twhile (a != false)\n" +
            "\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\tend for;\n" +
            "\t\t\t\telse\n" +
            "\t\t\t\t\tb = 1;\n" +
            "\t\t\t\tend if;\n" +
            "\t\t\tend while;\n" +
            "\t\tend if;\n" +
            "\t\tz = z + 1;\n" +
            "\tend while;\n" +
            "end for;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "for each dog in my_dogs\n" +
            "\twhile (z != 7)\n" +
            "\t\trelate dog to my_wife across R8;\n" +
            "\t\tif (not a)\n" +
            "\t\t\ta = true;\n" +
            "\t\telse\n" +
            "\t\t\twhile (a != false)\n" +
            "\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\tend for;\n" +
            "\t\t\t\telse\n" +
            "\t\t\t\t\tb = 1;\n" +
            "\t\t\t\tend if;\n" +
            "\t\t\tend while;\n" +
            "\t\tend if;\n" +
            "\t\tz = z + 1;\n" +
            "\tend while;\n" +
            "end for;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCombinations_06()
        {
            string oalexample = "for each dog in my_dogs\n" +
            "\twhile (z != 7)\n" +
            "\t\trelate dog to my_wife across R8;\n" +
            "\t\tif (not a)\n" +
            "\t\t\ta = true;\n" +
            "\t\telse\n" +
            "\t\t\twhile (a != false)\n" +
            "\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a == false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b != 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R111;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = zaklad + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1 + 2;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\tend for;\n" +
            "\t\t\t\telse\n" +
            "\t\t\t\t\tb = 1;\n" +
            "\t\t\t\tend if;\n" +
            "\t\t\tend while;\n" +
            "\t\tend if;\n" +
            "\t\tz = z + 1;\n" +
            "\tend while;\n" +
            "end for;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "for each dog in my_dogs\n" +
            "\twhile (z != 7)\n" +
            "\t\trelate dog to my_wife across R8;\n" +
            "\t\tif (not a)\n" +
            "\t\t\ta = true;\n" +
            "\t\telse\n" +
            "\t\t\twhile (a != false)\n" +
            "\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a == false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b != 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R111;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = zaklad + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1 + 2;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\t\t\t\tend for;\n" +
            "\t\t\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\t\t\tend if;\n" +
            "\t\t\t\t\t\t\tz = z + 1;\n" +
            "\t\t\t\t\t\tend while;\n" +
            "\t\t\t\t\tend for;\n" +
            "\t\t\t\telse\n" +
            "\t\t\t\t\tb = 1;\n" +
            "\t\t\t\tend if;\n" +
            "\t\t\tend while;\n" +
            "\t\tend if;\n" +
            "\t\tz = z + 1;\n" +
            "\tend while;\n" +
            "end for;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeParallel_01()
        {
            string oalexample = "par\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                "\tend thread;\n" +
                "end par;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "par\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                "\tend thread;\n" +
                "end par;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeParallel_02()
        {
            string oalexample = "par\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R10;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R12;\n" +
                "\tend thread;\n" +
                "end par;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "par\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R10;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R12;\n" +
                "\tend thread;\n" +
                "end par;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeParallel_03()
        {
            string oalexample = "par\n" +
                "\tthread\n" +
                "\t\twhile (x != 7)\n" +
                "\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                "\t\tend while;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R10;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R12;\n" +
                "\tend thread;\n" +
                "end par;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "par\n" +
                "\tthread\n" +
                "\t\twhile (x != 7)\n" +
                "\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                "\t\tend while;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R10;\n" +
                "\tend thread;\n" +
                "\tthread\n" +
                "\t\tcall from Clock::Update() to AnalogWidget::Update() across R12;\n" +
                "\tend thread;\n" +
                "end par;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_01()
        {
            string oalexample = "if(x != 7)\n" +
                "x = x + 1;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x != 7)\n" +
                "\tx = x + 1;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_02()
        {
            string oalexample = "if(x != 7)\n" +
                "x = x + 1;\n" +
                "create object instance of Observer;\n" +
                "relate dog to owner across R7;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();

            String expectedResult = "if (x != 7)\n" +
                "\tx = x + 1;\n" +
                "\tcreate object instance of Observer;\n" +
                "\trelate dog to owner across R7;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_03()
        {
            string oalexample = "create object instance of Observer;\n" +
                "if(x != 7)\n" +
                "   x = x + 1;\n" +
                "   create object instance of Observer;\n" +
                "   relate dog to owner across R7;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Observer;\n" +
                "if (x != 7)\n" +
                "\tx = x + 1;\n" +
                "\tcreate object instance of Observer;\n" +
                "\trelate dog to owner across R7;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_04()
        {
            string oalexample = "if(x != 7)\n" +
                "   x = x + 1;\n" +
                "   create object instance of Observer;\n" +
                "   relate dog to owner across R7;\n" +
                "end if;\n" +
                "create object instance of Observer;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x != 7)\n" +
                "\tx = x + 1;\n" +
                "\tcreate object instance of Observer;\n" +
                "\trelate dog to owner across R7;\n" +
                "end if;\n" +
                "create object instance of Observer;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_05()
        {
            string oalexample = "create object instance of Observer;\n" +
                "if(x != 7)\n" +
                "   x = x + 1;\n" +
                "   create object instance of Observer;\n" +
                "   relate dog to owner across R7;\n" +
                "end if;\n" +
                "create object instance of Observer;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Observer;\n" +
                "if (x != 7)\n" +
                "\tx = x + 1;\n" +
                "\tcreate object instance of Observer;\n" +
                "\trelate dog to owner across R7;\n" +
                "end if;\n" +
                "create object instance of Observer;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_06()
        {
            string oalexample = "if(x != 7)\n" +
                "if(y != 7)\n" +
                "y = y + 1;\n" +
                "end if;\n" +
                "x = x + 1;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x != 7)\n" +
                "\tif (y != 7)\n" +
                "\t\ty = y + 1;\n" +
                "\tend if;\n" +
                "\tx = x + 1;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_07()
        {
            string oalexample = "create object instance of Observer;\n" +
                "if(x != 7)\n" +
                "if(y != 7)\n" +
                "y = y + 1;\n" +
                "end if;\n" +
                "x = x + 1;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Observer;\n" +
                "if (x != 7)\n" +
                "\tif (y != 7)\n" +
                "\t\ty = y + 1;\n" +
                "\tend if;\n" +
                "\tx = x + 1;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_08()
        {
            string oalexample = "create object instance of Observer;\n" +
                "if(x != 7)\n" +
                "if(y != 7)\n" +
                "y = y + 1;\n" +
                "end if;\n" +
                "x = x + 1;\n" +
                "end if;\n" +
                "create object instance of Observer;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Observer;\n" +
                "if (x != 7)\n" +
                "\tif (y != 7)\n" +
                "\t\ty = y + 1;\n" +
                "\tend if;\n" +
                "\tx = x + 1;\n" +
                "end if;\n" +
                "create object instance of Observer;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_09()
        {
            string oalexample = "create object instance of Observer;\n" +
                "if(x != 7)\n" +
                "create object instance of Observer;\n" +
                "if(y != 7)\n" +
                "create object instance of Observer;\n" +
                "y = y + 1;\n" +
                "create object instance of Observer;\n" +
                "end if;\n" +
                "create object instance of Observer;\n" +
                "x = x + 1;\n" +
                "create object instance of Observer;\n" +
                "end if;\n" +
                "create object instance of Observer;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance of Observer;\n" +
                "if (x != 7)\n" +
                "\tcreate object instance of Observer;\n" +
                "\tif (y != 7)\n" +
                "\t\tcreate object instance of Observer;\n" +
                "\t\ty = y + 1;\n" +
                "\t\tcreate object instance of Observer;\n" +
                "\tend if;\n" +
                "\tcreate object instance of Observer;\n" +
                "\tx = x + 1;\n" +
                "\tcreate object instance of Observer;\n" +
                "end if;\n" +
                "create object instance of Observer;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_10()
        {
            string oalexample = "if(x != 7)\n" +
                "if(y != 7)\n" +
                "if(z != 7)\n" +
                "z = z + 1;\n" +
                "end if;\n" +
                "y = y + 1;\n" +
                "end if;\n" +
                "x = x + 1;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x != 7)\n" +
                "\tif (y != 7)\n" +
                "\t\tif (z != 7)\n" +
                "\t\t\tz = z + 1;\n" +
                "\t\tend if;\n" +
                "\t\ty = y + 1;\n" +
                "\tend if;\n" +
                "\tx = x + 1;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_11()
        {
            string oalexample = "if(true)\n" +
                "if(y != 7 and not car.state)\n" +
                "if(z != 7 or empty bottle)\n" +
                "z = z + 1 - 1 * 1 / 1 % 1;\n" +
                "end if;\n" +
                "y = y + 1 + 1;\n" +
                "end if;\n" +
                "x = (((x + 1)*(1 + x)) + 5) / 0;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (true)\n" +
                "\tif (y != 7 and not car.state)\n" +
                "\t\tif (z != 7 or empty bottle)\n" +
                "\t\t\tz = z + 1 - 1 * 1 / 1 % 1;\n" +
                "\t\tend if;\n" +
                "\t\ty = y + 1 + 1;\n" +
                "\tend if;\n" +
                "\tx = ((x + 1) * (1 + x) + 5) / 0;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_12()
        {
            string oalexample = "if (door.lock_id == key.id)\n" +
                "\tdoor.opened = TRUE;\n" +
                "\tresponse_text = \"Door opens\";\n" +
                "else\n" +
                "\tresponse_text = \"Wrong key\";\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (door.lock_id == key.id)\n" +
                "\tdoor.opened = TRUE;\n" +
                "\tresponse_text = \"Door opens\";\n" +
                "else\n" +
                "\tresponse_text = \"Wrong key\";\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_13()
        {
            string oalexample = "if (door.lock_id == key.id)\n" +
                "\tdoor.opened = TRUE;\n" +
                "\tresponse_text = \"Door opens\";\n" +
                "else\n" +
                "\tif (true)\n" +
                "\t\tx = x;\n" +
                "\tend if;\n" +
                "\tresponse_text = \"Wrong key\";\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (door.lock_id == key.id)\n" +
                "\tdoor.opened = TRUE;\n" +
                "\tresponse_text = \"Door opens\";\n" +
                "else\n" +
                "\tif (true)\n" +
                "\t\tx = x;\n" +
                "\tend if;\n" +
                "\tresponse_text = \"Wrong key\";\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_14()
        {
            string oalexample = "if (door.lock_id == key.id)\n" +
                "\tdoor.opened = TRUE;\n" +
                "\tresponse_text = \"Door opens\";\n" +
                "else\n" +
                "\tif (true)\n" +
                "\t\tx = x;\n" +
                "\telse\n" +
                "\t\tx = y;\n" +
                "\tend if;\n" +
                "\tresponse_text = \"Wrong key\";\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (door.lock_id == key.id)\n" +
                "\tdoor.opened = TRUE;\n" +
                "\tresponse_text = \"Door opens\";\n" +
                "else\n" +
                "\tif (true)\n" +
                "\t\tx = x;\n" +
                "\telse\n" +
                "\t\tx = y;\n" +
                "\tend if;\n" +
                "\tresponse_text = \"Wrong key\";\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_15()
        {
            string oalexample = "if (true)\n" +
                "\tx = x;\n" +
                "else\n" +
                "\tif (true)\n" +
                "\t\tx = y;\n" +
                "\telse\n" +
                "\t\ty = x;\n" +
                "\tend if;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (true)\n" +
                "\tx = x;\n" +
                "else\n" +
                "\tif (true)\n" +
                "\t\tx = y;\n" +
                "\telse\n" +
                "\t\ty = x;\n" +
                "\tend if;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_16()
        {
            string oalexample = "if (true)\n" +
                "\tx = x;\n" +
                "else\n" +
                "\tif (true)\n" +
                "\t\tx = y;\n" +
                "\telse\n" +
                "\t\ty = x;\n" +
                "\tend if;\n" +
                "\tresponse_text = \"Wrong key\";\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (true)\n" +
                "\tx = x;\n" +
                "else\n" +
                "\tif (true)\n" +
                "\t\tx = y;\n" +
                "\telse\n" +
                "\t\ty = x;\n" +
                "\tend if;\n" +
                "\tresponse_text = \"Wrong key\";\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_17()
        {
            string oalexample = "if (true)\n" +
                "\tx = x;\n" +
                "else\n" +
                "\tif (true)\n" +
                "\t\tx = y;\n" +
                "\t\tfor each dog in my_dogs\n" +
                "\t\t\twhile (z != 7)\n" +
                "\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\tz = z + 1;\n" +
                "\t\t\tend while;\n" +
                "\t\tend for;\n" +
                "\telse\n" +
                "\t\ty = x;\n" +
                "\tend if;\n" +
                "\tresponse_text = \"Wrong key\";\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (true)\n" +
                "\tx = x;\n" +
                "else\n" +
                "\tif (true)\n" +
                "\t\tx = y;\n" +
                "\t\tfor each dog in my_dogs\n" +
                "\t\t\twhile (z != 7)\n" +
                "\t\t\t\trelate dog to my_wife across R8;\n" +
                "\t\t\t\tz = z + 1;\n" +
                "\t\t\tend while;\n" +
                "\t\tend for;\n" +
                "\telse\n" +
                "\t\ty = x;\n" +
                "\tend if;\n" +
                "\tresponse_text = \"Wrong key\";\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_18()
        {
            string oalexample = "if (action.id == 0)\n" +
                "\tx = 15 * y;\n" +
                "elif (action.id == 1)\n" +
                "\tx = 7.5 * y - 16;\n" +
                "elif (action.id == 2)\n" +
                "\tx = 0.2 * y + 1.0;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (action.id == 0)\n" +
                "\tx = 15 * y;\n" +
                "elif (action.id == 1)\n" +
                "\tx = 7.5 * y - 16;\n" +
                "elif (action.id == 2)\n" +
                "\tx = 0.2 * y + 1.0;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_19()
        {
            string oalexample = "if (action.id == 0)\n" +
                "\tx = 15 * y;\n" +
                "elif (action.id == 1)\n" +
                "\tx = 7.5 * y - 16;\n" +
                "else\n" +
                "\tx = 0.2 * y + 1.0;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (action.id == 0)\n" +
                "\tx = 15 * y;\n" +
                "elif (action.id == 1)\n" +
                "\tx = 7.5 * y - 16;\n" +
                "else\n" +
                "\tx = 0.2 * y + 1.0;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_20()
        {
            string oalexample = "if (action.id == 0)\n" +
                "\tx = 15 * y;\n" +
                "elif (action.id == 1)\n" +
                "\tx = 7.5 * y - 16;\n" +
                "elif (action.id == 2)\n" +
                "\tx = 7.5 * y - 16;\n" +
                "else\n" +
                "\tx = 0.2 * y + 1.0;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (action.id == 0)\n" +
                "\tx = 15 * y;\n" +
                "elif (action.id == 1)\n" +
                "\tx = 7.5 * y - 16;\n" +
                "elif (action.id == 2)\n" +
                "\tx = 7.5 * y - 16;\n" +
                "else\n" +
                "\tx = 0.2 * y + 1.0;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_21()
        {
            string oalexample = "if (action.id == 0)\n" +
                "\tx = 15 * y;\n" +
                "elif (action.id == 1)\n" +
                "\tif (action.id == 0)\n" +
                "\t\tx = 15 * y;\n" +
                "\telif (action.id == 1)\n" +
                "\t\tx = 7.5 * y - 16;\n" +
                "\telif (action.id == 2)\n" +
                "\t\tx = 7.5 * y - 16;\n" +
                "\telse\n" +
                "\t\tif (action.id == 0)\n" +
                "\t\t\tx = 15 * y;\n" +
                "\t\telif (action.id == 1)\n" +
                "\t\t\tx = 7.5 * y - 16;\n" +
                "\t\telif (action.id == 2)\n" +
                "\t\t\tx = 7.5 * y - 16;\n" +
                "\t\telse\n" +
                "\t\t\tx = 0.2 * y + 1.0;\n" +
                "\t\tend if;\n" +
                "\t\tx = 0.2 * y + 1.0;\n" +
                "\tend if;\n" +
                "\tx = 7.5 * y - 16;\n" +
                "elif (action.id == 2)\n" +
                "\tx = 7.5 * y - 16;\n" +
                "else\n" +
                "\tif (action.id == 0)\n" +
                "\t\tx = 15 * y;\n" +
                "\telif (action.id == 1)\n" +
                "\t\tx = 7.5 * y - 16;\n" +
                "\telif (action.id == 2)\n" +
                "\t\tx = 7.5 * y - 16;\n" +
                "\telse\n" +
                "\t\tx = 0.2 * y + 1.0;\n" +
                "\tend if;\n" +
                "\tx = 0.2 * y + 1.0;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (action.id == 0)\n" +
                "\tx = 15 * y;\n" +
                "elif (action.id == 1)\n" +
                "\tif (action.id == 0)\n" +
                "\t\tx = 15 * y;\n" +
                "\telif (action.id == 1)\n" +
                "\t\tx = 7.5 * y - 16;\n" +
                "\telif (action.id == 2)\n" +
                "\t\tx = 7.5 * y - 16;\n" +
                "\telse\n" +
                "\t\tif (action.id == 0)\n" +
                "\t\t\tx = 15 * y;\n" +
                "\t\telif (action.id == 1)\n" +
                "\t\t\tx = 7.5 * y - 16;\n" +
                "\t\telif (action.id == 2)\n" +
                "\t\t\tx = 7.5 * y - 16;\n" +
                "\t\telse\n" +
                "\t\t\tx = 0.2 * y + 1.0;\n" +
                "\t\tend if;\n" +
                "\t\tx = 0.2 * y + 1.0;\n" +
                "\tend if;\n" +
                "\tx = 7.5 * y - 16;\n" +
                "elif (action.id == 2)\n" +
                "\tx = 7.5 * y - 16;\n" +
                "else\n" +
                "\tif (action.id == 0)\n" +
                "\t\tx = 15 * y;\n" +
                "\telif (action.id == 1)\n" +
                "\t\tx = 7.5 * y - 16;\n" +
                "\telif (action.id == 2)\n" +
                "\t\tx = 7.5 * y - 16;\n" +
                "\telse\n" +
                "\t\tx = 0.2 * y + 1.0;\n" +
                "\tend if;\n" +
                "\tx = 0.2 * y + 1.0;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_01()
        {
            string oalexample = "if (x == 7)\n" +
                "\tx = x + 7;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 7)\n" +
                "\tx = x + 7;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_02()
        {
            string oalexample = "if (x == false)\n" +
                "\tx = true;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == false)\n" +
                "\tx = true;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_03()
        {
            string oalexample = "if (not x)\n" +
                "\tx = not x;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (not x)\n" +
                "\tx = not x;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_04()
        {
            string oalexample = "if (not (x and y))\n" +
                "\tx = not x and not y;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (not x and y)\n" +
                "\tx = not x and not y;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_05()
        {
            string oalexample = "if (x <= y and z != 0)\n" +
                "\tx = y + z;\n" +
                "\tz = y - x;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x <= y and z != 0)\n" +
                "\tx = y + z;\n" +
                "\tz = y - x;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_06()
        {
            string oalexample = "if (a == 7)\n" +
                "\ta = 0;\n" +
                "end if;\n" +
                "if (not b)\n" +
                "\ty = not y;\n" +
                "end if;\n" +
                "if (c == d and d < e and e > f or f != g and g <= h and h >= i and not i)\n" +
                "\tc = d and d < e;\n" +
                "\te = f or f != g;\n" +
                "\th = i and not i;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (a == 7)\n" +
                "\ta = 0;\n" +
                "end if;\n" +
                "if (not b)\n" +
                "\ty = not y;\n" +
                "end if;\n" +
                "if (((c == d and d < e and e > f) or f != g) and g <= h and h >= i and not i)\n" +
                "\tc = d and d < e;\n" +
                "\te = f or f != g;\n" +
                "\th = i and not i;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_07()
        {
            string oalexample = "if (x == 7)\n" +
                "\tif (y == 7)\n" +
                "\t\ty = x;\n" +
                "\tend if;\n" +
                "\tx = y;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 7)\n" +
                "\tif (y == 7)\n" +
                "\t\ty = x;\n" +
                "\tend if;\n" +
                "\tx = y;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_08()
        {
            string oalexample = "if (x == 7)\n" +
                "\tif (y == 7)\n" +
                "\t\ty = x;\n" +
                "\tend if;\n" +
                "\tx = y;\n" +
                "end if;\n" +
                "if (17 == x)\n" +
                "\tif (17 == y)\n" +
                "\t\ty = x + x;\n" +
                "\tend if;\n" +
                "\tx = y - y;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 7)\n" +
                "\tif (y == 7)\n" +
                "\t\ty = x;\n" +
                "\tend if;\n" +
                "\tx = y;\n" +
                "end if;\n" +
                "if (17 == x)\n" +
                "\tif (17 == y)\n" +
                "\t\ty = x + x;\n" +
                "\tend if;\n" +
                "\tx = y - y;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_else_01()
        {
            string oalexample = "if (x == 7)\n" +
                "else\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 7)\n" +
                "else\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_else_02()
        {
            string oalexample = "if (x == 7)\n" +
                "\tx = x + 7;\n" +
                "else\n" +
                "\tx = x - 7;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 7)\n" +
                "\tx = x + 7;\n" +
                "else\n" +
                "\tx = x - 7;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_else_03()
        {
            string oalexample = "if (x == 7)\n" +
                "else\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 7)\n" +
                "else\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_else_04()
        {
            string oalexample = "if (x == false)\n" +
                "\tx = true;\n" +
                "else\n" +
                "\ty = false;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == false)\n" +
                "\tx = true;\n" +
                "else\n" +
                "\ty = false;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_else_05()
        {
            string oalexample = "if (x == 7)\n" +
                "\tif (y != 7)\n" +
                "\t\ty = 7;\n" +
                "\telse\n" +
                "\t\ty = 0;\n" +
                "\tend if;\n" +
                "else\n" +
                "\tx = 0;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 7)\n" +
                "\tif (y != 7)\n" +
                "\t\ty = 7;\n" +
                "\telse\n" +
                "\t\ty = 0;\n" +
                "\tend if;\n" +
                "else\n" +
                "\tx = 0;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_else_06()
        {
            string oalexample = "if (x == 7)\n" +
                "\tif (y != 7)\n" +
                "\t\ty = 7;\n" +
                "\telse\n" +
                "\t\ty = 0;\n" +
                "\tend if;\n" +
                "else\n" +
                "\tif (x == 7)\n" +
                "\t\tif (y != 7)\n" +
                "\t\t\ty = 7;\n" +
                "\t\telse\n" +
                "\t\t\ty = 0;\n" +
                "\t\tend if;\n" +
                "\telse\n" +
                "\t\tx = 0;\n" +
                "\tend if;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 7)\n" +
                "\tif (y != 7)\n" +
                "\t\ty = 7;\n" +
                "\telse\n" +
                "\t\ty = 0;\n" +
                "\tend if;\n" +
                "else\n" +
                "\tif (x == 7)\n" +
                "\t\tif (y != 7)\n" +
                "\t\t\ty = 7;\n" +
                "\t\telse\n" +
                "\t\t\ty = 0;\n" +
                "\t\tend if;\n" +
                "\telse\n" +
                "\t\tx = 0;\n" +
                "\tend if;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_else_07()
        {
            string oalexample = "if (x == 1)\n" +
                "else\n" +
                "\tif (y == 2)\n" +
                "\telse\n" +
                "\t\tif (z == 3)\n" +
                "\t\telse\n" +
                "\t\t\tif (a == y)\n" +
                "\t\t\telse\n" +
                "\t\t\t\tx = 1;\n" +
                "\t\t\tend if;\n" +
                "\t\tend if;\n" +
                "\tend if;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 1)\n" +
                "else\n" +
                "\tif (y == 2)\n" +
                "\telse\n" +
                "\t\tif (z == 3)\n" +
                "\t\telse\n" +
                "\t\t\tif (a == y)\n" +
                "\t\t\telse\n" +
                "\t\t\t\tx = 1;\n" +
                "\t\t\tend if;\n" +
                "\t\tend if;\n" +
                "\tend if;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_else_08()
        {
            string oalexample = "if (x == 1)\n" +
                "else\n" +
                "\tif (y == 2)\n" +
                "\telse\n" +
                "\t\tif (z == 3)\n" +
                "\t\telse\n" +
                "\t\t\tif (a == y)\n" +
                "\t\t\telse\n" +
                "\t\t\t\tx = 1;\n" +
                "\t\t\tend if;\n" +
                "\t\tend if;\n" +
                "\tend if;\n" +
                "end if;\n" +
                "if (x == 1)\n" +
                "else\n" +
                "\tif (y == 2)\n" +
                "\telse\n" +
                "\t\tif (z == 3)\n" +
                "\t\telse\n" +
                "\t\t\tif (a == y)\n" +
                "\t\t\telse\n" +
                "\t\t\t\tx = 1;\n" +
                "\t\t\tend if;\n" +
                "\t\tend if;\n" +
                "\tend if;\n" +
                "end if;\n" +
                "if (x == 1)\n" +
                "else\n" +
                "\tif (y == 2)\n" +
                "\telse\n" +
                "\t\tif (z == 3)\n" +
                "\t\telse\n" +
                "\t\t\tif (a == y)\n" +
                "\t\t\telse\n" +
                "\t\t\t\tx = 1;\n" +
                "\t\t\tend if;\n" +
                "\t\tend if;\n" +
                "\tend if;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 1)\n" +
                "else\n" +
                "\tif (y == 2)\n" +
                "\telse\n" +
                "\t\tif (z == 3)\n" +
                "\t\telse\n" +
                "\t\t\tif (a == y)\n" +
                "\t\t\telse\n" +
                "\t\t\t\tx = 1;\n" +
                "\t\t\tend if;\n" +
                "\t\tend if;\n" +
                "\tend if;\n" +
                "end if;\n" +
                "if (x == 1)\n" +
                "else\n" +
                "\tif (y == 2)\n" +
                "\telse\n" +
                "\t\tif (z == 3)\n" +
                "\t\telse\n" +
                "\t\t\tif (a == y)\n" +
                "\t\t\telse\n" +
                "\t\t\t\tx = 1;\n" +
                "\t\t\tend if;\n" +
                "\t\tend if;\n" +
                "\tend if;\n" +
                "end if;\n" +
                "if (x == 1)\n" +
                "else\n" +
                "\tif (y == 2)\n" +
                "\telse\n" +
                "\t\tif (z == 3)\n" +
                "\t\telse\n" +
                "\t\t\tif (a == y)\n" +
                "\t\t\telse\n" +
                "\t\t\t\tx = 1;\n" +
                "\t\t\tend if;\n" +
                "\t\tend if;\n" +
                "\tend if;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_elif_01()
        {
            string oalexample = "if (action.id == 0)\n" +
                "elif (action.id == 1)\n" +
                "\tx = 7;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (action.id == 0)\n" +
                "elif (action.id == 1)\n" +
                "\tx = 7;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_elif_02()
        {
            string oalexample = "if (action.id == 0)\n" +
                "\tx = 7;\n" +
                "elif (action.id == 1)\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (action.id == 0)\n" +
                "\tx = 7;\n" +
                "elif (action.id == 1)\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_elif_03()
        {
            string oalexample = "if (x == 7)\n" +
                "\tx = 7 + x;\n" +
                "elif (y == 7)\n" +
                "\ty = 7 + y;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 7)\n" +
                "\tx = 7 + x;\n" +
                "elif (y == 7)\n" +
                "\ty = 7 + y;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_elif_04()
        {
            string oalexample = "if (x == 7)\n" +
                "\tif (x == 7)\n" +
                "\t\tx = x + 7;\n" +
                "\tend if;\n" +
                "elif (y == 7)\n" +
                "\tif (x == 7)\n" +
                "\t\tx = y + 7;\n" +
                "\tend if;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 7)\n" +
                "\tif (x == 7)\n" +
                "\t\tx = x + 7;\n" +
                "\tend if;\n" +
                "elif (y == 7)\n" +
                "\tif (x == 7)\n" +
                "\t\tx = y + 7;\n" +
                "\tend if;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_elif_else_01()
        {
            string oalexample = "if (x == 0)\n" +
                "\tx = x;\n" +
                "elif (y == 1)\n" +
                "\tx = 7;\n" +
                "else\n" +
                "\ty = 7;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 0)\n" +
                "\tx = x;\n" +
                "elif (y == 1)\n" +
                "\tx = 7;\n" +
                "else\n" +
                "\ty = 7;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXEScopeCondition_if_elif_else_02()
        {
            string oalexample = "if (x == 0)\n" +
                "\tx = x;\n" +
                "elif (y == 1)\n" +
                "\tx = 7;\n" +
                "elif (z == 2)\n" +
                "\tz = 5;\n" +
                "else\n" +
                "\ty = 7;\n" +
                "end if;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "if (x == 0)\n" +
                "\tx = x;\n" +
                "elif (y == 1)\n" +
                "\tx = 7;\n" +
                "elif (z == 2)\n" +
                "\tz = 5;\n" +
                "else\n" +
                "\ty = 7;\n" +
                "end if;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
