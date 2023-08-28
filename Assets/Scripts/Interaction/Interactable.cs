using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Timer = Cooking.Timer;

namespace Interaction
{
    public delegate void InteractionHandler(Interactable interactable);
    
    public class Interactable : MonoBehaviour
    {
        [Header("Hint light for this interactable")]
        public Hintable hintable;

        public float interactionDuration = 1f;
        public bool interactionEnabled = true;
        public bool requiresContinuousInteraction = false;
        private bool _inProgress;
        private IEnumerator _enumerator;

        public event InteractionHandler InteractStart;
        public event InteractionHandler InteractCancel;
        public event InteractionHandler InteractComplete;

       

        public void Interact()
        {
            if (!interactionEnabled) return;
            
            _inProgress = true;
            
            InteractStart?.Invoke(this);

            StartCoroutine(CompleteHandler());
        }
    
        public void Cancel()
        {
            if (!interactionEnabled) return;

            if (requiresContinuousInteraction && _inProgress)
            {
                Debug.Log("Oops! You didn't hold the interaction long enough :(");
                _inProgress = false;
            }

            InteractCancel?.Invoke(this);
        }
    
        private IEnumerator CompleteHandler()
        {
            yield return new WaitForSeconds(interactionDuration);

            if (!interactionEnabled) yield return null;

            if (_inProgress)
            {
                _inProgress = false;
                
                InteractComplete?.Invoke(this);
            }
        }

        public void ShowHint()
        {
            if (!hintable)
            {
                Debug.Log("No hintable is available for " + name);
                return;
            }

            Debug.Log("Hint now showing for " + name);
            hintable.ShowHint();
        }

        public void HideHint()
        {
            if (hintable)
            {
                hintable.HideHint();
            }
        }
    }
}