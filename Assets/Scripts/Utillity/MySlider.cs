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
        tMP.text = slider.value.ToString("0") + "%";
    }

}
