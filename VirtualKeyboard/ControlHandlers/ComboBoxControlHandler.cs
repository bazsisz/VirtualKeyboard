using System.Windows.Controls;

namespace VirtualKeyboard.ControlHandlers
{
    public class ComboBoxControlHandler : IControlHandler
    {
        private ComboBox _comboBox;

        public ComboBoxControlHandler(ComboBox comboBox)
        {
            _comboBox = comboBox;
        }

        public string TextValue { get => _comboBox.Text; set => _comboBox.Text = value; }
    }
}
