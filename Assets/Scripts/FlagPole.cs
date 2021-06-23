using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    [SerializeField] private GameObject _confetti;

    private Animator _animator;

    private int _animHash = Animator.StringToHash("RaiseFlag");
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void EnableConfetti()
    {
        _confetti.SetActive(true);
    }

    private void StartAnimation()
    {
        _animator.SetBool(_animHash, true);
    }

    private void OnEnable()
    {
        PressurePad.OnPressurePad += StartAnimation;
    }

    private void OnDisable()
    {
        PressurePad.OnPressurePad -= StartAnimation;
    }
}
