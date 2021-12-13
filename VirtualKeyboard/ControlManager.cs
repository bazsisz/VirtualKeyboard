using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VirtualKeyboard.Bases;

namespace VirtualKeyboard
{
    public class ControlManager : NotifyPropertyChanged_Base
    {
        private ControlShiftStates _currentCtrlState;
        public ControlShiftStates CurrentCtrlState
        {
            get => _currentCtrlState;
            set => SetProperty(ref _currentCtrlState, ref value);
        }

        public bool IsControlActive
        {
            get => CurrentCtrlState == ControlShiftStates.ActiveUntilButtonPressed || CurrentCtrlState == ControlShiftStates.AlwaysActive;
        }

        public bool IsCtrlActiveButtonPressed()
        {
            bool isActive = IsControlActive;
            CurrentCtrlState = CurrentCtrlState == ControlShiftStates.ActiveUntilButtonPressed ? ControlShiftStates.NotActive : CurrentCtrlState;
            return isActive;
        }
    }
}
