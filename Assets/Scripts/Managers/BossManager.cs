using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : BattleManager
{
    
    public void WaveCountUp() { wave++; }
    public int GetWave() {  return wave; }


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
}
