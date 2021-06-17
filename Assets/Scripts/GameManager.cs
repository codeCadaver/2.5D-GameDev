using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField]
    private int _lives = 3;
    public int Lives
    {
        get => _lives;
        private set => _lives = value;
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void RemoveLive()
    {
        Lives -= 1;
        if (Lives <= 0)
        {
            // TODO: Create Lose Screen
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnEnable()
    {
        Player.OnDeath += RemoveLive;
    }

    private void OnDisable()
    {
        Player.OnDeath -= RemoveLive;
    }
}
