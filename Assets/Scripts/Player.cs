using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity;
    private CharacterController _characterController;
    private bool _canFall;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        StartCoroutine(FallDelayRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (_canFall)
        {
            CharacterMovement();
        }
    }

    private void CharacterMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 velocity = Vector3.right * (h * _speed);
        
        // Set Gravity
        if (_characterController.isGrounded)
        {
            Debug.Log("Is Grounded");
        }
        else
        {
            velocity.y -= _gravity;
        }
        
        // Change Direction
        if (h != 0)
        {
            _characterController.transform.rotation = Quaternion.Euler(new Vector3(0, -90 * Mathf.Sign(h), 0));
        }
        _characterController.Move(velocity * Time.deltaTime);
    }

    IEnumerator FallDelayRoutine()
    {
        yield return new WaitForSeconds(1f);
        _canFall = true;
    }
}
