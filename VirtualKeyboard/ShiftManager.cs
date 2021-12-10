using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VirtualKeyboard
{

    public class ShiftManager : INotifyPropertyChanged
    {
        private ControlShiftStates _currentShiftState;
        public ControlShiftStates CurrentShiftState
        {
            get => _currentShiftState;
            set
            {
                if (_currentShiftState != value)
                {
                    _currentShiftState = value;
                    OnPropertyChanged();

                }
            }
        }

        public char ApplyCasing(char character, bool resetFirstLetterUpperCaseToLowerCase)
        {
            switch (_currentShiftState)
            {
                case ControlShiftStates.NotActive:
                    return char.ToLower(character);

                case ControlShiftStates.ActiveUntilButtonPressed:
                    CurrentShiftState = resetFirstLetterUpperCaseToLowerCase ? ControlShiftStates.NotActive : _currentShiftState;
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
