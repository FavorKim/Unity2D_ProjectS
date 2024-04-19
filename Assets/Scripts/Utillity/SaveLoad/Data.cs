using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public bool isCleared = false;
    public float aimVal = 50.0f;
    public float mainVol = 0.5f;
    public float bgmVol = 0.5f;
    public float sfxVol = 0.5f;
    public bool isFullscreen = true;
    public int resolution = 0;
    public Vector2 savePos = Vector2.zero;
    public string savedScene = "Tutorial";
    public string difficulty = "";
}
