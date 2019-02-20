using System;
using System.Windows;
using System.Windows.Controls;

namespace Presenter
{
    public class Slide : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof (String),
                                                                                              typeof (Slide),
                                                                                              new UIPropertyMetadata(
                                                                                                  String.Empty));

        public String Title
        {
            get { return (String) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
    }
}