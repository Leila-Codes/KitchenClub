using UnityEngine;

public class FridgeFreezer : Interactable
{
    private Animator _animator;
    private static readonly int FridgeOpen = Animator.StringToHash("fridgeOpen");
    private bool _opened = false;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        _opened = !_opened;
        
        _animator.SetBool(FridgeOpen, _opened);
    }

    public override void Cancel()
    {
        _opened = false;
        _animator.SetBool(FridgeOpen, _opened);
    }
}
