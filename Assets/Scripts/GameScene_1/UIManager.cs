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
                    TargetScoreText.text = "�ְ� ����� �����غ���!";
                }
                else if (currentScene == "GameScene_1")
                {
                    TargetScoreText.text = "��ǥ ���� : 5��";
                }
                else if (currentScene == "GameScene_2")
                {
                    TargetScoreText.text = "��ǥ ���� : 7��";
                }
                else if (currentScene == "GameScene_3")
                {
                    TargetScoreText.text = "��ǥ ���� : 10��";
                }
            }

            if (GameManager.Instance != null && GameManager.Instance.isLv3Clear)
            {
                int bestScore = PlayerPrefs.GetInt("BestScore", 0);

                if (bestScoreText != null)
                {
                    bestScoreText.text = "�ְ� ���� : " + bestScore.ToString();
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
            Debug.LogError("UIManager.Awake ����: " + ex.Message);
        }
    }

    void Start()
    {
        try
        {
            if (GameOverImage == null) Debug.LogError("GameOverImage�� null�Դϴ�.");
            if (scoreText == null) Debug.LogError("scoreText�� null�Դϴ�.");

            if (GameOverImage != null)
                GameOverImage.gameObject.SetActive(false);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("UIManager.Start ����: " + ex.Message);
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
            Debug.LogError("GameOver ����: " + ex.Message);
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
            Debug.LogError("GameClear ����: " + ex.Message);
        }
    }

    public void UpdateScore(int score)
    {
        try
        {
            if (scoreText != null)
                scoreText.text = "���� : " + score.ToString();

            if (scoreText2 != null)
                scoreText2.text = "���� : " + score.ToString();

            int bestScore = PlayerPrefs.GetInt("BestScore", 0);

            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.SetInt("BestScore", bestScore);
                PlayerPrefs.Save();
            }

            if (bestScoreText != null)
                bestScoreText.text = "�ְ� ���� : " + bestScore.ToString();

            if (bestScoreText2 != null)
                bestScoreText2.text = "�ְ� ���� : " + bestScore.ToString();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("UpdateScore ����: " + ex.Message);
        }
    }
}
