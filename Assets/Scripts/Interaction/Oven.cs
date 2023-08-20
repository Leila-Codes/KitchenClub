using System;
using UnityEngine;

namespace Interaction
{
    public class Oven : Interactable
    {
        private Animator _animator;
        private static readonly int Open = Animator.StringToHash("open");
        private bool _opened = false;

        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            InteractStart += OnInteractStarted;
            InteractCancel += OnInteractCancelled;
        }

        private void OnInteractStarted()
        {
            _opened = !_opened;
            _animator.SetBool(Open, _opened);
        }

        private void OnInteractCancelled()
        {
            _opened = false;
            _animator.SetBool(Open, _opened);
        }
    }
}