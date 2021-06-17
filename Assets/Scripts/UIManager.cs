using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] private Text _scoreText, _livesText;

    public int Score { get; private set; } = 0;
    
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

    private void Start()
    {
        UpdateScore(0);
    }

    private void UpdateScore(int points)
    {
        Score += points;
        _scoreText.text = $"Score: {Score}";
    }

    private void UpdateLives()
    {
        var lives = GameManager.Instance.Lives;
        _livesText.text = $"Lives: {lives}";
    }

    private void OnEnable()
    {
        Coin.OnCollected += UpdateScore;
        Player.OnDeath += UpdateLives;
    }

    private void OnDisable()
    {
        Coin.OnCollected -= UpdateScore;
        Player.OnDeath -= UpdateLives;
    }
}
