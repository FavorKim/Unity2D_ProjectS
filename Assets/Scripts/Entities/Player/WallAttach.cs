using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAttach : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Aim")) return;
        if (player.GetState() == PlayerController.AttachState.Instance || player.GetState() == PlayerController.MonAttachState.Instance || player.GetState() == PlayerController.SpinState.Instance || collision.CompareTag("CantAttach") || player.GetState() == PlayerController.BossAttackState.Instance || player.GetState() == PlayerController.QTEState.Instance) return;
        else if (collision.CompareTag("Attachable") || collision.CompareTag("Platform") || collision.CompareTag("PushPlatform"))
        {
            if(player.GetState()!=PlayerController.ClimbState.Instance)
            {
                player.SetState(PlayerController.ClimbState.Instance);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Aim")) return;
        if (player.GetState() == PlayerController.AttachState.Instance|| player.GetState() == PlayerController.MonAttachState.Instance|| player.GetState() == PlayerController.SpinState.Instance || player.GetState() == PlayerController.BossAttackState.Instance || player.GetState() == PlayerController.QTEState.Instance) return;
        else if (collision.CompareTag("Attachable") || collision.CompareTag("Platform") || collision.CompareTag("PushPlatform"))
        {
            player.SetState(PlayerController.AirState.Instance);
        }
    }
}
