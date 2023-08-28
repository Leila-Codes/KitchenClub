using Cooking;
using UnityEngine;

namespace Interaction
{
    public class CookingAppliance : Interactable
    {
        private Timer _cookingTimer;

        protected void Awake()
        {
            _cookingTimer = GetComponent<Timer>();
            if (_cookingTimer == null)
                Debug.LogWarning("Timer for " + name + " does not appear to exist.");
        }

        public Timer Timer()
        {
            return _cookingTimer;
        }
    }
}