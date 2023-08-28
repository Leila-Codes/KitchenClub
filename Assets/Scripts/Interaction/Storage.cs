using System;
using UnityEngine;

namespace Interaction
{
    public class FridgeFreezer : Interactable
    {
        private Animator _animator;
        private static readonly int Open = Animator.StringToHash("opened");
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
            _animator.SetBool(Open, _opened);
        }

        private void OnInteractCancel(Interactable interactable)
        {
            _opened = false;
            _animator.SetBool(Open, _opened);
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Player") && _opened)
            {
                _opened = false;
                _animator.SetBool(Open, _opened);
            }
        }
    }
}