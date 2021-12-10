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

        public Selection TextSelected { get; set; }

        public string CopiedText { get; set; }

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


        public int CaretPos { get; set; }

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
            TextSelected = new Selection();
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
            if (TextSelected.CharacterCount != 0)
            {
                Text = Text.Remove(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
                textBox.CaretIndex = CaretPos = TextSelected.StartIndex;
                AddCharToText(ShiftManager.ApplyCasing(' ', true));
                TextSelected.Reset();
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
                if (TextSelected.CharacterCount != 0)
                {
                    Text = Text.Remove(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
                    textBox.CaretIndex = CaretPos = TextSelected.StartIndex;
                    AddCharToText(ShiftManager.ApplyCasing(Convert.ToChar(((Button)sender).Content), true));
                    TextSelected.Reset();
                }
                else
                {
                    AddCharToText(ShiftManager.ApplyCasing(Convert.ToChar(((Button)sender).Content), true));
                }
            }
        }


        private void DecresaseCaretPos_Click(object sender, RoutedEventArgs e)
        {
            if (ControlManager.IsCtrlActiveButtonPressed())
            {
                if (CaretPos > 0)
                {
                    CaretPos--;
                    TextSelected.CharacterCount--;
                    TextSelected.StartIndex = TextSelected.CharacterCount < 0 ? CaretPos : CaretPos - TextSelected.CharacterCount;
                }
                textBox.Focus();
                textBox.Select(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
            }
            else
            {
                if (TextSelected.CharacterCount != 0)
                {
                    TextSelected.Reset();
                }
                if (CaretPos > 0)
                {
                    CaretPos--;
                }
                textBox.Focus();
                textBox.CaretIndex = CaretPos;
            }
        }

        private void IncreaseCaretPos_Click(object sender, RoutedEventArgs e)
        {
            if (ControlManager.IsCtrlActiveButtonPressed())
            {
                if (CaretPos < Text.Length)
                {
                    CaretPos++;
                    TextSelected.CharacterCount++;
                    TextSelected.StartIndex = TextSelected.CharacterCount < 0 ? CaretPos : CaretPos - TextSelected.CharacterCount;
                }
                textBox.Focus();
                textBox.Select(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
            }
            else
            {
                if (TextSelected.CharacterCount != 0)
                {
                    TextSelected.Reset();
                }
                if (CaretPos < Text.Length)
                {
                    CaretPos++;
                }
                textBox.Focus();
                textBox.CaretIndex = CaretPos;
            }
        }

        private void BackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (TextSelected.CharacterCount != 0)
            {
                Text = Text.Remove(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
                TextSelected.Reset();
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
            if (TextSelected.CharacterCount != 0)
            {
                Text = Text.Remove(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
                TextSelected.Reset();
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
            if (TextSelected.CharacterCount != 0)
            {
                Text = Text.Remove(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
                textBox.CaretIndex = CaretPos = TextSelected.StartIndex;
                AddCharToText(ShiftManager.ApplyCasing('\n', true));
                TextSelected.Reset();
            }
            else
            {
                AddCharToText(ShiftManager.ApplyCasing('\n', true));
            }
        }

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (TextSelected.CharacterCount != 0)
            {
                textBox.Focus();
                textBox.Select(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
            }
            IsSpeacialCharsChecked = false;
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
            CaretPos = TextSelected.StartIndex = 0;
            TextSelected.CharacterCount = Text.Length;
            textBox.SelectAll();
        }

        private void CtrlCCombinationPressed()
        {
            textBox.Focus();
            CopiedText = Text.Substring(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
            textBox.CaretIndex = CaretPos;
            if (TextSelected.CharacterCount != 0)
            {
                textBox.Select(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
            }
        }

        private void CtrlVCombinationPressed()
        {

            if (TextSelected.CharacterCount != 0)
            {
                Text = Text.Remove(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
                textBox.CaretIndex = CaretPos = TextSelected.StartIndex;
                TextSelected.Reset();
            }

            textBox.Focus();
            Text = Text.Insert(CaretPos, CopiedText);
            textBox.CaretIndex = CaretPos = CaretPos + CopiedText.Length;


        }

        private void CtrlXCombinationPressed()
        {
            textBox.Focus();
            if (TextSelected.CharacterCount != 0)
            {
                CopiedText = Text.Substring(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
                Text = Text.Remove(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
                textBox.CaretIndex = CaretPos = TextSelected.StartIndex;
                TextSelected.Reset();
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

        //protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        //{

        //    if (sizeInfo.WidthChanged)
        //    {
        //        CharFontsize = Height / _charFontsize;
        //    }
        //    else
        //    {
        //        //this.Width = sizeInfo.NewSize.Width * _aspectRation;
        //    }
        //}

        private void TextSelectedViaClick(object sender, MouseButtonEventArgs e)
        {
            if (textBox.SelectionLength > 0)
            {
                TextSelected.StartIndex = textBox.SelectionStart;
                TextSelected.CharacterCount = textBox.SelectionLength;
                textBox.CaretIndex = CaretPos = textBox.SelectionStart;
                textBox.Select(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
            }
            else
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
    }
}