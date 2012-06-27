using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Services.Rendering.Cameras;

namespace WorldBeardEngine.Services.Rendering
{
    public class CameraFactory
    {

        public static Camera GetCamera(String cameraType)
        {
            switch (cameraType.ToUpper())
            {
                case "PLAYER":
                    SpriteFollowingCamera camera = new SpriteFollowingCamera();
                    camera.SetTarget(SingletonFactory.GetPlayerSprite());
                    return camera;
                case "POSITIONABLE":
                    return new StaticPositionableCamera();
                case "SCROLLING":
                    return new ScrollingCamera();
                case "STATIC":
                    return new StaticCamera();
                default:
                    throw new Exception("cameraType not recognized by CameraFactory: " + cameraType);
            }
        }

    }
}
