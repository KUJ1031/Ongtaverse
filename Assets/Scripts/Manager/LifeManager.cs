using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] public Sprite[] LifeSprites;

    public static LifeManager instance;

    private void Awake()
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
}
