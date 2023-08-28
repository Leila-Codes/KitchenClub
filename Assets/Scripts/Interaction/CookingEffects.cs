using Cooking;
using UnityEngine;

namespace Interaction
{
    public class CookingEffects : MonoBehaviour
    {
        public ParticleSystem sparkleEffect;
        private ParticleSystem.EmissionModule _sparkleEffect;
        public ParticleSystem steamEffect;
        private ParticleSystem.EmissionModule _steamEffect;
        public ParticleSystem burnEffect;
        private ParticleSystem.EmissionModule _burnEffect;
    
        private Timer _cookingTimer;

        private void Awake()
        {

            if (sparkleEffect) _sparkleEffect = sparkleEffect.emission;
            if (steamEffect) _steamEffect = steamEffect.emission;
            if (burnEffect) _burnEffect = burnEffect.emission;
        }

        private void Start()
        {
            _sparkleEffect.enabled = false;
            _steamEffect.enabled = false;
            _burnEffect.enabled = false;
            
            _cookingTimer = GetComponent<Timer>();

            _cookingTimer.TimerStarted += OnTimerStart;
            _cookingTimer.TimerTick += OnTimerTick;
            _cookingTimer.TimerStopped += OnTimerStop;
        }

        private void OnTimerStart(float newTime)
        {
            _steamEffect.enabled = true;
        }

        private void OnTimerTick(float timeLeft)
        {
            if (timeLeft == 0)
            {
                _sparkleEffect.enabled = true;
            } else if (timeLeft < -3)
            {
                _sparkleEffect.enabled = false;
                _steamEffect.enabled = false;

                _burnEffect.enabled = true;
            }
        }

        private void OnTimerStop()
        {
            _sparkleEffect.enabled = false;
            _steamEffect.enabled = false;
            _burnEffect.enabled = false;
        }
    }
}
