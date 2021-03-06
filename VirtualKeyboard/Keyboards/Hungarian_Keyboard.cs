using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard
{
    class Hungarian_Keyboard : Keyboard_Base
    {
        public Hungarian_Keyboard()
        {
            Alphabet.NumberOfRows = 4;
            Alphabet.KeysInRows = new List<int>() { 13, 12, 12, 11 };
            SpeacialCharacters.NumberOfRows = 4;
            SpeacialCharacters.KeysInRows = new List<int>() { 13, 12, 12, 11 };
            GetAlphabet();
            GetSpecialCharacters();
            ConstructKeyList(Alphabet);
            ConstructKeyList(SpeacialCharacters);
        }
        protected override void GetAlphabet()
        {
            Alphabet.Keys = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' ,'ö', 'ü', 'ó',
                                        'q', 'w', 'e', 'r', 't', 'z', 'u', 'i', 'o', 'p', 'ő', 'ú',
                                        'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'é', 'á', 'ű',
                                        'í','y', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '-' };
        }
        protected override void GetSpecialCharacters()
        {
            SpeacialCharacters.Keys = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' ,'~', 'ˇ', '^',
                                                     '+', '×', '÷', '=', '/', '_', '{', '}', '<', '>' ,'"', '\'', '˘',
                                                     '!', '@', '#', '$', '|', '%', '&', '€', '*', '(', ')', '\\',
                                                     '¨', '˝', '´', '˙', '`', '°', '[', ']', ';', '-', ':', '?'};
        }
    }
}
