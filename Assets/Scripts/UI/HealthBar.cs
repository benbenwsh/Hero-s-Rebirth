using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI label;

    public void SetHP(int hp, int maxHP)
    {
        slider.maxValue = maxHP;
        slider.value = hp;
        label.text = $"{hp} OF {maxHP}";
    }
}
