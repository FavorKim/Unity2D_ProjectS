using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlatform : MonoBehaviour
{
    [SerializeField] PlayerController player;
    Rigidbody2D rb;
    [SerializeField] float pushForce = 5.0f;
    Vector2 dir;
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
                player.SetState(PlayerController.AirState.Instance);
                dir = transform.position - collision.transform.position;
                rb.AddForce(dir * pushForce, ForceMode2D.Impulse);
            }
        }
    }

}
