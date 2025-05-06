using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gamemanager;

    public static GameManager Instance { get { return gamemanager; } }

    private int currentScore = 0;

    UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }

    public bool isClear = false;

    private void Awake()
    {
        gamemanager = this;
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        uiManager.UpdateScore(0);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        uiManager.GameOver();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score : " + currentScore);
        uiManager.UpdateScore(currentScore);


        if (currentScore == 3)
        {
            ClearGame();
        }
    }

    public void ClearGame()
    {
        Debug.Log("Game Clear");
        isClear = true;
        uiManager.GameClear();
    }
}
