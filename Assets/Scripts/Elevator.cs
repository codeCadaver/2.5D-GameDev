using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float _flickerDelay = 0.05f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _platform; // so player doesn't rotate
    [SerializeField] private int _coinsRequired = 10;
    [SerializeField] private Material[] _emissionMaterials;
    [SerializeField] private Transform _start, _end;

    private bool _movingUp = true;
    private bool _startElevator = false;
    private bool _requireCoins = true;
    private int _coinsCollected = 0;
    private MeshRenderer _renderer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = _emissionMaterials[0];
    }

    private void FixedUpdate()
    {
        Move();
        RotateElevator();
        _platform.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_requireCoins == true)
            {
                StartCoroutine(DeniedRoutine(_flickerDelay));
            }
            else
            {
                _renderer.material = _emissionMaterials[1];
                _startElevator = true;
            }
        }
    }

    IEnumerator DeniedRoutine(float delay)
    {
        int count = 0;
        while (count < 4)
        {
            _renderer.material = _emissionMaterials[0];
            yield return new WaitForSeconds(delay);
            _renderer.material = _emissionMaterials[1];
            yield return new WaitForSeconds(delay);
            count++;
        }
        _renderer.material = _emissionMaterials[0];
    }

    private void CoinsCollected(int value)
    {
        _coinsCollected ++;
        if (_coinsCollected >= _coinsRequired)
        {
            _requireCoins = false;
        }
    }

    private void OnEnable()
    {
        Coin.OnCollected += CoinsCollected;
    }

    private void OnDisable()
    {
        Coin.OnCollected -= CoinsCollected;
    }
    

    private Vector3 GetDirection()
    {
        return _movingUp == true ? _end.position : _start.position;
    }

    private void Move()
    {
        if (_startElevator == false) return;
        
        transform.position = Vector3.MoveTowards(transform.position, GetDirection(), Time.deltaTime * _speed);
        
        if (transform.position == _start.position)
        {
            StartCoroutine(DelayRoutine());
        }

         if (transform.position == _end.position)
        {
            StartCoroutine(DelayRoutine());
        }
    }

    IEnumerator DelayRoutine()
    {
        _startElevator = false;
        yield return new WaitForSeconds(0.5f);
        _movingUp = !_movingUp;
        _startElevator = true;
    }

    private void RotateElevator()
    {
        // get slope
    /* Slope = rotation2 - rotation1 / distance2 - distance1
                360 - 0 / End location - Start Location
                slope = 360 / _end.position - _start.position;
                
                rotation - startRotation = slope * (transform.position - _start.position)
                
        // local rotation = m * Vector3.distance(transform.position - _start.position);
     
     */
    
        var m = 360 / Vector3.Distance(_end.position, _start.position);
        var currentRotation = transform.localEulerAngles;
        currentRotation.y = m * (Vector3.Distance(transform.position, _start.position));
        transform.localEulerAngles = currentRotation;
    }
    
}
