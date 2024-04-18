using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] UnityEngine.UI.Slider entire;
    [SerializeField] UnityEngine.UI.Slider bgm;
    [SerializeField] UnityEngine.UI.Slider sfx;

    private void Awake()
    {
        audioMixer = Resources.Load<AudioMixer>("Master");
        entire.onValueChanged.AddListener(SetEntireVolume);
        bgm.onValueChanged.AddListener(SetBGMVolume);
        sfx.onValueChanged.AddListener(SetSFXVolume);

    }

    public void SetEntireVolume(float vol)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(vol) * 20);
    }
    public void SetBGMVolume(float vol)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(vol) * 20);
    }
    public void SetSFXVolume(float vol)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(vol) * 20);
    }
}
