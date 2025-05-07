using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

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
