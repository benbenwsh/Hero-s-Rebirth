using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    public Slider slider;



    public void SetMaxTime(float reloadTime)
    {
        slider.maxValue = reloadTime;
    }



    public void SetTime(float timeRemaining)
    {
        slider.value = timeRemaining;
    }
}
