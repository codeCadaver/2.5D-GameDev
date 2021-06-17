using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Medium : MonoBehaviour
{
    private CharacterController _character;
    private bool _canDoubleJump = false;
    private float _gravity = 1;
    private float _jumpHeight = 40, _doubleJumpHeight = 50, _speed = 7;
    private float _yVelocity;
    
    void Start()
    {
        _character = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 velocity = Vector3.right * (h * _speed);
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
        velocity.y = _yVelocity;
        _character.Move(velocity * Time.deltaTime);
    }
}
