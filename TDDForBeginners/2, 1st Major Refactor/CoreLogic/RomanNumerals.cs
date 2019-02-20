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

        public static String Convert(Int32 input)
        {
            StringBuilder sb = new StringBuilder();
            if (input <= 3)
            {
                for (int value = 1; value <= input; value++)
                {
                    sb.Append("I");
                }
            }
            else if (input == 4)
            {
                sb.Append("IV");
            }
            else if ((input >= 5) && (input <= 8))
            {
                sb.Append("V");
                sb.Append(Convert(input - 5));
            }
            else if (input == 9)
            {
                sb.Append("IX");
            } 
            else
            {
                sb.Append("X");
            }
            return sb.ToString();
        }
    }
}
