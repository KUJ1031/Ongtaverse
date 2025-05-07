using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = 1f;

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;



    private void Start()
    {
       
    }

    public Vector3 SetRandomPlace(Vector3 lastPostion, int obstacleCount)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "GameScene_1")
        {
            holeSizeMin = 3f;
            holeSizeMax = 5f;
            widthPadding = 5f;
        }
        if (currentScene == "GameScene_2")
        {
            holeSizeMin = 2f;
            holeSizeMax = 4f;
            widthPadding = 4f;
        }
        if (currentScene == "GameScene_3")
        {
            holeSizeMin = 1f;
            holeSizeMax = 3f;
            widthPadding = 3f;
        }
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPostion + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            GameManager.Instance.AddScore(1);
        }
    }
}
