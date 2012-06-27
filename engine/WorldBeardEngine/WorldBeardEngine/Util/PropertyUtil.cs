using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine
{
    public class PropertyUtil
    {

        public static Dictionary<string, string> LoadPropertiesFromFile(string filePathAndName)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            string[] lines = System.IO.File.ReadAllLines(filePathAndName);
            foreach (string line in lines)
            {
                if (line.Length == 0) { continue; } // Skip empty lines
                if (line.Substring(0, 2).Equals(@"//")) { continue; } // Ignore comments
                string[] keyAndValue = line.Split('=');
                if (keyAndValue.Length == 1) { throw new Exception("Invalid value in property file " + filePathAndName + ": " + line); }
                if (keyAndValue.Length > 2) // Some reflection names have additional "="s in the value
                {
                    for (int i = 2; i < keyAndValue.Length; i++)
                    {
                        keyAndValue[1] += "=" + keyAndValue[i];
                    }
                }
                properties.Add(keyAndValue[0].Trim(), keyAndValue[1].Trim());
            }
            return properties;
        }

    }
}
