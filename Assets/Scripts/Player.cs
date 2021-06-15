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
        if (h != 0)
        {
            _character.transform.rotation = Quaternion.Euler(new Vector3(0, -90 * Mathf.Sign(h), 0));
        }

        velocity.y = _yVelocity;
        _character.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (_character.isGrounded)
        {
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
                    _canDoubleJump = false;
                }
            }
            _yVelocity -= _gravity;
        }
    }
}
