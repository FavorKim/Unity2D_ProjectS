using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T Instance
    {
        get
        {

            // 인스턴스가 null이면
            if (instance == null)
            {
                // 씬에 이미 해당 타입의 인스턴스가 존재하는지 찾아서 가져오고
                instance = (T)FindObjectOfType(typeof(T));

                // 찾아도 없으면
                if (instance == null)
                {
                    // 새로 생성
                    GameObject singtonObj = new GameObject();
                    instance = singtonObj.AddComponent<T>();

                    // Scene이 변경되도 파괴되지 않도록 설정
                    DontDestroyOnLoad(singtonObj);
                }

            }
            return instance;
        }
    }
    protected virtual void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }
    }
}
