using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] protected GameObject[] walls;
    [SerializeField] protected GameObject[] enemies;
    [SerializeField] protected Transform[] spawnPoint;
    [SerializeField] protected int wave = 3;
    [SerializeField] protected Animator[] wallAnim;
    [SerializeField] protected BoxCollider2D[] wallCol;


    protected GameObject[] obj;

    protected bool isOver = false;

    protected virtual void Start()
    {
        
        obj = new GameObject[enemies.Length];

        for(int i = 0; i < walls.Length; i++)
        {
            wallCol[i] = walls[i].GetComponent<BoxCollider2D>();
            wallCol[i].enabled = false;
            wallAnim[i] = walls[i].GetComponent<Animator>();
            //walls[i].SetActive(false);
        }


        for (int i = 0; i < enemies.Length; i++)
        {
            obj[i] = Instantiate(enemies[i]);
            obj[i].SetActive(false);
        }

    }

    protected void Enter()
    {
        foreach (BoxCollider2D wall in wallCol) 
            wall.enabled = true;
        foreach (Animator wall in wallAnim)
            wall.SetTrigger("Close");
            

        StartCoroutine(CorWave());
    }

    protected virtual void Spawn()
    {
        
        isOver = false;
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].transform.position = spawnPoint[i].position;
            obj[i].SetActive(true);
        }
    }

    protected IEnumerator CorWave()
    {
        while (wave >= 0)
        {
            yield return null;
            int count = 0;

            foreach (GameObject g in obj)
            {
                if (g.activeSelf == true)
                    count++;
            }

            if (count != 0) isOver = false;
            else { isOver = true; wave--; }

            if (isOver && wave >= 0)
                Spawn();
        }
    }

    protected void WaveEnd()
    {
        for(int i = 0; i < walls.Length; i++)
        {
            wallAnim[i].SetTrigger("Open");
            wallCol[i].enabled = false;
            //walls[i].SetActive(false); 
        }

    }

    protected void Update()
    {
        if (wave < 0)
        {
            StopCoroutine(CorWave());
            WaveEnd();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Enter();
    }
}


/*
wave 3

 
 */