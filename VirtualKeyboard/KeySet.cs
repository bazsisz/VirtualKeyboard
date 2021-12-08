using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard
{
    public class KeySet
    {
        public List<char> Keys { get; set; }
        public int NumberOfRows { get; set; }
        public List<int> KeysInRows { get; set; }

        public List<List<char>> CharactersListByRow { get; set; }

        public KeySet()
        {
            Keys = new List<char>();
            CharactersListByRow = new List<List<char>>();
            KeysInRows = new List<int>();
        }
    }
}
