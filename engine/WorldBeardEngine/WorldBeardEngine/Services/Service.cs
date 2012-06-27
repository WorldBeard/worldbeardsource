using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Services
{
    public abstract class Service
    {
        protected ComponentType _validRegisterType;

        protected List<Component> _registrees = new List<Component>();

        public void Register(Component component)
        {
            if (component.Type == _validRegisterType)
            {
                _registrees.Add(component);
            }
            else
            {
                throw new Exception("Component of type " + component.Type.ToString() + ", object id " + component.Parent.ID + " cannot be registered to service of type " + _validRegisterType.ToString());
            }
        }

        public void Unregister(Component component)
        {
            _registrees.Remove(component);
        }

        public abstract void Update(double elapsedTime);

    }
}
