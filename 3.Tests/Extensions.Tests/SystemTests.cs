using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Extensions.Tests
{
    [TestClass]
    public class SystemTests
    {
        [TestMethod]
        public void TestStringExtensions()
        {
            string url = "http://dasd.dvadc.ad/asoiduhasjo;dl,.asdasdakjdlashdkjasdh";
            Assert.IsTrue(url.IsUrl());
            string url1 = "adcad";
            Assert.IsFalse(url1.IsUrl());
        }
    }
}
