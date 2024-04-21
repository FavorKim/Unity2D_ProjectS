using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    #region Vars
    #region player
    [Header("Player")]
    [Space(20)]
    Dictionary<string, AudioClip> playerClips;
    [SerializeField] AudioSource player;
    [Space(5)]
    [SerializeField] AudioClip chargeStart;
    [SerializeField] AudioClip chargeloop;
    [SerializeField] AudioClip chargeComplete;
    [SerializeField] AudioClip Dash;
    [SerializeField] AudioClip damaged;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip swingdash;
    #endregion
    #region arm
    [Header("Arm")]
    [Space(20)]
    Dictionary<string, AudioClip> armClips;
    [SerializeField] AudioSource arm;
    [Space(5)]
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip returnArm;
    [SerializeField] AudioClip rapeling;
    #endregion
    #region aim
    [Header("aim")]
    [Space(10)]
    Dictionary<string, AudioClip> aimClips;
    [SerializeField] AudioSource aim;
    [Space(5)]
    [SerializeField] AudioClip grab;
    [SerializeField] AudioClip excute;
    [SerializeField] AudioClip chargeHit;
    #endregion
    #region gate
    [Header("gate")]
    [Space(10)]
    Dictionary<string, AudioClip> gateClips;
    [SerializeField] AudioSource gate;
    [Space(5)]
    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;
    #endregion
    #region ui
    [Header("ui")]
    [Space(10)]
    Dictionary<string, AudioClip> UIClips;
    [SerializeField] AudioSource UISource;
    [Space(5)]
    [SerializeField] AudioClip cancel;
    [SerializeField] AudioClip click;
    [SerializeField] AudioClip select;
    #endregion

    Dictionary<string, AudioSource> sources;
    Dictionary<string, Dictionary<string, AudioClip>> dics;

    #endregion


    protected override void Start()
    {
        base.Start();
        playerClips = new Dictionary<string, AudioClip>();
        playerClips.Add("chargeStart", chargeStart);
        playerClips.Add("chargeloop", chargeloop);
        playerClips.Add("chargeComplete", chargeComplete);
        playerClips.Add("dash", Dash);
        playerClips.Add("damaged", damaged);
        playerClips.Add("jump", jump);
        playerClips.Add("swingdash", swingdash);

        armClips = new Dictionary<string, AudioClip>();
        armClips.Add("shoot", shoot);
        armClips.Add("returnArm", returnArm);
        armClips.Add("rapeling", rapeling);

        aimClips = new Dictionary<string, AudioClip> ();
        aimClips.Add("grab", grab);
        aimClips.Add("excute", excute);
        aimClips.Add("chargeHit", chargeHit);

        gateClips = new Dictionary<string, AudioClip>();
        gateClips.Add("open", open);
        gateClips.Add("close", close);

        UIClips = new Dictionary<string, AudioClip>();
        UIClips.Add("cancel", cancel);
        UIClips.Add("click", click);
        UIClips.Add("select", select);

        sources = new Dictionary<string, AudioSource>();
        sources.Add("player", player);
        sources.Add("arm", arm);
        sources.Add("aim", aim);
        sources.Add("gate", gate);
        sources.Add("ui", UISource);

        dics = new Dictionary<string, Dictionary<string, AudioClip>> ();
        dics.Add("player", playerClips);
        dics.Add("arm", armClips);
        dics.Add("aim", aimClips);
        dics.Add("ui", UIClips);
        dics.Add("gate",gateClips);
    }

    

    public void PlaySFX(string clipName, string sourceName)
    {
        AudioSource source = sources[sourceName];
        AudioClip clip = dics[sourceName][clipName];

        source.clip = clip;
        source.Play();
    }

}
