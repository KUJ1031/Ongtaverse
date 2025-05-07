using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI TargetScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestScoreText2;
    public Image GameOverImage;
    public Image GameClearImage;

    public Button restartButton;
    public Button exitButton;

    public static UIManager instance;

    private void Awake()
    {
        try
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (TargetScoreText != null)
            {
                if (PlayerPrefs.GetInt("AllClear", 0) == 1)
                {
                    TargetScoreText.text = "최고 기록을 갱신해보자!";
                }
                else if (currentScene == "GameScene_1")
                {
                    TargetScoreText.text = "목표 점수 : 5점";
                }
                else if (currentScene == "GameScene_2")
                {
                    TargetScoreText.text = "목표 점수 : 7점";
                }
                else if (currentScene == "GameScene_3")
                {
                    TargetScoreText.text = "목표 점수 : 10점";
                }
            }

            if (GameManager.Instance != null && GameManager.Instance.isLv3Clear)
            {
                int bestScore = PlayerPrefs.GetInt("BestScore", 0);

                if (bestScoreText != null)
                {
                    bestScoreText.text = "최고 점수 : " + bestScore.ToString();
                    bestScoreText.gameObject.SetActive(true);
                }

                if (bestScoreText2 != null)
                {
                    bestScoreText2.gameObject.SetActive(true);
                }
            }

            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else
            {
                //Destroy(gameObject);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("UIManager.Awake 예외: " + ex.Message);
        }
    }

    void Start()
    {
        try
        {
            if (GameOverImage == null) Debug.LogError("GameOverImage가 null입니다.");
            if (scoreText == null) Debug.LogError("scoreText가 null입니다.");

            if (GameOverImage != null)
                GameOverImage.gameObject.SetActive(false);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("UIManager.Start 예외: " + ex.Message);
        }
    }

    public void GameOver()
    {
        try
        {
            if (GameOverImage != null)
                GameOverImage.gameObject.SetActive(true);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("GameOver 예외: " + ex.Message);
        }
    }

    public void GameClear()
    {
        try
        {
            if (GameClearImage != null)
                GameClearImage.gameObject.SetActive(true);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("GameClear 예외: " + ex.Message);
        }
    }

    public void UpdateScore(int score)
    {
        try
        {
            if (scoreText != null)
                scoreText.text = "점수 : " + score.ToString();

            if (scoreText2 != null)
                scoreText2.text = "점수 : " + score.ToString();

            int bestScore = PlayerPrefs.GetInt("BestScore", 0);

            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.SetInt("BestScore", bestScore);
                PlayerPrefs.Save();
            }

            if (bestScoreText != null)
                bestScoreText.text = "최고 점수 : " + bestScore.ToString();

            if (bestScoreText2 != null)
                bestScoreText2.text = "최고 점수 : " + bestScore.ToString();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("UpdateScore 예외: " + ex.Message);
        }
    }
}
