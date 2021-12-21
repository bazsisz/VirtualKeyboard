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
using VirtualKeyboard.InputChecks;
using static VirtualKeyboard.VirtualKeyboardControl;

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

        private KeyboardModel KeyboardModel;
        private double _sideButtonsWidthRatio;
        private double _minSideButtonWidth = 50;
        private double _sideButtonsHeightRatio;
        private double _minSideButtonHeight = 40;

        public int CaretPos { get; set; }
        public string CopiedText { get; set; }

        public string Text
        {
            get => _textModel.Text;
            set
            {
                if (_textModel.Text != value)
                {
                    _textModel.Text = value;
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

        private double _sideButtonsHeight;
        public double SideButtonsHeight
        {
            get => _sideButtonsHeight;
            set
            {
                if (_sideButtonsHeight != value)
                {
                    _sideButtonsHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _sideButtonsWidth;
        public double SideButtonsWidth
        {
            get => _sideButtonsWidth;
            set
            {
                if (_sideButtonsWidth != value)
                {
                    _sideButtonsWidth = value;
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

                    KeyChars = _isSpeacialCharsChecked ? KeyboardModel.CharSetKeyboard.SpeacialCharacters.CharactersListByRow
                                                       : KeyboardModel.CharSetKeyboard.Alphabet.CharactersListByRow;

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

        private TextModel _textModel;
        public TextModel TextModel
        {
            get => _textModel;
            set
            {
                if (_textModel != value)
                {
                    _textModel = value;
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

        public WpfInputKeyboard(CharSetLanguage charset, InputCheckType inputCheckType)
        {

            InitializeComponent();
            DataContext = this;

            CopiedText = "";

            _sideButtonsHeightRatio = _minSideButtonHeight / Height;
            _sideButtonsWidthRatio = _minSideButtonWidth / Width;

            TextModel = new TextModel();
            KeyboardModel = new KeyboardModel();
            ShiftManager = new ShiftManager();
            ControlManager = new ControlManager();

            ShiftManager.PropertyChanged += ShiftManager_PropertyChanged;
            ControlManager.PropertyChanged += ControlManager_PropertyChanged;

            TextModel.SetInputCheck(inputCheckType);
            KeyboardModel.SetCharSetKeyboard(charset);
            KeyChars = KeyboardModel.CharSetKeyboard.Alphabet.CharactersListByRow;
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
            AppendText(' ');
        }

        private void Character_Click(object sender, RoutedEventArgs e)
        {
            string charachter = ((TextBlock)((Viewbox)((Button)sender).Content).Child).Text;
            if (ControlManager.IsCtrlActiveButtonPressed())
            {
                CombinationPressed(Convert.ToChar(charachter));
            }
            else
            {
                AppendText(Convert.ToChar(charachter));
            }
        }

        private void DecresaseCaretPos_Click(object sender, RoutedEventArgs e)
        {
            textBox.Focus();
            if (ShiftManager.IsShiftActiveButtonPressed())
            {
                CombinationPressed('<');
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
            if (ShiftManager.IsShiftActiveButtonPressed())
            {
                CombinationPressed('>');
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
            AppendText('\n');
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
                case '<':
                    CtrlDecreaseCombinationPressed();
                    break;
                case '>':
                    CtrlIncreaseCombinationPressed();
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

        private void CtrlDecreaseCombinationPressed()
        {
            if (CaretPos > 0)
            {
                CaretPos--;
                if (textBox.SelectionLength > 0)
                {
                    if (CaretPos >= textBox.SelectionStart)
                    {
                        textBox.SelectionLength--;
                    }
                    else
                    {
                        textBox.SelectionStart = CaretPos;
                        textBox.SelectionLength++;
                    }
                }
                else
                {
                    textBox.SelectionStart = CaretPos;
                    textBox.SelectionLength = 1;
                }
            }
        }

        private void CtrlIncreaseCombinationPressed()
        {
            if (CaretPos < Text.Length)
            {
                CaretPos++;
                if (textBox.SelectionLength > 0)
                {
                    if (CaretPos <= textBox.SelectionStart + textBox.SelectionLength)
                    {
                        textBox.SelectionLength--;
                        textBox.SelectionStart = CaretPos;
                    }
                    else
                    {
                        textBox.SelectionLength++;
                    }
                }
                else
                {
                    textBox.SelectionStart = CaretPos - 1;
                    textBox.SelectionLength = 1;
                }
            }
        }

        private void AppendText(char character)
        {
            if (textBox.SelectionLength > 0)
            {
                int selectionStartIndex = textBox.SelectionStart;
                Text = Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
                textBox.CaretIndex = CaretPos = selectionStartIndex;
                InsertCharToText(ShiftManager.ApplyCasing(character, true));
            }
            else
            {
                InsertCharToText(ShiftManager.ApplyCasing(character, true));
            }
        }

        private void InsertCharToText(char character)
        {
            textBox.Focus();
            Text = Text.Insert(CaretPos, character.ToString());
            CaretPos++;
            textBox.CaretIndex = CaretPos;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SideButtonsHeight = Height * _sideButtonsHeightRatio * 1.25;
            SideButtonsWidth = Width * _sideButtonsWidthRatio * 1.2;
            BottomButtonHeight = MainWindow.Height / 10;
        }

        private void TextSelectedViaClick(object sender, MouseButtonEventArgs e)
        {
            if (textBox.SelectionLength == 0)
            {
                CaretPos = textBox.CaretIndex;
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

        private void KeyUpOnKeyboard(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control) ||
                (e.Key == Key.X && Keyboard.Modifiers == ModifierKeys.Control))
            {
                CopiedText = textBox.SelectedText;
            }
            else
            {
                CaretPos = textBox.CaretIndex;
            }
        }
    }
}