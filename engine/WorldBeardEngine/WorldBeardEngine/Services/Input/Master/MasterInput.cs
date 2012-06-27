using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine.Input;

namespace WorldBeardEngine.Services.Input.Master
{
    public abstract class MasterInput
    {
        protected EarthCoreEngine.Input.Input _input = SingletonFactory.GetInput();
        protected MasterInputState _currentState = MasterInputState.NULL;
        public MasterInputState CurrentState { get { return _currentState; } }

        /// <summary>
        /// Handles input prior to passing it to InputComponents. Returns 'false' control should not be passed through to components.
        /// </summary>
        /// <param name="input">EarthCoreEngine.Input.Input whose current state is evaluated.</param>
        /// <returns>Returns false if input should not fall through to GameObjects.</returns>
        internal abstract bool HandleMasterInput();

    }
}
