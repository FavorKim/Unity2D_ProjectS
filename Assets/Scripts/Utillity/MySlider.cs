using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class MySlider : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI tMP;
    //TextMeshProUGUI tMPUGUI;
    Slider slider;
    void Start()
    {
        //tMPUGUI = GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        //tMPUGUI.text = ;
        string curVal = ((slider.value/slider.maxValue)*100).ToString("0") + "%";
        tMP.text = curVal;//slider.value.ToString("0") + "%";

    }

    public void OnChangeValue()
    {
        GameManager.Instance.SetAimRad(slider);
    }

}
