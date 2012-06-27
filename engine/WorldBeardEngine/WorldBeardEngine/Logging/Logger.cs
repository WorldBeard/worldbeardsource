using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Logging
{
    public class Logger : IDisposable
    {

        private string _fileName;
        private LoggerLevel _logThreshholdLevel;
        private List<ComponentType> componentTypesToLog = new List<ComponentType>();

        private StreamWriter _streamWriter;

        public Logger()
        {
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int year = DateTime.Now.Year;

            Dictionary<string, string> loggerProps = PropertyUtil.LoadPropertiesFromFile(ConfigSettings.PROP_DIR_ROOT + @"log.prop");

            _fileName = loggerProps["logger.directory"] + loggerProps["logger.name"] + month + day + year + ".log";
            _logThreshholdLevel = (LoggerLevel)Convert.ToInt32(loggerProps["logger.level"]);

            for (int i = 0; i < loggerProps.Count; i++)
            {
                if (loggerProps.Values.ToArray()[i] == "logger.component")
                {
                    componentTypesToLog.Add(ComponentTypeUtil.GetTypeFromString(loggerProps.Keys.ToArray()[i]));
                }
            }

            _streamWriter = new StreamWriter(_fileName, true);
        }

        public void Log(String logMessage)
        {
            Log(logMessage, LoggerLevel.NULL);
        }

        public void Log(String logMessage, LoggerLevel messageLevel)
        {
            Log(logMessage, messageLevel, ComponentType.NULL);
        }

        public void Log(String logMessage, params ComponentType[] componentTypes)
        {
            Log(logMessage, LoggerLevel.NULL, componentTypes);
        }

        public void Log(String logMessage, LoggerLevel messageLevel, params ComponentType[] messageComponentTypes)
        {
            if (messageLevel < _logThreshholdLevel) { return; }

            string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string level = messageLevel == LoggerLevel.NULL ? "   " : messageLevel.ToString();
            string type = messageComponentTypes[0] == ComponentType.NULL ? "   " : ComponentTypeUtil.GetCodeFromType(messageComponentTypes[0]);

            // If no component types are specified, log everything.
            if (componentTypesToLog == null || componentTypesToLog.Count == 0)
            {
                _streamWriter.WriteLine(date + " | " + level + " | " + type + " | " + logMessage);
                _streamWriter.Flush();
                return;
            }
            else // Else, only log messages matching one of the specified component types.
            {
                foreach (ComponentType componentType in messageComponentTypes)
                {
                    if (componentTypesToLog.Contains(componentType))
                    {
                        _streamWriter.WriteLine(date + " | " + level + " | " + type + " | " + logMessage);
                        _streamWriter.Flush();
                        return;
                    }
                }
            }
        }

        public void Dispose()
        {
            if (_streamWriter != null) { _streamWriter.Close(); _streamWriter = null; }
        }
    }
}
