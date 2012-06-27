using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public class Tween
    {
        double _original; // Starting value
        double _distance; // Total amount to increase starting value by
        double _current; // Current value
        double _totalTimePassed = 0; // How much time passed since Tween was started
        double _totalDuration = 5; // How long Tween lasts
        bool _finished = false; // True if tween is done
        TweenFunction _tweenF = null; // Various different Tween equations, e.g. linear, exponential

        public delegate double TweenFunction(double timePassed, double start, double distance, double duration);

        // Return values of _current, _finished
        public double Value()
        {
            return _current;
        }

        public bool IsFinished()
        {
            return _finished;
        }


        // Constructor w/ default linear function
        public Tween(double start, double end, double time)
        {
            Construct(start, end, time, Tween.Linear);
        }


        // Constructor w/ TweenFunction parameter
        public Tween(double start, double end, double time, TweenFunction tweenF)
        {
            Construct(start, end, time, tweenF);
        }


        // Initialize members
        public void Construct(double start, double end, double time, TweenFunction tweenF)
        {
            _distance = end - start;
            _original = start;
            _current = start;
            _totalDuration = time;
            _tweenF = tweenF;
        }


        // Update _totalTimePassed, _current and _finished
        public void Update(double elapsedTime)
        {
            _totalTimePassed += elapsedTime;
            _current = _tweenF(_totalTimePassed, _original, _distance, _totalDuration);

            if (_totalTimePassed > _totalDuration)
            {
                _current = _original + _distance;
                _finished = true;
            }
        }


        // Reset tween
        public void Reset()
        {
            _current = _original;
            _finished = false;
            _totalTimePassed = 0;
        }

        // Reset tween with new values
        public void Reset(double start, double end, double time, TweenFunction tweenF)
        {
            Construct(start, end, time, tweenF);
            Reset();
        }



        // TweenFunction methods

        // Linear
        public static double Linear(double timePassed, double start, double distance, double duration)
        {
            return distance * timePassed / duration + start;
        }

        // Ease-out exponential
        public static double EaseOutExpo(double timePassed, double start, double distance, double duration)
        {
            if (timePassed == duration)
            {
                return start + distance;
            }

            return distance * (-Math.Pow(2, -10 * timePassed / duration) + 1) + start;
        }

        // Ease-in exponential
        public static double EaseInExpo(double timePassed, double start, double distance, double duration)
        {
            if (timePassed == 0)
            {
                return start;
            }

            return distance * Math.Pow(2, 10 * (timePassed / duration - 1)) + start;
        }

        // Ease-out circular
        public static double EaseOutCirc(double timePassed, double start, double distance, double duration)
        {
            return distance * Math.Sqrt(1 - (timePassed = timePassed / duration - 1) * timePassed) + start;
        }

        // Ease-in circular
        public static double EaseInCirc(double timePassed, double start, double distance, double duration)
        {
            return -distance * (Math.Sqrt(1 - (timePassed /= duration) * timePassed) - 1) + start;
        }


    }
}
