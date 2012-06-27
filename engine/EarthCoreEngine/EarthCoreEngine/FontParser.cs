using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EarthCoreEngine
{
    public class FontParser
    {
        static int HeaderSize = 4;

        // Gets the value after an equal sign and converts it
        // to an integer
        private static int GetValue(string s)
        {
            string value = s.Substring(s.IndexOf('=') + 1);
            return int.Parse(value);
        }


        public static Dictionary<char, CharacterData> Parse(string filePath)
        {
            // create a dictionary that will be filled and returned by Parse
            Dictionary<char, CharacterData> charDictionary = new Dictionary<char, CharacterData>();

            // fill string array with lines from the specified file
            string[] lines = File.ReadAllLines(filePath);

            for (int i = HeaderSize /*ignore header lines*/; i < lines.Length; i++)
            {
                string firstLine = lines[i];

                // splits each line into an array using " " as the delimiter
                string[] typesAndValues = firstLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
               
                // All the data comes in a certain order
                // used to make the parser shorter
                CharacterData charData = new CharacterData
                {
                    Id = GetValue(typesAndValues[1]),
                    X = GetValue(typesAndValues[2]),
                    Y = GetValue(typesAndValues[3]),
                    Width = GetValue(typesAndValues[4]),
                    Height = GetValue(typesAndValues[5]),
                    XOffset = GetValue(typesAndValues[6]),
                    YOffset = GetValue(typesAndValues[7]),
                    XAdvance = GetValue(typesAndValues[8]),
                };
                charDictionary.Add((char)charData.Id, charData);
            }

            return charDictionary;
        }
    }
}
