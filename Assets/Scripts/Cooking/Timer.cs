using System;
using System.Collections;
using UnityEngine;

namespace Cooking
{
    public delegate void TimerCallback();

    public delegate void TimerTick(float progress);

    public class Timer : MonoBehaviour
    {
        /** ====== TIMER EVENTS ======= */
        public event TimerTick TimerStarted;
        public event TimerCallback TimerStopped;
        public event TimerCallback TimerComplete;
        public event TimerTick TimerTick;
        public event TimerTick TimerProgress;
        /** ==== END TIMER EVENTS ===== */

        
        /** ==== PRIVATE MEMBER VARS ==== */
        private int _endTime;
        private int _elapsed;
        private Coroutine _ticker;
        /** ====== END MEMBER VARS ======= */

        
        public void SetEndTime(int target)
        {
            _endTime = target;
        }
        
        public void StartTimer()
        {
            StopTimer(); // Make sure timer is stopped first!!
            
            _ticker = StartCoroutine(Tick());
            
            TimerStarted?.Invoke(_endTime - _elapsed);
        }

        private void ResetTimer()
        {
            _elapsed = 0;
           
            TimerTick?.Invoke(_endTime - _elapsed);
        }

        public void RestartTimer()
        {
            StopTimer();
            ResetTimer();
            StartTimer();
        }

        // Returns duration of the timer before stopping.
        public int StopTimer()
        {
            if (_ticker != null)
            {
                StopCoroutine(_ticker);
            }
            
            TimerStopped?.Invoke();

            return _elapsed;
        }

        private IEnumerator Tick()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                _elapsed++;

                if (Math.Abs(_elapsed - _endTime) < 0.1)
                {
                    TimerComplete?.Invoke();
                }

                TimerTick?.Invoke(_endTime - _elapsed);
                TimerProgress?.Invoke((float) _elapsed / _endTime);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}