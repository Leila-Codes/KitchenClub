using System.Collections;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public int movementSpeed = 4;
    public int turnSpeed = 20;
    public GameObject interactionSprite;
    
    private Animator _animator;
    private Rigidbody _rigidbody;
    private float _rot;
    private Interactable _interactable;
    private bool _interacting = false;

    private static readonly int XInput = Animator.StringToHash("x_input");
    private static readonly int YInput = Animator.StringToHash("y_input");
    private static readonly int Performing = Animator.StringToHash("performing");

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _rot = transform.rotation.eulerAngles.y;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            _interactable = other.gameObject.GetComponent<Interactable>();
            
            if (interactionSprite != null) 
                interactionSprite.SetActive(true);
        }
    }

    private void OnCollisionExit()
    {
        if (interactionSprite != null) 
            interactionSprite.SetActive(true);
        
        if (_interacting)
        {
            _interacting = false;
            _interactable.Cancel();
            StopCoroutine(InteractAnimation(_interactable.interactionDuration));
        }
        
        _interactable = null;
        
        if (interactionSprite != null) 
            interactionSprite.SetActive(false);
    }

    void FixedUpdate()
    { 
        HandlePlayerMovement();

        if (Input.GetKey(KeyCode.E) 
            && _interacting == false 
            && _interactable != null)
        {
            _interactable.Interact();
            _interacting = true;
            StartCoroutine(InteractAnimation(_interactable.interactionDuration));
        }
    }

    private IEnumerator InteractAnimation(float durationSecs)
    {
        _animator.SetBool(Performing, true);
        yield return new WaitForSeconds(durationSecs);
        _animator.SetBool(Performing, false);
    }

    private void HandlePlayerMovement()
    {
        float yInput = Input.GetAxisRaw("Vertical");
        float xInput = Input.GetAxisRaw("Horizontal");
        
        // _animator.SetFloat(XInput, xInput);
        _animator.SetFloat(YInput, yInput);

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
