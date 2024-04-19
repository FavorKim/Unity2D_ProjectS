using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueBTN : MonoBehaviour
{
    private void OnEnable()
    {
        if(DataManager.Instance.data.difficulty =="")
            gameObject.SetActive(false);
    }
}
