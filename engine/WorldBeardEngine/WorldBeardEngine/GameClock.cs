using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine
{
    public class GameClock
    {
        private double _totalElapsed;
        private double _justElapsed;
        private bool _isClockRunning = false;
        private double _clockSpeed;

        private static GameClock _clock;
        public static double TotalElapsedTime { get { return _clock._totalElapsed; } }
        public static double ElapsedTime { get { return _clock._justElapsed; } }

        public GameClock()
        {
            _totalElapsed = 0;
            _justElapsed = 0;
            _clockSpeed = ConfigSettings.CLOCK_SPEED;
        }

        public static void InitializeClock()
        {
            _clock = new GameClock();
            _clock._isClockRunning = true;
        }

        public static void StartClock()
        {
            _clock._isClockRunning = true;
        }

        public static void StopClock()
        {
            _clock._isClockRunning = false;
        }

        public static void Update(double elapsedTime)
        {
            if (_clock._isClockRunning)
            {
                _clock._justElapsed = elapsedTime * _clock._clockSpeed;
                _clock._totalElapsed += elapsedTime * _clock._clockSpeed;
            }
        }

        public static void SetClockSpeed(double clockSpeed)
        {
            _clock._clockSpeed = clockSpeed;
        }

    }
}
