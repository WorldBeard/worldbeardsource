using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Assets;
using WorldBeardEngine.Components;
using WorldBeardEngine.EventHandlers;
using WorldBeardEngine.Events;
using WorldBeardEngine.Events.EventHub;
using WorldBeardEngine.Services;
using WorldBeardEngine.Services.Animation;
using WorldBeardEngine.Services.Collision;
using WorldBeardEngine.Services.Input;
using WorldBeardEngine.Services.Input.Controls;
using WorldBeardEngine.Services.Movement;
using WorldBeardEngine.Services.Physics.PhysicsServices;
using WorldBeardEngine.Services.Rendering;
using WorldBeardEngine.Logging;
using EarthCoreEngine;
using EarthCoreEngine.Input;

namespace WorldBeardEngine
{
    public class SingletonFactory
    {
        private static Logger _log;
        public static Logger LOG
        {
            get { if (_log == null) { _log = new Logger(); } return _log; }
        }

        private static GameObject _player;
        private static Input _input;
        private static Camera _camera;
        private static FormData _formData;
        private static AssetTextureManager _textureManager;

        private static ServiceRegistrar _serviceRegistrar;
        private static EventHub _eventHub;
        private static FilterInitializer _filterInitializer;
        private static EventFactory _eventFactory;
        private static EventHandlerDecorator _eventHandlerDecorator;

        private static Dictionary<string, string> _serviceProperties;
        public static Dictionary<string, string> ServiceProperties {
            get
            {
                if (_serviceProperties == null)
                {
                    _serviceProperties = PropertyUtil.LoadPropertiesFromFile(ConfigSettings.PROP_DIR_ROOT + @"services.prop");
                }
                return _serviceProperties;
            }
        }
        private static Service _animationService;
        private static Service _collisionService;
        private static Service _guiService;
        private static Service _inputService;
        private static Service _movementService;
        private static Service _physicsService;
        private static Service _renderingService;

        public static void SetPlayer(GameObject player)
        {
            _player = player;
        }

        public static GameObject GetPlayer()
        {
            return _player;
        }

        public static Sprite GetPlayerSprite()
        {
            return ((RenderingComponent)_player.GetComponent(Components.ComponentType.Rendering)).Model;
        }

        public static Input GetInput()
        {
            if (_input == null) {
                _input = new EarthCoreEngine.Input.Input();
            }
            return _input;
        }

        public static Camera GetCamera()
        {
            if (_camera == null) { _camera = CameraFactory.GetCamera(ConfigSettings.CAMERA_TYPE); }
            return _camera;
        }

        public static FormData GetFormData()
        {
            if (_formData == null) { _formData = new FormData(0, 0); }
            return _formData;
        }

        public static AssetTextureManager GetTextureManager()
        {
            if (_textureManager == null) { _textureManager = new AssetTextureManager(ConfigSettings.ASSEMBLY_MAIN_DIR + ConfigSettings.TEXTURE_DIRECTORY); }
            return _textureManager;
        }

        public static ServiceRegistrar GetServiceRegistrar()
        {
            if (_serviceRegistrar == null) { _serviceRegistrar = new ServiceRegistrar(); }
            return _serviceRegistrar;
        }

        public static EventHub GetEventHub()
        {
            if (_eventHub == null) { _eventHub = new EventHub(); }
            return _eventHub;
        }

        public static FilterInitializer GetFilterInitializer()
        {
            if (_filterInitializer == null && ServiceProperties.ContainsKey("FilterInitializer"))
            {
                _filterInitializer = (FilterInitializer)ReflectionUtil.GetObjectFromTypeName(ServiceProperties["FilterInitializer"]);
            }
            return _filterInitializer;
        }

        public static EventFactory GetEventFactory()
        {
            if (_eventFactory == null) { _eventFactory = new EventFactory(); }
            return _eventFactory;
        }

        public static EventHandlerDecorator GetEventHandlerDecorator()
        {
            if (_eventHandlerDecorator == null) { _eventHandlerDecorator = new EventHandlerDecorator(); }
            return _eventHandlerDecorator;
        }

        public static Service GetAnimationService()
        {
            if (_animationService == null)
            {
                _animationService = (Service)ReflectionUtil.GetObjectFromTypeName(ServiceProperties["AnimationService"]);
            }
            return _animationService;
        }

        public static Service GetCollisionService()
        {
            if (_collisionService == null)
            {
                _collisionService = (Service)ReflectionUtil.GetObjectFromTypeName(ServiceProperties["CollisionService"]);
            }
            return _collisionService;
        }

        public static Service GetGuiService()
        {
            if (_guiService == null)
            {
                _guiService = (Service)ReflectionUtil.GetObjectFromTypeName(ServiceProperties["GuiService"]);
            }
            return _guiService;
        }

        public static Service GetInputService()
        {
            if (_inputService == null)
            {
                _inputService = (Service)ReflectionUtil.GetObjectFromTypeName(ServiceProperties["InputService"]);
            }
            return _inputService;
        }

        public static Service GetMovementService()
        {
            if (_movementService == null)
            {
                _movementService = (Service)ReflectionUtil.GetObjectFromTypeName(ServiceProperties["MovementService"]);
            }
            return _movementService;
        }

        public static Service GetPhysicsService()
        {
            if (_physicsService == null)
            {
                _physicsService = (Service)ReflectionUtil.GetObjectFromTypeName(ServiceProperties["PhysicsService"]);
            }
            return _physicsService;
        }

        public static Service GetRenderingService()
        {
            if (_renderingService == null)
            {
                _renderingService = (Service)ReflectionUtil.GetObjectFromTypeName(ServiceProperties["RenderingService"]);
            }
            return _renderingService;
        }

    }
}
