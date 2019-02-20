using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreLogic
{
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

        private static void AddDigitRangeToString(ref Int32 input, Int32 highValue, String highDigit,
            Int32 middleValue, String middleDigit, Int32 lowValue, String lowDigit, StringBuilder sb)
        {
            AddDigitToString(ref input, highValue-lowValue, String.Concat(lowDigit,highDigit), sb);
            AddDigitToString(ref input, middleValue, middleDigit, sb);
            AddDigitToString(ref input, middleValue - lowValue, String.Concat(lowDigit, middleDigit), sb);
            while (input >= lowValue)
            {
                AddDigitToString(ref input, lowValue, lowDigit, sb);
            }
        }

        public static String Convert(Int32 input)
        {
            StringBuilder sb = new StringBuilder();
            AddDigitRangeToString(ref input, 1000, "M", 500, "D", 100, "C", sb);
            AddDigitRangeToString(ref input, 100, "C", 50, "L", 10, "X", sb);
            AddDigitRangeToString(ref input, 10, "X", 5, "V", 1, "I", sb);
            return sb.ToString();
        }
    }
}
