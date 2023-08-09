using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Interactable
{
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("open");
    private bool _opened = false;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public override void Interact()
    {
        _opened = !_opened;
        _animator.SetBool(Open, _opened);
    }

    public override void Cancel()
    {
        _opened = false;
        _animator.SetBool(Open, _opened);
    }
}
