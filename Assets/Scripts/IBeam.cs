using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBeam : MonoBehaviour
{
    [SerializeField] private Transform _startPos, _endPos;
    [SerializeField] private float _speed = 2f;

    private bool _movingRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (_movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPos.position, _speed * Time.deltaTime);
            if (transform.position == _endPos.position)
            {
                _movingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPos.position, _speed * Time.deltaTime);
            if (transform.position == _startPos.position)
            {
                _movingRight = true;
            }
        }
    }

}
