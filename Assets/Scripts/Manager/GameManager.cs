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
            Debug.Log("���� ���Ƚ��ϴ�." + IsLv1DoorOpened);
            if (boxCollider_1 != null)
            {
                // boxCollider ��Ȱ��ȭ
                boxCollider_1.enabled = false;
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
            if (boxCollider_2 != null)
            {
                // boxCollider ��Ȱ��ȭ
                boxCollider_2.enabled = false;
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
            if (boxCollider_3 != null)
            {
                // boxCollider ��Ȱ��ȭ
                boxCollider_3.enabled = false;
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
            if (boxCollider_Exit != null)
            {
                // boxCollider ��Ȱ��ȭ
                boxCollider_Exit.enabled = false;
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
}


