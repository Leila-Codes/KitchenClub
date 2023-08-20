using System;
using Interaction;

namespace Cooking
{
    [Serializable]
    public class CookingStep
    {
        public Interactable target;
        public ActionType action;
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
