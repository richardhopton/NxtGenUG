using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Presenter
{
    public class OnClickShellExecute
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof (String), typeof (OnClickShellExecute),
                                                new UIPropertyMetadata(new PropertyChangedCallback(OnCommandChanged)));

        public static String GetCommand(DependencyObject obj)
        {
            return (String) obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, String value)
        {
            obj.SetValue(CommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...

        private static void OnCommandChanged(DependencyObject p, DependencyPropertyChangedEventArgs args)
        {
            if (p is Button)
            {
                var button = p as Button;
                button.Click += OnButton_Click;
            }
        }

        private static void OnButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                var button = sender as Button;
                String commandPath = Path.GetFullPath(GetCommand(button));
                if (File.Exists(commandPath))
                {
                    Process.Start(commandPath);
                }
            }
        }
    }
}