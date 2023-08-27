using UnityEngine;

public class Hintable : MonoBehaviour
{
    private Animator _animator;
    private readonly int _hintActive = Animator.StringToHash("active");
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowHint()
    {
        _animator.SetBool(_hintActive, true);
    }
    
    public void HideHint()
    {
        _animator.SetBool(_hintActive, false);
    }
}
