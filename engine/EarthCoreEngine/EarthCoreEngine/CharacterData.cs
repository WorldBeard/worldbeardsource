using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public class CharacterData
    {
        public int Id { get; set; }  // ASCII # for character
        public int X { get; set; } // i.e. U value within texture
        public int Y { get; set; } // i.e. V value within texture
        public int Width { get; set; }
        public int Height { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int XAdvance { get; set; }
    }
}
