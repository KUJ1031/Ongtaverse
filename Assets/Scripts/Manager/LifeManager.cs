using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] public Sprite[] LifeSprites;

    public static LifeManager instance;

    private void Awake()
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
}
