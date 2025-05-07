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
            bestScoreText.text = "�ְ� ���� : " + bestScore.ToString();
            bestScoreText.gameObject.SetActive(true);
            bestScoreText2.gameObject.SetActive(true);
        }
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // �� ��ȯ �Ŀ��� �ı����� �ʵ���
        }
        else
        {
           // Destroy(gameObject); // �ߺ� ������ ��ü�� �ı�
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
        scoreText.text = "���� : " + score.ToString();
        scoreText2.text = "���� : " + score.ToString();

        // ����� �ְ� ���� �ҷ�����
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // ���ο� ������ �ְ� �������� ���ٸ� ����
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save(); // ����
        }

        // �ְ� ���� ǥ��
        bestScoreText.text = "�ְ� ���� : " + bestScore.ToString();
        bestScoreText2.text = "�ְ� ���� : " + bestScore.ToString();
    }
}
