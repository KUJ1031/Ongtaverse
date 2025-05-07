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
        string currentScene = SceneManager.GetActiveScene().name;

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
        if (GameManager.Instance.isLv3Clear)
        {
            int bestScore = PlayerPrefs.GetInt("BestScore", 0);
            bestScoreText.text = "최고 점수 : " + bestScore.ToString();
            bestScoreText.gameObject.SetActive(true);
            bestScoreText2.gameObject.SetActive(true);
        }
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // 씬 전환 후에도 파괴되지 않도록
        }
        else
        {
           // Destroy(gameObject); // 중복 생성된 객체는 파괴
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameOverImage == null) Debug.LogError("GameOverImage null");
        if (scoreText == null) Debug.LogError("scoreText null");

        GameOverImage.gameObject.SetActive(false);        
    }

    public void GameOver()
    {
        GameOverImage.gameObject.SetActive(true);
    }

    public void GameClear()
    {
        GameClearImage.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "점수 : " + score.ToString();
        scoreText2.text = "점수 : " + score.ToString();

        // 저장된 최고 점수 불러오기
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // 새로운 점수가 최고 점수보다 높다면 갱신
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save(); // 저장
        }

        // 최고 점수 표시
        bestScoreText.text = "최고 점수 : " + bestScore.ToString();
        bestScoreText2.text = "최고 점수 : " + bestScore.ToString();
    }
}
