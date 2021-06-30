using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    public static Action<Vector3> OnLedgeGrabbed;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ledge"))
        {
            return;
        }
        OnLedgeGrabbed?.Invoke(other.transform.position);
    }
}
