using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

   


    protected override void Start()
    {
        base.Start();
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
        pointPref.transform.rotation = Quaternion.identity;
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
        pointPref.transform.rotation = Quaternion.identity;
        pointPref.transform.position = position;
        anim.Play(name);
    }
    public void PlayVFX(Vector2 position, string name, float duration)
    {
        pointPref.transform.rotation = Quaternion.identity;
        pointPref.transform.position = position;
        anim.Play(name,0, duration);
    }
    public void PlayVFX(Vector2 position, Vector2 dest, string name)
    {
        float rotz = GetRotateZ(position, dest);
        PlayVFX(position, name);
        pointPref.transform.rotation = Quaternion.Euler(0, 0, rotz);
    }
    public void PlayVFXNewInstance(Vector2 position, string name)
    {
        var obj = Instantiate(pointPref);
        obj.transform.position = position;
        var anim = obj.GetComponent<Animator>();
        anim.Play(name);
        
    }
    public void PlayVFXNewInstance(Vector2 position, Vector2 dest, string name)
    {
        var obj = Instantiate(pointPref);
        obj.transform.position = position;
        var anim = obj.GetComponent<Animator>();
        float rotz = GetRotateZ(position, dest);
        obj.transform.rotation = Quaternion.Euler(0, 0, rotz);
        anim.Play(name);
    }


    public float GetRotateZ(Vector2 orgPos, Vector2 destPos)
    {
        Vector2 dir = destPos - orgPos;
        float rotZ = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return rotZ;
    }



    public void SetVFXBool(bool val) { }
}