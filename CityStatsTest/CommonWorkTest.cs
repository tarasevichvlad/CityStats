using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CityStats;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CityStatsTest
{
    [TestClass]
    public class CommonWorkTest
    {
        private static CommonWork common;
        private static Dictionary<string, int> expected;
        private static string[] expected_s;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            expected_s = File.ReadAllText(@"C:\Users\typev\source\repos\Test_IHS\Test_IHS\bin\Debug\1.txt", Encoding.UTF8)
                .Split(new string[] { ",", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            common = new FileModeOfOperation();
            expected = new Dictionary<string, int>();
            for (int i = 0; i < expected_s.Length; i+=2)
            {
                if (expected.ContainsKey(expected_s[i].Substring(0, 1).ToUpper() + expected_s[i].Substring(1)))
                    expected[expected_s[i].Substring(0, 1).ToUpper() + expected_s[i].Substring(1)] += Convert.ToInt32(expected_s[i + 1]);
                else expected.Add(expected_s[i].Substring(0, 1).ToUpper() + expected_s[i].Substring(1), Convert.ToInt32(expected_s[i + 1]));
            }
            
        }
        [TestMethod]
        public void ConvertOfDictionary()
        {
            CollectionAssert.AreEqual(expected, (Dictionary<string, int>)GetResultIsMethod("ConvertOfDictionary", new object[] { expected_s }));
        }

        [TestMethod]
        public void GetParsedLine_Return_True()
        {
            Assert.IsTrue(Enumerable.SequenceEqual(expected_s, (string[])GetResultIsMethod("GetParsedLine", 
                new object[] { File.ReadAllText(@"C:\Users\typev\source\repos\Test_IHS\Test_IHS\bin\Debug\1.txt", Encoding.UTF8) })));
        }

        private object GetResultIsMethod(string nameMethod, object[] paramObjects)
        {
            return typeof(CommonWork).GetMethod(nameMethod, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance).Invoke(common, paramObjects);
        }

        [TestMethod]
        public void IsFileUtf8_Return_True()
        {
            Assert.IsTrue((bool)GetResultIsMethod("IsFileUtf8", new object[] { @"C:\Users\typev\source\repos\Test_IHS\Test_IHS\bin\Debug\1.txt" }));
        }


    }
}
