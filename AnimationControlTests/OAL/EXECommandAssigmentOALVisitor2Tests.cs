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
    public class EXECommandAssigmentOALVisitor2Tests
    {
        [TestMethod()]
        public void EXECommandAssigment_01()
        {
            string oalexample = "x = y;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = y;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandAssigment_02()
        {
            string oalexample = "x = 7;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = 7;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandAssigment_03()
        {
            string oalexample = "x = y + 3;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = y + 3;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandAssigment_04()
        {
            string oalexample = "x = (5 + 3) * 5 / (9 * 2);";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = (5 + 3) * 5 / 9 * 2;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandAssigment_05()
        {
            string oalexample = "x = \"ahoj\" + \"te\";";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = \"ahoj\" + \"te\";\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandAssigment_06()
        {
            string oalexample = "assign result = (x < 15 or y< 66) and is_allowed;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "result = (x < 15 or y < 66) and is_allowed;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandAssigment_07()
        {
            string oalexample = "x = not x;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = not x;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandAssigment_08()
        {
            string oalexample = "x = not x and not y;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = not x and not y;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandAssigment_09()
        {
            string oalexample = "x = dog1.age > dog2.age;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = dog1.age > dog2.age;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandInstanceAssigment_01()
        {
            string oalexample = "object.coordinate_x = 17.22;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "object.coordinate_x = 17.22;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandInstanceAssigment_02()
        {
            string oalexample = "archer.hp = archer.hp * 0.50 - 16;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "archer.hp = archer.hp * 0.50 - 16;\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandInstanceAssigment_03()
        {
            string oalexample = "assign dog.name = \"Koronko\";";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "dog.name = \"Koronko\";\n";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void EXECommandInstanceAssigment_04()
        {
            string oalexample = "assign door.locked = not (door.key == used_key);";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "door.locked = not door.key == used_key;\n";// zly overovaci mechanizmus

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