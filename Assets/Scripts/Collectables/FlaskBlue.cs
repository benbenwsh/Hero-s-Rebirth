using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskBlue : Collectables
{
    public override void Consume(Player player)
    {
        CreateFloatingText("SWIFTNESS");
        SwiftnessCountdown.instance.StartTimer(player);
        Destroy(this.gameObject);
    } 
}
