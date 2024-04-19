using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] GameObject cleared;
    [SerializeField] GameObject unCleared;

    [SerializeField] GameObject newstartPopup;

    public void OnClickStart()
    {
        if(DataManager.Instance.data.isCleared)
            cleared.SetActive(true);
        else
            unCleared.SetActive(true);
    }

    public void NewStart()
    {
        if (DataManager.Instance.data.difficulty == "")
        {
            OnClickStart();
            return;
        }
        else
            newstartPopup.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            DOTween.CompleteAll();
        }
    }

}
