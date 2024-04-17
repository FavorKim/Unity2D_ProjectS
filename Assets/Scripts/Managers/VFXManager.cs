using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    

    [SerializeField] PlayerController player;
    [SerializeField] private Transform[] VFXPos;
    private Dictionary<string, Transform> VFXPosDict;

    public Dictionary<string, Transform> GetVFXDict()
    {
        return VFXPosDict;
    }

    



    [SerializeField] private GameObject pointPref;
    private Animator anim;

   


    private void Start()
    {
        //if (Instance != null) Destroy(gameObject);

        pointPref = Instantiate(pointPref);
        anim = pointPref.GetComponent<Animator>();
        VFXPosDict = new Dictionary<string, Transform>();
        VFXPosDict.Add("VFX_Jump", VFXPos[0]);
        VFXPosDict.Add("VFX_WallJump", VFXPos[1]);
        VFXPosDict.Add("VFX_WallSlide", VFXPos[2]);
        VFXPosDict.Add("VFX_Land", VFXPos[3]);
    }


    public Vector2 GetPos(string name)
    {
        return VFXPosDict[name].position;
    }

    public void ExitVFX() { anim.SetTrigger("Exit"); }

    public void PlayVFX(string name)
    {
        if (player.GetSR().flipX == true)
            transform.Rotate(0, 90, 0);
        pointPref.transform.SetParent(VFXPosDict[name]);
        pointPref.transform.localPosition = Vector2.zero;
        anim.Play(name);
        pointPref.transform.SetParent(null);

        transform.rotation = Quaternion.identity;
    }

    public void PlayVFX(Vector2 position, string name)
    {
        pointPref.transform.position = position;
        anim.Play(name);
    }
    public void PlayVFX(Vector2 position, string name, float duration)
    {
        pointPref.transform.position = position;
        anim.Play(name,0, duration);
    }
    public void PlayVFX(Vector2 position, Vector2 dest, string name)
    {
        float rotz = GetRotateZ(position, dest);
        pointPref.transform.rotation = Quaternion.Euler(0, 0, rotz);
        PlayVFX(position, name);
    }

    public float GetRotateZ(Vector2 orgPos, Vector2 destPos)
    {
        Vector2 dir = destPos - orgPos;
        float rotZ = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    

    public void SetVFXBool(bool val) { }
    //public void PlayVFX(Vector2 position, string name, int layer, float duration)
    //{
    //    pointPref.transform.position = position;
    //    anim.Play(name, layer, duration);
    //}
}