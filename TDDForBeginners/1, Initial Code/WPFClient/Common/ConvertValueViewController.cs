using System;
using System.ComponentModel;

namespace WPFClient.Common
{
    public class ConvertValueViewController<TInput, TOutput> : DynamicViewControllerBase
    {
        private readonly Func<TInput, Boolean> _validateInputMethod;
        private static readonly PropertyChangedEventArgs _outputChangedEventArgs =
            GetPropertyChangedArgs<ConvertValueViewController<TInput, TOutput>>(x => x.Output);

        public ConvertValueViewController(String viewName,
                                          Func<TInput, Boolean> validateInputMethod,
                                          Func<TInput, TOutput> convertMethod)
            : base(viewName)
        {
            if (convertMethod == null)
            {
                throw new ArgumentNullException("convertMethod");
            }
            _validateInputMethod = validateInputMethod;
            ConvertCommand = new DelegateCommand(
                () =>
                    {
                        Output = convertMethod(Input);
                        NotifyPropertyChanged(_outputChangedEventArgs);
                    },
                () => ErrorCount == 0);
        }

        public DelegateCommand ConvertCommand { get; private set; }
        public TInput Input { get; set; }
        public TOutput Output { get; protected set; }

        protected override void IsValidChanged()
        {
            ConvertCommand.RaiseCanExecuteChanged();
        }

        protected override bool Validate(string propertyName)
        {
            if (propertyName.Equals("Input", StringComparison.InvariantCultureIgnoreCase))
            {
                if (_validateInputMethod != null)
                {
                    try
                    {
                        return _validateInputMethod(Input);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}