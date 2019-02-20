using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Presenter
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Main
    {
        private readonly SlideManager _slideManager;
        private readonly DispatcherTimer _timer;

        public Main()
        {
            InitializeComponent();
            _slideManager = (SlideManager) FindResource("slideManager");

            DateTime latestWrittenDateTime = DateTime.MinValue;
            Int32 latestWrittenIndex = 0;

            var directory = new DirectoryInfo(".");
            foreach (FileSystemInfo fileInfo in directory.GetFileSystemInfos("*.slide"))
            {
                if (fileInfo.LastWriteTime.CompareTo(latestWrittenDateTime) > 0)
                {
                    latestWrittenDateTime = fileInfo.LastWriteTime;
                    latestWrittenIndex = _slideManager.Slides.Count;
                }
                _slideManager.Slides.Add(fileInfo.Name);
            }

            if (_slideManager.Slides.Count == 0)
            {
                MessageBox.Show("No Slides Found");
                Application.Current.Shutdown();
            }
            else
            {
                FileInfo[] style = directory.GetFiles("presentation.style");
                if (style.Length == 1)
                {
                    _slideManager.SlideStyle = style[0].Name;
                }
                _slideManager.CurrentIndex = HasDebugAttribute ? latestWrittenIndex : 0;
            }

            // Initializes timer that causes the mouse cursor to disappear after 5 seconds.
            _timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(5)};
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private bool HasDebugAttribute
        {
            get
            {
                Assembly assemb = GetType().Assembly;
                bool retVal = false;
                foreach (object att in assemb.GetCustomAttributes(false))
                {
                    if (att.GetType() == Type.GetType("System.Diagnostics.DebuggableAttribute"))
                    {
                        retVal = ((DebuggableAttribute) att).IsJITTrackingEnabled;
                    }
                }
                return retVal;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.None;
        }

        private void PreviousSlide_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_slideManager != null)
            {
                e.CanExecute = _slideManager.CanGoBack;
            }
        }

        private void PreviousSlide_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _slideManager.GoBack();
        }

        private void NextSlide_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_slideManager != null)
            {
                e.CanExecute = _slideManager.CanGoNext;
            }
        }

        private void NextSlide_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _slideManager.GoNext();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                _slideManager.GoBack();
                e.Handled = true;
            }
            else if (e.Key == Key.Right)
            {
                _slideManager.GoNext();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
                e.Handled = true;
            }
        }

        private void ShowCursor()
        {
            _timer.Start();
            ClearValue(CursorProperty);
        }

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowCursor();
            Point currentPoint = e.GetPosition(this);
            var transform = new TranslateTransform(currentPoint.X, currentPoint.Y);
            clickCanvas.RenderTransform = transform;
        }

        private void Window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            ShowCursor();
        }
    }
}