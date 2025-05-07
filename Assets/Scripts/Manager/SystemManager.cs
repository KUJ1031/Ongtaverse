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
        // 이미 인스턴스가 있는지 확인하고, 없으면 새로운 인스턴스를 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 오브젝트가 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있으면 새로운 오브젝트는 파괴
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
            Debug.Log("문이 열렸습니다." + IsLv1DoorOpened);
            if (boxCollider_Door1 != null)
            {
                // boxCollider 비활성화
                boxCollider_Door1.enabled = false;
                Debug.Log("boxCollider_1 비활성화했습니다.");
                imageComponent.sprite = DoorSprites[1];
            }
            else
            {
                // boxCollider 못찾음
                Debug.Log("boxCollider_1 없습니다."); 
            }
        }
        else
        {
            Debug.Log("문이 열리지 않았습니다." + IsLv1DoorOpened);
        }
    }

    public void OpenDoor_Lv2()
    {
        SpriteRenderer imageComponent = Door_Lv2.GetComponentInChildren<SpriteRenderer>();
        if (IsLv2DoorOpened)
        {
            Debug.Log("문이 열렸습니다." + IsLv2DoorOpened);
            if (boxCollider_Door2 != null)
            {
                // boxCollider 비활성화
                boxCollider_Door2.enabled = false;
                Debug.Log("boxCollider_2 비활성화했습니다.");
                imageComponent.sprite = DoorSprites[1];
            }
            else
            {
                // boxCollider 못찾음
                Debug.Log("boxCollider_2 없습니다.");
            }
        }
        else
        {
            Debug.Log("문이 열리지 않았습니다." + IsLv2DoorOpened);
        }
    }

    public void OpenDoor_Lv3()
    {
        SpriteRenderer imageComponent = Door_Lv3.GetComponentInChildren<SpriteRenderer>();
        if (IsLv3DoorOpened)
        {
            Debug.Log("문이 열렸습니다." + IsLv3DoorOpened);
            if (boxCollider_Door3 != null)
            {
                // boxCollider 비활성화
                boxCollider_Door3.enabled = false;
                Debug.Log("boxCollider_3 비활성화했습니다.");
                imageComponent.sprite = DoorSprites[1];
            }
            else
            {
                // boxCollider 못찾음
                Debug.Log("boxCollider_3 없습니다.");
            }
        }
        else
        {
            Debug.Log("문이 열리지 않았습니다." + IsLv3DoorOpened);
        }
    }

    public void OpenDoor_Exit()
    {
        SpriteRenderer imageComponent = Door_Exit.GetComponentInChildren<SpriteRenderer>();
        if (IsExit)
        {
            Debug.Log("문이 열렸습니다." + IsExit);
            if (boxCollider_DoorExit != null)
            {
                // boxCollider 비활성화
                boxCollider_DoorExit.enabled = false;
                Debug.Log("boxCollider_Exit 비활성화했습니다.");
                imageComponent.sprite = DoorSprites[1];
            }
            else
            {
                // boxCollider 못찾음
                Debug.Log("boxCollider_Exit 없습니다.");
            }
        }
        else
        {
            Debug.Log("문이 열리지 않았습니다." + IsExit);
        }
    }
    public void Button_Exit()
    {
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // 에디터 모드 종료
#else
            Application.Quit(); // 빌드된 앱 종료
#endif
        };
    }
}


