using System.Collections.Generic;

namespace VirtualKeyboard.InputChecks
{
    public class InputCheck_Double : IInputCheck
    {
        public bool CheckInputText(List<string> errors, string text)
        {
            if (string.IsNullOrEmpty(text)) 
            {
                return true;
            }

            if (!double.TryParse(text, out _))
            {
                errors?.Add("Input needs to be a number!");
                return false;
            }
            return true;
        }
    }
}
