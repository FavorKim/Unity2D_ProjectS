using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    bool isFull = true;
    float aimRad;

    int recoverMax = 4;
    GameObject menu;
    AudioMixer audioMixer;
    public int GetRecoverMax() { return recoverMax; }
    public string difficulty = "";
    public string deadScene;

    Texture2D cursor;


    private void Start()
    {
        DataManager.Instance.LoadGameData();
        DontDestroyOnLoad(gameObject);

        cursor = Resources.Load<Texture2D>("Cursor");
        audioMixer = Resources.Load<AudioMixer>("Master");

        UnityEngine.Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);

        if (FindAnyObjectByType<MyMenu>() == null)
            menu = Instantiate(Resources.Load<GameObject>("Menu"));
        else
            menu = FindAnyObjectByType<MyMenu>().gameObject;
        menu.SetActive(false);

        Application.targetFrameRate = 60;

        InitSetting();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                SFXManager.Instance.PlaySFX("cancel", "ui");
                menu.SetActive(false);
            }
            else if (SceneManager.GetActiveScene().name != "MainScene" && SceneManager.GetActiveScene().name != "Dead" && SceneManager.GetActiveScene().name != "Clear")
                OpenMenu();
        }
    }

    private void InitSetting()
    {
        SetAim();
        SetResolution(DataManager.Instance.data.resolution);
        Screen.fullScreen = DataManager.Instance.data.isFullscreen;
        SetDifficulty();
    }

    public void OpenMenu()
    {
        SFXManager.Instance.PlaySFX("click", "ui");
        menu.SetActive(true);
    }

    public void SetAimRad(float value)
    {
        DataManager.Instance.data.aimVal = value;
        SetAim();
    }

    public void SetAim()
    {
        if (Anchor.anc == null) return;
        aimRad = Anchor.GetInitRad() + (Anchor.GetInitRad() * (DataManager.Instance.data.aimVal / 100));

        if (Anchor.anc != null)
        {
            Anchor.anc.radius = aimRad;

            if (DataManager.Instance.data.aimVal == default)
                Anchor.anc.radius = Anchor.GetInitRad();
        }
    }
    public void SetResolution(int dropvalue)
    {
        DataManager.Instance.data.resolution = dropvalue;

        switch (DataManager.Instance.data.resolution)
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
    public void SetFullorWindow(UnityEngine.UI.Toggle toggle)
    {
        DataManager.Instance.data.isFullscreen = toggle.isOn;
        Screen.fullScreen = DataManager.Instance.data.isFullscreen;
    }

    public void SoftReset()
    {
        DataManager.Instance.SoftResetGameData();
    }

    public void HardReset()
    {
        DataManager.Instance.HardResetData();
    }

    public void Continue()
    {
        MySceneManager.Instance.ChangeScene(DataManager.Instance.data.savedScene, 0.5f);
    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void SetDifficultyEasy() 
    {
        DataManager.Instance.data.difficulty = difficulty = "easy"; 
        Continue(); 
    }
    public void SetDifficultyNormal() 
    {
        DataManager.Instance.data.difficulty = difficulty = "normal";
        recoverMax = 4;
        Continue(); 
    }
    public void SetDifficultyBeteran() 
    {
        DataManager.Instance.data.difficulty = difficulty = "beteran";
        recoverMax = 2;
        Continue(); 
    }
    public void SetDifficultyLegend() 
    {
        DataManager.Instance.data.difficulty = difficulty = "legend";
        Continue(); 
    }
    public void SetDifficulty() 
    {
        difficulty = DataManager.Instance.data.difficulty; 
    }


    public void Clear()
    {
        DataManager.Instance.data.isCleared = true;
        DataManager.Instance.SaveGameData();
    }

    public void ToMain()
    {
        MySceneManager.Instance.ChangeScene("MainScene", 2f);
    }
    public void ReTry()
    {
        MySceneManager.Instance.ChangeScene(deadScene, 2f);
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
    }

    public void SetDebugInvincible(UnityEngine.UI.Toggle toggle)
    {
        PlayerController.DebugInvincible(toggle.isOn);
    }
}
