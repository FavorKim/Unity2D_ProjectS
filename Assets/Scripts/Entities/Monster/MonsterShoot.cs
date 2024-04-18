using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShoot : MonoBehaviour
{
    [SerializeField] protected PlayerController player;
    [SerializeField] protected Animator anim;
    //[SerializeField] protected GameObject playerObj;
    [SerializeField]CapsuleCollider2D col;
    CircleCollider2D circle;
    Rigidbody2D rb;
    LineRenderer lR;
    AudioSource audioSource;
    //GameObject bulletPref;

    //[SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float detectRange = 20.0f;
    [SerializeField] float atkTime = 3.0f;
    [SerializeField] float size = 0.5f;
    float aimDuration = 0f;

    bool isAttached = false;


    public void SetAttach(bool val) { isAttached = val; }
    public bool GetAttach() { return isAttached; }

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        col = GetComponent<CapsuleCollider2D>();
        circle = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        lR = GetComponent<LineRenderer>();

        lR.startWidth = size;

        lR.enabled = false;
    }

    private void OnEnable()
    {
        lR.enabled = false;
        isAttached = false;
        col.isTrigger = false;
    }


    void OnAttach()
    {
        if (isAttached)
        {
            aimDuration = 0f;
            col.isTrigger = true;
            circle.isTrigger = true;
            if (gameObject.CompareTag("FlyingMonster"))
                rb.gravityScale = 0f;

            //anim.Play("mon_Idle");
            transform.SetParent(player.transform, true);
            transform.localPosition = Vector3.zero;

            //rb.velocity = Vector2.zero;
        }
    }


    private void Update()
    {
        //moveTime += Time.deltaTime;

        lR.SetPosition(0, transform.position);
        lR.SetPosition(1, player.transform.position);
        OnAttach();
        Aim();
        Attack();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.Enemy = gameObject;
        }
    }

    private void Aim()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        if (dist < detectRange)
            aimDuration += Time.deltaTime;
    }

    private void Attack()
    {
        if(isAttached) { aimDuration = 0f; return; }
        if (aimDuration >= atkTime)
        {
            aimDuration = 0f;
            lR.enabled = true;
            anim.SetTrigger("Ready");
            audioSource.Play();
        }
    }

    void Shoot()
    {
        lR.enabled = false;
        var obj = BulletPoolManager.Instance.Dequeue();
        obj.transform.position = base.transform.position;
        Vector2 newPos = player.transform.position - obj.transform.position;
        float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        obj.SetActive(true);
    }


}
