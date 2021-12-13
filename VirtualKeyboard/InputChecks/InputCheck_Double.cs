using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.InputChecks
{
    public class InputCheck_Double : InputCheck_Base
    {
        protected override bool CheckInputText(List<string> errors)
        {
            {
                if (!double.TryParse(Text, out _))
                {
                    errors?.Add("Input needs to be a number!");
                    return false;
                }
                return true;
            }
        }
    }
}
