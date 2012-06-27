using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Input;
using WorldBeardEngine.Services.Input.Master;

namespace WorldBeardEngine.Services
{
    public class ServiceRegistrar
    {
        private Dictionary<int, GameObject> _gameObjects = new Dictionary<int, GameObject>();
        private Dictionary<String, Component> _ledger = new Dictionary<String, Component>();

        private Service _animationService = SingletonFactory.GetAnimationService();
        private Service _collisionService = SingletonFactory.GetCollisionService();
        private Service _guiService = SingletonFactory.GetGuiService();
        private Service _inputService = SingletonFactory.GetInputService();
        private Service _movementService = SingletonFactory.GetMovementService();
        private Service _physicsService = SingletonFactory.GetPhysicsService();
        private Service _renderingService = SingletonFactory.GetRenderingService();
 
        public Component GetComponent(String componentFullName)
        {
            return _ledger[componentFullName];
        }

        public GameObject GetObject(int objectID)
        {
            return _gameObjects[objectID];
        }

        public bool ExistsObject(int objectID)
        {
            return _gameObjects.ContainsKey(objectID);
        }

        public void Register(Component component)
        {
            getService(component.Type).Register(component);
            _ledger.Add(component.FullName, component);
            if(!_gameObjects.ContainsKey(component.ID)) {
                _gameObjects.Add(component.ID, component.Parent);
            }
        }

        public void Unregister(Component component)
        {
            getService(component.Type).Unregister(component);
            _ledger.Remove(component.FullName);
        }

        private Service getService(ComponentType type)
        {
            switch (type)
            {
                case ComponentType.Animation:
                    return _animationService;
                case ComponentType.Collision:
                    return _collisionService;
                case ComponentType.GUI:
                    return _guiService;
                case ComponentType.Input:
                    return _inputService;
                case ComponentType.Movement:
                    return _movementService;
                case ComponentType.Physics:
                    return _physicsService;
                case ComponentType.Rendering:
                    return _renderingService;
                default:
                    throw new Exception("ComponentType not recognized by ServiceRegistrar: " + type.ToString());
            }
        }

        public void Update(double elapsedTime)
        {
            // Use this method to provide explicit update order, i.e., to control the order in which inter-dependent services are updated.
            _inputService.Update(elapsedTime);
            // TODO - do this better
            if (!(((InputService)_inputService).GetMasterState() == MasterInputState.NULL)) { return; }

            _guiService.Update(elapsedTime);
            _physicsService.Update(elapsedTime);
            _movementService.Update(elapsedTime);
            _animationService.Update(elapsedTime);
            _collisionService.Update(elapsedTime);
            _renderingService.Update(elapsedTime);

            foreach(GameObject gameObject in _gameObjects.Values) {
                if (gameObject.ChildCount == 0)
                {
                    _gameObjects.Remove(gameObject.ID);
                }
            }
        }

    }
}
