using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.EventHandlers
{
    public class EventHandlerArgReadUtil
    {
        public static int GetPropertyAsInt(string argName, Dictionary<string, Object> handlerArgs)
        {
            if (handlerArgs[argName] == null) { throw new Exception("Could not find event property: " + argName); }
            try { return (int) handlerArgs[argName]; }
            catch (Exception) { throw new Exception("Could cast argument " + argName + " as int with value " + handlerArgs[argName]); }
        }
    }
}
