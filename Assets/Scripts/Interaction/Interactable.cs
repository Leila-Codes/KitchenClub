using System;
using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interaction
{
    public delegate void InteractionHandler(Interactable interactable);
    
    public class Interactable : MonoBehaviour
    {
        public float interactionDuration = 1f;
        [FormerlySerializedAs("inteactionEnabled")] public bool interactionEnabled = true;
        public bool requiresContinuousInteraction = false;
        private bool _inProgress;
        private IEnumerator _enumerator;
        private Hintable _hintable;

        public event InteractionHandler InteractStart;
        public event InteractionHandler InteractCancel;
        public event InteractionHandler InteractComplete;

        private void Start()
        {
            _hintable = GetComponentInChildren<Hintable>();
        }

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
            if (_hintable)
            {
                _hintable.ShowHint();
            }
        }

        public void HideHint()
        {
            if (_hintable)
            {
                _hintable.HideHint();
            }
        }
    }
}