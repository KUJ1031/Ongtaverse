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

    [HideInInspector] public bool isLv1Clear = false;
    [HideInInspector] public bool isLv2Clear = false;
    [HideInInspector] public bool isLv3Clear = false;
    [HideInInspector] public bool isLvAllClear = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        if (gamemanager == null)
        {
            gamemanager = this;
            // DontDestroyOnLoad(gameObject);
        }
        else if (gamemanager != this)
        {
            // Destroy(gameObject); // 중복 객체 제거
        }
    }

    private void Start()
    {
        try
        {
            if (uiManager == null)
            {
                uiManager = FindObjectOfType<UIManager>();
            }

            if (uiManager != null)
            {
                uiManager.UpdateScore(0);
            }
            else
            {
                Debug.LogWarning("UIManager가 씬에서 발견되지 않았습니다.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Start 예외: " + ex.Message);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        try
        {
            uiManager?.GameOver();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("GameOver 예외: " + ex.Message);
        }
    }

    public void RestartGame()
    {
        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("RestartGame 예외: " + ex.Message);
        }
    }

    public void LoadMainScene()
    {
        try
        {
            SceneManager.LoadScene("MainScene");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("LoadMainScene 예외: " + ex.Message);
        }
    }

    public void AddScore(int score)
    {
        try
        {
            currentScore += score;
            Debug.Log("Score : " + currentScore);
            uiManager?.UpdateScore(currentScore);

            if (PlayerPrefs.GetInt("AllClear", 0) == 1) return;

            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "GameScene_1" && currentScore == 5)
            {
                ClearGame();
            }
            else if (currentScene == "GameScene_2" && currentScore == 7)
            {
                ClearGame();
            }
            else if (currentScene == "GameScene_3" && currentScore == 10)
            {
                ClearGame();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("AddScore 예외: " + ex.Message);
        }
    }

    public void ClearGame()
    {
        try
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "GameScene_1")
            {
                if (SystemManager.instance != null)
                    SystemManager.instance.IsLv2DoorOpened = true;
                isLv1Clear = true;
                Time.timeScale = 0f;
            }
            else if (currentScene == "GameScene_2")
            {
                if (SystemManager.instance != null)
                    SystemManager.instance.IsLv3DoorOpened = true;
                isLv2Clear = true;
                Time.timeScale = 0f;
            }
            else if (currentScene == "GameScene_3")
            {
                isLv3Clear = true;
                PlayerPrefs.SetInt("AllClear", 1);
                Time.timeScale = 0f;
            }

            Debug.Log("Game Clear");
            uiManager?.GameClear();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("ClearGame 예외: " + ex.Message);
        }
    }
}
