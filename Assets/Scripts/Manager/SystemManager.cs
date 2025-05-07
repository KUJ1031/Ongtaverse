using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{

    [HideInInspector] public bool IsLv1DoorOpened = false;
    [HideInInspector] public bool IsLv2DoorOpened = false;
    [HideInInspector] public bool IsLv3DoorOpened = false;
    [HideInInspector] public bool IsExit = false;
    private BoxCollider2D boxCollider_Door1;
    private BoxCollider2D boxCollider_Door2;
    private BoxCollider2D boxCollider_Door3;
    private BoxCollider2D boxCollider_DoorExit;

    private BoxCollider2D boxCollider_Ladder1;
    private BoxCollider2D boxCollider_Ladder2;
    private BoxCollider2D boxCollider_Ladder3;

    [SerializeField] private GameObject Door_Lv1;
    [SerializeField] private GameObject Door_Lv2;
    [SerializeField] private GameObject Door_Lv3;
    [SerializeField] private GameObject Door_Exit;
    [SerializeField] private Sprite[] DoorSprites;

    [SerializeField] private GameObject Ladder_Lv1;
    [SerializeField] private GameObject Ladder_Lv2;
    [SerializeField] private GameObject Ladder_Lv3;
    [SerializeField] private Button ExitButton;

    public static SystemManager instance;

    void Awake()
    {
        // �̹� �ν��Ͻ��� �ִ��� Ȯ���ϰ�, ������ ���ο� �ν��Ͻ��� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ������Ʈ�� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� ������ ���ο� ������Ʈ�� �ı�
        }
    }

    private void Start()
    {
        boxCollider_Door1 = Door_Lv1.GetComponent<BoxCollider2D>();
        boxCollider_Door2 = Door_Lv2.GetComponent<BoxCollider2D>();
        boxCollider_Door3 = Door_Lv3.GetComponent<BoxCollider2D>();
        boxCollider_DoorExit = Door_Exit.GetComponent<BoxCollider2D>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            if (Door_Lv1 == null) Door_Lv1 = GameObject.Find("Door_Lv1");
            if (Door_Lv1 != null) boxCollider_Door1 = Door_Lv1.GetComponent<BoxCollider2D>();
            if (Door_Lv2 == null) Door_Lv2 = GameObject.Find("Door_Lv2");
            if (Door_Lv2 != null) boxCollider_Door2 = Door_Lv2.GetComponent<BoxCollider2D>();
            if (Door_Lv3 == null) Door_Lv3 = GameObject.Find("Door_Lv3");
            if (Door_Lv3 != null) boxCollider_Door3 = Door_Lv3.GetComponent<BoxCollider2D>();
            if (Door_Exit == null) Door_Exit = GameObject.Find("Door_Exit");
            if (Door_Exit != null) boxCollider_DoorExit = Door_Exit.GetComponent<BoxCollider2D>();

            if (Ladder_Lv1 == null) Ladder_Lv1 = GameObject.Find("Ladder_Lv1");
            if (Ladder_Lv1 != null) boxCollider_Ladder1 = Ladder_Lv1.GetComponent<BoxCollider2D>();
            if (Ladder_Lv2 == null) Ladder_Lv2 = GameObject.Find("Ladder_Lv2");
            if (Ladder_Lv2 != null) boxCollider_Ladder2 = Ladder_Lv2.GetComponent<BoxCollider2D>();
            if (Ladder_Lv3 == null) Ladder_Lv3 = GameObject.Find("Ladder_Lv3");
            if (Ladder_Lv3 != null) boxCollider_Ladder3 = Ladder_Lv3.GetComponent<BoxCollider2D>();

            if (ExitButton == null) ExitButton = GameObject.Find("ExitButton")?.GetComponent<Button>();
        }
    }


    public void Init_Level1()
    {
       SceneManager.LoadScene("GameScene_1");
    }
    public void Init_Level2()
    {
        SceneManager.LoadScene("GameScene_2");
    }
    public void Init_Level3()
    {
        SceneManager.LoadScene("GameScene_3");
    }

    public void OpenDoor_Lv1()
    {
        SpriteRenderer imageComponent = Door_Lv1.GetComponentInChildren<SpriteRenderer>();
        if (IsLv1DoorOpened)
        {
            Debug.Log("���� ���Ƚ��ϴ�." + IsLv1DoorOpened);
            if (boxCollider_Door1 != null)
            {
                // boxCollider ��Ȱ��ȭ
                boxCollider_Door1.enabled = false;
                Debug.Log("boxCollider_1 ��Ȱ��ȭ�߽��ϴ�.");
                imageComponent.sprite = DoorSprites[1];
            }
            else
            {
                // boxCollider ��ã��
                Debug.Log("boxCollider_1 �����ϴ�."); 
            }
        }
        else
        {
            Debug.Log("���� ������ �ʾҽ��ϴ�." + IsLv1DoorOpened);
        }
    }

    public void OpenDoor_Lv2()
    {
        SpriteRenderer imageComponent = Door_Lv2.GetComponentInChildren<SpriteRenderer>();
        if (IsLv2DoorOpened)
        {
            Debug.Log("���� ���Ƚ��ϴ�." + IsLv2DoorOpened);
            if (boxCollider_Door2 != null)
            {
                // boxCollider ��Ȱ��ȭ
                boxCollider_Door2.enabled = false;
                Debug.Log("boxCollider_2 ��Ȱ��ȭ�߽��ϴ�.");
                imageComponent.sprite = DoorSprites[1];
            }
            else
            {
                // boxCollider ��ã��
                Debug.Log("boxCollider_2 �����ϴ�.");
            }
        }
        else
        {
            Debug.Log("���� ������ �ʾҽ��ϴ�." + IsLv2DoorOpened);
        }
    }

    public void OpenDoor_Lv3()
    {
        SpriteRenderer imageComponent = Door_Lv3.GetComponentInChildren<SpriteRenderer>();
        if (IsLv3DoorOpened)
        {
            Debug.Log("���� ���Ƚ��ϴ�." + IsLv3DoorOpened);
            if (boxCollider_Door3 != null)
            {
                // boxCollider ��Ȱ��ȭ
                boxCollider_Door3.enabled = false;
                Debug.Log("boxCollider_3 ��Ȱ��ȭ�߽��ϴ�.");
                imageComponent.sprite = DoorSprites[1];
            }
            else
            {
                // boxCollider ��ã��
                Debug.Log("boxCollider_3 �����ϴ�.");
            }
        }
        else
        {
            Debug.Log("���� ������ �ʾҽ��ϴ�." + IsLv3DoorOpened);
        }
    }

    public void OpenDoor_Exit()
    {
        SpriteRenderer imageComponent = Door_Exit.GetComponentInChildren<SpriteRenderer>();
        if (IsExit)
        {
            Debug.Log("���� ���Ƚ��ϴ�." + IsExit);
            if (boxCollider_DoorExit != null)
            {
                // boxCollider ��Ȱ��ȭ
                boxCollider_DoorExit.enabled = false;
                Debug.Log("boxCollider_Exit ��Ȱ��ȭ�߽��ϴ�.");
                imageComponent.sprite = DoorSprites[1];
            }
            else
            {
                // boxCollider ��ã��
                Debug.Log("boxCollider_Exit �����ϴ�.");
            }
        }
        else
        {
            Debug.Log("���� ������ �ʾҽ��ϴ�." + IsExit);
        }
    }
    public void Button_Exit()
    {
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // ������ ��� ����
#else
            Application.Quit(); // ����� �� ����
#endif
        };
    }
}


