using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public Image GameOverImage;
    public Image GameClearImage;

    public Button restartButton;
    public Button exitButton;

    public static UIManager instance;

    private void Awake()
    {

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
        scoreText.text = ("점수 : " + score.ToString());
        scoreText2.text = ("점수 : " + score.ToString());
    }
}
