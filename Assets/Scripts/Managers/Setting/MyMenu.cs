using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyMenu : Singleton<MyMenu>
{
    [SerializeField] UnityEngine.UI.Toggle toggle;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] UnityEngine.UI.Slider aim;

    private void Awake()
    {
        aim.value = DataManager.Instance.data.aimVal;
        dropdown.value = DataManager.Instance.data.resolution;
        toggle.isOn = DataManager.Instance.data.isFullscreen;
    }

    protected override void Start()
    {
        base.Start();
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        gameObject.SetActive(false);
    }

    public void OnPressMenu()
    {

        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
        SFXManager.Instance.PlaySFX("click", "ui");

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
        gameObject.SetActive(false);
        GameManager.instance.ToMain();
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
