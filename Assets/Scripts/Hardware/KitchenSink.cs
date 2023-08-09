using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenSink : Interactable
{
    private Animator _animator;
    private bool _filled;
    private static readonly int Filled = Animator.StringToHash("Filled");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public override void Interact()
    {
        _filled = !_filled;
        _animator.SetBool(Filled, _filled);
    }

    public override void Cancel()
    {
        // No cancel action for Sink.
    }
}
