using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.ControlHandlers
{
    public class NumericUpDownControlHandler : IControlHandler
    {
        private NumericUpDown _numericUpDown;
        private double? _originalvalue;

        public NumericUpDownControlHandler(NumericUpDown numericUpDown)
        {
            _numericUpDown = numericUpDown;
            _originalvalue = numericUpDown.Value;
        }

        public string TextValue
        {
            get => _numericUpDown.Value.ToString();
            set
            {
                if (double.TryParse(value, out double number))
                {
                    _numericUpDown.Value = number;
                }
                else
                {
                    _numericUpDown.Value = _originalvalue;
                }
            }
        }
    }
}
