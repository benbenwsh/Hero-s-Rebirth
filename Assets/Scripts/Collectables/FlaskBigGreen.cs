using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskBigGreen : Collectables
{

    public override void Consume(Player player)
    {
        CreateFloatingText("LIFE STEAL +0.1");

        player.LifeStealChance += 0.1f;

        Destroy(this.gameObject);
    }
}
