using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Vector3 _axis;

    private void Update()
    {
        transform.Rotate(_axis * (Time.deltaTime * _speed));
    }
}
