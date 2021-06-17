using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Action<int> OnCollected;

    [SerializeField] private GameObject _particles;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _value = 10;
    [SerializeField] private Vector3 _axis;

    private void Update()
    {
        transform.Rotate(_axis * (Time.deltaTime * _speed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnCollected?.Invoke(_value);
            DestroyCoin();
        }
    }

    private void DestroyCoin()
    {
        // TODO: add particle fx
        Instantiate(_particles, transform.position, Quaternion.identity);
        
        Destroy(this.gameObject);
    }
}
