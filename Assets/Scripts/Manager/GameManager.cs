using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public bool IsLv1DoorOpened = false;
    [HideInInspector] public bool IsLv2DoorOpened = false;
    [HideInInspector] public bool IsLv3DoorOpened = false;
    [HideInInspector] public bool IsExit = false;
    private BoxCollider2D boxCollider_1;
    private BoxCollider2D boxCollider_2;
    private BoxCollider2D boxCollider_3;
    private BoxCollider2D boxCollider_Exit;

    [SerializeField] private GameObject Door_Lv1;
    [SerializeField] private GameObject Door_Lv2;
    [SerializeField] private GameObject Door_Lv3;
    [SerializeField] private GameObject Door_Exit;
    [SerializeField] private Sprite[] DoorSprites;

    public static GameManager instance;

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
        boxCollider_1 = Door_Lv1.GetComponent<BoxCollider2D>();
        boxCollider_2 = Door_Lv2.GetComponent<BoxCollider2D>();
        boxCollider_3 = Door_Lv3.GetComponent<BoxCollider2D>();
        boxCollider_Exit = Door_Exit.GetComponent<BoxCollider2D>();
    }
    public void OpenDoor_Lv1()
    {
        SpriteRenderer imageComponent = Door_Lv1.GetComponentInChildren<SpriteRenderer>();
        if (IsLv1DoorOpened)
        {
            Debug.Log("문이 열렸습니다." + IsLv1DoorOpened);
            if (boxCollider_1 != null)
            {
                // boxCollider 비활성화
                boxCollider_1.enabled = false;
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
            if (boxCollider_2 != null)
            {
                // boxCollider 비활성화
                boxCollider_2.enabled = false;
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
            if (boxCollider_3 != null)
            {
                // boxCollider 비활성화
                boxCollider_3.enabled = false;
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
            if (boxCollider_Exit != null)
            {
                // boxCollider 비활성화
                boxCollider_Exit.enabled = false;
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
}


