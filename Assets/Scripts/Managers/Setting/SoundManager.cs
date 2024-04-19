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
        InitSound();
    }

    private void InitSound()
    {
        entire.value = DataManager.Instance.data.mainVol;
        SetEntireVolume(DataManager.Instance.data.mainVol);

        bgm.value = DataManager.Instance.data.bgmVol;
        SetBGMVolume(DataManager.Instance.data.bgmVol);

        sfx.value = DataManager.Instance.data.sfxVol;
        SetSFXVolume(DataManager.Instance.data.sfxVol);
    }

    public void SetEntireVolume(float vol)
    {
        DataManager.Instance.data.mainVol = vol;
        audioMixer.SetFloat("Master", Mathf.Log10(DataManager.Instance.data.mainVol) * 20);
        
    }
    public void SetBGMVolume(float vol)
    {
        DataManager.Instance.data.bgmVol = vol;
        audioMixer.SetFloat("BGM", Mathf.Log10(DataManager.Instance.data.bgmVol) * 20);
    }
    public void SetSFXVolume(float vol)
    {
        DataManager.Instance.data.sfxVol = vol;
        audioMixer.SetFloat("SFX", Mathf.Log10(DataManager.Instance.data.sfxVol) * 20);
    }
}
