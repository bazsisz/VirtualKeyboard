using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using VirtualKeyboard.Bases;

namespace VirtualKeyboard
{
    class InputKeyboardViewmodel : NotifyPropertyChanged_Base
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, ref value);
        }

        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, ref value);
        }

        private TextBox _textBox;
        public TextBox TextBox
        {
            get => _textBox;
            set => SetProperty(ref _textBox, ref value);
        }

        private List<List<char>> _keyChars;
        public List<List<char>> KeyChars
        {
            get => _keyChars;
            set => SetProperty(ref _keyChars, ref value);
        }

        private int _caretPos;
        public int CaretPos
        {
            get => _caretPos;
            set => SetProperty(ref _caretPos, ref value);
        }

        private ICommand _charachterPressedCommand;
        public ICommand CharachterPressedCommand
        {
            get
            {
                if (_charachterPressedCommand == null)
                {
                    _charachterPressedCommand = new RelayCommand(param => OnKeyPressed(param));
                }
                return _charachterPressedCommand;
            }
        }

        private ICommand _spacePressedCommand;
        public ICommand SpacePressedCommand
        {
            get
            {
                if (_spacePressedCommand == null)
                {
                    _spacePressedCommand = new RelayCommand(param => OnSpacePressed());
                }
                return _spacePressedCommand;
            }
        }

        private ICommand _backSpacePressedCommand;
        public ICommand BackSpacePressedCommand
        {
            get
            {
                if (_backSpacePressedCommand == null)
                {
                    _backSpacePressedCommand = new RelayCommand(param => OnBackSpacePressed());
                }
                return _backSpacePressedCommand;
            }
        }


        public InputKeyboardViewmodel(Keyboard_Base keyboard, string inputText)
        {
            //KeyChars = keyboard.CharactersListByRow;
            CaretPos = 0;
            InputText = inputText ?? "";
        }
        private void OnKeyPressed(object param)
        {
            InputText = InputText.Insert(CaretPos, param.ToString());
            CaretPos++;
        }

        private void OnSpacePressed() 
        {
            InputText.Insert(CaretPos, " ");
            CaretPos++;
        }

        private void OnBackSpacePressed() 
        { 
            
        }
    }
}
