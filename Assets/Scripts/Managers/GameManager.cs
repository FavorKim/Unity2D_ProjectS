using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    /*
    void Update()
    {

    }
    
    private void Start()
    {

    }
    */
}
