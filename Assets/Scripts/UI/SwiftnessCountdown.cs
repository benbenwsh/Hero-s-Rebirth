using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwiftnessCountdown : MonoBehaviour
{
    public static SwiftnessCountdown instance;
    public TextMeshProUGUI label;
    private Player player;

    private const float countdownTime = 15;
    private float timeRemaining;
    private const float speedMultiplier = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        timeRemaining = countdownTime;
        label.text = timeRemaining.ToString();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            player.Speed = player.OriginalSpeed;
            this.gameObject.SetActive(false);
        }
        label.text = Mathf.Ceil(timeRemaining).ToString();
    }

    public void StartTimer(Player player)
    {
        this.gameObject.SetActive(true);
        this.player = player;
        timeRemaining = countdownTime;

        if (player.Speed == player.OriginalSpeed)
        {
            player.Speed *= speedMultiplier;
        }
    }
}
