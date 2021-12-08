using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard
{
    public class Selection
    {
        public int StartIndex { get; set; }

        public int CharacterCount { get; set; }

        internal void Reset()
        {
            StartIndex = 0;
            CharacterCount = 0;
        }
    }
}
