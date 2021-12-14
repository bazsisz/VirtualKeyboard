using System;
using System.Collections.Generic;
using System.ComponentModel;
using VirtualKeyboard.Bases;
using VirtualKeyboard.InputChecks;
using static VirtualKeyboard.VirtualKeyboardTextBoxControl;

namespace VirtualKeyboard
{
    public class TextModel : NotifyPropertyChanged_Base, IDataErrorInfo
    {
        private IInputCheck InputCheck_None = new InputCheck_None();
        private IInputCheck InputCheck_Double = new InputCheck_Double();

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, ref value);
        }

        public IInputCheck InputCheck { get; private set; }

        public void SetInputCheck(InputCheckType inputCheckType) 
        {
            switch (inputCheckType)
            {
                case InputCheckType.None:
                    InputCheck = InputCheck_None;
                    break;
                case InputCheckType.Double:
                    InputCheck = InputCheck_Double;
                    break;
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => ValidateProperty(columnName);

        public virtual string ValidateProperty(string propertyName)
        {
            List<string> result = new List<string>();

            if (propertyName == nameof(Text))
            {
                InputCheck.CheckInputText(result, Text);
            }

            return string.Join(Environment.NewLine, result);
        }
    }
}
