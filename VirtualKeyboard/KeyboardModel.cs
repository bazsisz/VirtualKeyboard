using static VirtualKeyboard.VirtualKeyboardControl;

namespace VirtualKeyboard
{
    public class KeyboardModel
    {
        private Keyboard_Base Hungarian_Keyboard = new Hungarian_Keyboard();
        private Keyboard_Base English_US_Keyboard = new English_US_Keyboard();

        public Keyboard_Base CharSetKeyboard { get; private set; }

        public void SetCharSetKeyboard(CharSetLanguage charSetLanguage)
        {
            switch (charSetLanguage)
            {
                case CharSetLanguage.HU:
                    CharSetKeyboard = Hungarian_Keyboard;
                    break;
                case CharSetLanguage.EN:
                    CharSetKeyboard = English_US_Keyboard;
                    break;
            }
        }
    }
}
