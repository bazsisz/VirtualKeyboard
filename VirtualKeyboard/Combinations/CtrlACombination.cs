using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Combinations
{
    class CtrlACombination : ICombination
    {
        public WpfInputKeyboard InputKeyboard { get; set; }

        public CtrlACombination(WpfInputKeyboard inputKeyboard)
        {
            InputKeyboard = inputKeyboard;
        }

        public void HandleComninationPressed()
        {
            //InputKeyboard.textBox.Focus();
            //InputKeyboard.CaretPos = 0;
            //InputKeyboard.textBox.SelectAll();
            //InputKeyboard.Selection = InputKeyboard.textBox.Text;
            //InputKeyboard.IsControlActive = false;
        }
    }
}
