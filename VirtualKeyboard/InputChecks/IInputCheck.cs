using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.InputChecks
{
    public interface IInputCheck
    {
        /// <summary>
        /// Checks for valid input
        /// </summary>
        /// <param name="errors">List of the errors found</param>
        /// <param name="text">Text to validate</param>
        /// <returns>Returns true if text validated without errors, otherwise false</returns>
        bool CheckInputText(List<string> errors, string text);
    }
}
