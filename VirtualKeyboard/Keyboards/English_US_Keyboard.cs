using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard
{
    class English_US_Keyboard : Keyboard_Base
    {
        public English_US_Keyboard()
        {
            Alphabet.NumberOfRows = 4;
            Alphabet.KeysInRows = new List<int>() { 10, 10, 9, 9 };
            SpeacialCharacters.NumberOfRows = 4;
            SpeacialCharacters.KeysInRows = new List<int>() { 10, 10, 9, 9 };
            GetAlphabet();
            GetSpecialCharacters();
            ConstructKeyList(Alphabet);
            ConstructKeyList(SpeacialCharacters);
        }

        protected override void GetAlphabet()
        {
            Alphabet.Keys = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                        'q', 'w', 'e', 'r', 't', 'z', 'u', 'i', 'o', 'p',
                                        'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l',
                                        'y', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.' };
        }

        protected override void GetSpecialCharacters()
        {
            throw new NotImplementedException();
        }
    }
}
