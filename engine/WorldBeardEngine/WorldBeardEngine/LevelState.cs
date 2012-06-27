using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;
using WorldBeardEngine.Components;
using WorldBeardEngine.Events.EventHub;
using WorldBeardEngine.Load;
using WorldBeardEngine.Services.Rendering;

namespace WorldBeardEngine
{
    public class LevelState : GameState
    {
        private Dictionary<int, GameObject> _levelObjects;

        public LevelState(String levelName)
        {
            _levelObjects = new LevelLoader().LoadLevelObjects(levelName);
            // TODO - Can set player in a better way than this
            if (SingletonFactory.GetServiceRegistrar().ExistsObject(10000)) { SingletonFactory.SetPlayer(_levelObjects[10000]); }

            ((RenderingService)SingletonFactory.GetRenderingService()).Initialize();

            GameClock.InitializeClock();
        }

        public override void Update(double elapsedTime)
        {
            GameClock.Update(elapsedTime);
            SingletonFactory.GetServiceRegistrar().Update(GameClock.ElapsedTime);
        }

        public override void Render()
        {
            // INTENTIONALLY BLANK
        }

        new public void OnUnload()
        {
            SingletonFactory.GetTextureManager().Dispose();
        }

    }
}
