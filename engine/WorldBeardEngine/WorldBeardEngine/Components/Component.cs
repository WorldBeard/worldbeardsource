using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.EventHandlers;
using WorldBeardEngine.Events;

namespace WorldBeardEngine.Components
{
    public abstract class Component : IDisposable, IEventReceiver
    {
        protected string _subclass;
        public string Subclass { get { return _subclass; } }
        public string ShortName { get { return _parent.ID + "/" + ComponentTypeUtil.GetStringFromType(_type); } } 
        public string FullName { get { return _parent.ID + "(" + _parent.Name + ")" + "/" + ComponentTypeUtil.GetStringFromType(_type) + "/" + _subclass; } }

        protected ComponentType _type;
        public ComponentType Type { get { return _type; } }

        protected GameObject _parent;
        public GameObject Parent { get { return _parent; } set { _parent = value; } }

        private HashSet<GameEventHandler> _handlers = new HashSet<GameEventHandler>();

        public int ID { get { return _parent.ID; } }

        public Component(string subclass, ComponentType type)
        {
            _subclass = subclass;
            _type = type;
        }

        public void RegisterEventHandler(GameEventHandler handler)
        {
            handler.Parent = this;
            _handlers.Add(handler);
        }

        public void OnEvent(GameEvent gameEvent) {
            if (gameEvent.Source != _parent)
            {
                SingletonFactory.GetEventHub().SendEvent(gameEvent, _parent);
            }
            else
            {
                foreach(GameEventHandler handler in _handlers)
                {
                    if (handler.TypeToHandle == gameEvent.Type)
                    {
                        handler.OnEvent(gameEvent);
                    }
                }
            }
        }

        public abstract void Dispose();

        public override string ToString()
        {
            return FullName;
        }
    }
}
