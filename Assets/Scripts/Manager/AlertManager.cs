using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public static AlertManager instance;

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



}
