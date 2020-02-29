using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class OALParser2Tests
    {
        [TestMethod]
        public void Tokenize_Normal_01()
        {
            String Code = "x = a.bb *ba.a;";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(new String[] { "x", "=", "a", ".", "bb", "*", "ba", ".", "a", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Tokenize_Normal_02()
        {
            String Code = "x = a.bb *ba.a;\ncreate object instance obj of Object;\n";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(new String[] { "x", "=", "a", ".", "bb", "*", "ba", ".", "a", ";", "create", "object", "instance", "obj", "of", "Object", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Tokenize_Normal_03()
        {
            String Code = "if((not( x and y) or TRUE)) x = 4;\nend if;\n";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(new String[] { "if", "(", "(", "not", "(", "x", "and", "y", ")",
                "or", "TRUE", ")", ")", "x", "=", "4", ";", "end", "if", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Tokenize_Normal_04()
        {
            String Code = "if( x<=limit || result == TRUE) break;\nend if;\n";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(new String[] { "if", "(", "x", "<=", "limit", "||", "result", "==", "TRUE",
                ")", "break", ";", "end", "if", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Tokenize_Normal_05()
        {
            String Code = "user_name\t\n  = \"Matt\"  + (\"0;    8\");\n";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(new String[] { "user_name", "=", "\"Matt\"", "+", "(", "\"0;    8\"", ")", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Tokenize_Normal_06()
        {
            String Code = "  a   \t\n  = \"M\\\"M\";\n";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(new String[] { "a", "=", "\"M\\\"M\"", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Tokenize_Normal_07()
        {
            String Code = "relate o to s across Observer->Subject[R46];\n";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(new String[] { "relate", "o", "to", "s", "across", "Observer", "->", "Subject", "[", "R46", "]", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Tokenize_Normal_08()
        {
            String Code = "select many my_observer related by my_subject->Observer[R46];\n";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(new String[] { "select", "many", "my_observer", "related", "by", "my_subject", "->", "Observer", "[", "R46", "]", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Tokenize_Normal_09()
        {
            String Code = "par thread x =6;y = y + 1; thread y = y + 2;\t\nthread y = y+ 3; end par;\n";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(
                new String[] { "par", "thread", "x", "=", "6", ";", "y", "=", "y", "+", "1", ";", "thread", "y", "=", "y", "+", "2", ";", "thread", "y", "=", "y", "+", "3", ";", "end", "par", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Tokenize_Normal_10()
        {
            String Code = "unit.coord_x=(unit.coord_x+2)*1.2;\n";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(
                new String[] { "unit", ".", "coord_x", "=", "(", "unit", ".", "coord_x", "+", "2", ")", "*", "1", ".", "2", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
        [TestMethod]
        public void Tokenize_Normal_11()
        {
            String Code = "select any integer related by\n\t input_reader->PrimitiveTypeUtil[R1]->Integer[R3];\n";

            OALParser2 Parser = new OALParser2();
            List<String> ActualOutput = Parser.Tokenize(Code);

            List<String> ExpectedOutput = new List<String>(
                new String[] { "select", "any", "integer", "related", "by", "input_reader", "->", "PrimitiveTypeUtil", "[", "R1", "]", "->", "Integer", "[", "R3", "]", ";" });

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
    }
}