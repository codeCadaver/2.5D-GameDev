using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private int _value = 10;
    public static Action<int> OnCoinCollected;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnCoinCollected?.Invoke(_value); // check if anyone is listening
            Destroy(this.gameObject);
        }
    }
}
