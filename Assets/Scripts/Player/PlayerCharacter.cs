using System;
using System.Collections;
using Cooking;
using Interaction;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public int movementSpeed = 4;
    public int turnSpeed = 30;
    public GameObject interactionSprite;
    
    [NonSerialized]
    public Ingredient carrying = null;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private float _rot;
    private Interactable _interactable;
    private IEnumerator _interacting;

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
            
            if (!_interactable.interactionEnabled) return;
            
            if (interactionSprite != null) 
                interactionSprite.SetActive(true);
        }
    }

    private void OnCollisionExit()
    {
        if (_interactable && !_interactable.interactionEnabled) return;

        if (interactionSprite != null) 
            interactionSprite.SetActive(true);
        
        if (_interacting != null)
        {
            _interacting = null;
            _interactable.Cancel();
            StopCoroutine(PlayerInteract(_interactable.interactionDuration));
        }
        
        _interactable = null;
        
        if (interactionSprite != null) 
            interactionSprite.SetActive(false);
    }

    void FixedUpdate()
    { 
        if (_interacting == null)
            HandlePlayerMovement();
    }

    private bool IsInteracting()
    {
        return _interacting != null;
    }

    private void Update()
    {
        if (_interactable != null)
        {
            if (IsInteracting())
            {
                // If I was interacting with something, that requires continuous press, and the key was released...
                if (Input.GetKeyUp(KeyCode.E) && _interactable.requiresContinuousInteraction)
                {
                    StopCoroutine(_interacting);
                    _animator.SetBool(Performing, false);
                    _interacting = null;
                    _interactable.Cancel();
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                // Else, am I pressing the key and is there something to interact with?
                _animator.SetBool(Performing, true);
                _interacting = PlayerInteract(_interactable.interactionDuration);
                StartCoroutine(_interacting);
            }
        }
    }

    private IEnumerator PlayerInteract(float durationSecs)
    {
        _animator.SetBool(Performing, true);
        _interactable.Interact();
        
        yield return new WaitForSeconds(durationSecs);
        
        _animator.SetBool(Performing, false);
        _interacting = null;
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
