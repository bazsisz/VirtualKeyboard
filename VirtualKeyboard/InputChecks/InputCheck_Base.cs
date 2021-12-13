using System;
using System.Collections.Generic;
using System.ComponentModel;
using VirtualKeyboard.Bases;

namespace VirtualKeyboard.InputChecks
{
    public abstract class InputCheck_Base : NotifyPropertyChanged_Base, IDataErrorInfo
    {
        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, ref value);
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => ValidateProperty(columnName);

        public virtual string ValidateProperty(string propertyName)
        {
            List<string> result = new List<string>();

            if (propertyName == nameof(Text))
            {
                CheckInputText(result);
            }

            return string.Join(Environment.NewLine, result);
        }

        protected abstract bool CheckInputText(List<string> errors);
    }
}
