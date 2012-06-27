using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public class AnimatedSprite : Sprite
    {
        int _framesX;
        int _framesY;
        int _currentFrame = 0;
        double _currentFrameTime = 0.03;

        private double _secondsPerFrame;
        public int FPS { set { _secondsPerFrame = 1 / ((double)value); } }

        private bool _isLooping;
        private bool _hasFinishedLooping;

        public AnimatedSprite() : this(1, 1) {}

        public AnimatedSprite(int framesX, int framesY) : this(new Texture(), new Vector(0, 0, 0), 0, 0, framesX, framesY) { }

        public AnimatedSprite(Texture texture, Vector startingPosition) : this(texture, startingPosition, 0, 0) { }

        public AnimatedSprite(Texture texture, Vector startingPosition, double width, double height) : this(texture, startingPosition, width, height, 1, 1) { }

        public AnimatedSprite(Texture texture, Vector startingPosition, double width, double height, int framesX, int framesY)
            : base(texture, startingPosition, width, height)
        {
            SetAnimation(framesX, framesY);

            _isLooping = true;
            _hasFinishedLooping = false;
            this.FPS = 30; // This sets _secondsPerFrame
            _currentFrameTime = _secondsPerFrame;
        }

        public System.Drawing.Point GetIndexFromFrame(int frame)
        {
            System.Drawing.Point point = new System.Drawing.Point();
            point.Y = frame / _framesX;
            point.X = frame - (point.Y * _framesX);
            return point;
        }

        public void UpdateUVs()
        {
            System.Drawing.Point index = GetIndexFromFrame(_currentFrame);
            float frameWidth = 1.0f / (float)_framesX;
            float frameHeight = 1.0f / (float)_framesY;
            SetUVs(new Point(index.X * frameWidth, index.Y * frameHeight),
                new Point((index.X + 1) * frameWidth, (index.Y + 1) * frameHeight));
        }

        public void SetAnimation(int framesX, int framesY)
        {
            _framesX = framesX;
            _framesY = framesY;
            UpdateUVs();
        }

        private int GetFrameCount()
        {
            return _framesX * _framesY;
        }

        public void AdvanceFrame()
        {
            int numberOfFrames = GetFrameCount();
            _currentFrame = (_currentFrame + 1) % numberOfFrames;
        }

        public void Update(double elapsedTime)
        {
            if (_currentFrame == GetFrameCount() - 1 && _isLooping == false)
            {
                _hasFinishedLooping = true;
                return;
            }

            _currentFrameTime -= elapsedTime;
            if (_currentFrameTime < 0)
            {
                AdvanceFrame();
                _currentFrameTime = _secondsPerFrame;
                UpdateUVs();
            }
        }

    }
}
