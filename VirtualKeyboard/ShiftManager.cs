using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard
{

    public class ShiftManager : INotifyPropertyChanged
    {
        public enum Casing
        {
            LowerCase,
            FirstLetterUpperCase,
            UpperCase
        }

        private Casing _currentCasing;
        public Casing CurrentCasing
        {
            get => _currentCasing;
            set
            {
                if (_currentCasing != value)
                {
                    _currentCasing = value;
                    OnPropertyChanged();

                }
            }
        }

        public char ApplyCasing(char character, bool resetFirstLetterUpperCaseToLowerCase)
        {
            switch (_currentCasing)
            {
                case Casing.LowerCase:
                    return char.ToLower(character);

                case Casing.FirstLetterUpperCase:
                    CurrentCasing = resetFirstLetterUpperCaseToLowerCase ? Casing.LowerCase : _currentCasing;
                    return char.ToUpper(character);

                default:
                    return char.ToUpper(character);
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
