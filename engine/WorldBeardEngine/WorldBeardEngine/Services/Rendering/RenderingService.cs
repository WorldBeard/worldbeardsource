using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;
using WorldBeardEngine.Services.Rendering.Cameras;
using WorldBeardEngine.Components;
using Tao.OpenGl;

namespace WorldBeardEngine.Services.Rendering
{
    public class RenderingService : Service
    {
        private CameraRenderer _renderer;

        public RenderingService()
        {
            _validRegisterType = ComponentType.Rendering;

            Gl.glClearColor(0, 0, 0, 0);
        }

        public void Initialize()
        {
            _renderer = new CameraRenderer();
        }

        public override void Update(double elapsedTime)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            if (_registrees.Count == 0) { return; }

            _renderer.Update(); // Updates the camera in case it's a moving camera

            for (int i = 0; i < _registrees.Count; i++)
            {
                _renderer.DrawSprite(((RenderingComponent)_registrees[i]).Model);
            }

            _renderer.Render();
        }

    }
}
