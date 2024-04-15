using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimRadManager : Singleton<AimRadManager>
{
    Slider slider;
    public float GetValue() {  return slider.value; }
}
