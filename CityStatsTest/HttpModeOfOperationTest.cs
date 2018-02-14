using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using CityStats;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CityStatsTest
{
    [TestClass]
    public class HttpModeOfOperationTest
    {
        private static HttpModeOfOperation common;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            common = new HttpModeOfOperation();
        }

        [TestMethod]
        public void DownloadContentPage_Result_True()
        {
            string str = new WebClient().DownloadString("file://C:/Users/typev/source/repos/Test_IHS/Test_IHS/bin/Debug/1.HTML");
            object[] paramObjects = new object[] { "file://C:/Users/typev/source/repos/Test_IHS/Test_IHS/bin/Debug/1.HTML" };
            object resultIsMethod = GetResultIsMethod("DownloadContentPage", paramObjects);
            Assert.IsTrue(str == ((string)resultIsMethod));
        }

        [TestMethod]
        public void GetListUrl()
        {
            Assert.IsTrue(((List<string>)GetResultIsMethod("GetListUrl", new object[] { @"G:\TestHttpMode\test.txt" })).Count == 8);
        }

        private object GetResultIsMethod(string nameMethod, object[] paramObjects)
        {
            return typeof(HttpModeOfOperation).GetMethod(nameMethod, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Invoke(common, paramObjects);
        }

        [TestMethod]
        public void Start_1()
        {
            GetResultIsMethod("Start", new object[] { @"G:\TestHttpMode\test.txt" });
            Assert.IsTrue(File.Exists(@".\output.txt"));
        }

    }
}
