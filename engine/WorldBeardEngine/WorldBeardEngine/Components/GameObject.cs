using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Events;
using WorldBeardEngine.Services;
using WorldBeardEngine.Services.Rendering;
using EarthCoreEngine;

namespace WorldBeardEngine.Components
{
    public class GameObject : IDisposable, IEventReceiver
    {
        /**GENERAL NOTES
         * 
         */
        protected int _id;
        public int ID { get { return _id; } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private bool _isSaved;
        public bool IsSaved { get { return _isSaved; } set { _isSaved = value; } }

        public Sprite Model
        {
            get
            {
                if (HasComponent(ComponentType.Rendering))
                {
                    return ((RenderingComponent)GetComponent(ComponentType.Rendering)).Model;
                }
                return null;
            }
        }

        protected List<Component> _children = new List<Component>();

        public int ChildCount { get { return _children.Count; } }

        // TODO Constructor
        public GameObject(int id)
        {
            _id = id;
        }

        // COMPONENT METHODS
        public void AddComponent(Component newChild)
        {
            newChild.Parent = this;
            _children.Add(newChild);
            SingletonFactory.GetServiceRegistrar().Register(newChild);
        }

        public void RemoveComponent(ComponentType type)
        {
            foreach (Component child in _children) {
                if (child.Type == type) {
                    _children.Remove(child);
                    SingletonFactory.GetServiceRegistrar().Unregister(child);
                    return;
                }
            }
        }

        public void RemoveComponent(String name)
        {
            foreach (Component child in _children)
            {
                if (child.Subclass == name)
                {
                    _children.Remove(child);
                    SingletonFactory.GetServiceRegistrar().Unregister(child);
                    return;
                }
            }
        }

        public Component GetComponent(ComponentType type)
        {
            foreach (Component child in _children)
            {
                if (child.Type == type)
                {
                    return child;
                }
            }
            return null;
        }

        public Component GetComponent(String name)
        {
            foreach (Component child in _children)
            {
                if (child.Subclass == name)
                {
                    return child;
                }
            }
            return null;
        }

        public Boolean HasComponent(ComponentType type)
        {
            foreach (Component child in _children)
            {
                if (child.Type == type)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean HasComponent(String name)
        {
            foreach (Component child in _children)
            {
                if (child.Subclass == name)
                {
                    return true;
                }
            }
            return false;
        }

        public void Dispose()
        {
            foreach (Component child in _children) {
                SingletonFactory.GetServiceRegistrar().Unregister(child);
                child.Dispose();
            }
        }

        public void OnEvent(GameEvent gameEvent)
        {
            gameEvent.Source = this;
            foreach (Component child in _children)
            {
                child.OnEvent(gameEvent);
            }
        }

        public override string ToString()
        {
            return "Obj" + _id + "(" + _name + ")";
        }

    }
}
