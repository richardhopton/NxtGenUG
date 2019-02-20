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

        public static String Convert(Int32 input)
        {
            StringBuilder sb = new StringBuilder();
            AddDigitToString(ref input, 90, "XC", sb);
            AddDigitToString(ref input, 50, "L", sb);
            AddDigitToString(ref input, 40, "XL", sb);
            while (input >= 10)
            {
                AddDigitToString(ref input, 10, "X", sb);
            }

            AddDigitToString(ref input, 9, "IX", sb);
            AddDigitToString(ref input, 5, "V", sb);
            AddDigitToString(ref input, 4, "IV", sb);
            while (input >= 1)
            {
                AddDigitToString(ref input, 1, "I", sb);
            }
            return sb.ToString();
        }
    }
}
