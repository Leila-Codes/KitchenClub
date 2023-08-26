using System;
using Interaction;
using UnityEngine;

namespace Cooking
{
    [Serializable]
    public class CookingStep
    {
        public Interactable target;
        public ActionType action;
        public GameObject popup;
        
        public float cookingTime;
        [NonSerialized]
        private bool _complete;

        public enum ActionType
        {
            Collect,
            Cut,
            Stir,
            Cook
        }

        public void SetCompleted(bool complete)
        {
            _complete = complete;
        }

        public bool IsComplete()
        {
            return _complete;
        }
    }
}
