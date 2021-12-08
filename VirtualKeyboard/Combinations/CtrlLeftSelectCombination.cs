using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Combinations
{
    class CtrlLeftSelectCombination : ICombination
    {
        public WpfInputKeyboard InputKeyboard { get; set; }

        public CtrlLeftSelectCombination(WpfInputKeyboard inputKeyboard)
        {
            InputKeyboard = inputKeyboard;
        }
        public void HandleComninationPressed()
        {
            //InputKeyboard.Selection = InputKeyboard.Selection.Insert(0, InputKeyboard.textBox.Text[InputKeyboard.CaretPos - 1].ToString());
            //InputKeyboard.textBox.Select(InputKeyboard.CaretPos - 1, InputKeyboard.Selection.Length);
            //InputKeyboard.CaretPos--;
        }
    }
}
