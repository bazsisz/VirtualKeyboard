using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard
{
    public abstract class Keyboard_Base
    {
        public KeySet Alphabet { get; set; }
        public KeySet SpeacialCharacters { get; set; }

        public Keyboard_Base()
        {
            Alphabet = new KeySet();
            SpeacialCharacters = new KeySet();
        }

        protected abstract void GetAlphabet();
        protected abstract void GetSpecialCharacters();
        protected void ConstructKeyList(KeySet keySet)
        {
            List<char> replica = new List<char>(keySet.Keys);
            List<char> charList = new List<char>();
            for (int i = 0; i < keySet.KeysInRows.Count; i++)
            {
                charList = replica.Take(keySet.KeysInRows[i]).ToList();
                replica.RemoveRange(0, keySet.KeysInRows[i]);
                keySet.CharactersListByRow.Add(charList);
            }
        }
    }
}
