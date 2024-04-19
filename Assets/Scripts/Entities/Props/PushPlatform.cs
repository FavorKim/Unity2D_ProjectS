using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlatform : MonoBehaviour
{
    [SerializeField] PlayerController player;
    Rigidbody2D rb;
    [SerializeField] float pushForce = 5.0f;
    Vector2 dir;
    bool move = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.IsSpinning)
            {
                move = true;
                player.SetState(PlayerController.AirState.Instance);
                dir = transform.position - collision.transform.position;
                rb.AddForce(dir * pushForce, ForceMode2D.Impulse);
            }
        }
    }

    private void Update()
    {
        if (rb.velocity.magnitude < 0.4f)
            move = false;
        if (!move)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
