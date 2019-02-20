using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace WPFPresenter
{
    /// <summary>
    /// Interaction logic for _03Slide.xaml
    /// </summary>
    public partial class _05Slide : Page
    {
        public _05Slide()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("devenv.exe");
            }
            catch
            {

            }
        }
    }
}
