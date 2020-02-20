using AnimationControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AnimationControlTests
{
    [TestClass]
    public class OALCommandParserTests
    {
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_NormalArithmetic1()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("-", 9);

            String Input = "(6 + 12) - (3 * (7 + 2) + (6))";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_NormalArithmetic2()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("*", 13);

            String Input = "(15000 + 47) * 9";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_NormalString1()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("+", 8);

            String Input = "\"D(+) \" + \"si...\"";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State1InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("", -1);

            String Input = "(5 + 6 * 7 - (6 * 9 + 2))";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State2InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("+", 2);

            String Input = "5 + 6 * 7 - (6 * 9 + 2)";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State3InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("-", 6);

            String Input = "6 * 7 - (6 * 9 + 2)";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State4InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("*", 2);

            String Input = "6 * 7";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State5InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("+", 6);

            String Input = "6 * 9 + 2";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void IdentifyFirstTopLevelOperator_Case1State6InBracketsArithmetic()
        {
            OALCommandParser OCP = new OALCommandParser();

            (String, int) ExpectedOutput = ("*", 2);

            String Input = "6 * 9";
            (String, int) ActualOutput = OCP.IdentifyFirstTopLevelOperator(Input);

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
    }
}
