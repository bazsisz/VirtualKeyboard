using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VirtualKeyboard.Bases;
using VirtualKeyboard.InputChecks;
using static VirtualKeyboard.VirtualKeyboardControl;

namespace VirtualKeyboard
{
    public class TextModel : NotifyPropertyChanged_Base
    {
        private IInputCheck InputCheck_None = new InputCheck_None();
        private IInputCheck InputCheck_Double = new InputCheck_Double();
        private List<string> _errorList = new List<string>();

        private string _errors;
        public string Errors
        {
            get => _errors;
            set
            {
                SetProperty(ref _errors, ref value);
                OnPropertyChanged(nameof(HasError));
            }
        }

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                SetProperty(ref _text, ref value);
                Errors = ValidateText();
            }
        }

        public bool HasError => !string.IsNullOrEmpty(Errors);

        public IInputCheck InputCheck { get; private set; }

        private string ValidateText()
        {
            _errorList.Clear();
            InputCheck.CheckInputText(_errorList, Text);
            return string.Join(Environment.NewLine, _errorList);
        }


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
    }
}
