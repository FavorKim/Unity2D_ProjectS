using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    Animator anim;
    GameObject deadUI;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("SNB_Dead");
        deadUI = FindAnyObjectByType<Canvas>().gameObject;
        deadUI.SetActive(false);
    }
    void StartDeadUI()
    {
        StartCoroutine(MoveCam());
        deadUI.SetActive(true);
    }

    IEnumerator MoveCam()
    {
        while (true)
        {
            yield return null;
            Camera.main.transform.position += new Vector3(0, Time.deltaTime);
            if (Camera.main.transform.position.y > 1.5f)
                break;
        }
        StopCoroutine(MoveCam());
    }

    public void OnRetry()
    {
        GameManager.Instance.ReTry();
    }

    public void OnMain()
    {
        GameManager.Instance.ToMain();
    }
}
