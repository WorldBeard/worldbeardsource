using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Events;
using WorldBeardEngine.Load;

namespace WorldBeardEngine.Load
{
    class LevelDefReader
    {

        List<GameObjectDO> _gameObjectDOs = new List<GameObjectDO>();

        string _fileName;

        int _currentDepth = 0;
        int _currentLine = 0;
        
        bool _finishedString = false;
        string _currentString = "";

        string _currentKey = "";
        string _currentValue = "";

        bool _startedMeta = false;
        string _currentMetaTag = "";

        bool _objectUnderConstruction = false;
        GameObjectDO _gameObjectDO;

        bool _componentUnderConstruction = false;
        ComponentDO _componentDO;

        bool _eventHandlerUnderConstruction = false;
        EventHandlerDO _eventHandlerDO;

        bool _eventUnderConstruction = false;
        GameEventDO _eventDO;

        internal List<GameObjectDO> GetGameObjectsFromLevelDef(string fileName)
        {
            _fileName = fileName;
            
            // Get the file as an array of lines
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // Loop through, parsing each line
            for (int currentLineNumber = 0; currentLineNumber < lines.Length; currentLineNumber++)
            {
                _currentLine = currentLineNumber;
                // Move char by char through the current line; look for special characters { } : ; | #
                foreach (char currentChar in lines[currentLineNumber].ToCharArray())
                {
                    // After switch, we want to handle the current string if we hit a special character
                    _finishedString = true;
                    switch (currentChar)
                    {
                        case '{':
                            HandleLeftCurl();
                            break;
                        case '}':
                            HandleRightCurl();
                            break;
                        case ':':
                            HandleColon();
                            break;
                        case ';':
                            HandleSemiColon();
                            break;
                        case '|':
                            HandleVerticalBar();
                            break;
                        case '#':
                            HandlePoundSign();
                            break;
                        case ' ':
                            // Ignore spaces
                            _finishedString = false;
                            break;
                        case '\t':
                            // Ignore tabs
                            _finishedString = false;
                            break;
                        default:
                            _currentString += currentChar;
                            _finishedString = false;
                            break;
                    }
                    if (_finishedString) { HandleFinishedString(); }
                }
            }
            return _gameObjectDOs;
        }

        private void HandleLeftCurl()
        {
            HandleValueSet();
            _currentDepth += 1;
            if (_currentDepth > 4) { throw new Exception("Incorrect left curl of depth > 4 " + GetErrorInfo()); }
        }

        private void HandleRightCurl()
        {
            HandleValueSet();
            _currentDepth -= 1;
            if (_currentDepth < 0) { throw new Exception("Incorrect right curl of depth < 0 " +  GetErrorInfo()); }

            if (_eventUnderConstruction && _currentDepth == 3) { HandleFinishedEvent(); }
            else if (_eventHandlerUnderConstruction && _currentDepth == 2) { HandleFinishedEventHandler(); }
            else if (_componentUnderConstruction && _currentDepth == 1) { HandleFinishedComponent(); }
            else if (_objectUnderConstruction && _currentDepth == 0) { HandleFinishedObject(); }
        }

        private void HandleColon()
        {
            _currentKey = _currentString;
        }

        private void HandleSemiColon()
        {
            if (_currentKey.Equals("")) { throw new Exception("Invalid ; " + GetErrorInfo()); }
            HandleValueSet();
        }

        private void HandleVerticalBar()
        {
            if (_currentKey.Equals("")) { throw new Exception("Invalid | " + GetErrorInfo()); }
            HandleValueSet();
        }

        private void HandlePoundSign()
        {
            if (_startedMeta)
            {
                _startedMeta = false;
                _currentMetaTag = _currentString;
            }
            else
            {
                _startedMeta = true;
            }
        }

        private void HandleFinishedString()
        {
            switch (_currentMetaTag.ToUpper())
            {
                case "META-INF":
                    // TODO
                    break;
                case "STATIC":
                    HandleObjectFinishedString();
                    break;
                case "SAVE":
                    HandleObjectFinishedString();
                    break;
                case "BUNDLES":
                    // TODO
                    break;
                case "":
                    // i.e. at start of file
                    break;
                default:
                    throw new Exception("Invalid meta-tag: " + _currentMetaTag + GetErrorInfo());
            }
            _currentString = "";
        }

        private void HandleObjectFinishedString()
        {
            switch (_currentString.ToUpper())
            {
                case "OBJ":
                    StartObject();
                    break;
                case "COMPONENT":
                    StartComponent();
                    break;
                case "EVENTHANDLER":
                    StartEventHandler();
                    break;
                case "EVENT":
                    StartEvent();
                    break;
                case "BUNDLE":
                    // TODO
                    break;
                default:
                    break;
            }
        }

        private void StartObject()
        {
            if (_gameObjectDO.ComponentDOs == null) { _gameObjectDO.ComponentDOs = new List<ComponentDO>(); }
            if (_currentDepth != 0) { throw new Exception("Attempted to initialize an obj when depth not 0 " + GetErrorInfo()); }

            if (_currentMetaTag.ToUpper().Equals("STATIC")) { _gameObjectDO.IsSaved = false; }
            else if (_currentMetaTag.ToUpper().Equals("SAVE")) { _gameObjectDO.IsSaved = true; }
            else { throw new Exception("Attemped to initialize an obj when meta-tag was " + _currentMetaTag + " " + GetErrorInfo()); }

            _objectUnderConstruction = true;
        }

        private void HandleFinishedObject()
        {
            if (_gameObjectDO.Name.Equals("")) { throw new Exception("Must provide Name for GameObject " + GetErrorInfo()); }
            if (_gameObjectDO.ID == 0) { throw new Exception("Must provide non-zero ObjectID for GameObject " + GetErrorInfo()); }
            _gameObjectDOs.Add(_gameObjectDO);
            _gameObjectDO = new GameObjectDO();
            _objectUnderConstruction = false;
        }

        private void StartComponent()
        {
            if (_componentDO.Properties == null) { _componentDO.Properties = new Dictionary<string, string>(); }
            if (_componentDO.GameEventHandlerDOs == null) { _componentDO.GameEventHandlerDOs = new List<EventHandlerDO>(); }
            if (_currentDepth != 1) { throw new Exception("Attempted to initialize a component when depth not 1 " + GetErrorInfo()); }
            _componentUnderConstruction = true;
        }

        private void HandleFinishedComponent()
        {
            if (_componentDO.Subclass == null) { _componentDO.Subclass = "Basic"; }
            if (_componentDO.Type == ComponentType.NULL) { throw new Exception("Must provide Type for Component " + GetErrorInfo()); }
            if (_gameObjectDO.ComponentDOs == null) { _gameObjectDO.ComponentDOs = new List<ComponentDO>(); }
            _gameObjectDO.ComponentDOs.Add(_componentDO);
            _componentDO = new ComponentDO();
            _componentUnderConstruction = false;
        }

        private void StartEventHandler()
        {
            if (_eventHandlerDO.HandlerArgs == null) { _eventHandlerDO.HandlerArgs = new Dictionary<string, string>(); }
            if (_eventHandlerDO.ThrownEvents == null) { _eventHandlerDO.ThrownEvents = new List<GameEventDO>(); }
            if (_currentDepth != 2) { throw new Exception("Attempted to initialize an eventhandler when depth not 2 " + GetErrorInfo()); }
            _eventHandlerUnderConstruction = true;
        }

        private void HandleFinishedEventHandler()
        {
            if (_eventHandlerDO.Name.Equals("")) { throw new Exception("Must provide Name for EventHandler " + GetErrorInfo()); }
            if (_eventHandlerDO.TypeToHandle == EventType.NULL) { throw new Exception("Must provide TypeToHandle for EventHandler " + GetErrorInfo()); }
            if (_componentDO.GameEventHandlerDOs == null) { _componentDO.GameEventHandlerDOs = new List<EventHandlerDO>(); }
            _componentDO.GameEventHandlerDOs.Add(_eventHandlerDO);
            _eventHandlerDO = new EventHandlerDO();
            _eventHandlerUnderConstruction = false;
        }

        private void StartEvent()
        {
            if (_currentDepth != 3) { throw new Exception("Attempted to initialize an event when depth not 3 " + GetErrorInfo()); }
            _eventUnderConstruction = true;
        }

        private void HandleFinishedEvent()
        {
            if (_eventDO.Name.Equals("")) { throw new Exception("Must provide Name for GameEvent " + GetErrorInfo()); }
            if (_eventDO.Type == EventType.NULL) { throw new Exception("Must provide Type for GameEvent " + GetErrorInfo()); }
            if (_eventHandlerDO.ThrownEvents == null) { _eventHandlerDO.ThrownEvents = new List<GameEventDO>(); }
            _eventHandlerDO.ThrownEvents.Add(_eventDO);
            _eventDO = new GameEventDO();
            _eventUnderConstruction = false;
        }

        private void HandleValueSet()
        {
            _currentValue = _currentString;

            if (_currentKey.Equals("") && _currentValue.Equals("")) { return; }

            if (_objectUnderConstruction && !_componentUnderConstruction)
            {
                if (_currentKey.ToUpper().Equals("OBJ")) { _gameObjectDO.ID = int.Parse(_currentValue); }
                else if (_currentKey.ToUpper().Equals("NAME")) { _gameObjectDO.Name = _currentValue; }
            }
            if (_componentUnderConstruction && !_eventHandlerUnderConstruction)
            {
                if (_currentKey.ToUpper().Equals("COMPONENT")) { _componentDO.Type = ComponentTypeUtil.GetTypeFromString(_currentValue); }
                else if (_currentKey.ToUpper().Equals("SUBCLASS")) { _componentDO.Subclass = _currentValue; }
                else
                {
                    if (_componentDO.Properties == null) { _componentDO.Properties = new Dictionary<string, string>(); }
                    _componentDO.Properties.Add(_currentKey, _currentValue);
                }
            }
            if (_eventHandlerUnderConstruction && !_eventUnderConstruction)
            {
                if (_currentKey.ToUpper().Equals("EVENTHANDLER")) { _eventHandlerDO.Name = _currentValue; }
                else if (_currentKey.ToUpper().Equals("TYPETOHANDLE")) { _eventHandlerDO.TypeToHandle = EventTypeUtil.GetTypeFromString(_currentValue); }
                else
                {
                    if (_eventHandlerDO.HandlerArgs == null) { _eventHandlerDO.HandlerArgs = new Dictionary<string, string>(); }
                    _eventHandlerDO.HandlerArgs.Add(_currentKey, _currentValue);
                }
            }
            if (_eventUnderConstruction)
            {
                if (_currentKey.ToUpper().Equals("EVENT")) { _eventDO.Name = _currentValue; }
                else if (_currentKey.ToUpper().Equals("TYPE")) { _eventDO.Type = EventTypeUtil.GetTypeFromString(_currentValue); }
                else
                {
                    if (_eventDO.EventArgs == null) { _eventDO.EventArgs = new Dictionary<string, Object>(); }
                    _eventDO.EventArgs.Add(_currentKey, _currentValue);
                }
            }
            _currentKey = "";
            _currentValue = "";
        }

        private string GetErrorInfo()
        {
            return "(Line " + _currentLine + " of file " + _fileName + ")";
        }

    }
}
