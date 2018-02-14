using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CityStats;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CityStatsTest
{
    [TestClass]
    public class FileModeOfOperationTest
    {
        private static FileModeOfOperation common;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            common = new FileModeOfOperation();
        }

        [TestMethod]
        public void GetAllFileUtf8_Result_true()
        {
            
            string[] paramObjects = new string[] { @"G:\TestFileMode" };
            Assert.IsTrue(((List<string>)GetResultIsMethod("GetAllFileUtf8", paramObjects)).Count == 8);
        }

        private object GetResultIsMethod(string nameMethod, object[] paramObjects)
        {
            return typeof(FileModeOfOperation).GetMethod(nameMethod, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Invoke(common, paramObjects);
        }

        [TestMethod]
        public void OpenFileAndDownloadContent_Return_True()
        {
            object[] paramObjects = new object[] { @"G:\TestFileMode\1.txt" };
            Assert.IsTrue(File.ReadAllText(@"G:\TestFileMode\1.txt") == ((string)GetResultIsMethod("OpenFileAndDownloadContent", paramObjects)));
        }

        [TestMethod]
        public void Start()
        {
            object[] paramObjects = new object[] { @"G:\TestFileMode" };
            GetResultIsMethod("Start", paramObjects);
            Assert.IsTrue(File.Exists(@".\output.txt"));
        }

    }
}
