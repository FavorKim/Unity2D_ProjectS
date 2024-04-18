using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : BattleManager
{
    
    public void WaveCountUp() { wave++; }
    public int GetWave() {  return wave; }
    float curQTETime;
    [SerializeField] float maxQTETime = 5;
    [SerializeField] float valuePerClick = 15;
    [SerializeField] float secPerHold = 1.5f;
    [SerializeField] float secToZero = 2;

    [SerializeField] Image gauge;
    [SerializeField] GameObject QTEUI;
    [SerializeField] PlayerController player;
    KangSeonController KS;
    [SerializeField] Animator QTEanim;
    [SerializeField] BGMManager bgm;

    protected override void Start()
    {
        base.Start();
        KS = obj[0].GetComponent<KangSeonController>();
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        bgm.gameObject.SetActive(true);
    }

    protected override void Spawn()
    {
        Shuffle();
        base.Spawn();
    }

    void Shuffle()
    {
        int num = Random.Range(0, spawnPoint.Length);
        Transform temp = spawnPoint[0];
        spawnPoint[0] = spawnPoint[num];
        spawnPoint[num] = temp;
    }

    public void EnterQTE()
    {
        curQTETime = 0;
        gauge.fillAmount = 0;
        QTEUI.SetActive(true);
        if (wave > 3)
        {
            QTEanim.Play("QTE_Click");
            StartCoroutine(CorQTEClick());
        }
        else
        {
            QTEanim.Play("QTE_Hold");
            StartCoroutine(CorQTEHold());
        }

    }

    void EndQTE()
    {
        curQTETime = 0;
        QTEUI.SetActive(false);
        if (gauge.fillAmount >= 1)
        {
            player.GetAnimator().SetTrigger("ClashEnd");
            KS.GetAnimator().SetTrigger("QTEWon");
        }
        else
        {
            KS.GetAnimator().Play("KangSeon_Disappear");
            player.SetState(PlayerController.NormalState.Instance);
        }
        if (wave > 3)
            StopCoroutine(CorQTEClick());
        else
            StopCoroutine(CorQTEHold());
    }

    IEnumerator CorQTEClick()
    {
        while (true)
        {
            yield return null;

            if (gauge.fillAmount >= 1 || curQTETime > maxQTETime)
                break;

            curQTETime += Time.deltaTime;

            gauge.fillAmount -= Time.deltaTime / secToZero;

            if(Input.GetMouseButtonDown(0))
            {
                gauge.fillAmount += valuePerClick / 100;
            }
        }
        EndQTE();
    }

    IEnumerator CorQTEHold()
    {
        while (true)
        {
            yield return null;

            if (gauge.fillAmount >= 1 || curQTETime > maxQTETime)
                break;

            curQTETime += Time.deltaTime;

            gauge.fillAmount -= Time.deltaTime / secToZero;

            if (Input.GetMouseButton(0))
            {
                gauge.fillAmount += Time.deltaTime * secPerHold;
            }
        }
        EndQTE();
    }
}
