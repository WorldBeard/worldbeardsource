using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorldBeardEngine.Services.Input.Controls
{
    public class ControlMapping
    {
        private static EarthCoreEngine.Input.Input _input = SingletonFactory.GetInput();

        private static Dictionary<InputCommand, List<Keys>> _keyBindings;

        private Dictionary<InputCommand, List<Keys>> LoadKeyBindings()
        {
            Dictionary<InputCommand, List<Keys>> keyBindings = new Dictionary<InputCommand, List<Keys>>();
            Dictionary<string, string> keyBindingProps = PropertyUtil.LoadPropertiesFromFile(@"Assets/Properties/keybindings.prop");
            InputCommand tempCommand;
            Keys tempKey;
            foreach (string key in keyBindingProps.Keys)
            {
                tempKey = EarthCoreEngine.Input.KeyUtil.GetKeyFromString(key);
                tempCommand = InputCommandUtil.GetCommandFromString(keyBindingProps[key]);
                if (keyBindings.ContainsKey(tempCommand))
                {
                    keyBindings[tempCommand].Add(tempKey);
                }
                else
                {
                    keyBindings.Add(tempCommand, new List<Keys>());
                    keyBindings[tempCommand].Add(tempKey);
                }
            }
            return keyBindings;
        }

        public static bool IsKeyPressed(InputCommand inputCommand)
        {
            if (_keyBindings == null) { _keyBindings = new ControlMapping().LoadKeyBindings(); } // This weird 'new' statement prevents the one-use method LoadKeyBindings from being held in memory.

            if(!_keyBindings.ContainsKey(inputCommand)) { throw new Exception("No key binding found for input command: " + inputCommand.ToString()); }
            foreach(Keys key in _keyBindings[inputCommand])
            {
                if (_input.Keyboard.IsKeyPressed(key)) { return true; }
            }
            return false;
        }

        public static bool IsKeyHeld(InputCommand inputCommand)
        {
            if (_keyBindings == null) { _keyBindings = new ControlMapping().LoadKeyBindings(); } // This weird 'new' statement prevents the one-use method LoadKeyBindings from being held in memory.

            if (!_keyBindings.ContainsKey(inputCommand)) { throw new Exception("No key binding found for input command: " + inputCommand.ToString()); }
            foreach (Keys key in _keyBindings[inputCommand])
            {
                if (_input.Keyboard.IsKeyHeld(key)) { return true; }
            }
            return false;
        }

    }
}
