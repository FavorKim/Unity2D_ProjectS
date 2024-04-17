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
    AudioSource sfx;
    [SerializeField] AudioClip atkClip;
    [SerializeField] AudioClip evadeClip;
    [SerializeField] AudioClip readyClip;

    public KangSeonLaser GetLaser() { return laser; }

    [SerializeField] float atkCool = 5.0f;


    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
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

    void OnEndQTE()
    {
        player.SetState(PlayerController.NormalState.Instance);
    }

    void OnAttacked()
    {
        gameObject.SetActive(false);
        
    }

    void OnDisappear()
    {
        sfx.clip = evadeClip;
        sfx.Play();
        bM.WaveCountUp();
        gameObject.SetActive(false);
    }

    void OnDisappearSound()
    {
        sfx.clip = evadeClip;
        sfx.Play();
    }

    void OnShoot()
    {
        sfx.clip = readyClip;
        sfx.Play();

        laser.Shoot();
    }

    void OnShooting()
    {
        sfx.clip = atkClip;
        sfx.Play();
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
