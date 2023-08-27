using System;
using System.Collections;
using UnityEngine;

namespace Cooking
{
    public delegate void TimerComplete();

    public delegate void TimerTick(float progress);

    public class Timer : MonoBehaviour
    {
        public event TimerComplete TimerComplete;
        public event TimerTick TimerTick;

        private int _target;
        private int _elapsed;
        // private bool _active;
        private Coroutine _ticker;


        public void Setup(int target)
        {
            _target = target;
            _elapsed = 0;
        }

        private void OnDestroy()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            _ticker = StartCoroutine(Tick());
        }

        public void Reset()
        {
            _elapsed = 0;
        }

        // Returns duration of the timer before stopping.
        public int Stop()
        {
            if (_ticker != null)
            {
                StopCoroutine(_ticker);
            }

            return _elapsed;
        }

        private IEnumerator Tick()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                _elapsed++;

                if (Math.Abs(_elapsed - _target) < 0.1)
                {
                    TimerComplete?.Invoke();
                }

                TimerTick?.Invoke((float) _elapsed / _target);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}