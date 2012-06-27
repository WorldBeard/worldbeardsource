using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Rendering
{
    public class CameraRenderer : Renderer
    {
        private Camera _camera;

        public CameraRenderer()
            : base()
        {
            _camera = SingletonFactory.GetCamera();
        }

        public bool IsOnScreen(Sprite sprite)
        {
            return _camera.IsOnScreen(sprite);
        }

        public void Update()
        {
            _camera.Update();
        }

        public override void DrawSprite(Sprite sprite)
        {
            sprite.SetPosition(sprite.XPosition + _camera.XOffset, sprite.YPosition + _camera.YOffset);
            if (IsOnScreen(sprite))
            {
                base.DrawSprite(sprite);
            }
        }
    }
}