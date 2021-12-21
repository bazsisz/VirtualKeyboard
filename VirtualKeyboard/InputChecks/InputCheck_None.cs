using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.InputChecks
{
    class InputCheck_None : IInputCheck
    {
        public bool CheckInputText(List<string> errors, string text)
        {
            return true;
        }
    }
}
