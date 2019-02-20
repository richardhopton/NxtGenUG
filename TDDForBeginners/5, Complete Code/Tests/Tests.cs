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
        public void Should_ValidateInput_Given_Input_1()
        {
            Assert.IsTrue(RomanNumerals.ValidateInput(1));
        }

        [TestMethod]
        public void Should_ValidateInput_Given_Input_2()
        {
            Assert.IsTrue(RomanNumerals.ValidateInput(2));
        }

        [TestMethod]
        public void Should_ValidateInput_Given_Input_Between1And3999()
        {
            for (int input = 1; input <= 3999; input++)
            {
                Assert.IsTrue(RomanNumerals.ValidateInput(input), String.Format("Value {0} not correctly validated", input));
            }
        }

        [TestMethod]
        public void Should_NotValidateInput_Given_Input_LessThan1()
        {
            for (int input = -999999; input <= 0; input++)
            {
                Assert.IsFalse(RomanNumerals.ValidateInput(input), String.Format("Value {0} not correctly validated", input));
            }
        }


        [TestMethod]
        public void Should_NotValidateInput_Given_Input_GreaterThan3999()
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

        [TestMethod]
        public void Should_Return_XI_Given_Input_11()
        {
            Assert.AreEqual("XI", RomanNumerals.Convert(11));
        }


        [TestMethod]
        public void Should_Return_XII_Given_Input_12()
        {
            Assert.AreEqual("XII", RomanNumerals.Convert(12));
        }

        [TestMethod]
        public void Should_Return_XIII_Given_Input_13()
        {
            Assert.AreEqual("XIII", RomanNumerals.Convert(13));
        }

        [TestMethod]
        public void Should_Return_XIV_Given_Input_14()
        {
            Assert.AreEqual("XIV", RomanNumerals.Convert(14));
        }

        [TestMethod]
        public void Should_Return_XV_Given_Input_15()
        {
            Assert.AreEqual("XV", RomanNumerals.Convert(15));
        }

        [TestMethod]
        public void Should_Return_XVI_Given_Input_16()
        {
            Assert.AreEqual("XVI", RomanNumerals.Convert(16));
        }

        [TestMethod]
        public void Should_Return_XVII_Given_Input_17()
        {
            Assert.AreEqual("XVII", RomanNumerals.Convert(17));
        }

        [TestMethod]
        public void Should_Return_XVIII_Given_Input_18()
        {
            Assert.AreEqual("XVIII", RomanNumerals.Convert(18));
        }

        [TestMethod]
        public void Should_Return_XIX_Given_Input_19()
        {
            Assert.AreEqual("XIX", RomanNumerals.Convert(19));
        }

        [TestMethod]
        public void Should_Return_XX_Given_Input_20()
        {
            Assert.AreEqual("XX", RomanNumerals.Convert(20));
        }

        [TestMethod]
        public void Should_Return_XL_Given_Input_40()
        {
            Assert.AreEqual("XL", RomanNumerals.Convert(40));
        }

        [TestMethod]
        public void Should_Return_L_Given_Input_50()
        {
            Assert.AreEqual("L", RomanNumerals.Convert(50));
        }

        [TestMethod]
        public void Should_Return_XC_Given_Input_90()
        {
            Assert.AreEqual("XC", RomanNumerals.Convert(90));
        }

        [TestMethod]
        public void Should_Return_C_Given_Input_100()
        {
            Assert.AreEqual("C", RomanNumerals.Convert(100));
        }

        [TestMethod]
        public void Should_Return_CC_Given_Input_200()
        {
            Assert.AreEqual("CC", RomanNumerals.Convert(200));
        }

        [TestMethod]
        public void Should_Return_M_Given_Input_1000()
        {
            Assert.AreEqual("M", RomanNumerals.Convert(1000));
        }

        [TestMethod]
        public void Should_Return_MM_Given_Input_2000()
        {
            Assert.AreEqual("MM", RomanNumerals.Convert(2000));
        }

        [TestMethod]
        public void Should_Return_MMMCMXCIX_Given_Input_3999()
        {
            Assert.AreEqual("MMMCMXCIX", RomanNumerals.Convert(3999));
        }
    }
}
