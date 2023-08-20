using System.Collections;
using UnityEngine;

namespace Interaction
{
    public delegate void InteractionHandler();
    
    public abstract class Interactable : MonoBehaviour
    {
        public float interactionDuration = 1f;
        public bool requiresContinuousInteraction = false;
        private bool _inProgress;
        private IEnumerator _enumerator;

        public event InteractionHandler InteractStart;
        public event InteractionHandler InteractCancel;
        public event InteractionHandler InteractComplete;

        public void Interact()
        {
            _inProgress = true;
            
            InteractStart?.Invoke();

            StartCoroutine(CompleteHandler());
        }
    
        public void Cancel()
        {
            if (requiresContinuousInteraction && _inProgress)
            {
                Debug.Log("Oops! You didn't hold the interaction long enough :(");
                _inProgress = false;
            }

            InteractCancel?.Invoke();
        }
    
        private IEnumerator CompleteHandler()
        {
            yield return new WaitForSeconds(interactionDuration);
            if (_inProgress)
            {
                _inProgress = false;
                InteractComplete?.Invoke();
            }
        }
    }
}