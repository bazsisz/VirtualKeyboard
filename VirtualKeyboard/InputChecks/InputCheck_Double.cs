using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.InputChecks
{
    public class InputCheck_Double : IInputCheck
    {
        bool IInputCheck.CheckInputText(List<string> errors, string text)
        {
            if (!double.TryParse(text, out _))
            {
                errors?.Add("Input needs to be a number!");
                return false;
            }
            return true;
        }
    }
}
