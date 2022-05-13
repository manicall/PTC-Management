using Microsoft.VisualStudio.TestTools.UnitTesting;

using PTC_Management.EF;
using PTC_Management.Model;

using System;

namespace UnitTests
{
    [TestClass]
    public class MyLiteralsTest
    {
        [TestMethod]
        public void FilterText_Test()
        {
            string expected = "FilterEmployeeText";

            string actual = MyLiterals<Employee>.FilterText;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Items_Test()
        {
            string expected = "EmployeeItems";

            string actual = MyLiterals<Employee>.Items;

            Assert.AreEqual(expected, actual);
        }
    }
}
