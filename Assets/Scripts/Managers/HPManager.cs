using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Animator HP;
    float recoverTime;
    [SerializeField] static float MaxRecoverTime = 5.0f;

    public static float GetMaxRecoverTime() { return MaxRecoverTime; }

    private void OnEnable()
    {
        if (GameManager.Instance.difficulty == "easy" || GameManager.Instance.difficulty == "legend")
            Destroy(gameObject);
        else
            StartCoroutine(CorRecover());
    }

    public void OnDamaged()
    {
        ResetBool();
        switch (player.GetCurHp)
        {
            case 3:
                HP.Play("HP_4to3");
                HP.SetBool("to3", true);
                break;
            case 2:
                HP.Play("HP_3to2");
                HP.SetBool("to2", true);
                break;
            case 1:
                HP.Play("HP_2to1");
                HP.SetBool("to1", true);
                break;
            case 0:
                break;
        }
        recoverTime = 0;
    }

    IEnumerator CorRecover()
    {
        while (true)
        {
            yield return null;
            if (player.GetCurHp >= GameManager.Instance.GetRecoverMax())
                recoverTime = 0;
            else
                recoverTime += Time.deltaTime;
            if (recoverTime > MaxRecoverTime)
                Recover();
        }
    }

    void ResetBool()
    {
        if (gameObject == null) return;
        HP.SetBool("to1", false);
        HP.SetBool("to2", false);
        HP.SetBool("to3", false);
        HP.SetBool("to4", false);
    }


    void Recover()
    {
        switch (player.GetCurHp)
        {
            case 3:
                HP.Play("HP_Recover_3to4");
                HP.SetBool("to4", true);
                player.Recover();
                break;
            case 2:
                HP.Play("HP_Recover_2to3");
                HP.SetBool("to3", true);
                player.Recover();
                break;
            case 1:
                HP.Play("HP_Recover_1to2");
                HP.SetBool("to2", true);
                player.Recover();
                break;
            case 0:
                break;
        }
        recoverTime = 0;
        

    }
}
