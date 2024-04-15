using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] float aimRad;
    [SerializeField]float sliderValue;
    bool isFull = true;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void SetAimRad(Slider select)
    {
        sliderValue = select.value;
        aimRad = Anchor.GetInitRad() + (Anchor.GetInitRad() * (sliderValue / 100));
        SetAim();
    }

    public void SetAim()
    {
        if (Anchor.anc != null)
        {
            Anchor.anc.radius = aimRad;

            if (sliderValue == default)
                Anchor.anc.radius = Anchor.GetInitRad();
        }
    }

    public void SetResolution(Dropdown drop)
    {
        int dropval = drop.value;
        switch (dropval)
        {
            case 0:
                Screen.SetResolution(3840, 2160, isFull);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, isFull);
                break;
            case 2:
                Screen.SetResolution(1280, 720, isFull);
                break;
            default:
                break;
        }
    }

    public void SetFullorWindow(Toggle toggle)
    {
        isFull = toggle.isOn;
        Screen.fullScreen = toggle.isOn;
    }

    public void GameStart()
    {
        MySceneManager.Instance.ChangeScene("Tutorial", 5f);
    }

    public void GameExit()
    {
        Application.Quit();
    }

}
