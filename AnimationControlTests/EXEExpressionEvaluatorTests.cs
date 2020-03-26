using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXEExpressionEvaluatorTests
    {
        // SetUloh1 Create more unit tests for method EXEExpressionEvaluator.Evaluate()
        // Keep the naming convention as you see it here. Check the bottom 2 tests - they are a bit different (Assert.IsNull)
        //Prepare unit tests for each primitive type a and each operator -> experiment with edge values (negative, 0, null, empty string and so on)
        [TestMethod]
        public void Evaluate_Normal_Plus_Integer_01()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "1", "1"}));

            String ExpectedOutput = "2";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Plus_Integer_02()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "0", "5", "12", "6"}));

            String ExpectedOutput = "23";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Plus_Integer_03()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "-5", "-2"}));

            String ExpectedOutput = "-7";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Plus_Integer_04()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "-5", "9" }));

            String ExpectedOutput = "4";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Plus_Integer_05()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "-5", "9", "-6", "0", "1", "2", "-1" }));

            String ExpectedOutput = "0";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
  


        [TestMethod]
        public void Evaluate_Normal_Plus_String_01()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "\"abc\"", "\"def\"" }));

            String ExpectedOutput = "\"abcdef\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Plus_String_02()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "\"asdasdasdbbc\"", "\"123\"" }));

            String ExpectedOutput = "\"asdasdasdbbc123\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Plus_String_03()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "\"34567\"", "\"123\"" }));

            String ExpectedOutput = "\"34567123\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Plus_String_04()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "\"34567\"", "\"123\"", "\"34567\"", "\"34567\"" }));

            String ExpectedOutput = "\"345671233456734567\"";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Plus_String_01()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "34567", "\"123\"" }));
            
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Plus_String_02()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "\"34567", "123" }));

            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Plus_String_03()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "\"34567\"" }));

            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Plus_String_04()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "\"34567\"", "\"34567\"", "\"34567\"", "\"34567" }));

            Assert.IsNull(ActualOutput);
        }


        public void Evaluate_Normal_Minus_Integer_01()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("-", new List<String>(new String[] { "12", "4" }));

            String ExpectedOutput = "8";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Minus_Integer_02()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("-", new List<String>(new String[] { "5", "17" }));

            String ExpectedOutput = "-12";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Minus_Integer_03()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("-", new List<String>(new String[] { "3", "-22" }));

            String ExpectedOutput = "25";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Minus_Integer_04()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("-", new List<String>(new String[] { "-7", "-5" }));

            String ExpectedOutput = "-2";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Minus_Integer_05()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("-", new List<String>(new String[] { "100", "5", "0", "10", "-5", "0" }));

            String ExpectedOutput = "90";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Multiply_Integer_01()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "6", "7" }));

            String ExpectedOutput = "42";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Multiply_Integer_02()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "10", "5", "1", "10", "-5", "1" }));

            String ExpectedOutput = "-2500";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Multiply_Integer_03()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "1", "-1", "2", "2", "-2", "2", "1", "-1" }));

            String ExpectedOutput = "-16";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Multiply_Integer_04()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "1", "-1", "2", "2", "-2", "2", "1", "0", "-1" }));

            String ExpectedOutput = "0";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Divide_Integer_01()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("/", new List<String>(new String[] { "36", "4"}));

            String ExpectedOutput = "9";

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Bad_Divide_01_IntegerBool()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("*", new List<String>(new String[] { "78", EXETypes.BooleanTrue }));

            Assert.IsNull(ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Bad_Plus_01_FloatString()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("+", new List<String>(new String[] { "2.87", "\"2.13\"" }));

            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Equal_Integer_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { "11", "11" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }


        [TestMethod]
        public void Evaluate_Normal_Equal_FloatString_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { "11.456", "11.456" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }


        [TestMethod]
        public void Evaluate_Normal_Equal_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { "\"a\"", "\"a\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Equal_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { "\"asdasds\"", "\"asdasds\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Equal_Boolean_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanTrue }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Equal_Boolean_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { EXETypes.BooleanFalse, EXETypes.BooleanFalse }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Equal_Boolean_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { EXETypes.BooleanFalse, EXETypes.BooleanTrue }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Equal_Boolean_4()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { EXETypes.BooleanTrue , EXETypes.BooleanFalse }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_BAD_Equal_Boolean_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { EXETypes.BooleanTrue }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_BAD_Equal_Boolean_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanTrue, EXETypes.BooleanTrue }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_NOT_Equal_Integer_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { "11", "13" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }


        [TestMethod]
        public void Evaluate_Normal_NOT_Equal_FloatString_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { "1.456", "3.670" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }


        [TestMethod]
        public void Evaluate_Normal_Not_Equal_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { "\"3\"", "\"b\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Not_Equal_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { "\"xzy\"", "\"asdasds\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Not_Equal_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { "\"xzy\"", "\"xzy\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }


        [TestMethod]
        public void Evaluate_Bad_Not_Equal_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { "\"xzy\"", "\"sda\"", "10" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Not_Equal_Boolean_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanFalse }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_Normal_Not_Equal_Boolean_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanFalse }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Not_Equal_Boolean_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { EXETypes.BooleanFalse, EXETypes.BooleanFalse }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Normal_Not_Equal_Boolean_4()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanTrue }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_BAD_Not_Equal_Boolean_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("!=", new List<String>(new String[] { EXETypes.BooleanTrue }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_BAD_Not_Equal_Boolean_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("==", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanTrue, EXETypes.BooleanTrue }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Less_Than_Integer_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "13", "25" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Less_Than_Integer_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "25", "13" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Less_Than_Integer_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "25", "25" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Integer_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "25", "25", "25" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Integer_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "\"25", "25" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Integer_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "25" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Less_Than_Real_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "13.4", "25.9" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Less_Than_Real_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "25.5", "13.23" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Less_Than_Real_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "25.6", "25.6" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Real_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "25.5", "25.5", "25.5" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Real_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "\"25.67", "25.88" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Real_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "25.677" }));
            Assert.IsNull(ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Less_Than_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "\"abcdef\"", "\"xzy\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Less_Than_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "\"xyz\"", "\"abcdef\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Less_Than_String_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "\"xyz\"", "\"xyz\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "\"abc\"", "\"abc\"", "\"abc\"" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "\"25.67\"", "25.88" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_String_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<", new List<String>(new String[] { "\"25.677\"" }));
            Assert.IsNull(ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Integer_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "34", "25" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Integer_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "13", "25" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Integer_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "25", "25" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Integer_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "25", "25", "25" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Integer_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "\"25", "25" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Integer_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "25" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Real_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "25.8", "12.3" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Real_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "13.233", "25.55" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Real_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "25.6", "25.6" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Real_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "25.5", "25.5", "25.5" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Real_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "\"25.67", "25.88" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Real_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "25.677" }));
            Assert.IsNull(ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Greater_Than_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "\"xzy\"", "\"abcdef\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Greater_Than_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "\"abcdef\"", "\"xzy\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Greater_Than_String_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "\"xyz\"", "\"xyz\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "\"abc\"", "\"abc\"", "\"abc\"" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "\"25.67\"", "25.88" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_String_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">", new List<String>(new String[] { "\"25.677\"" }));
            Assert.IsNull(ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Less_Than_Or_Equal_To_Integer_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "13", "25" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Less_Than_Or_Equal_To_Integer_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "13", "13" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Less_Than_Or_Equal_To_Integer_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "25", "13" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Or_Equal_To_Integer_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "25", "25", "25" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Integer_Or_Equal_To_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "\"25", "25" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Or_Equal_To_Integer_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "25" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Less_Than_Or_Equal_To_Real_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "13.4", "25.9" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Less_Than_Or_Equal_To_Real_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "25.5", "13.23" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Less_Than_Or_Equal_To_Real_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "25.6", "25.6" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Or_Equal_To_Real_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "25.5", "25.5", "25.5" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Or_Equal_To_Real_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "\"25.67", "25.88" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Or_Equal_To_Real_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "25.677" }));
            Assert.IsNull(ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Less_Than_Or_Equal_To_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "\"abcdef\"", "\"xzy\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Less_Than_Or_Equal_To_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "\"xyz\"", "\"abcdef\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Less_Than_Or_Equal_To_String_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "\"xyz\"", "\"xyz\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Or_Equal_To_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "\"abc\"", "\"abc\"", "\"abc\"" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Or_Equal_To_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "\"25.67\"", "25.88" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Less_Than_Or_Equal_To_String_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("<=", new List<String>(new String[] { "\"25.677\"" }));
            Assert.IsNull(ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Or_Equal_To_Integer_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "34", "25" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Or_Equal_To_Integer_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "13", "25" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Or_Equal_To_Integer_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "25", "25" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Or_Equal_To_Integer_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "25", "25", "25" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Or_Equal_To_Integer_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "\"25", "25" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Or_Equal_To_Integer_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "25" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Greater_Or_Equal_To_Than_Real_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "25.8", "12.3" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Greater_Or_Equal_To_Than_Real_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "13.233", "25.55" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Or_Equal_To_Real_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "25.6", "25.6" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Or_Equal_To_Real_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "25.5", "25.5", "25.5" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Or_Equal_To_Real_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "\"25.67", "25.88" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Or_Equal_To_Real_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "25.677" }));
            Assert.IsNull(ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Or_Equal_To_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "\"xzy\"", "\"abcdef\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }


        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Or_Equal_To_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "\"abcdef\"", "\"xzy\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Normal_Greater_Than_Or_Equal_To_String_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "\"xyz\"", "\"xyz\"" }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Or_Equal_To_String_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "\"abc\"", "\"abc\"", "\"abc\"" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Or_Equal_To_String_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "\"25.67\"", "25.88" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_Bad_Greater_Than_Or_Equal_To_String_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate(">=", new List<String>(new String[] { "\"25.677\"" }));
            Assert.IsNull(ActualOutput);

        }


        [TestMethod]
        public void Evaluate_AND_TRUE()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("and", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanTrue }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
       
        [TestMethod]
        public void Evaluate_AND_FALSE_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("and", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanFalse }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_AND_FALSE_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("and", new List<String>(new String[] { EXETypes.BooleanFalse, EXETypes.BooleanTrue}));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_AND_FALSE_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("and", new List<String>(new String[] { EXETypes.BooleanFalse, EXETypes.BooleanFalse }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_BAD_AND_FALSE_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("and", new List<String>(new String[] { EXETypes.BooleanFalse }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_BAD_AND_FALSE_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("and", new List<String>(new String[] { EXETypes.BooleanTrue }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_BAD_AND_FALSE_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("and", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanTrue, EXETypes.BooleanTrue, "22" }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Evaluate_OR_TRUE_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("or", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanTrue }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_OR_TRUE_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("or", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanFalse }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_OR_TRUE_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("or", new List<String>(new String[] { EXETypes.BooleanFalse, EXETypes.BooleanTrue }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanTrue;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_OR_FALSE()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("or", new List<String>(new String[] { EXETypes.BooleanFalse, EXETypes.BooleanFalse }));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Evaluate_BAD_OR_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("or", new List<String>(new String[] { EXETypes.BooleanFalse }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_BAD_OR_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("or", new List<String>(new String[] { EXETypes.BooleanTrue }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_BAD_OR_3()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("or", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanTrue, EXETypes.BooleanTrue, "\"TRUE\"" }));
            Assert.IsNull(ActualOutput);

        }

        [TestMethod]
        public void Evaluate_NOT_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("not", new List<String>(new String[]{ EXETypes.BooleanTrue}));
            Console.WriteLine(ActualOutput);
            String ExpectedOutput = EXETypes.BooleanFalse;
            Console.WriteLine(ExpectedOutput);
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Evaluate_NOT_2()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.Evaluate("not", new List<String>(new String[] { EXETypes.BooleanTrue, EXETypes.BooleanFalse }));
            Assert.IsNull(ActualOutput);
        }

        [TestMethod]
        public void Normal_Clear_String_Test_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.GetClearStringFromEXETypeString("\"abcasdasdasdasdasdasdasdasd\"");
            String ExpectedOutput = "abcasdasdasdasdasdasdasdasd";
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void Normal_CreateEXETypeString_Test()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.CreateEXETypeString("a123bcasdasdasdasdasdasdasdasd");
            String ExpectedOutput = "\"a123bcasdasdasdasdasdasdasdasd\"";
            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }

        [TestMethod]
        public void IsValid_Normal_Equal_FloatString_1()
        {
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            String ActualOutput = Evaluator.IsValid("==", new String[] { "11.456", "11.456" });
            String ExpectedOutput = EXETypes.BooleanTrue;

            Assert.AreEqual(ExpectedOutput, ActualOutput);
        }
    }
}