using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KangSeonLaser : MonoBehaviour
{
    [SerializeField] Transform KS;
    [SerializeField] PlayerController player;
    LayerMask playerlayer;
    [SerializeField] GameObject laser;
    public GameObject GetLaser() { return laser; }
    LineRenderer lR;
    Vector2 pos = Vector2.zero;

    bool isShoot = false;

    public void SetLaserOff() { lR.enabled = false; }


    private void Awake()
    {
        lR = GetComponent<LineRenderer>();
        player = FindAnyObjectByType<PlayerController>();
        playerlayer = 1 << 8;
        laser = Instantiate(Resources.Load<GameObject>("Pivot"));
        laser.transform.parent = gameObject.transform;

    }

    private void OnEnable()
    {
        lR.SetPosition(0, KS.position);
        lR.SetPosition(1, KS.position);
        lR.startWidth = 0.5f;
        lR.endWidth = 0.5f;
        lR.enabled = false;
        laser.transform.localScale = new Vector3(160, 5, 1);

        laser.gameObject.SetActive(false);
    }

    public void Ready()
    {
        lR.enabled = true;
        isShoot = false;
        StartCoroutine(CorAiming());
    }

    public void Shoot()
    {
        isShoot = true;
        StopCoroutine(CorAiming());
        pos = player.transform.position;
        lR.SetPosition(1, pos);
    }

    public void Shooting()
    {
        lR.enabled = false;
        Vector2 newPos = pos - (Vector2)transform.position;
        float Z = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        laser.transform.localRotation = Quaternion.Euler(0, 0, Z);

        laser.SetActive(true);
    }

    public void ShootEnd()
    {
        laser.SetActive(false);
    }

    IEnumerator CorAiming()
    {
        while (true)
        {
            if (!isShoot)
                lR.SetPosition(1, player.transform.position);
            yield return null;
        }
    }
    /*
    private void Update()
    {
        if (!isShoot) { pos = player.transform.position; }
        lR.SetPosition(1, pos);
    }
    */


}
