using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            // �ν��Ͻ��� null�̸�
            if(instance == null)
            {
                // ���� �̹� �ش� Ÿ���� �ν��Ͻ��� �����ϴ��� ã�Ƽ� ��������
                instance = (T)FindObjectOfType(typeof(T));
                
                // ã�Ƶ� ������
                if(instance == null)
                {
                    // ���� ����
                    GameObject singtonObj = new GameObject();
                    instance = singtonObj.AddComponent<T>();

                    // Scene�� ����ǵ� �ı����� �ʵ��� ����
                    DontDestroyOnLoad(singtonObj);
                }
            }
            return instance;
        }
    }
    protected virtual void Awake()
    {
        if(instance !=null && instance != this)
        {
            Destroy(gameObject);
        }
    }

}
