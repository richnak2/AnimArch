using AnimationControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnimationControlTests
{
    [TestClass]
    public class OALCommandParserTests
    {
        [TestMethod]
        public void TestTokenizeTopLevelBracketChunks_Normal1()
        {
            OALCommandParser OCP = new OALCommandParser();

            List<String> ExpectedOutput = new List<String>(new String[] { "6 + 12", " - ", "3 * (7 + 2) + (6)" });

            String Input = "(6 + 12) - (3 * (7 + 2) + (6))";
            List<String> ActualOutput = OCP.TokenizeTopLevelBracketChunks(Input);

            CollectionAssert.AreEqual(ExpectedOutput, ActualOutput);
        }
    }
}
