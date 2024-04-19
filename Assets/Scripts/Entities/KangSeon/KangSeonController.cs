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
    [SerializeField] AudioClip smokeBtn;
    [SerializeField] AudioClip smoke;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip drag;
    [SerializeField] AudioClip cut;

    public KangSeonLaser GetLaser() { return laser; }

    [SerializeField] float atkCool = 2.0f;


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
        bM.WaveCountUp();
        gameObject.SetActive(false);
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

    void OnDisappearSound()
    {
        sfx.clip = evadeClip;
        sfx.Play();
    }
    void SmokeShellBtn()
    {
        sfx.clip = smokeBtn;
        sfx.Play();
    }
    void Smoke()
    {
        sfx.clip = smoke;
        sfx.Play();
    }

    void OnHitted()
    {
        sfx.clip = hit;
        sfx.Play();
    }
    void OnDrag()
    {
        sfx.clip = drag;
        sfx.Play();
    }
    void OnCut()
    {
        sfx.clip = cut;
        sfx.Play();
    }

}
