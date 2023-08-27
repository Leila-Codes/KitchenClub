using UnityEngine;

namespace Interaction
{
    public class KitchenSink : Interactable
    {
        private Animator _animator;
        private bool _filled;
        private static readonly int Filled = Animator.StringToHash("Filled");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            InteractStart += OnInteractStarted;
        }

        private void OnInteractStarted(Interactable interactable)
        {
            _filled = !_filled;
            _animator.SetBool(Filled, _filled);
        }
    }
}