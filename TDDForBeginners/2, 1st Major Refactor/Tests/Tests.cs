using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreLogic;

namespace Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class Tests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Should_Validate_Input_Given_Input_1()
        {
            Assert.IsTrue(RomanNumerals.ValidateInput(1));
        }

        [TestMethod]
        public void Should_Validate_Input_Given_Input_2()
        {
            Assert.IsTrue(RomanNumerals.ValidateInput(2));
        }

        [TestMethod]
        public void Should_Validate_Input_Given_Input_Between1And3999()
        {
            for (int input = 1; input <= 3999; input++)
            {
                Assert.IsTrue(RomanNumerals.ValidateInput(input), String.Format("Value {0} not correctly validated", input));
            }
        }

        [TestMethod]
        public void Should_NotValidate_Input_Given_Input_LessThan1()
        {
            for (int input = -999999; input <= 0; input++)
            {
                Assert.IsFalse(RomanNumerals.ValidateInput(input), String.Format("Value {0} not correctly validated", input));
            }
        }


        [TestMethod]
        public void Should_NotValidate_Input_Given_Input_GreaterThan3999()
        {
            for (int input = 4000; input <= 999999; input++)
            {
                Assert.IsFalse(RomanNumerals.ValidateInput(input), String.Format("Value {0} not correctly validated", input));
            }
        }

        [TestMethod]
        public void Should_Return_I_Given_Input_1()
        {
            Assert.AreEqual("I", RomanNumerals.Convert(1));
        }

        [TestMethod]
        public void Should_Return_II_Given_Input_2()
        {
            Assert.AreEqual("II", RomanNumerals.Convert(2));
        }

        [TestMethod]
        public void Should_Return_III_Given_Input_3()
        {
            Assert.AreEqual("III", RomanNumerals.Convert(3));
        }

        [TestMethod]
        public void Should_Return_IV_Given_Input_4()
        {
            Assert.AreEqual("IV", RomanNumerals.Convert(4));
        }

        [TestMethod]
        public void Should_Return_V_Given_Input_5()
        {
            Assert.AreEqual("V", RomanNumerals.Convert(5));
        }

        [TestMethod]
        public void Should_Return_VI_Given_Input_6()
        {
            Assert.AreEqual("VI", RomanNumerals.Convert(6));
        }

        [TestMethod]
        public void Should_Return_VII_Given_Input_7()
        {
            Assert.AreEqual("VII", RomanNumerals.Convert(7));
        }

        [TestMethod]
        public void Should_Return_VIII_Given_Input_8()
        {
            Assert.AreEqual("VIII", RomanNumerals.Convert(8));
        }

        [TestMethod]
        public void Should_Return_IX_Given_Input_9()
        {
            Assert.AreEqual("IX", RomanNumerals.Convert(9));
        }

        [TestMethod]
        public void Should_Return_X_Given_Input_10()
        {
            Assert.AreEqual("X", RomanNumerals.Convert(10));
        }
    }
}
