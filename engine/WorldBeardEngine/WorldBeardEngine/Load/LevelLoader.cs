using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Load
{
    public class LevelLoader
    {

        public Dictionary<int, GameObject> LoadLevelObjects(string levelName)
        {
            AbstractObjectFactory objectFactory = new AbstractObjectFactory();

            List<GameObjectDO> gameObjectDOS = new LevelDefReader().GetGameObjectsFromLevelDef(ConfigSettings.ASSEMBLY_MAIN_DIR + @"Assets\LevelDefs\" + levelName);

            Dictionary<int, GameObject> gameObjects = new Dictionary<int, GameObject>();

            foreach (GameObjectDO gameObjectDO in gameObjectDOS)
            {
                gameObjects.Add(gameObjectDO.ID, objectFactory.GetGameObjectFromDO(gameObjectDO));
            }

            return gameObjects;
        }

    }
}
