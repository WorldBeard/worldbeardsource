using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Load
{
    internal class AbstractObjectFactory
    {
        private AbstractComponentFactory _componentFactory = new AbstractComponentFactory();

        internal GameObject GetGameObjectFromDO(GameObjectDO gameObjectDO)
        {
            GameObject gameObject = new GameObject(gameObjectDO.ID);
            gameObject.Name = gameObjectDO.Name;
            gameObject.IsSaved = gameObjectDO.IsSaved;

            foreach (ComponentDO componentDO in gameObjectDO.ComponentDOs)
            {
                gameObject.AddComponent(_componentFactory.GetComponentFromDO(componentDO));
            }

            return gameObject;
        }
    }
}
