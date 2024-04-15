using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Animator HP;

    public void OnDamaged()
    {
        switch (player.GetCurHp)
        {
            case 3:
                HP.SetBool("4to3", true);
                break;
            case 2:
                HP.SetBool("3to2", true);
                HP.SetBool("4to3", false);
                break;
            case 1:
                HP.SetBool("2to1", true);
                HP.SetBool("3to2", false);
                break;
            case 0:
                break;
        }
    }
}
