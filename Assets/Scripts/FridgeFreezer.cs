using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeFreezer : MonoBehaviour
{
    private Animator _animator;
    private static readonly int FridgeOpen = Animator.StringToHash("fridgeOpen");
    // private static readonly int FreezerOpen = Animator.StringToHash("freezerOpen");

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool(FridgeOpen, true);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool(FridgeOpen, false);
        }
    }
}
