using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine
{
    public class StringUtil
    {

        public static string LogDivider { get { return "____________________________________________________________"; } }

        public static string PadLeft(object obj, int totalLength, char paddingChar)
        {
            return obj.ToString().PadLeft(totalLength, paddingChar);
        }

        public static string PadRight(object obj, int totalLength, char paddingChar)
        {
            return obj.ToString().PadRight(totalLength, paddingChar);
        }

    }
}
