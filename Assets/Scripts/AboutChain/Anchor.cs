using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerController;


public class Anchor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float maxDist = 25.0f;
    [SerializeField] private PlayerController pC;
    [SerializeField] private LayerMask maskLayer;
    [SerializeField] Animator ancAnim;
    [SerializeField] static float initialAimRad = 0.5f;
    SpriteRenderer sR;
    public GameObject PointPrefs;

    public static CircleCollider2D anc;
    public static float GetInitRad() { return initialAimRad; }

    DistanceJoint2D joint;
    public DistanceJoint2D GetJoint() { return joint; }
    float curDist;

    //bool isLinked = false;
    bool canShoot = false;
    public bool GetCanShoot() { return canShoot; }

    Camera main;
    RaycastHit2D hit;
    GameObject point;
    Transform target;
    public Transform GetTarget() { return target; }

    private void Awake()
    {
        main = Camera.main;
        joint = GetComponent<DistanceJoint2D>();
        point = Instantiate(PointPrefs);
        ancAnim = point.gameObject.GetComponent<Animator>();
        sR = point.gameObject.GetComponent<SpriteRenderer>();
        sR.enabled = false;
        anc = GetComponent<CircleCollider2D>();
        GameManager.Instance.SetAim();
        StartCoroutine(HangTime());
    }

    private void Update()
    {
        joint.distance = curDist;

    }

    private void FixedUpdate()
    {
        DrawAim();

    }

    private void DrawAim()
    {
        Vector2 mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - (Vector2)player.position;
        transform.rotation = Quaternion.LookRotation(dir);
        if (pC.GetState() != AttachState.Instance && pC.GetState() != MonAttachState.Instance)
            hit = Physics2D.Raycast((Vector2)player.position, transform.forward, maxDist * 1.5f, maskLayer);
    }

    private void Aim()
    {
        if (hit == false || hit.collider.CompareTag("CantAttach")||hit.collider.CompareTag("HeavyMonster")|| hit.collider.CompareTag("DamageTile"))
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }
    }
    private void Shoot()
    {
        if (canShoot)
        {
            if (hit.collider.CompareTag("Monster") || hit.collider.CompareTag("FlyingMonster") || hit.collider.CompareTag("Boss"))
                transform.position = hit.collider.transform.position;
            else
                transform.position = hit.point;


            if (Input.GetMouseButtonDown(0))
            {
                joint.autoConfigureDistance = false;
                curDist = Vector2.Distance(transform.position, player.position)-1f;
                if (curDist > maxDist)
                    curDist = maxDist;

                sR.enabled = true;

                ancAnim.SetBool("Grabbing", true);
                ancAnim.SetTrigger("WireShoot");
                VFXManager.Instance.PlayVFX(transform.position, "VFX_ExcuteStart");

                if (hit.collider.CompareTag("Monster") || hit.collider.CompareTag("FlyingMonster"))
                    target = hit.transform;

                point.transform.position = transform.position;
                point.transform.SetParent(hit.transform);

                if (Input.GetMouseButton(0))
                    pC.GetState().CheckAttach(hit);
            }

        }
        else
            transform.position = player.position;

    }
    private void Hang()
    {

        if (pC.GetState() == AttachState.Instance || pC.GetState() == MonAttachState.Instance)
        {
            transform.position = point.transform.position;
            joint.distance = curDist;
        }
        else
        {
            point.transform.position = player.transform.position;
            joint.enabled = false;
            pC.ResetDashCool();
        }

    }

    private IEnumerator HangTime()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonUp(0))
            {
                Reset();
            }

            Aim();
            Shoot();
            Hang();
        }
    }

    public void Reset()
    {
        sR.enabled = false;
        ancAnim.SetBool("Grabbing", false);
        target = null;
        point.transform.parent = null;
        point.transform.position = player.transform.position;
        transform.position = player.position;

        if (pC.GetState() != BossAttackState.Instance)
            pC.SetState(AirState.Instance);
    }



}
