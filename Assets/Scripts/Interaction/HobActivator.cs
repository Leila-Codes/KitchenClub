using UnityEngine;

namespace Interaction
{
    public class HobActivator : Interactable
    {
        public GameObject flame;
        public GameObject steam;

        private bool _active;

        private void Start()
        {
            InteractStart += OnInteractStarted;
        }

        private void OnInteractStarted(Interactable interactable)
        {
            _active = !_active;
            flame.gameObject.SetActive(_active);
        }
    }
}