using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FileBase
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 7f;
        [SerializeField] private float _gravity = 1;
        [SerializeField] private float _jumpHeight = 40f;
        
        private CharacterController _character;
        private Vector3 _velocity;
        
        // Start is called before the first frame update
        void Start()
        {
            _character = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            _character.Move(_velocity * Time.deltaTime);
        }

        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            if (_character.isGrounded)
            {
                _velocity = Vector3.forward * (h * _speed);
                // jump
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _velocity.y = _jumpHeight;
                }
            }
            else
            {
                _velocity.y -= _gravity;
            }
        }
    }
}
