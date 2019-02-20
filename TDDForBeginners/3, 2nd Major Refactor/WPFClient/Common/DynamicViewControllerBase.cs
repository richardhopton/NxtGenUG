using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WPFClient.Common
{
    public abstract class DynamicViewControllerBase : INotifyPropertyChanged, IDataErrorInfo, IViewController
    {
        private readonly Window _view;

        protected DynamicViewControllerBase(String viewName) : this(LoadWindow(viewName))
        {
        }

        private DynamicViewControllerBase(Window view)
        {
            _view = view;
            _view.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
            _view.DataContext = this;
        }

        protected int ErrorCount { get; private set; }

        #region IDataErrorInfo Members

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                if (!Validate(columnName))
                {
                    return columnName;
                }
                return null;
            }
        }

        #endregion

        private readonly PropertyChangedEventArgs _nullArgs = new PropertyChangedEventArgs(null);

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, args);
            }
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void NotifyPropertyChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, _nullArgs);
            }
        }

        protected static PropertyChangedEventArgs GetPropertyChangedArgs<T>(
            Expression<Func<T, Object>> propertyExpression)
        {
            return new PropertyChangedEventArgs(PropertyHelper.GetPropertyName(propertyExpression));
        }

        #region IViewController Members

        public void ShowDialog()
        {
            _view.ShowDialog();
        }

        #endregion

        private static Window LoadWindow(String viewName)
        {
            if (File.Exists(viewName))
            {
                using (FileStream fs = File.OpenRead(viewName))
                {
                    return XamlReader.Load(fs) as Window;
                }
            }
            return null;
        }

        private void OnErrorEvent(object sender, RoutedEventArgs e)
        {
            var validationEventArgs = e as ValidationErrorEventArgs;
            if (validationEventArgs == null)
                throw new Exception("Unexpected event args");
            switch (validationEventArgs.Action)
            {
                case ValidationErrorEventAction.Added:
                    ErrorCount++;
                    if (ErrorCount == 1)
                    {
                        IsValidChanged();
                    }
                    break;
                case ValidationErrorEventAction.Removed:
                    ErrorCount--;
                    if (ErrorCount == 0)
                    {
                        IsValidChanged();
                    }
                    break;
                default:
                    throw new Exception("Unknown action");
            }
        }

        protected abstract void IsValidChanged();
        protected abstract Boolean Validate(String propertyName);
    }
}