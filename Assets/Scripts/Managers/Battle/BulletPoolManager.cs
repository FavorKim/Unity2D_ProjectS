using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;
    protected static BulletPoolManager instance;
    public static BulletPoolManager Instance
    {
        get
        {
            return instance;
        }
    }
    private Queue<GameObject> queue;

    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        if (instance == null)
        {

            instance = this;
            queue = new Queue<GameObject>();
            Spawn(30);
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(instance);
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(bulletPref);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            Enqueue(obj);
        }
    }

    public void Enqueue(GameObject obj)
    {
        queue.Enqueue(obj);
    }

    public GameObject Dequeue()
    {
        GameObject obj = queue.Dequeue();
        return obj;
    }

}
