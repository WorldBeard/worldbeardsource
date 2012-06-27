using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public class Font
    {

        Texture _texture;
        Dictionary<char, CharacterData> _characterData;


        public Font(Texture texture, Dictionary<char, CharacterData> characterData)
        {
            _texture = texture;
            _characterData = characterData;
        }


        public CharacterSprite CreateSprite(char c)
        {
            CharacterData charData = _characterData[c];
            Sprite sprite = new Sprite();
            sprite.Texture = _texture;

            // Set up UVs
            Point topLeft = new Point((float)charData.X / (float)_texture.Width,
                (float)charData.Y / (float)_texture.Height);
            Point bottomRight = new Point((float)topLeft.X + (float) charData.Width / (float) _texture.Width,
                (float)topLeft.Y + (float) charData.Height / (float) _texture.Height);

            sprite.SetUVs(topLeft, bottomRight);
            sprite.Width = charData.Width;
            sprite.Height = charData.Height;
            sprite.Color = new Color(1, 1, 1, 1);

            return new CharacterSprite(sprite, charData);
        }


        public Vector MeasureFont(string text)
        {
            return MeasureFont(text, -1);
        }


        public Vector MeasureFont(string text, double maxWidth)
        {
            Vector dimensions = new Vector();

            foreach (char c in text)
            {
                CharacterData data = _characterData[c];
                dimensions.X += data.XAdvance;
                dimensions.Y = Math.Max(dimensions.Y, data.Height + data.YOffset);
            }

            return dimensions;
        }

    }
}
