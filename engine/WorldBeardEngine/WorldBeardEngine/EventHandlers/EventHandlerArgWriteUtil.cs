using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine.EventHandlers
{
    public class EventHandlerArgWriteUtil
    {

        public Dictionary<string, Object> GetHandlerArgsFromDO(Dictionary<string, string> handlerDOArgs)
        {
            Dictionary<string, Object> handlerArgs = new Dictionary<string, object>();
            foreach (string key in handlerDOArgs.Keys)
            {
                handlerArgs.Add(key, GetArgValueAsObject(key, handlerDOArgs[key]));
            }
            return handlerArgs;
        }

        private Object GetArgValueAsObject(string name, string value)
        {
            switch(name.ToUpper())
            {
                case "HEIGHT":
                    return double.Parse(value);
                case "WIDTH":
                    return double.Parse(value);
                default:
                    throw new Exception("EventHandlerArgWriteUtil did not recognize argument name: " + name);
            }
        }

    }
}
