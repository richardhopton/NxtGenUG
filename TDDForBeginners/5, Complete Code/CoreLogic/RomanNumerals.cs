using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreLogic
{
    internal struct ValueDigitPair
    {
        public String Digit;
        public Int32 Value;

        public ValueDigitPair(Int32 value, String digit)
        {
            Value = value;
            Digit = digit;
        }
    }

    public static class RomanNumerals
    {
        public static Boolean ValidateInput(Int32 input)
        {
            return ((input >= 1) && (input <= 3999));
        }

        private static void AddDigitToString(ref Int32 input, Int32 value, String digit, StringBuilder sb)
        {
            if (input >= value)
            {
                sb.Append(digit);
                input -= value;
            }
        }

        private static void AddDigitRangeToString(ref Int32 input, ValueDigitPair high, ValueDigitPair middle,
                                                  ValueDigitPair low, StringBuilder sb)
        {
            AddDigitToString(ref input, high.Value - low.Value, String.Concat(low.Digit, high.Digit), sb);
            AddDigitToString(ref input, middle.Value, middle.Digit, sb);
            AddDigitToString(ref input, middle.Value - low.Value, String.Concat(low.Digit, middle.Digit), sb);
            while (input >= low.Value)
            {
                AddDigitToString(ref input, low.Value, low.Digit, sb);
            }
        }

        public static String Convert(Int32 input)
        {
            var sb = new StringBuilder();
            var thousand = new ValueDigitPair(1000, "M");
            var fiveHundred = new ValueDigitPair(500, "D");
            var oneHundred = new ValueDigitPair(100, "C");
            var fifty = new ValueDigitPair(50, "L");
            var ten = new ValueDigitPair(10, "X");
            var five = new ValueDigitPair(5, "V");
            var one = new ValueDigitPair(1, "I");
            while (input >= 1000)
            {
                AddDigitToString(ref input, thousand.Value, thousand.Digit, sb);
            }
            AddDigitRangeToString(ref input, thousand, fiveHundred, oneHundred, sb);
            AddDigitRangeToString(ref input, oneHundred, fifty, ten, sb);
            AddDigitRangeToString(ref input, ten, five, one, sb);
            return sb.ToString();
        }
    }
}
