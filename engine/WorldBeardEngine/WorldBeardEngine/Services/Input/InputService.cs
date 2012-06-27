using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine.Input;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Input.Master;

namespace WorldBeardEngine.Services.Input
{
    public class InputService : Service
    {
        private EarthCoreEngine.Input.Input _input = SingletonFactory.GetInput();

        private MasterInput _masterInput = (MasterInput)ReflectionUtil.GetObjectFromTypeName(SingletonFactory.ServiceProperties["MasterInput"]);

        public InputService()
        {
            _validRegisterType = ComponentType.Input;
        }

        public override void Update(double elapsedTime)
        {
            if (!_masterInput.HandleMasterInput()) { return; }

            if (_registrees.Count == 0) { return; }

            foreach (InputComponent component in _registrees)
            {
                component.Update(elapsedTime);
            }
        }

        public MasterInputState GetMasterState()
        {
            return _masterInput.CurrentState;
        }
    }
}
