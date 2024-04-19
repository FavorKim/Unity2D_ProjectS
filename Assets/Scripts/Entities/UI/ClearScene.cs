using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScene : MonoBehaviour
{
    [SerializeField] DOTweenAnimation first;
    [SerializeField] DOTweenAnimation cleared;
    [SerializeField] DOTweenAnimation legend;

    private void Start()
    {
        if (!DataManager.Instance.data.isCleared)
            first.DOPlay();
        else
        {
            if (GameManager.Instance.difficulty == "legend")
                legend.DOPlay();
            else
                cleared.DOPlay();
        }
        DataManager.Instance.SoftResetGameData();
    }
}
