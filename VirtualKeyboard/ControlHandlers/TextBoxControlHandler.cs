using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VirtualKeyboard.ControlHandlers
{
    public class TextBoxControlHandler : IControlHandler
    {
        private TextBox _textBox;

        public TextBoxControlHandler(TextBox textBox)
        {
            _textBox = textBox;
        }

        public string TextValue { get => _textBox.Text; set => _textBox.Text = value; }
    }
}
