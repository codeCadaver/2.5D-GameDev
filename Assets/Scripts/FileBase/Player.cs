using System;
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
        public GameObject ledge;
        [SerializeField] private Transform _ledgeGrabPosition;
        [SerializeField] private Vector3 _ledgeGrabOffset;
        [SerializeField] private Vector3 _climpTransitionOffset;

        private Animator _animator;
        private bool _canJump;
        private bool _isHanging = false;
        private float _idleTime = 0;
        private float h;
        private int _angryHash = Animator.StringToHash("isAngry");
        private int _climbHash = Animator.StringToHash("ClimbTrigger");
        private int _jumpHash = Animator.StringToHash("isJumping");
        private int _ledgeHash = Animator.StringToHash("HangingTrigger");
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
            _velocity.y -= .00000001f;
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
                _canJump = true;
                _animator.SetBool(_jumpHash, false);
                _velocity = Vector3.forward * (h * _speed);
                _animator.SetFloat(_speedHash, Mathf.Abs(h));
               

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
            
            // jump
            if (Input.GetKeyDown(KeyCode.Space) && _canJump)
            {
                _animator.SetBool(_jumpHash, true);
                _velocity.y = _jumpHeight;
                _canJump = false;
            }
            
            // Climb from ledge
            if (_isHanging)
            {
                _animator.SetBool(_jumpHash, false);
                _animator.SetFloat(_speedHash, 0);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _animator.SetTrigger(_climbHash);
                    _isHanging = false;
                }
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

        private void GrabLedge(Vector3 ledgePos)
        {
            _isHanging = true;
            _animator.SetTrigger(_ledgeHash);
            _character.enabled = false;
            // transform.position = _ledgeGrabPosition.position;
            transform.position = ledgePos + _ledgeGrabOffset;
        }

        private void OnEnable()
        {
            LedgeGrab.OnLedgeGrabbed += GrabLedge;
            ClimbTransition.OnClimbComplete += RepositionPlayer;
        }

        private void OnDisable()
        {
            LedgeGrab.OnLedgeGrabbed -= GrabLedge;
            ClimbTransition.OnClimbComplete -= RepositionPlayer;
        }

        private void RepositionPlayer()
        {
            transform.position += _climpTransitionOffset;
            _character.enabled = true;
        }

        IEnumerator ClimbToIdleRoutine()
        {
            yield return new WaitForSeconds(0.1f);
            _character.enabled = true;
        }

    }
}
