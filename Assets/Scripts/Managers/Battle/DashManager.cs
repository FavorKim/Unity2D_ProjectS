using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckDash();
    }

    void CheckDash()
    {
        if (player.CanDash())
            anim.SetBool("CanDash", true);
        else
            anim.SetBool("CanDash", false);
    }
}
