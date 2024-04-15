using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed = 40.0f;
    Animator animator;
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        animator.Play("Shoot");
        speed = 40.0f;
    }
    void Update()
    {
        //transform.position += Vector3.right * Time.deltaTime * speed;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Monster") || collision.transform.CompareTag("FlyingMonster") || collision.transform.CompareTag("Aim") ||collision.CompareTag("HeavyMonster") )return;
        speed = 0f;
        animator.Play("Hitted");
    }
    void OnHitted()
    {
        gameObject.SetActive(false);
        BulletPoolManager.Instance.Enqueue(gameObject);
    }
}
