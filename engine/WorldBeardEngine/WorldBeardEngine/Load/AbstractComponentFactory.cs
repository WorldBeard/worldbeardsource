using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Load.ComponentFactories;
using WorldBeardEngine.EventHandlers;

namespace WorldBeardEngine.Load
{
    internal class AbstractComponentFactory
    {
        private AnimationFactory _animationFactory = new AnimationFactory();
        private CollisionFactory _collisionFactory = new CollisionFactory();
        private GuiFactory _guiFactory = new GuiFactory();
        private InputFactory _inputFactory = new InputFactory();
        private MovementFactory _movementFactory = new MovementFactory();
        private PhysicsFactory _physicsFactory = new PhysicsFactory();
        private RenderingFactory _renderingFactory = new RenderingFactory();

        private AbstractEventHandlerFactory _eventHandlerFactory = new AbstractEventHandlerFactory();

        internal Component GetComponentFromDO(ComponentDO componentDO)
        {
            Component component = GetComponentFactory(componentDO.Type).GetComponent(componentDO);
            if (componentDO.GameEventHandlerDOs != null && componentDO.GameEventHandlerDOs.Count > 0)
            {
                foreach (EventHandlerDO eventHandlerDO in componentDO.GameEventHandlerDOs)
                {
                    _eventHandlerFactory.AddEventHandlerFromDO(component, eventHandlerDO);
                }
            }
            return component;
        }

        private IComponentFactory GetComponentFactory(ComponentType type)
        {
            switch (type)
            {
                case ComponentType.Animation:
                    return _animationFactory;
                case ComponentType.Collision:
                    return _collisionFactory;
                case ComponentType.GUI:
                    return _guiFactory;
                case ComponentType.Input:
                    return _inputFactory;
                case ComponentType.Movement:
                    return _movementFactory;
                case ComponentType.Physics:
                    return _physicsFactory;
                case ComponentType.Rendering:
                    return _renderingFactory;
                default:
                    throw new Exception("Illegal component type in AbstractComponentFactory: " + type);
            }
        }
    }
}
