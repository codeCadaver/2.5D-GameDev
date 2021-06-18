using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    [SerializeField] Text _scoreText;

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

    private void UpdateScore(int score)
    {
        Debug.Log(Score);
        Score += score;
        _scoreText.text = $"Score: {Score}";
    }

    private void OnEnable()
    {
        Coin.OnCollected += UpdateScore;
    }

    private void OnDisable()
    {
        Coin.OnCollected -= UpdateScore;
    }
}
