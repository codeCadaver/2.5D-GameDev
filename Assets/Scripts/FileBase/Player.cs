using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace FileBase
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 7f;
        [SerializeField] private float _gravity = 1;
        [SerializeField] private float _jumpHeight = 40f;

        private Animator _animator;
        private float _idleTime = 0;
        private float h;
        private int _angryHash = Animator.StringToHash("isAngry");
        private int _jumpHash = Animator.StringToHash("isJumping");
        private int _speedHash = Animator.StringToHash("Speed");
        
        private CharacterController _character;
        private Vector3 _velocity;
        
        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
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
            // rotate character
            if (h < 0)
            {
                _character.transform.rotation = Quaternion.Euler(0, 180, 0);
            } 
            else if (h > 0)
            {
                _character.transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            
            
            h = Input.GetAxisRaw("Horizontal");
            if (_character.isGrounded)
            {
                _velocity.y -= _gravity;
                _animator.SetBool(_jumpHash, false);
                _velocity = Vector3.forward * (h * _speed);
                _animator.SetFloat(_speedHash, Mathf.Abs(h));
                // jump
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _animator.SetBool(_jumpHash, true);
                    _velocity.y += _jumpHeight;
                }

                if (Mathf.Abs(h) < 0.1)
                {
                    _idleTime += Time.deltaTime;
                }
                else
                {
                    _idleTime = 0;
                }
                GetAngry();
            }
            else
            {
                _velocity.y -= _gravity;
                Debug.Log("Not Grounded!");
            }
        }

        private void GetAngry()
        {
            if (_idleTime > 5f)
            {
                _animator.SetBool(_angryHash, true);
            }
            else
            {
                _animator.SetBool(_angryHash, false);
            }
        }
    }
}
