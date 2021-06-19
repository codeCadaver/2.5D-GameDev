using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform _start, _end;
    [SerializeField] private float _speed = 5f;
    
    private bool _movingRight = false;
    
    void FixedUpdate()
    {
        Move();
        RotateElevator();
    }
    
    private Vector3 MoveHorizontal()
    {
        Vector3 location;
        location = _movingRight == true ? _end.position : _start.position;
        return location;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, MoveHorizontal(), _speed * Time.deltaTime);
        
        if (transform.position == _end.position)
        {
            _movingRight = false;
        }

        if (transform.position == _start.position)
        {
            _movingRight = true;
        }
    }
    
    private void MovePlatform()
    {
        if (_movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, _end.position, Time.deltaTime * _speed);
            if (transform.position == _end.position)
            {
                _movingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _start.position, Time.deltaTime * _speed);
            if (transform.position == _start.position)
            {
                _movingRight = true;
            }
        }
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
