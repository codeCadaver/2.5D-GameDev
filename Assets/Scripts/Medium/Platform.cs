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
}
