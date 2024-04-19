using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    BoxCollider2D col;
    Animator anim;
    AudioSource sfx;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("Activate");
            col.enabled = false;
            sfx.Play();
        }
    }

}
