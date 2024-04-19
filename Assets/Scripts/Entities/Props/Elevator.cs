using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnBoard()
    {
        DataManager.Instance.data.savePos = Vector2.zero;
        DataManager.Instance.data.savedScene = "KangSeon";
        MySceneManager.Instance.ChangeScene("KangSeon", 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            anim.Play("ele");
    }
}
