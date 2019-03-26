using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;

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

        [TestMethod]
        public void GetFullChinesePhoneticAlphabet_Test()
        {
            var a = "专业版";
            Console.WriteLine(a.GetFullChinesePhoneticAlphabet());
        }

        enum Test
        {
            Value1 = 1,
            Value2 = 2
        }

        [TestMethod]
        public void ToEnum_Test()
        {
            byte value1 = 1;
            int value2 = 2;
            Console.WriteLine(value1.ToEnum<Test>());
            Console.WriteLine(value2.ToEnum<Test>());
            Console.WriteLine("Value1".ToEnum<Test>().ByteValue());
            Console.WriteLine("Value2".ToEnum<Test>().IntValue());
        }


        public class DataItem
        {
            public string ItemName { get; set; }
            public DateTime ItemDate { get; set; }
        }

        [TestMethod]
        public void ToDataTable_Test()
        {
            var items = new List<DataItem>
            {
                new DataItem{ ItemName = "itemName1", ItemDate = DateTime.Now.AddDays(-1)},
                new DataItem{ ItemName = "itemName2", ItemDate = DateTime.Now.AddDays(-2)},
                new DataItem{ ItemName = "itemName3", ItemDate = DateTime.Now.AddDays(-3)},
                new DataItem{ ItemName = "itemName4", ItemDate = DateTime.Now.AddDays(-4)},
                new DataItem{ ItemName = "itemName5", ItemDate = DateTime.Now.AddDays(-5)},
                new DataItem{ ItemName = "itemName6", ItemDate = DateTime.Now.AddDays(-6)},
                new DataItem{ ItemName = "itemName7", ItemDate = DateTime.Now.AddDays(-7)},
                new DataItem{ ItemName = "itemName8", ItemDate = DateTime.Now.AddDays(-8)},
                new DataItem{ ItemName = "itemName9", ItemDate = DateTime.Now.AddDays(-9)},
                new DataItem{ ItemName = "itemName10", ItemDate = DateTime.Now.AddDays(-10)},
            };

            var dataTable = items.ToDataTable();
            Assert.AreEqual(10, dataTable.Rows.Count);
            Assert.AreEqual(2, dataTable.Columns.Count);
            Assert.AreEqual("ItemName", dataTable.Columns[0].ColumnName);
            Assert.AreEqual("ItemDate", dataTable.Columns[1].ColumnName);

            foreach (DataRow item in dataTable.Rows)
            {
                Console.WriteLine(item[0] + " === " + item[1]);
            }
        }

        [TestMethod]
        public void Test_Object_To()
        {
            Console.WriteLine("True".To<int>(19));
        }
    }
}
