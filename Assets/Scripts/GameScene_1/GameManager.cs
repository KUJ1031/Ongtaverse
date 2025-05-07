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
        if (gamemanager == null)
        {
            gamemanager = this;
           // DontDestroyOnLoad(gameObject);
        }
        else if (gamemanager != this)
        {
          //  Destroy(gameObject); // 중복 객체 제거
        }
    }

    private void Start()
    {
        if (uiManager == null) // 만약 UIManager가 null이면
        {
            uiManager = FindObjectOfType<UIManager>();
        }
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

        if (PlayerPrefs.GetInt("AllClear", 0) == 1)
        {
        }
        else
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "GameScene_1")
            {
                if (currentScore == 5)
                {
                    ClearGame();
                }
            }
            if (currentScene == "GameScene_2")
            {
                if (currentScore == 7)
                {
                    ClearGame();
                }
            }
            if (currentScene == "GameScene_3")
            {
                if (currentScore == 10)
                {
                    ClearGame();
                }
            }
           
        }
    }

    public void ClearGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "GameScene_1")
        {
            SystemManager.instance.IsLv2DoorOpened = true;
            isLv1Clear = true;
        }
        if (currentScene == "GameScene_2")
        {
            SystemManager.instance.IsLv3DoorOpened = true;
            isLv2Clear = true;
        }
        if (currentScene == "GameScene_3")
        {
            isLv3Clear = true;
            PlayerPrefs.SetInt("AllClear", 1);
        }
        Debug.Log("Game Clear");
       
        uiManager.GameClear();
    }
}
