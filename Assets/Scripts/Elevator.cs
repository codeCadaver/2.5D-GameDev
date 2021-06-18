using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float _flickerDelay = 0.05f;
    [SerializeField] private int _coinsRequired = 10;
    [SerializeField] private Material[] _emissionMaterials;
    
    private bool _requireCoins = true;
    private int _coinsCollected = 0;
    private MeshRenderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = _emissionMaterials[0];
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
                // start elevator up down routine
            }
        }
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (_requireCoins) return;
    //
    //     if (other.CompareTag("Player"))
    //     {
    //         _renderer.material = _emissionMaterials[1];
    //     }
    // }

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
}
