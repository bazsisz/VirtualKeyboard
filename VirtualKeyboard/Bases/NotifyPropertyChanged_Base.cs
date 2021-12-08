using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Bases
{
    public abstract class NotifyPropertyChanged_Base : INotifyPropertyChanged
    {
        protected void SetProperty<T>(ref T target, ref T source, [CallerMemberName] string propertyName = null)
        {
            if (target == null ||
                !target.Equals(source))
            {
                target = source;
                OnPropertyChanged(propertyName);
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
