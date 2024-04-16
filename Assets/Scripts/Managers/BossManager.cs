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
    [SerializeField] float secToZero = 2;

    [SerializeField] Image gauge;
    [SerializeField] GameObject QTEUI;
    [SerializeField] PlayerController player;
    KangSeonController KS;

    protected override void Start()
    {
        base.Start();
        KS = obj[0].GetComponent<KangSeonController>();
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
        QTEUI.SetActive(true);
        StartCoroutine(CorQTE());
    }

    void EndQTE()
    {
        Debug.Log("EndQTE");
        curQTETime = 0;
        QTEUI.SetActive(false);
        if (gauge.fillAmount >= 1)
            KS.GetAnimator().SetTrigger("QTEWon");
        else
        {
            // qte defeat;
        }
        StopCoroutine(CorQTE());
        player.SetState(PlayerController.NormalState.Instance);
    }

    IEnumerator CorQTE()
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
}
