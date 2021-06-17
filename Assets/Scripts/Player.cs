using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 7;
    [SerializeField] private float _gravity = 1;
    [SerializeField] private float _jumpHeight = 40, _doubleJumpHeight = 50;

    private bool _canDoubleJump = false;
    private CharacterController _character;
    private float _yVelocity;
    private Quaternion _currentRotation;
        
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
        Vector3 velocity = Vector3.right * (h * _speed);
            
        // Set Gravity
        Jump();
            
        // Change Direction
        if (h != 0 && _character.isGrounded)
        {
            _character.transform.rotation = Quaternion.Euler(new Vector3(0, -90 * Mathf.Sign(h), 0));
            _currentRotation = _character.transform.rotation;
        }

        velocity.y = _yVelocity;
        _character.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (_character.isGrounded)
        {
            transform.rotation = _currentRotation;
            _canDoubleJump = true;
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
        if (other.CompareTag("iBeam"))
        {
            Debug.Log("Landed on iBeam");
            transform.SetParent(other.transform, true);
        }
    }
    
}
