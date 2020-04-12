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
            string oalexample = "relate subject to observer across R15;\n" +
                                    "relate dog to owner across R7;\n" +
                                    "select any dog from instances of Dog;\n" +
                                    "select many all_dogs from instances of Dog;\n" +
                                    "select any young_dog from instances of Dog where selected.age < 5 OR 6;\n" +
                                    "select any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;" +
                                    "select any dog from instances of Dog where x == (1+2)*3;" +
                                    "select any dog from instances of Dog where (1 /(obj1.x * 0.22 + object1.y * 0.55 - object1.z /2) == \"ahoj\" + \"te deti\" or (x+3 != y%4+2)) and (x<=1 or x+1>=y or obj1.x<2 or obj1.y>1.2 * cardinality observers) and (empty k or not_empty g) and not x>5;" +
                                    "create object instance of Visitor;" +
                                    "create object instance myUserAccount of UserAccount;" +
                                    "unrelate subject from observer across R15;" +
                                    "unrelate dog from owner across R7;" +
                                    "delete object instance current_user;" +
                                    "delete object instance observer3;" +
                                    "x = 15.0 * y;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "relate subject to observer across R15;\n" +
                                    "relate dog to owner across R7;\n" +
                                    "select any dog from instances of Dog;\n" +
                                    "select many all_dogs from instances of Dog;\n" +
                                    "select any young_dog from instances of Dog where selected.age < 5 OR 6;\n" +
                                    "select any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;\n" +
                                    "select any dog from instances of Dog where x == (1 + 2) * 3;\n" +
                                    "select any dog from instances of Dog where (1 / (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2) == \"ahoj\" + \"te deti\" or x + 3 != y % 4 + 2) and (x <= 1 or x + 1 >= y or obj1.x < 2 or obj1.y > 1.2 * cardinality observers) and (empty k or not_empty g) and not x > 5;\n" +
                                    "create object instance of Visitor;\n" +
                                    "create object instance myUserAccount of UserAccount;\n" +
                                    "unrelate subject from observer across R15;\n" +
                                    "unrelate dog from owner across R7;\n" +
                                    "delete object instance current_user;\n" +
                                    "delete object instance observer3;\n" +
                                    "x = 15.0 * y;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);


        }

        [TestMethod()]
        public void VisitExeCommandQueryCreate_normal_02()
        {
            string oalexample = "x = (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2);";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitExeCommandQueryCreate_normal_03()
        {
            string oalexample = "x = (obj1.x * 0.22 + object1.y * 0.55);";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = obj1.x * 0.22 + object1.y * 0.55;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_01()
        {
            string oalexample = "select any my_random_dog related by current_owner->Dog[R4];";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select any my_random_dog related by current_owner->Dog[R4];\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_02()
        {
            string oalexample = "select many all_my_dogs related by owner_veronica->Dog[R4];";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select many all_my_dogs related by owner_veronica->Dog[R4];\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_03()
        {
            string oalexample = "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_04()
        {
            string oalexample = "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_05()
        {
            string oalexample = "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1];";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1];\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_06()
        {
            string oalexample = "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }



        private EXEScope Init(String oalexample)
        {
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

            return test.globalExeScope;
        }
    }
}