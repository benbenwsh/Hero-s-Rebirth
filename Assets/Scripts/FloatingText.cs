using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshPro label;

    public void SetText(string text)
    {
        label.SetText(text);
    } 
}
