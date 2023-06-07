using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private CanvasGroup banner;
    public static LevelLabel instance;

    private float fadingTime = 1f;

    private void Awake()
    {
        instance = this;
    }


    public IEnumerator Animate(int level)
    {
        Debug.Log("hello");
        label.text = $"LEVEL {level/3 + 1}-{level%3 + 1}";
        //banner.alpha = 0;
        //banner.LeanAlpha(1, 0.5f);

        transform.localPosition = new Vector2(-Screen.width / 2 - this.GetComponent<RectTransform>().rect.width / 2, transform.localPosition.y);
        transform.LeanMoveLocalX(0, 1f).setEaseOutExpo();

        yield return new WaitForSeconds(fadingTime);

        //banner.LeanAlpha(0, 0.5f);

        transform.LeanMoveLocalX(Screen.width / 2 + this.GetComponent<RectTransform>().rect.width / 2, 1f).setEaseInExpo();
        //this.gameObject.SetActive(false);
    }
}
