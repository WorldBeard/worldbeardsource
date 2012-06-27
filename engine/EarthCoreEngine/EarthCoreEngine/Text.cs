using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public class Text
    {
        Font _font;
        List<CharacterSprite> _bitmapText = new List<CharacterSprite>();
        string _text;
        Color _color;
        Vector _dimensions;
        int _maxWidth = -1;


        public List<CharacterSprite> CharacterSprites
        {
            get { return _bitmapText; }
        }


        public Text(string text, Font font) : this(text, font, -1) { }

        public Text(string text, Font font, int maxWidth)
        {
            _font = font;
            _text = text;
            _maxWidth = maxWidth;
            CreateText(0, 0, _maxWidth);
        }


        private void CreateText(double x, double y)
        {
            CreateText(x, y, _maxWidth);
        }


        private void CreateText(double x, double y, double maxWidth)
        {
            _bitmapText.Clear();
            double currentX = x;
            double currentY = y;
            double totalY;
            string[] words = _text.Split(' ');

            totalY = _font.MeasureFont(words[0]).Y;

            //START foreach
            foreach (string word in words)
            {
                Vector nextWordLength = _font.MeasureFont(word);

                if (maxWidth != -1 && (currentX + nextWordLength.X) > (x + maxWidth))
                {
                    currentX = x;
                    currentY -= nextWordLength.Y;
                    totalY += nextWordLength.Y;
                }

                string wordWithSpace = word + " "; // Add the space character that was removed

                foreach (char c in wordWithSpace)
                {
                    CharacterSprite sprite = _font.CreateSprite(c);
                    float xOffset = ((float) sprite.Data.XOffset) / 2;
                    float yOffset = ((float) sprite.Data.YOffset) / 2;
                    sprite.Sprite.SetPosition(currentX + xOffset, currentY - yOffset);
                    currentX += sprite.Data.XAdvance;
                    _bitmapText.Add(sprite);
                }
            }
            // END foreach

            _dimensions = _font.MeasureFont(_text);
            _dimensions.Y = totalY;
            SetColor(_color);

        }


        public void SetPosition(double x, double y)
        {
            CreateText(x, y);
        }


        public void SetColor()
        {
            foreach (CharacterSprite s in _bitmapText)
            {
                s.Sprite.Color = _color;
            }
        }


        public void SetColor(Color color)
        {
            _color = color;
            foreach (CharacterSprite s in _bitmapText)
            {
                s.Sprite.Color = _color;
            }
        }


        public void SetText(string text)
        {
            _text = text;
        }


        public double Width
        {
            get { return _dimensions.X; }
        }


        public double Height
        {
            get { return _dimensions.Y; }
        }

    }
}
