using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Aim")) return;
        
        player.JumpCount = 0;
        if (player.GetState() == PlayerController.AttachState.Instance || player.GetState() == PlayerController.MonAttachState.Instance || player.GetState() == PlayerController.SpinState.Instance || player.GetState() == PlayerController.BossAttackState.Instance|| player.GetState() == PlayerController.QTEState.Instance) return;
        player.GetArm().SetTrigger("Land");
        player.GetAnimator().Play("SNB_Land");
        player.GetAnimator().SetBool("isLand", true);
        VFXManager.Instance.PlayVFX("VFX_Land");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Aim")) return;
        if (player.GetState() == PlayerController.AttachState.Instance || player.GetState() == PlayerController.MonAttachState.Instance || player.GetState() == PlayerController.SpinState.Instance || player.GetState() == PlayerController.BossAttackState.Instance || player.GetState() == PlayerController.QTEState.Instance) return;
        else player.SetState(PlayerController.NormalState.Instance);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Aim")) return;
        if (player.GetState() == PlayerController.AttachState.Instance || player.GetState() == PlayerController.MonAttachState.Instance || player.GetState() == PlayerController.SpinState.Instance || player.GetState() == PlayerController.BossAttackState.Instance || player.GetState() == PlayerController.QTEState.Instance) return;

        else
        {
            player.SetState(PlayerController.AirState.Instance);
            player.GetAnimator().SetBool("isLand", false);
        }
    }
}
