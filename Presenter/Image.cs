using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Presenter
{
    public class LocalImage : ContentControl
    {
        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName",
                                                                                                 typeof (String),
                                                                                                 typeof (LocalImage),
                                                                                                 new UIPropertyMetadata(
                                                                                                     new PropertyChangedCallback
                                                                                                         (OnFileNameChanged)));

        public String FileName
        {
            get { return (String) GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        private static void OnFileNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d is LocalImage) && (e.NewValue is String))
            {
                var localImage = d as LocalImage;
                String imagePath = Path.GetFullPath(e.NewValue as String);
                var uri = new Uri(imagePath, UriKind.Absolute);
                var bitmapImage = new BitmapImage(uri);
                localImage.Content = new Bitmap(bitmapImage);
            }
        }
    }
}