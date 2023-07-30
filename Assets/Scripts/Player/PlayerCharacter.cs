using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Vector2 movementSpeed;
    private Animator _animator;

    private static readonly int XInput = Animator.StringToHash("x_input");
    private static readonly int YInput = Animator.StringToHash("y_input");
    private static readonly int Performing = Animator.StringToHash("performing");

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        float yInput = Input.GetAxisRaw("Vertical");
        float xInput = Input.GetAxisRaw("Horizontal");
        
        _animator.SetFloat(XInput, xInput);
        _animator.SetFloat(YInput, yInput);
        _animator.SetBool(Performing, Input.GetButton("Submit"));

        if (yInput != 0)
            transform.Translate(transform.forward * (movementSpeed.y * (yInput * Time.deltaTime)));
        
        if (xInput != 0)
            transform.Translate(transform.right * (movementSpeed.x * (xInput * Time.deltaTime)));
    }
}
