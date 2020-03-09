using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXETypesTests
    {
        [TestMethod]
        public void DetermineVariableType_String_Normal_01()
        {
            Assert.AreEqual(EXETypes.StringTypeName, EXETypes.DetermineVariableType("name", "\"Margaret\""));
        }
        [TestMethod]
        public void DetermineVariableType_Real_Normal_01()
        {
            Assert.AreEqual(EXETypes.RealTypeName, EXETypes.DetermineVariableType(null, "11.456"));
        }
        [TestMethod]
        public void DetermineVariableType_Boolean_Normal_01()
        {
            Assert.AreEqual(EXETypes.BooleanTypeName, EXETypes.DetermineVariableType(null, EXETypes.BooleanTrue));
        }
        [TestMethod]
        public void DetermineVariableType_Integer_Normal_01()
        {
            Assert.AreEqual(EXETypes.IntegerTypeName, EXETypes.DetermineVariableType(null, "3452"));
        }
    }
}