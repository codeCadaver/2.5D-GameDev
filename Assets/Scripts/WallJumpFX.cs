using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpFX : MonoBehaviour
{
    [SerializeField] private float _life = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, _life);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
