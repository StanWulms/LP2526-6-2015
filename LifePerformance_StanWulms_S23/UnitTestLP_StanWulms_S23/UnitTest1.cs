using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLP_StanWulms_S23
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckConnection()
        {
            
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}
