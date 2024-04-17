using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    float aimRad;
    float sliderValue;
    bool isFull = true;

    int recoverMax = 4;
    GameObject menu;
    public int GetRecoverMax() { return recoverMax; }
    public string difficulty = "";
    public string deadScene;

    

    Texture2D cursor;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        cursor = Resources.Load<Texture2D>("Cursor");

        UnityEngine.Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);

        if (FindAnyObjectByType<MyMenu>() == null)
            menu = Instantiate(Resources.Load<GameObject>("Menu"));

        else
            menu = FindAnyObjectByType<MyMenu>().gameObject;
        menu.SetActive(false);

        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
                menu.SetActive(false);
            else if (SceneManager.GetActiveScene().name != "MainScene" && SceneManager.GetActiveScene().name != "Dead")
                OpenMenu();
        }
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void SetAimRad(UnityEngine.UI.Slider select)
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
    public void SetResolution(int dropvalue)
    {
        switch (dropvalue)
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
        isFull = toggle.isOn;
        Screen.fullScreen = toggle.isOn;
    }

    public void GameStart()
    {
        MySceneManager.Instance.ChangeScene("Tutorial", 0.5f);
    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void SetDifficultyEasy() { difficulty = "easy"; GameStart(); }
    public void SetDifficultyNormal() { difficulty = "normal"; recoverMax = 4; GameStart(); }
    public void SetDifficultyBeteran() { difficulty = "beteran"; recoverMax = 2; GameStart(); }
    public void SetDifficultyLegend() { difficulty = "legend"; GameStart(); }

    public void ToMain()
    {
        MySceneManager.Instance.ChangeScene("MainScene", 2f);
    }
    public void ReTry()
    {
        MySceneManager.Instance.ChangeScene(deadScene, 2f);
    }

}
