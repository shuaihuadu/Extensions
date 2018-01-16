using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
        [TestMethod]
        public void ReplaceSpecialSharacters_Test()
        {
            string url = "http://dasd.dvadc.ad/asoiduhasjo;dl,.asdasdakjdlashdkjasdh";

            Console.WriteLine(url.ReplaceSpecialSharacters());
        }
        [TestMethod]
        public void FirstCharToUpper_Test()
        {
            string url = "http://dasd.dvadc.ad/asoiduhasjo;dl,.asdasdakjdlashdkjasdh";

            Console.WriteLine(url.ReplaceSpecialSharacters().FirstCharToUpper());
        }
        [TestMethod]
        public void ReplaceSpecialSharactersAndFirstCharToUpper_Test()
        {
            string url = "Public Company Limited by Shares (Listed)";
            List<string> strs = new List<string>();
            foreach (var item in url.ReplaceSpecialSharacters(' ').Split(' '))
            {
                strs.Add(item.FirstCharToUpper());
            }
            Console.WriteLine(string.Join("", strs.ToArray()));
        }
        [TestMethod]
        public void Between_Test()
        {
            int a = 1;
            Console.WriteLine(a.Between(1, 2));
        }
        [TestMethod]
        public void Is_Test()
        {
            Assert.IsTrue("12873".IsInt32());
            Assert.IsFalse("dasd".IsInt32());
        }
    }
}
