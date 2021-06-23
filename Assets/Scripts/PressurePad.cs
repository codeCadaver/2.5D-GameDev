using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public static Action OnPressurePad;
    
    [SerializeField] private float _rotationSpeed = 20f;

    private ParticleSystem _particle;
    // Detect box
    // set rigidbody to kinematic
    // raise flag animation
    private bool _detectTrigger = true;

    private void Start()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_detectTrigger == false) return;
        
        if (other.gameObject.CompareTag("Moveable"))
        {
            float distance = Vector3.Distance(this.transform.position, other.transform.position);
            if (distance <= 2.02)
            {
                _particle.gameObject.SetActive(false);
                other.attachedRigidbody.isKinematic = true;
                _detectTrigger = false;
                OnPressurePad?.Invoke();
            }
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}
