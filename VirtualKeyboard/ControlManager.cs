using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VirtualKeyboard.Combinations;

namespace VirtualKeyboard
{
    public class ControlManager : INotifyPropertyChanged
    {
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

        public string Selection { get; set; }

        public TextBox TextBox { get; set; }



        public ControlManager()
        {
            Selection = "";
        }

        public void CombinationPressed(ICombination combination)
        {
            combination.HandleComninationPressed();
        }

        //public void CombinationPressed(char character)
        //{
        //    switch (character) 
        //    {
        //        case 'a':
        //            CombinationPressed(new CtrlACombination(this));
        //            break;
        //        case '<':
        //            CombinationPressed(new CtrlLeftSelectCombination(this));
        //            break;
        //    }
        //}

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
