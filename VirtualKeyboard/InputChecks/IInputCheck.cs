using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.InputChecks
{
    public interface IInputCheck
    {
        bool CheckInputText(List<string> errors, string text);
    }
}
