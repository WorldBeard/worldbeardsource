using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public class CharacterSprite
    {
        public Sprite Sprite { get; set; }
        public CharacterData Data { get; set; }

        public CharacterSprite(Sprite sprite, CharacterData data)
        {
            Sprite = sprite;
            Data = data;
        }
    }
}
