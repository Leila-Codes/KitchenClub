using System;
using UnityEngine;

namespace Interaction
{
    public class Oven : CookingAppliance
    {
        private Animator _animator;
        private static readonly int Open = Animator.StringToHash("open");
        private bool _opened = false;

        private new void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            base.Awake();
        }

        private void Start()
        {
            InteractStart += OnInteractStarted;
            InteractCancel += OnInteractCancelled;
        }

        private void OnInteractStarted(Interactable interactable)
        {
            _opened = !_opened;
            _animator.SetBool(Open, _opened);
        }

        private void OnInteractCancelled(Interactable interactable)
        {
            _opened = false;
            _animator.SetBool(Open, _opened);
        }
    }
}