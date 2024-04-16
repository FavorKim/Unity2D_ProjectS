using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KangSeonController : MonoBehaviour
{
    Animator anim;
    public Animator GetAnimator() { return anim; }
    PlayerController player;
    [SerializeField] KangSeonLaser laser;
    [SerializeField] BossManager bM;

    public KangSeonLaser GetLaser() { return laser; }

    [SerializeField] float atkCool = 5.0f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerController>();
        bM = FindAnyObjectByType<BossManager>();
    }

    private void OnEnable()
    {
        StartCoroutine(CorShoot());
    }

    public void EnterQTE()
    {
        anim.Play("KangSeon_Clash_Start");
        StopCoroutine(CorShoot());
    }

    void OnAttacked()
    {
        gameObject.SetActive(false);
        
    }

    void OnDisappear()
    {
        bM.WaveCountUp();
        gameObject.SetActive(false);
    }

    void OnShoot()
    {
        laser.Shoot();
    }

    void OnShooting()
    {
        laser.Shooting();
    }

    void OnShootEnd()
    {
        laser.ShootEnd();
    }

    void OnReady()
    {
        laser.Ready();
        StopCoroutine(CorShoot());
    }

    IEnumerator CorShoot()
    {
        yield return new WaitForSeconds(atkCool);
        anim.SetTrigger("Shoot");

    }

    
}
