using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // public Vector2 movementSpeed;
    public int movementSpeed = 4;
    public int turnSpeed = 20;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private float _rot;

    private static readonly int XInput = Animator.StringToHash("x_input");
    private static readonly int YInput = Animator.StringToHash("y_input");
    private static readonly int Performing = Animator.StringToHash("performing");

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _rot = transform.rotation.eulerAngles.y;
    }

    void FixedUpdate()
    {
        float yInput = Input.GetAxisRaw("Vertical");
        float xInput = Input.GetAxisRaw("Horizontal");
        
        // _animator.SetFloat(XInput, xInput);
        _animator.SetFloat(YInput, yInput);
        _animator.SetBool(Performing, Input.GetButton("Submit"));

        if (xInput != 0)
        {
            transform.Rotate(transform.up, turnSpeed * xInput * Time.deltaTime);
            float newRot = Mathf.Clamp(
                _rot + (turnSpeed * Time.deltaTime),
                0f, 
                360f
            );
            
            _rigidbody.MoveRotation(
                Quaternion.AngleAxis(newRot, transform.up)
            );
        }

        if (yInput != 0)
            transform.Translate(Vector3.forward * (movementSpeed * (yInput * Time.deltaTime)));
        
    }
}
