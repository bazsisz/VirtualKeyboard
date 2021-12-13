using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.InputChecks
{
    class InputCheck_None : InputCheck_Base
    {
        protected override bool CheckInputText(List<string> errors)
        {
            return true;
        }
    }
}
