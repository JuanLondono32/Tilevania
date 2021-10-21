using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void PlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeDamage();
        }
        else
        {
            ResetGame();
        }
    }

    private void TakeDamage()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void PickupCoin(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

}
