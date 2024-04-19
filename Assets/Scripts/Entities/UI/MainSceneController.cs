using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] GameObject cleared;
    [SerializeField] GameObject unCleared;
    public void OnClickStart()
    {
        if(DataManager.Instance.data.isCleared)
            cleared.SetActive(true);
        else
            unCleared.SetActive(true);
    }

    public void NewStart()
    {

    }

}
