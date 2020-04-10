using Microsoft.VisualStudio.TestTools.UnitTesting;
using OALProgramControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace OALProgramControl.Tests
{
    [TestClass]
    public class EXEReferenceHandleTests
    {
        [TestMethod]
        public void GetReferencedIds_Normal_01()
        {
            EXEReferenceHandle RefHandle = new EXEReferencingVariable("observer", "Observer", 15);
            
            List<long> ActualIds = RefHandle.GetReferencedIds();
            List<long> ExpectedIds = new List<long>(new long[] { 15 });

            CollectionAssert.AreEquivalent(ExpectedIds, ActualIds);
        }
    }
}