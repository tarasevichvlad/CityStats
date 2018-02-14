using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CityStats;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace CityStatsTest
{
    [TestClass]
    public class SityAmountTest
    {

        private static CityAmount cityAmount;
        private static Dictionary<string, int> expected;
        private static FieldInfo fieldInfo;
        private static Dictionary<string, int> сityCount;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            cityAmount = new CityAmount();
            fieldInfo = typeof(CityAmount).GetField("cityAmounts", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null)
            {
                сityCount = (Dictionary<string, int>)fieldInfo.GetValue(cityAmount);
            }
            Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
            dictionary1.Add("Париж", 5);
            dictionary1.Add("Венеция", 14);
            dictionary1.Add("Лондон", 2);
            dictionary1.Add("Великобритания", 0x16);
            expected = dictionary1;
        }

        [TestMethod]
        public void AddCityAmount_AddItem_return_Count4()
        {
            cityAmount.AddCityAmount(expected);
            if (fieldInfo != null)
            {
                сityCount = (Dictionary<string, int>)fieldInfo.GetValue(cityAmount);
            }
            CollectionAssert.AllItemsAreNotNull(сityCount);
            CollectionAssert.AreEqual(expected.Keys, сityCount.Keys);
        }

        [TestMethod]
        public void AddCityAmount_Unique()
        {
            cityAmount.AddCityAmount(expected);
            cityAmount.AddCityAmount(expected);
            if (сityCount != null)
            {
                CollectionAssert.AllItemsAreUnique(сityCount);
            }
        }

        [TestMethod]
        public void SaveAllItemСity_Save_Return_True()
        {
            cityAmount.AddCityAmount(expected);
            cityAmount.SaveAllItemСity();
            Assert.IsTrue(File.Exists(@".\output.txt"));
        }
    }
}
