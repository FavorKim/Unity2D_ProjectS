using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float setDirCool = 1.5f;
    [SerializeField] bool isFlying = false;
    [SerializeField] float gravityGap = 5.0f;
    [SerializeField] SpriteRenderer sR;
    Rigidbody2D rb;
    MonsterShoot mS;
    Vector2 dir;
    int randX;
    int randY;
    void Awake()
    {
        mS = GetComponent<MonsterShoot>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //dir = Vector2.zero;
        if (!CompareTag("Monster"))
            isFlying = true;
        else isFlying = false;

        StartCoroutine(CorSetDir());
    }
    private void OnDisable()
    {
        StopCoroutine(CorSetDir());
    }

    void Update()
    {
        if (mS.GetAttach() == false)
            Move();
    }

    void Move()
    {
        if (randX == 0) rb.velocity = Vector2.zero;
        if (randX < 0)
            sR.flipX = true;
        else if (randX > 0)
            sR.flipX = false;
        if (isFlying)
            rb.AddForce(dir * moveSpeed);
        else
            rb.AddForce(dir * moveSpeed * gravityGap);

    }

    IEnumerator CorSetDir()
    {
        while (true)
        {
            yield return new WaitForSeconds(setDirCool);
            randX = Random.Range(-1, 2);
            randY = Random.Range(-1, 2);
            if (!isFlying)
                dir = new Vector2(randX, 0);
            else
                dir = new Vector2(randX, randY);
        }
    }
}
