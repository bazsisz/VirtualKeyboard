using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VirtualKeyboard
{
    public class ControlManager : INotifyPropertyChanged
    {
        private ControlShiftStates _currentCtrlState;
        public ControlShiftStates CurrentCtrlState
        {
            get { return _currentCtrlState; }
            set
            {
                if (_currentCtrlState != value)
                {
                    _currentCtrlState = value;
                    OnPropertyChanged();

                }
            }
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
