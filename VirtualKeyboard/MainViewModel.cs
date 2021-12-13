using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualKeyboard.Bases;

namespace VirtualKeyboard
{
    class MainViewModel : NotifyPropertyChanged_Base
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, ref value);
        }

        public MainViewModel()
        {
            Title = "TextBox";
        }
    }
}
