using System;
using UnityEngine;

namespace Interaction
{
    public class FridgeFreezer : Interactable
    {
        private Animator _animator;
        private static readonly int FridgeOpen = Animator.StringToHash("opened");
        private bool _opened;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            InteractStart += OnInteractStart;
            InteractCancel += OnInteractCancel;
        }

        private void OnInteractStart(Interactable interactable)
        {
            _opened = !_opened;
            _animator.SetBool(FridgeOpen, _opened);
        }

        private void OnInteractCancel(Interactable interactable)
        {
            _opened = false;
            _animator.SetBool(FridgeOpen, _opened);

        }
    }
}