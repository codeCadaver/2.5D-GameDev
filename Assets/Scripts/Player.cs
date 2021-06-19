using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Action OnDeath;

    [SerializeField] private float _speed = 7;
    [SerializeField] private float _gravity = 1;
    [SerializeField] private float _jumpHeight = 40, _doubleJumpHeight = 50, _wallJumpForce = 10;
    [SerializeField] private Transform _start;

    private bool _canDoubleJump = false;
    private bool _canWallJump = false;
    private CharacterController _character;
    private float _yVelocity;
    private Quaternion _currentRotation;
    private Vector3 _velocity;
    private Vector3 _wallSurfaceNormal;
        
    // Start is called before the first frame update
    void Start()
    {
        _character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
            
        // Set Gravity
        Jump();

        if (_character.isGrounded)
        {
            _velocity = Vector3.right * (h * _speed);
            
            // Change Direction
            if (h != 0)
            {
                _character.transform.rotation = Quaternion.Euler(new Vector3(0, -90 * Mathf.Sign(h), 0));
                _currentRotation = _character.transform.rotation;
            }
        }

        _velocity.y = _yVelocity;
        _character.Move(_velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (_character.isGrounded)
        {
            transform.rotation = _currentRotation;
            _canDoubleJump = true;
            _canWallJump = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            if (_canDoubleJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _yVelocity += _doubleJumpHeight;
                    StartCoroutine(FlipRoutine());
                    _canDoubleJump = false;
                }
            }
            if (_canWallJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _yVelocity = _doubleJumpHeight;
                    _velocity = _wallSurfaceNormal * _speed;
                    transform.localRotation = Quaternion.Inverse(transform.rotation);
                    _canWallJump = false;
                }
            }
            _yVelocity -= _gravity;
        }
    }

    IEnumerator FlipRoutine()
    {
        var angle = 0;
        var amount = -5;
        while (angle > -360)
        {
            transform.Rotate(Vector3.right, amount, Space.Self);
            angle += amount;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathBox"))
        {
            OnDeath?.Invoke();
            _character.enabled = false;
            transform.position = _start.position;
            _character.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Moving"))
        {
            _character.transform.SetParent(other.transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Moving"))
        {
            transform.parent = null;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_character.isGrounded)
        {
            return;
        }

        if (hit.collider.CompareTag("Block"))
        {
            _canWallJump = true;
            _canDoubleJump = false;
            _wallSurfaceNormal = hit.normal;
        }
        
    }
}
