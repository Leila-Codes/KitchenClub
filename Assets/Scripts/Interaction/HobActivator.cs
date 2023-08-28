using UnityEngine;

namespace Interaction
{
    public class HobActivator : CookingAppliance
    {

        private bool _active;

        private void Start()
        {
            InteractStart += OnInteractStarted;
        }

        private void OnInteractStarted(Interactable interactable)
        {
            _active = !_active;
        }
    }
}