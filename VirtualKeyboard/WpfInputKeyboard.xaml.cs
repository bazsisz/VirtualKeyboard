using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VirtualKeyboard.Combinations;

namespace VirtualKeyboard
{
    /// <summary>
    /// Interaction logic for WpfInputKeyboard.xaml
    /// </summary>
    public partial class WpfInputKeyboard : MetroWindow, INotifyPropertyChanged
    {
        private Keyboard_Base _charSetKeyboard;

        public Selection TextSelected { get; set; }

        public string CopiedText { get; set; }

        private bool _isControlActive;
        public bool IsControlActive
        {
            get => _isControlActive;
            set
            {
                if (_isControlActive != value)
                {
                    _isControlActive = value;
                    OnPropertyChanged();
                }
            }
        }

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
            ShiftManager.PropertyChanged += ShiftManager_PropertyChanged;


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
            if (e.PropertyName == nameof(ShiftManager.CurrentCasing))
            {
                OnPropertyChanged(nameof(ShiftManager));
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
            AddCharToText(' ');
        }

        private void Character_Click(object sender, RoutedEventArgs e)
        {
            if (IsControlActive)
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

        private void CaretPosSelected(object sender, MouseButtonEventArgs e)
        {
            if (TextSelected.CharacterCount != 0)
            {
                TextSelected.Reset();
            }
            Task.Run(() =>
            {
                Task.Delay(200).Wait();
                CaretPos = textBox.CaretIndex;
            });
        }

        private void DecresaseCaretPos_Click(object sender, RoutedEventArgs e)
        {
            if (IsControlActive)
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
            if (IsControlActive)
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
            if (CaretPos > 0)
            {
                Text = Text.Remove(CaretPos - 1, 1);
                CaretPos--;
            }
            textBox.Focus();
            textBox.CaretIndex = CaretPos;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            textBox.Focus();

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
            textBox.CaretIndex = CaretPos;
        }

        private void SpeacialChars_Click(object sender, RoutedEventArgs e)
        {
            KeyChars = (bool)specialCharsToogleButton.IsChecked ? _charSetKeyboard.SpeacialCharacters.CharactersListByRow : _charSetKeyboard.Alphabet.CharactersListByRow;
            specialCharsToogleButton.Content = (bool)specialCharsToogleButton.IsChecked ? "abc" : "!#1";
            ShiftManager.CurrentCasing = ShiftManager.Casing.LowerCase;
        }

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (TextSelected.CharacterCount != 0)
            {
                textBox.Focus();
                textBox.Select(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
            }
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
            TextSelected.CharacterCount = -Text.Length;
            textBox.SelectAll();
        }

        private void CtrlCCombinationPressed()
        {
            textBox.Focus();
            CopiedText = Text.Substring(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
            textBox.CaretIndex = CaretPos;
            textBox.Select(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
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
            if (textBox.IsSelectionActive)
            {
                CopiedText = Text.Substring(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
                Text = Text.Remove(TextSelected.StartIndex, Math.Abs(TextSelected.CharacterCount));
                textBox.CaretIndex = CaretPos = TextSelected.StartIndex;
                textBox.CaretIndex = CaretPos;
                TextSelected.Reset();
            }

        }

        private void AddCharToText(char character)
        {
            Text = Text.Insert(CaretPos, character.ToString());
            CaretPos++;
            textBox.Focus();
            textBox.CaretIndex = CaretPos;
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