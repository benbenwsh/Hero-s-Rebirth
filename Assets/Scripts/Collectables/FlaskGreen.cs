using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskGreen : Collectables
{

    public override void Consume(Player player)
    {
        CreateFloatingText("MAX HP +1");
        player.SetHp(player.Hp, player.MaxHp + 1);
        Destroy(this.gameObject);
    }
}
