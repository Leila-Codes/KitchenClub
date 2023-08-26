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

        private void OnInteractStart()
        {
            _opened = !_opened;
            _animator.SetBool(FridgeOpen, _opened);
        }

        private void OnInteractCancel()
        {
            _opened = false;
            _animator.SetBool(FridgeOpen, _opened);

        }
    }
}