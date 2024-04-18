using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MyMenu : Singleton<MyMenu>
{
    [SerializeField] UnityEngine.UI.Toggle toggle;
    [SerializeField] TMP_Dropdown dropdown;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
