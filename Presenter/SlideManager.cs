using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace Presenter
{
    public class SlideManager : INotifyPropertyChanged
    {
        private int _currentIndex = -1;
        private Slide _currentSlide;

        private readonly List<String> _slides = new List<String>();

        public List<String> Slides
        {
            get { return _slides; }
        }

        public String SlideStyle { get; set; }

        public Slide CurrentSlide
        {
            get { return _currentSlide; }
        }

        public Int32 CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                if (_currentIndex != value)
                {
                    _currentIndex = value;
                    _currentSlide = LoadCurrentSlide();
                    OnPropertyChanged("CurrentSlide");
                    OnPropertyChanged("CanGoBack");
                    OnPropertyChanged("CanGoNext");
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public Boolean CanGoBack
        {
            get { return _currentIndex > 0; }
        }

        public Boolean CanGoNext
        {
            get { return _currentIndex < _slides.Count - 1; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private static Object LoadXamlFile(String fileName)
        {
            if (File.Exists(fileName))
            {
                var reader = new StreamReader(fileName);
                try
                {
                    return XamlReader.Load(reader.BaseStream);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return null;
        }

        private Slide LoadCurrentSlide()
        {
            var slide = LoadXamlFile(_slides[_currentIndex]) as Slide;
            if (slide != null)
            {
                var style = LoadXamlFile(SlideStyle) as ResourceDictionary;
                if (style != null)
                {
                    slide.Resources.MergedDictionaries.Add(style);
                }
            }
            return slide;
        }

        public void GoBack()
        {
            if (CanGoBack)
            {
                CurrentIndex--;
            }
        }

        public void GoNext()
        {
            if (CanGoNext)
            {
                CurrentIndex++;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}