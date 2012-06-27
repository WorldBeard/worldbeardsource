using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorldBeardEngine.Services.Input.Controls;

namespace WorldBeardEngine.Services.Input.Master.MasterInputs
{
    public class BasicMasterInput : MasterInput
    {
        public BasicMasterInput() : base() { } // For Reflection

        internal override bool HandleMasterInput()
        {
            if (ControlMapping.IsKeyPressed(InputCommand.Pause))
            {
                _currentState = _currentState == MasterInputState.NULL ? MasterInputState.Paused : MasterInputState.NULL;
            }

            if(ControlMapping.IsKeyPressed(InputCommand.Exit))
            {
                if (MessageBox.Show("Are you sure you wish to exit?", "EXIT GAME", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }

            return (_currentState == MasterInputState.NULL);
        }

    }
}
