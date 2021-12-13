using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VirtualKeyboard.Bases;

namespace VirtualKeyboard
{

    public class ShiftManager : NotifyPropertyChanged_Base
    {
        private ControlShiftStates _currentShiftState;
        public ControlShiftStates CurrentShiftState
        {
            get => _currentShiftState;
            set => SetProperty(ref _currentShiftState, ref value);
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
    }
}
