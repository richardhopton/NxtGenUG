using System;
using WPFClient.Common;

namespace WPFClient
{
    public partial class App
    {
        public App()
        {
            Func<Int32, Boolean> validateInput = x => true;  // RomanNumerals.ValidateInput;
            Func<Int32, String> convert = x => String.Empty; // RomanNumerals.Convert;

            IViewController view = new ConvertValueViewController<Int32, String>(
                "Views\\RomanNumeralsView.xaml", validateInput, convert);

            view.ShowDialog();
        }
    }
}