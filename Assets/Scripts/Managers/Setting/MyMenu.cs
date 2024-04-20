using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyMenu : Singleton<MyMenu>
{
    [SerializeField] UnityEngine.UI.Toggle toggle;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] UnityEngine.UI.Slider aim;

    private void Start()
    {
        instance = this;
    }

    protected void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        aim.value = DataManager.Instance.data.aimVal;
        dropdown.value = DataManager.Instance.data.resolution;
        toggle.isOn = DataManager.Instance.data.isFullscreen;
    }
    private void OnEnable()
    {
        PlayerController.isFreeze = true;
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        PlayerController.isFreeze = false;
        Time.timeScale = 1f;
    }
    public void OnExit()
    {
        Application.Quit();
    }

    public void OnFullscreenToggle()
    {
        GameManager.Instance.SetFullorWindow(toggle);
    }
    public void OnResolution()
    {
        GameManager.Instance.SetResolution(dropdown.value);
    }
}
