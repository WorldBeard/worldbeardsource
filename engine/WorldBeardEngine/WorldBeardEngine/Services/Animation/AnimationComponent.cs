using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;
using WorldBeardEngine.Services.Rendering;
using EarthCoreEngine;

namespace WorldBeardEngine.Services.Animation
{
    public class AnimationComponent : Component
    {

        private int _framesX;
        private int _framesY;

        protected AnimatedSprite _model;
        public AnimatedSprite Model
        {
            get
            {
                if (_model == null)
                {
                    _model = (AnimatedSprite)((RenderingComponent)_parent.GetComponent(ComponentType.Rendering)).Model;
                    _model.SetAnimation(_framesX, _framesY);
                }
                return _model;
            }
            set
            {
                _model = value;
            }
        }

        public AnimationComponent(String subclass, AnimatedSprite model)
            : base(subclass, ComponentType.Animation)
        {
            _model = model;
        }

        public AnimationComponent(String subclass, int framesX, int framesY)
            : base(subclass, ComponentType.Animation)
        {
            _framesX = framesX;
            _framesY = framesY;
        }

        public override void Dispose()
        {
            _model = null;
        }

    }
}
