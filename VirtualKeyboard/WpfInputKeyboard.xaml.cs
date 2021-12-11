using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VirtualKeyboard
{
    public enum ControlShiftStates
    {
        NotActive,
        ActiveUntilButtonPressed,
        AlwaysActive
    }

    /// <summary>
    /// Interaction logic for WpfInputKeyboard.xaml
    /// </summary>
    internal partial class WpfInputKeyboard : MetroWindow, INotifyPropertyChanged
    {

        private Keyboard_Base _charSetKeyboard;

        public int CaretPos { get; set; }

        public string CopiedText { get; set; }

        public string SelectedText { get; set; }

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _fieldTitle;
        public string FieldTitle
        {
            get => _fieldTitle;
            set
            {
                if (_fieldTitle != value)
                {
                    _fieldTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _bottomButtonHeight;
        public double BottomButtonHeight
        {
            get => _bottomButtonHeight;
            set
            {
                if (_bottomButtonHeight != value)
                {
                    _bottomButtonHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSpeacialCharsChecked;
        public bool IsSpeacialCharsChecked
        {
            get => _isSpeacialCharsChecked;
            set
            {
                if (_isSpeacialCharsChecked != value)
                {
                    _isSpeacialCharsChecked = value;
                    KeyChars = _isSpeacialCharsChecked ? _charSetKeyboard.SpeacialCharacters.CharactersListByRow : _charSetKeyboard.Alphabet.CharactersListByRow;
                    specialCharsText.Text = _isSpeacialCharsChecked ? "abc" : "!#1";
                    ShiftManager.CurrentShiftState = ControlShiftStates.NotActive;
                    OnPropertyChanged();
                }
            }
        }

        private ShiftManager _shiftManager;
        public ShiftManager ShiftManager
        {
            get => _shiftManager;
            set
            {
                if (_shiftManager != value)
                {
                    _shiftManager = value;
                    OnPropertyChanged();

                }
            }
        }

        private ControlManager _controlManager;
        public ControlManager ControlManager
        {
            get => _controlManager;
            set
            {
                if (_controlManager != value)
                {
                    _controlManager = value;
                    OnPropertyChanged();

                }
            }
        }

        private List<List<char>> _keyChars;
        public List<List<char>> KeyChars
        {
            get => _keyChars;
            set
            {
                if (_keyChars != value)
                {
                    _keyChars = value;
                    OnPropertyChanged();
                }
            }
        }



        public WpfInputKeyboard(string charset)
        {

            InitializeComponent();
            DataContext = this;

            CopiedText = "";
            ShiftManager = new ShiftManager();
            ControlManager = new ControlManager();

            ShiftManager.PropertyChanged += ShiftManager_PropertyChanged;
            ControlManager.PropertyChanged += ControlManager_PropertyChanged;


            switch (charset)
            {
                case "HU":
                    _charSetKeyboard = new Hungarian_Keyboard();
                    break;
                case "EN":
                    _charSetKeyboard = new English_US_Keyboard();
                    break;
            }
            KeyChars = _charSetKeyboard.Alphabet.CharactersListByRow;
        }



        private void ShiftManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ShiftManager.CurrentShiftState))
            {
                OnPropertyChanged(nameof(ShiftManager));
            }
        }

        private void ControlManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ControlManager.CurrentCtrlState))
            {
                OnPropertyChanged(nameof(ControlManager));
            }
            else if (e.PropertyName == nameof(ControlManager.IsControlActive))
            {
                OnPropertyChanged(nameof(ControlManager));
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void OnSpace_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.SelectionLength > 0)
            {
                int selectionStartIndex = textBox.SelectionStart;
                Text = Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
                textBox.CaretIndex = CaretPos = selectionStartIndex;
                AddCharToText(ShiftManager.ApplyCasing(' ', true));
            }
            else
            {
                AddCharToText(ShiftManager.ApplyCasing(' ', true));
            }
        }

        private void Character_Click(object sender, RoutedEventArgs e)
        {
            if (ControlManager.IsCtrlActiveButtonPressed())
            {
                CombinationPressed(Convert.ToChar(((Button)sender).Content));
            }
            else
            {
                if (textBox.SelectionLength > 0)
                {
                    int selectionStartIndex = textBox.SelectionStart;
                    Text = Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
                    textBox.CaretIndex = CaretPos = selectionStartIndex;
                    AddCharToText(ShiftManager.ApplyCasing(Convert.ToChar(((Button)sender).Content), true));
                }
                else
                {
                    AddCharToText(ShiftManager.ApplyCasing(Convert.ToChar(((Button)sender).Content), true));
                }
            }
        }

        private void DecresaseCaretPos_Click(object sender, RoutedEventArgs e)
        {
            textBox.Focus();
            if (ControlManager.IsCtrlActiveButtonPressed())
            {
                if (CaretPos > 0)
                {
                    CaretPos--;
                    textBox.SelectionStart = CaretPos;
                    if (textBox.SelectionLength > 0)
                    {
                        textBox.SelectionLength++;
                    }
                    else
                    {
                        textBox.SelectionLength = 1;
                    }
                }
            }
            else
            {
                if (CaretPos > 0)
                {
                    CaretPos--;
                }
                textBox.CaretIndex = CaretPos;
            }
        }

        private void IncreaseCaretPos_Click(object sender, RoutedEventArgs e)
        {
            textBox.Focus();
            if (ControlManager.IsCtrlActiveButtonPressed())
            {
                if (CaretPos < Text.Length)
                {
                    CaretPos++;
                    if (textBox.SelectionLength > 0)
                    {
                        textBox.SelectionLength++;
                    }
                    else
                    {
                        textBox.SelectionStart = CaretPos - 1;
                        textBox.SelectionLength = 1;
                    }
                }
            }
            else
            {
                if (CaretPos < Text.Length)
                {
                    CaretPos++;
                }
                textBox.CaretIndex = CaretPos;
            }
        }

        private void BackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.SelectionLength > 0)
            {
                Text = Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
            }
            else
            {
                if (CaretPos > 0)
                {
                    Text = Text.Remove(CaretPos - 1, 1);
                    CaretPos--;
                }
            }

            textBox.Focus();
            textBox.CaretIndex = CaretPos;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.SelectionLength > 0)
            {
                Text = Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
            }
            else
            {
                if (CaretPos < Text.Length)
                {
                    Text = Text.Remove(CaretPos, 1);
                }
            }

            textBox.Focus();
            textBox.CaretIndex = CaretPos;
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.SelectionLength > 0)
            {
                int selectionStartIndex = textBox.SelectionStart;
                Text = Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
                textBox.CaretIndex = CaretPos = selectionStartIndex;
                AddCharToText(ShiftManager.ApplyCasing('\n', true));
            }
            else
            {
                AddCharToText(ShiftManager.ApplyCasing('\n', true));
            }
        }

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            textBox.Focus();
            IsSpeacialCharsChecked = false;
        }

        private void Shift_Click(object sender, RoutedEventArgs e)
        {
            textBox.Focus();
        }

        private void CombinationPressed(char character)
        {
            switch (character)
            {
                case 'a':
                    CtrlACombinationPressed();
                    break;
                case 'c':
                    CtrlCCombinationPressed();
                    break;
                case 'x':
                    CtrlXCombinationPressed();
                    break;
                case 'v':
                    CtrlVCombinationPressed();
                    break;
            }
        }

        private void CtrlACombinationPressed()
        {
            textBox.Focus();
            textBox.SelectAll();
        }

        private void CtrlCCombinationPressed()
        {
            textBox.Focus();
            int selectionStart = textBox.SelectionStart;
            int selectionLength = textBox.SelectionLength;
            CopiedText = Text.Substring(textBox.SelectionStart, textBox.SelectionLength);
            textBox.CaretIndex = CaretPos;
            textBox.SelectionStart = selectionStart;
            textBox.SelectionLength = selectionLength;
        }

        private void CtrlVCombinationPressed()
        {
            if (textBox.SelectionLength > 0)
            {
                int selectionStartIndex = textBox.SelectionStart;
                Text = Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
                textBox.CaretIndex = CaretPos = selectionStartIndex;
            }

            textBox.Focus();
            Text = Text.Insert(CaretPos, CopiedText);
            textBox.CaretIndex = CaretPos = CaretPos + CopiedText.Length;


        }

        private void CtrlXCombinationPressed()
        {
            textBox.Focus();
            if (textBox.SelectionLength > 0)
            {
                int selectionStartIndex = textBox.SelectionStart;
                CopiedText = Text.Substring(textBox.SelectionStart, textBox.SelectionLength);
                Text = Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
                textBox.CaretIndex = CaretPos = selectionStartIndex;
            }
            textBox.CaretIndex = CaretPos;

        }

        private void AddCharToText(char character)
        {
            textBox.Focus();
            Text = Text.Insert(CaretPos, character.ToString());
            CaretPos++;
            textBox.CaretIndex = CaretPos;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BottomButtonHeight = MainWindow.Height / 10;
        }

        private void TextSelectedViaClick(object sender, MouseButtonEventArgs e)
        {
            if (textBox.SelectionLength == 0)
            {
                CaretPos = textBox.CaretIndex;
            }
        }
        private void SaveSelectedTextOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox.SelectionLength > 0)
            {
                SelectedText = textBox.SelectedText;
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PropertyChanged event handler FAILED : {ex.Message}");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}